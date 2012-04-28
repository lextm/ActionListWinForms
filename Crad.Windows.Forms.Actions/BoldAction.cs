using System;
using System.ComponentModel;
using System.Drawing;

namespace Crad.Windows.Forms.Actions
{
    [StandardAction]
    public class BoldAction: FontStyleAction
    {
        public BoldAction()
        {
            InitializeComponent();
        }

        #region InitializeComponent
        private void InitializeComponent()
        {
            // 
            // BoldAction
            // 
            this.CheckOnClick = true;
            this.Image = global::Crad.Windows.Forms.Actions.Properties.Resources.StyleBold;
            this.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.Text = "&Bold";
            this.ToolTipText = "Bold";

        }
        #endregion

        protected override FontStyle ActionFontStyle
        {
            get
            {
                return FontStyle.Bold;
            }
        }
    }
}
