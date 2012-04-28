using System;
using System.Windows.Forms;

namespace Crad.Windows.Forms.Actions
{
    [StandardAction]
    public class SelectAllAction: TextBoxBaseAction
    {
        public SelectAllAction()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // 
            // SelectAllAction
            // 
            this.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.NumPad5)));
            this.Text = "Select &All";
            this.ToolTipText = "Select All";

        }

        protected override void OnExecute(EventArgs e)
        {
            if (!DesignMode)
            {
                if (ActiveTextBox != null && ActiveTextBox.CanSelect)
                    ActiveTextBox.SelectAll();

            }
            base.OnExecute(e);
        }
        protected override void OnUpdate(EventArgs e)
        {
            if (!DesignMode)
            {
                this.Enabled = (ActiveTextBox != null && ActiveTextBox.CanSelect);
            }
            base.OnUpdate(e);
        }
    }
}
