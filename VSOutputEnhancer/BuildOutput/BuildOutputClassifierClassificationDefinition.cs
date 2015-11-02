//------------------------------------------------------------------------------
// <copyright file="BuildOutputClassifierClassificationDefinition.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.BuildOutput {
    /// <summary>
    /// Classification type definition export for BuildOutputClassifier
    /// </summary>
    internal static class BuildOutputClassifierClassificationDefinition {
        // This disables "The field is never used" compiler's warning. Justification: the field is used by MEF.
#pragma warning disable 169
        // ReSharper disable UnassignedField.Local

        /// <summary>
        /// Defines the "BuildSucceeded" classification type.
        /// </summary>
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("BuildSucceeded")]
        private static ClassificationTypeDefinition buildSucceededDefinition;

        /// <summary>
        /// Defines the "BuildFailed" classification type.
        /// </summary>
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("BuildFailed")]
        private static ClassificationTypeDefinition buildFailedDefinition;

        /// <summary>
        /// Defines the "BuildError" classification type.
        /// </summary>
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("BuildError")]
        private static ClassificationTypeDefinition buildErrorDefinition;

        /// <summary>
        /// Defines the "BuildWarning" classification type.
        /// </summary>
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("BuildWarning")]
        private static ClassificationTypeDefinition buildWarningDefinition;

        // ReSharper restore UnassignedField.Local
#pragma warning restore 169
    }
}
