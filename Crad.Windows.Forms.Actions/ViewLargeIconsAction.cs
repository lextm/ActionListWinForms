using System;
using System.Windows.Forms;

namespace Crad.Windows.Forms.Actions
{
    [StandardAction]
    public class ViewLargeIconsAction: ListAction
    {
        public ViewLargeIconsAction()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewLargeIconsAction));
            // 
            // ViewLargeIconsAction
            // 
            this.Image = ((System.Drawing.Image)(resources.GetObject("$this.Image")));
            this.Text = "View &large icons";
            this.ToolTipText = "View large icons";
        }

        protected override View ListViewStyle
        {
            get { return View.LargeIcon; }
        }
    }
}
