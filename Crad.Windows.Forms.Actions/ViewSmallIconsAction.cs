using System;
using System.Windows.Forms;

namespace Crad.Windows.Forms.Actions
{
    [StandardAction]
    public class ViewSmallIconsAction: ListAction
    {
        public ViewSmallIconsAction()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewSmallIconsAction));
            // 
            // ViewSmallIconsAction
            // 
            this.Image = ((System.Drawing.Image)(resources.GetObject("$this.Image")));
            this.Text = "View &small icons";
            this.ToolTipText = "View small icons";
        }

        protected override View ListViewStyle
        {
            get { return View.SmallIcon; }
        }
    }
}
