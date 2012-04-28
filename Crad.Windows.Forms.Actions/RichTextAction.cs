using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Crad.Windows.Forms.Actions
{
    public class RichTextAction: Action
    {
        [Browsable(false)]
        protected RichTextBox ActiveRichTextBox
        {
            get 
            {
                RichTextBox result = null;
                if (ActionList != null)
                    result = ActionList.ActiveControl as RichTextBox;

                return result;
            }
        }
    }
}
