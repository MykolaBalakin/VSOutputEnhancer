using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Balakin.VSOutputEnhancer.Tests.PerfomanceTests {
    [ExcludeFromCodeCoverage]
    internal static class Utils {
        public static IEnumerable<String> ReadLogFile(String relativePath) {
            ExtractResources();
            var lines = File.ReadLines(GetAbsolutePath(relativePath));
            foreach (var line in lines) {
                yield return line + "\r\n";
            }
        }

        private static String GetAbsolutePath(String relativePath) {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            var assemblyPath = Uri.UnescapeDataString(uri.Path);
            return Path.Combine(Path.GetDirectoryName(assemblyPath), relativePath);
        }


        private static Boolean resourcesExtracted;
        private static readonly Object extractResourcesLock = new Object();
        private static void ExtractResources() {
            if (resourcesExtracted) {
                return;
            }
            lock (extractResourcesLock) {
                if (resourcesExtracted) {
                    return;
                }
                var resourcesPath = GetAbsolutePath("Resources");
                var archives = Directory.EnumerateFiles(resourcesPath, "*.zip", SearchOption.AllDirectories);
                foreach (var archivePath in archives) {
                    var destinationPath = Path.GetDirectoryName(archivePath);
                    var archive = ZipFile.Open(archivePath, ZipArchiveMode.Read);
                    foreach (var archiveEntry in archive.Entries) {
                        var fullEntryPath = Path.Combine(destinationPath, archiveEntry.FullName);
                        var fileInfo = new FileInfo(fullEntryPath);
                        if (fileInfo.Exists) {
                            if (fileInfo.Length != archiveEntry.Length) {
                                fileInfo.Delete();
                            } else if (fileInfo.LastWriteTimeUtc < archiveEntry.LastWriteTime.UtcDateTime) {
                                fileInfo.Delete();
                            }
                        }
                        if (!fileInfo.Exists) {
                            archiveEntry.ExtractToFile(fullEntryPath);
                        }
                    }
                }
                resourcesExtracted = true;
            }
        }
    }
}
