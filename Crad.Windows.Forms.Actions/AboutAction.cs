using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Crad.Windows.Forms.Actions
{
    [StandardAction]
    public class AboutAction: Action, ISupportInitialize
    {
        public AboutAction()
        {
            productName = Application.ProductName;
            textPrefix = "About {0}...";
        }

        private string productName;

        private string textPrefix;
        [DefaultValue("About {0}...")]
        public string TextPrefix
        {
            get { return textPrefix; }
            set
            {
                textPrefix = value;
                (this as ISupportInitialize).EndInit();
            }
        }

        #region ISupportInitialize Members

        void ISupportInitialize.BeginInit()
        {
        }

        void ISupportInitialize.EndInit()
        {
            if (!DesignMode)
                this.Text = string.Format(textPrefix, productName);
        }

        #endregion
    }
}
