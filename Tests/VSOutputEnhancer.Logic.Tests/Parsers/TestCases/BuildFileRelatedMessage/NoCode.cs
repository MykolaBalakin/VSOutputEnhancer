using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers;
using Balakin.VSOutputEnhancer.Logic.Classifiers.BuildFileRelatedMessage;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Parsers.TestCases.BuildFileRelatedMessage
{
    [ExcludeFromCodeCoverage]
    public class NoCode : TestCaseBase
    {
        public override String Input { get; } = "1>C:\\Sources\\Some project\\SomeProject.csproj(163,5): error : This project references NuGet package(s) that are missing on this computer. Enable NuGet Package Restore to download them.  For more information, see http://www.postsharp.net/links/nuget-restore.\r\n";
        
        public override BuildFileRelatedMessageData ExpectedResult { get; } = new BuildFileRelatedMessageData(
            new ParsedValue<Int32>(1, new Span(0, 1)),
            new ParsedValue<MessageType>(MessageType.Error, new Span(53, 5)),
            new ParsedValue<String>(),
            new ParsedValue<String>("This project references NuGet package(s) that are missing on this computer. Enable NuGet Package Restore to download them.  For more information, see http://www.postsharp.net/links/nuget-restore.", new Span(61, 195)),
            new ParsedValue<String>("error : This project references NuGet package(s) that are missing on this computer. Enable NuGet Package Restore to download them.  For more information, see http://www.postsharp.net/links/nuget-restore.", new Span(53, 203)),
            new ParsedValue<String>("C:\\Sources\\Some project\\SomeProject.csproj", new Span(2, 42))
        );
    }
}