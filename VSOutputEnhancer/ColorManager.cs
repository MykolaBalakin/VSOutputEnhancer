using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Media;

namespace Balakin.VSOutputEnhancer {
    [Export]
    internal class ColorManager {
        public ColorManager() {
            ErrorColor = Color.FromRgb(216, 80, 80);
            WarningColor = Color.FromRgb(233, 213, 133);
            SuccessColor = Color.FromRgb(87, 166, 74);
        }

        public Color ErrorColor { get; private set; }
        public Color WarningColor { get; private set; }
        public Color SuccessColor { get; private set; }
    }
}
