using System;
using System.Windows.Forms;

namespace Crad.Windows.Forms.Actions
{
    [StandardAction]
    public class AlignCenterAction: AlignmentBaseAction
    {
        protected override HorizontalAlignment Alignment
        {
            get
            {
                return HorizontalAlignment.Center;
            }
        }

        public AlignCenterAction()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            // 
            // AlignCenterAction
            // 
            this.Image = global::Crad.Windows.Forms.Actions.Properties.Resources.AlignCenter;
            this.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.Text = "&Center";
            this.ToolTipText = "Center alignment";

        }
    }
}
