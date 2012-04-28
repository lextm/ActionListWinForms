using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Crad.Windows.Forms.Actions
{
    public class ListAction: Action
    {
        public ListAction()
        {
            InitializeComponent();
        }

        private ListView targetListView;
        
        [DefaultValue(null)]
        public ListView TargetListView
        {
            get { return targetListView; }
            set { targetListView = value; }
        }


        protected virtual View ListViewStyle
        {
            get { return View.Details; }
        }
        
        protected override void OnExecute(EventArgs e)
        {
            if (!DesignMode && TargetListView != null)
            {
                TargetListView.View = ListViewStyle;
            }
            base.OnExecute(e);
        }

        protected override void OnUpdate(EventArgs e)
        {
            if (!DesignMode)
            {
                this.Checked = (TargetListView != null &&
                    TargetListView.View == ListViewStyle);
            }
            base.OnUpdate(e);
        }

        private void InitializeComponent()
        {
            // 
            // ListAction
            // 
            this.CheckOnClick = true;

        }
    }
}
