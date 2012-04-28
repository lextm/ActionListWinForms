using System;
using System.ComponentModel;
using System.Drawing;

namespace Crad.Windows.Forms.Actions
{
    [StandardAction]
    public class ItalicAction: FontStyleAction
    {
        public ItalicAction()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // 
            // ItalicAction
            // 
            this.CheckOnClick = true;
            this.Image = global::Crad.Windows.Forms.Actions.Properties.Resources.StyleItalic;
            this.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.Text = "&Italic";
            this.ToolTipText = "Italic";

        }

        protected override FontStyle ActionFontStyle
        {
            get
            {
                return FontStyle.Italic;
            }
        }
    }
}
