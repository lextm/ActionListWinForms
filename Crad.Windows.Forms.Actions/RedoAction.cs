using System;
using System.Windows.Forms;

namespace Crad.Windows.Forms.Actions
{
    [StandardAction]
    public class RedoAction: RichTextAction
    {
        public RedoAction()
        {
            InitializeComponent();
        }

        protected override void OnExecute(EventArgs e)
        {
            if (!DesignMode && ActiveRichTextBox != null)
            {
                this.ActiveRichTextBox.Redo();
            }
            base.OnExecute(e);
        }
        protected override void OnUpdate(EventArgs e)
        {
            if (!DesignMode)
            {
                this.Enabled = (ActiveRichTextBox != null && ActiveRichTextBox.CanRedo);
            }

            base.OnUpdate(e);
        }

        private void InitializeComponent()
        {
            // 
            // RedoAction
            // 
            this.Image = global::Crad.Windows.Forms.Actions.Properties.Resources.Redo;
            this.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.Text = "&Redo";
            this.ToolTipText = "Redo";

        }
    }
}
