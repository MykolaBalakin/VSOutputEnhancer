using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Classifiers.BuildActionStart
{
    public class BuildActionCollection
    {
        private class BuildActionValue
        {
            public BuildActionState? State { get; set; }
            public SnapshotSpan? Span { get; set; }
        }

        private IEnumerable<String> EmptyStringArray = new String[0];

        private readonly IDictionary<String, IDictionary<String, BuildActionValue>> data = new Dictionary<String, IDictionary<String, BuildActionValue>>();
        private readonly IDictionary<String, String> latestProjects = new Dictionary<String, String>();
        private readonly IDictionary<String, IDictionary<Int32, String>> latestParallelProjects = new Dictionary<String, IDictionary<Int32, String>>();

        public IEnumerable<String> EnumerateProjects(String action)
        {
            return data.ContainsKey(action) ? data[action].Keys : EmptyStringArray;
        }

        public void HandleActionStart(String action, String projectName, Int32? buildTaskId, SnapshotSpan span)
        {
            var value = GetOrAddValue(action, projectName);
            value.Span = span;
            if (value.Span == null)
            {
                value.State = null;
            }

            SetLatestProject(action, projectName, buildTaskId);
        }

        public Boolean HandleStateChange(String action, String projectName, BuildActionState state)
        {
            var value = GetOrAddValue(action, projectName);
            if (value.State >= state)
            {
                return false;
            }

            value.State = state;
            return true;
        }

        public BuildActionState GetState(String action, String projectName)
        {
            var value = GetOrAddValue(action, projectName);
            return value.State ?? BuildActionState.Unknown;
        }

        public SnapshotSpan GetSpan(String action, String projectName)
        {
            var value = GetOrAddValue(action, projectName);
            return value.Span.Value;
        }

        public String GetLatestProject(String action, Int32? buildTaskId)
        {
            String projectName;

            if (buildTaskId == null)
            {
                if (!latestProjects.TryGetValue(action, out projectName))
                {
                    return null;
                }

                return projectName;
            }

            if (!latestParallelProjects.TryGetValue(action, out var projects))
            {
                return null;
            }

            if (!projects.TryGetValue(buildTaskId.Value, out projectName))
            {
                return null;
            }

            return projectName;
        }

        public void DeleteAll(String action)
        {
            latestProjects.Remove(action);
            latestParallelProjects.Remove(action);
        }

        private BuildActionValue GetOrAddValue(String action, String projectName)
        {
            if (!data.TryGetValue(action, out var projects))
            {
                projects = new Dictionary<String, BuildActionValue>();
                data.Add(action, projects);
            }

            if (!projects.TryGetValue(projectName, out var result))
            {
                result = new BuildActionValue();
                projects.Add(projectName, result);
            }

            return result;
        }

        private void SetLatestProject(String action, String projectName, Int32? buildTaskId)
        {
            if (buildTaskId == null)
            {
                latestProjects[action] = projectName;
            }
            else
            {
                if (!latestParallelProjects.TryGetValue(action, out var projects))
                {
                    projects = new Dictionary<Int32, String>();
                    latestParallelProjects.Add(action, projects);
                }

                projects[buildTaskId.Value] = projectName;
            }
        }
    }
}