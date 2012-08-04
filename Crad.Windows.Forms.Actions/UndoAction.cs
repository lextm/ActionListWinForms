using System;

namespace Crad.Windows.Forms.Actions
{
    [StandardAction]
    public class UndoAction: TextBoxBaseAction
    {
        public UndoAction()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // 
            // UndoAction
            // 
            this.Image = global::Crad.Windows.Forms.Actions.Properties.Resources.Undo;
            this.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.Text = "&Undo";
            this.ToolTipText = "Undo";
        }

        protected override void OnExecute(EventArgs e)
        {
            if (!ComponentExtension.IsInDesignMode(this) && ActiveTextBox != null)
            {
                this.ActiveTextBox.Undo();
            }
            base.OnExecute(e);
        }

        protected override void OnUpdate(EventArgs e)
        {
            if (!ComponentExtension.IsInDesignMode(this))
            {
                this.Enabled = (ActiveTextBox != null && ActiveTextBox.CanUndo);
            }

            base.OnUpdate(e);
        }
    }
}
