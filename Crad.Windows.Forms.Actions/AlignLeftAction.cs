using System;
using System.Windows.Forms;

namespace Crad.Windows.Forms.Actions
{
    [StandardAction]
    public class AlignLeftAction: AlignmentBaseAction
    {
        protected override HorizontalAlignment Alignment
        {
            get { return HorizontalAlignment.Left; }
        }

        public AlignLeftAction()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // 
            // AlignLeftAction
            // 
            this.Image = global::Crad.Windows.Forms.Actions.Properties.Resources.AlignLeft;
            this.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.Text = "&Left";
            this.ToolTipText = "Left alignment";

        }
    }
}
