using System;
using System.ComponentModel;
using System.Drawing;

namespace Crad.Windows.Forms.Actions
{
    [StandardAction]
    public class UnderlineAction: FontStyleAction
    {
        public UnderlineAction()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // 
            // UnderlineAction
            // 
            this.CheckOnClick = true;
            this.Image = global::Crad.Windows.Forms.Actions.Properties.Resources.StyleUnderline;
            this.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.Text = "&Underline";
            this.ToolTipText = "Underline";

        }

        protected override FontStyle ActionFontStyle
        {
            get
            {
                return FontStyle.Underline;
            }
        }
    }
}
