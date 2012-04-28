using System;
using System.Collections.Generic;
using System.Text;

namespace Crad.Windows.Forms.Actions
{
    [StandardAction]
    public class PasteAction: TextBoxBaseAction
    {
        public PasteAction()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // 
            // PasteAction
            // 
            this.Image = global::Crad.Windows.Forms.Actions.Properties.Resources.Paste;
            this.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.Text = "&Paste";
            this.ToolTipText = "Paste";

        }

        protected override void OnExecute(EventArgs e)
        {
            if (!DesignMode && ActiveTextBox != null)
            {
                ActiveTextBox.Paste();
            }
            base.OnExecute(e);
        }

        protected override void OnUpdate(EventArgs e)
        {
            base.OnUpdate(e);
        }
    }
}
