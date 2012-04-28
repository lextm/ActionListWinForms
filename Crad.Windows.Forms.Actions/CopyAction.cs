using System;

namespace Crad.Windows.Forms.Actions
{
    [StandardAction]
    public class CopyAction: TextBoxBaseAction
    {
        public CopyAction()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // 
            // CopyAction
            // 
            this.Image = global::Crad.Windows.Forms.Actions.Properties.Resources.Copy;
            this.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.Text = "&Copy";
            this.ToolTipText = "Copy";

        }

        protected override void OnExecute(EventArgs e)
        {
            if (!DesignMode && ActiveTextBox != null)
            {
                ActiveTextBox.Copy();
            }
            base.OnExecute(e);
        }

        protected override void OnUpdate(EventArgs e)
        {
            if (!DesignMode)
            {
                this.Enabled = (ActiveTextBox != null &&
                    ActiveTextBox.SelectionLength > 0);
            }
            base.OnUpdate(e);
        }
    }
}
