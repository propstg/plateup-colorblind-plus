using Colorblind.Settings;

namespace Colorblind.Labels {

    public class VariableLabel {

        private string compressed;
        private string expanded;

        public VariableLabel(string expanded) {
            this.expanded = expanded;
            this.compressed = expanded;
        }

        public VariableLabel(string expanded, string compressed) {
            this.expanded = expanded;
            this.compressed = compressed;
        }

        public VariableLabel(VariableLabel item1, VariableLabel item2) {
            this.expanded = item1.expanded + item2.expanded;
            this.compressed = item1.compressed + item2.compressed;
        }

        public override string ToString() {
            if (ColorblindPreferences.isDisplayStyle(DisplayStyles.COMPRESSED)) {
                return compressed;
            }
            return expanded;
        }
    }
}
