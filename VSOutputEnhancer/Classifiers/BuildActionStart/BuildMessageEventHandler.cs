using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Balakin.VSOutputEnhancer.Events;
using Balakin.VSOutputEnhancer.Parsers;
using Balakin.VSOutputEnhancer.Parsers.BuildFileRelatedMessage;

namespace Balakin.VSOutputEnhancer.Classifiers.BuildActionStart
{
    [Export(typeof(IEventHandler))]
    public class BuildMessageEventHandler : IEventHandler<SpanParsedEvent<BuildFileRelatedMessageData>>
    {
        private const String BuildAction = "Build";

        public IEnumerable<String> ContentTypes { get; } = new[]
        {
            ContentType.BuildOutput,
            ContentType.BuildOrderOutput
        };

        public void Handle(IDispatcher dispatcher, DataContainer data, SpanParsedEvent<BuildFileRelatedMessageData> @event)
        {
            var actionCollection = data.Get<BuildActionCollection>();

            var newState = Convert(@event.ParsedData.Type.Value);
            var buildTaskId = @event.ParsedData.BuildTaskId.ToNullable();

            var projectName = actionCollection.GetLatestProject(BuildAction, buildTaskId);
            if (string.IsNullOrEmpty(projectName))
            {
                return;
            }

            var changed = actionCollection.HandleStateChange(BuildAction, projectName, newState);
            if (changed)
            {
                var span = actionCollection.GetSpan(BuildAction, projectName);
                var classificationChanged = new ClassificationChangedEvent(span);
                dispatcher.Dispatch(classificationChanged, data);
            }
        }

        private BuildActionState Convert(MessageType messageType)
        {
            switch (messageType)
            {
                case MessageType.Warning:
                    return BuildActionState.Warning;
                case MessageType.Error:
                    return BuildActionState.Error;
                default:
                    return BuildActionState.Unknown;
            }
        }
    }
}