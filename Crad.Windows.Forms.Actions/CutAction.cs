using System;
using System.Collections.Generic;
using System.Text;

namespace Crad.Windows.Forms.Actions
{
    [StandardAction]
    public class CutAction: TextBoxBaseAction
    {
        public CutAction()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // 
            // CutAction
            // 
            this.Image = global::Crad.Windows.Forms.Actions.Properties.Resources.Cut;
            this.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.Text = "Cu&t";
            this.ToolTipText = "Cut";

        }

        protected override void OnExecute(EventArgs e)
        {
            if (!DesignMode && ActiveTextBox != null)
            {
                ActiveTextBox.Cut();
            }
            base.OnExecute(e);
        }
        protected override void OnUpdate(EventArgs e)
        {
            if (!DesignMode)
            {
                this.Enabled = (ActiveTextBox != null &&
                    ActiveTextBox.SelectionLength > 0 && !ActiveTextBox.ReadOnly);
            }
            base.OnUpdate(e);
        }
    }
}
