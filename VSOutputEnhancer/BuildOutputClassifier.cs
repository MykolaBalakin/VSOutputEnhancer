//------------------------------------------------------------------------------
// <copyright file="BuildOutputClassifier.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Balakin.VSOutputEnhancer.Parsers;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace Balakin.VSOutputEnhancer {
    /// <summary>
    /// Classifier that classifies all text as an instance of the "BuildOutputClassifier" classification type.
    /// </summary>
    internal class BuildOutputClassifier : IClassifier {
        private readonly IDictionary<String, IClassificationType> classificationTypes;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildOutputClassifier"/> class.
        /// </summary>
        /// <param name="registry">Classification registry.</param>
        internal BuildOutputClassifier(IClassificationTypeRegistryService registry) {
            classificationTypes = new Dictionary<String, IClassificationType> {
                { ClassificationType.BuildResultSucceeded, registry.GetClassificationType(ClassificationType.BuildResultSucceeded) },
                { ClassificationType.BuildResultFailed, registry.GetClassificationType(ClassificationType.BuildResultFailed) },
                { ClassificationType.BuildMessageError, registry.GetClassificationType(ClassificationType.BuildMessageError) },
                { ClassificationType.BuildMessageWarning, registry.GetClassificationType(ClassificationType.BuildMessageWarning) },
                { ClassificationType.PublishResultFailed, registry.GetClassificationType(ClassificationType.PublishResultFailed) },
                { ClassificationType.PublishResultSucceeded, registry.GetClassificationType(ClassificationType.PublishResultSucceeded) }
            };
        }

        #region IClassifier

#pragma warning disable 67

        /// <summary>
        /// An event that occurs when the classification of a span of text has changed.
        /// </summary>
        /// <remarks>
        /// This event gets raised if a non-text change would affect the classification in some way,
        /// for example typing /* would cause the classification to change in C# without directly
        /// affecting the span.
        /// </remarks>
        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;

#pragma warning restore 67

        /// <summary>
        /// Gets all the <see cref="ClassificationSpan"/> objects that intersect with the given range of text.
        /// </summary>
        /// <remarks>
        /// This method scans the given SnapshotSpan for potential matches for this classification.
        /// In this instance, it classifies everything and returns each span as a new ClassificationSpan.
        /// </remarks>
        /// <param name="span">The span currently being classified.</param>
        /// <returns>A list of ClassificationSpans that represent spans identified to be of this classification.</returns>
        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span) {
            return EnumerateClassificationSpans(span).ToList();
        }

        #endregion

        private IEnumerable<ClassificationSpan> EnumerateClassificationSpans(SnapshotSpan span) {
            foreach (var classificationSpan in EnumerateBuildResultSpans(span)) {
                yield return classificationSpan;
            }
            foreach (var classificationSpan in EnumerateBuildFileRelatedMessageSpans(span)) {
                yield return classificationSpan;
            }
            foreach (var classificationSpan in EnumeratePublishResultSpans(span)) {
                yield return classificationSpan;
            }
        }

        private IEnumerable<ClassificationSpan> EnumerateBuildResultSpans(SnapshotSpan span) {
            BuildResult buildResult;
            if (!BuildResult.TryParse(span, out buildResult)) {
                yield break;
            }
            if (buildResult.Failed == 0) {
                yield return new ClassificationSpan(span, classificationTypes[ClassificationType.BuildResultSucceeded]);
            } else {
                yield return new ClassificationSpan(span, classificationTypes[ClassificationType.BuildResultFailed]);
            }
        }

        private IEnumerable<ClassificationSpan> EnumeratePublishResultSpans(SnapshotSpan span) {
            PublishResultParser publishResult;
            if (!PublishResultParser.TryParse(span, out publishResult)) {
                yield break;
            }
            if (publishResult.Failed == 0) {
                yield return new ClassificationSpan(span, classificationTypes[ClassificationType.PublishResultSucceeded]);
            } else {
                yield return new ClassificationSpan(span, classificationTypes[ClassificationType.PublishResultFailed]);
            }
        }

        private IEnumerable<ClassificationSpan> EnumerateBuildFileRelatedMessageSpans(SnapshotSpan span) {
            BuildFileRelatedMessage message;
            if (!BuildFileRelatedMessage.TryParse(span, out message)) {
                yield break;
            }
            foreach (var classificationSpan in CreateSpansForBuildMessage(span, message)) {
                yield return classificationSpan;
            }
        }

        private IEnumerable<ClassificationSpan> CreateSpansForBuildMessage(SnapshotSpan span, BuildMessage message) {
            if (message.MessageType == BuildMessageType.Unknown) {
                yield break;
            }

            var classificatedSpan = new SnapshotSpan(span.Snapshot, span.Start.Position + message.Span.Start, message.Span.Length);

            if (message.MessageType == BuildMessageType.Error) {
                yield return new ClassificationSpan(classificatedSpan, classificationTypes[ClassificationType.BuildMessageError]);
            } else if (message.MessageType == BuildMessageType.Warning) {
                yield return new ClassificationSpan(classificatedSpan, classificationTypes[ClassificationType.BuildMessageWarning]);
            }
        }
    }
}
