//------------------------------------------------------------------------------
// <copyright file="BuildOutputClassifierFormat.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSColorfullOutput.BuildOutput.Formats {
    /// <summary>
    /// Defines an editor format for the BuildOutputClassifier type that has a purple background
    /// and is underlined.
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "BuildWarning")]
    [Name("BuildWarning")]
    [UserVisible(true)] // This should be visible to the end user
    [Order(Before = Priority.Default)] // Set the priority to be after the default classifiers
    internal sealed class BuildWarningClassifierFormat : ClassificationFormatDefinition {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildSucceededClassifierFormat"/> class.
        /// </summary>
        public BuildWarningClassifierFormat() {
            DisplayName = "Build warning";
            ForegroundColor = Colors.Yellow;
        }
    }
}
