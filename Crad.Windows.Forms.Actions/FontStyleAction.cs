using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Crad.Windows.Forms.Actions
{
    public class FontStyleAction: RichTextAction
    {
        protected override void OnExecute(EventArgs e)
        {
            if (!DesignMode && ActiveRichTextBox != null)
            {
                setFontStyle(ActiveRichTextBox, ActionFontStyle);
            }
            base.OnExecute(e);
        }
        protected override void OnUpdate(EventArgs e)
        {
            if (!DesignMode)
            {
                this.Checked =
                    (ActiveRichTextBox != null &&
                     hasFontStyle(ActiveRichTextBox, ActionFontStyle));
            }
            base.OnUpdate(e);            
        }

        private bool hasFontStyle(RichTextBox rtb, FontStyle style)
        {
            return (rtb.SelectionFont.Style & style) == style;
        }
        private void setFontStyle(RichTextBox rtb, FontStyle style)
        {
            if (rtb.SelectionLength == 0)
                setCharFontStyle(rtb, style);
            else
            {
                // to avoid screen refreshing, we create a fake RichTextBox
                using (RichTextBox a = new RichTextBox())
                {
                    a.SuspendLayout();
                    a.SelectedRtf = rtb.SelectedRtf;
                    a.SelectAll();
                    int selectionStart = a.SelectionStart;
                    int selectionLength = a.SelectionLength;
                    int selectionEnd = selectionStart + selectionLength;
                    for (int x = selectionStart; x < selectionEnd; ++x)
                    {
                        // Set temporary selection
                        a.Select(x, 1);
                        // Toggle font style of the selection
                        setCharFontStyle(a, style);
                    }
                    // Restore the original selection
                    a.SelectAll();
                    rtb.SelectedRtf = a.SelectedRtf;
                }
            }
        }

        private void setCharFontStyle(RichTextBox rtb, FontStyle style)
        {
            if (this.Checked)
                rtb.SelectionFont = new Font(rtb.SelectionFont,
                    rtb.SelectionFont.Style | style);
            else
                rtb.SelectionFont = new Font(rtb.SelectionFont,
                    rtb.SelectionFont.Style & (~style));
        }
        protected virtual FontStyle ActionFontStyle
        {
            get { return FontStyle.Regular; }
        }

        private void InitializeComponent()
        {
            // 
            // FontStyleAction
            // 
            this.CheckOnClick = true;

        }
    }
}
