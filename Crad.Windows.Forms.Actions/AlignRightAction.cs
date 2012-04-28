using System;
using System.Windows.Forms;

namespace Crad.Windows.Forms.Actions
{
    [StandardAction]
    public class AlignRightAction: AlignmentBaseAction
    {
        protected override HorizontalAlignment Alignment
        {
            get { return HorizontalAlignment.Right; }
        }

        public AlignRightAction()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // 
            // AlignRightAction
            // 
            this.Image = global::Crad.Windows.Forms.Actions.Properties.Resources.AlignRight;
            this.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.Text = "&Right";
            this.ToolTipText = "Right alignment";

        }
    }
}
