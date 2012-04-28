using System;
using System.Windows.Forms;

namespace Crad.Windows.Forms.Actions
{
    [StandardAction]
    public class ViewListAction: ListAction
    {
        public ViewListAction()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewListAction));
            // 
            // ViewListAction
            // 
            this.Image = ((System.Drawing.Image)(resources.GetObject("$this.Image")));
            this.Text = "View &list";
            this.ToolTipText = "View list";

        }

        protected override View ListViewStyle
        {
            get { return View.List; }
        }
    }
}
