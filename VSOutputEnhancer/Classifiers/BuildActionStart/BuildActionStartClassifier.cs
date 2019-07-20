using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Balakin.VSOutputEnhancer.Parsers;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Classifiers.BuildActionStart
{
    [Export(typeof(ISpanClassifier))]
    public class BuildActionStartClassifier : ParserBasedSpanClassifier<BuildActionStartData>
    {
        [ImportingConstructor]
        public BuildActionStartClassifier(IParser<BuildActionStartData> parser) : base(parser)
        {
        }

        public override IEnumerable<String> ContentTypes { get; } = new[]
        {
            ContentType.BuildOutput,
            ContentType.BuildOrderOutput
        };

        protected override IEnumerable<ProcessedParsedData> Classify(SnapshotSpan span, BuildActionStartData parsedData, DataContainer data)
        {
            var action = parsedData.Action.Value;
            var projectName = parsedData.ProjectName.Value;
            var buildTaskId = parsedData.BuildTaskId.ToNullable();
            var actionCollection = data.Get<BuildActionCollection>();

            actionCollection.HandleActionStart(action, projectName, buildTaskId, span);
            var state = actionCollection.GetState(action, projectName);

            switch (state)
            {
                case BuildActionState.Success:
                    return new[] { new ProcessedParsedData(parsedData.FullMessage.Span, ClassificationType.BuildStartedSuccess) };
                case BuildActionState.Warning:
                    return new[] { new ProcessedParsedData(parsedData.FullMessage.Span, ClassificationType.BuildStartedWarning) };
                case BuildActionState.Error:
                    return new[] { new ProcessedParsedData(parsedData.FullMessage.Span, ClassificationType.BuildStartedError) };
                default:
                    return EmptyClassification;
            }
        }
    }
}