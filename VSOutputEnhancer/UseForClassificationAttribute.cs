using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balakin.VSOutputEnhancer {
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    internal class UseForClassificationAttribute : Attribute {
        public UseForClassificationAttribute() {
        }

        public UseForClassificationAttribute(String contentType) {
            ContentType = contentType;
        }

        public String ContentType { get; set; }
        public Type DataProcessor { get; set; }
        public Type Data { get; set; }
    }
}
