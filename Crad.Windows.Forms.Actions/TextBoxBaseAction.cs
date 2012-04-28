using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Crad.Windows.Forms.Actions
{
    public class TextBoxBaseAction: Action
    {
        [Browsable(false)]
        protected TextBoxBase ActiveTextBox
        {
            get
            {
                TextBoxBase result = null;
                if (this.ActionList != null)
                    result = this.ActionList.ActiveControl as TextBoxBase;

                return result;
            }
        }
    }
}
