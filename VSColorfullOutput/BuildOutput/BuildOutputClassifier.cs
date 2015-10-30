//------------------------------------------------------------------------------
// <copyright file="BuildOutputClassifier.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Balakin.VSColorfullOutput.Parsers;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace Balakin.VSColorfullOutput.BuildOutput {
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
            classificationTypes = new Dictionary<String, IClassificationType>
            {
                { "BuildSucceeded", registry.GetClassificationType("BuildSucceeded") },
                { "BuildFailed", registry.GetClassificationType("BuildFailed") },
                { "BuildError", registry.GetClassificationType("BuildError") },
                { "BuildWarning", registry.GetClassificationType("BuildWarning") }
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
        }

        private IEnumerable<ClassificationSpan> EnumerateBuildResultSpans(SnapshotSpan span) {
            BuildResult buildResult;
            if (BuildResult.TryParse(span, out buildResult)) {
                if (buildResult.Failed == 0) {
                    yield return CreateClassificationSpan(span, classificationTypes["BuildSucceeded"]);
                } else {
                    yield return CreateClassificationSpan(span, classificationTypes["BuildFailed"]);
                }
            }
        }

        private IEnumerable<ClassificationSpan> EnumerateBuildFileRelatedMessageSpans(SnapshotSpan span) {
            BuildFileRelatedMessage message;
            if (BuildFileRelatedMessage.TryParse(span, out message)) {
                if (message.MessageType == BuildMessageType.Error) {
                    yield return CreateClassificationSpan(span, classificationTypes["BuildError"]);
                }else if (message.MessageType == BuildMessageType.Warning) {
                    yield return CreateClassificationSpan(span, classificationTypes["BuildWarning"]);
                }
            }
        }

        private ClassificationSpan CreateClassificationSpan(SnapshotSpan span, IClassificationType classificationType) {
            return new ClassificationSpan(span, classificationType);
        }
    }
}
