using System;
using System.Windows.Forms;

namespace Crad.Windows.Forms.Actions
{
    public class AlignmentBaseAction: RichTextAction
    {
        protected virtual HorizontalAlignment Alignment
        {
            get { return HorizontalAlignment.Left; }
        }

        protected override void OnExecute(EventArgs e)
        {
            if (!DesignMode && ActiveRichTextBox != null)
            {
                ActiveRichTextBox.SelectionAlignment = Alignment;
            }
            base.OnExecute(e);
        }
        protected override void OnUpdate(EventArgs e)
        {
            if (!DesignMode)
            {
                this.Checked =
                    (ActiveRichTextBox != null &&
                     ActiveRichTextBox.SelectionAlignment == Alignment);
            }
            base.OnUpdate(e);
        }

        public AlignmentBaseAction()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // 
            // ParagraphStyleAction
            // 
            this.CheckOnClick = true;
        }
    }
}
