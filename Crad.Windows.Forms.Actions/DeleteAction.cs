using System;
using System.Windows.Forms;

namespace Crad.Windows.Forms.Actions
{
    [StandardAction]
    public class DeleteAction: TextBoxBaseAction
    {
        public DeleteAction()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // 
            // DeleteAction
            // 
            this.Image = global::Crad.Windows.Forms.Actions.Properties.Resources.delete;
            this.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.Text = "&Delete";
            this.ToolTipText = "Delete";
        }

        protected override void OnExecute(EventArgs e)
        {
            if (!DesignMode && ActiveTextBox != null)
            {
                ActiveTextBox.SelectedText = string.Empty;
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
