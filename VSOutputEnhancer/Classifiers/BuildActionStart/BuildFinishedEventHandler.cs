using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Balakin.VSOutputEnhancer.Events;
using Balakin.VSOutputEnhancer.Parsers.BuildResult;

namespace Balakin.VSOutputEnhancer.Classifiers.BuildActionStart
{
    [Export(typeof(IEventHandler))]
    public class BuildFinishedEventHandler : IEventHandler<SpanParsedEvent<BuildResultData>>
    {
        private const String BuildAction = "Build";

        public IEnumerable<String> ContentTypes { get; } = new[]
        {
            ContentType.BuildOutput,
            ContentType.BuildOrderOutput
        };

        public void Handle(IDispatcher dispatcher, DataContainer data, SpanParsedEvent<BuildResultData> @event)
        {
            HandleActionFinished(BuildAction, dispatcher, data);
        }

        private void HandleActionFinished(String action, IDispatcher dispatcher, DataContainer data)
        {
            var actionCollection = data.Get<BuildActionCollection>();

            var projects = actionCollection.EnumerateProjects(action);
            foreach (var project in projects)
            {
                var changed = actionCollection.HandleStateChange(action, project, BuildActionState.Success);
                if (changed)
                {
                    var span = actionCollection.GetSpan(action, project);
                    var classificationChanged = new ClassificationChangedEvent(span);
                    dispatcher.Dispatch(classificationChanged, data);
                }
            }

            actionCollection.DeleteAll(action);
        }
    }
}