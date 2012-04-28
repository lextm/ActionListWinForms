using System;
using System.Windows.Forms;

namespace Crad.Windows.Forms.Actions
{
    [StandardAction]
    public class ViewDetailsAction: ListAction
    {
        public ViewDetailsAction()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewDetailsAction));
            // 
            // ViewDetailsAction
            // 
            this.Image = ((System.Drawing.Image)(resources.GetObject("$this.Image")));
            this.Text = "View &details";
            this.ToolTipText = "View details";

        }
        protected override View ListViewStyle
        {
            get { return View.Details; }
        }
    }
}
