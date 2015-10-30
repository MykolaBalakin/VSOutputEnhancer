using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Balakin.VSColorfullOutput {
    [Export]
    internal class ColorManager {
        public ColorManager() {
            ErrorColor = Color.FromRgb(216, 80, 80);
            WarningColor = Color.FromRgb(255, 219, 163);
            SuccessColor = Color.FromRgb(87, 166, 74);
        }

        public Color ErrorColor { get; private set; }
        public Color WarningColor { get; private set; }
        public Color SuccessColor { get; private set; }
    }
}
