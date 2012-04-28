using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing.Design;

namespace Crad.Windows.Forms.Actions
{
    [Editor(typeof(Design.ActionCollectionEditor), typeof(UITypeEditor))]
    public class ActionCollection: Collection<Action>
    {
        public ActionCollection(ActionList parent)
        {
            this.parent = parent;
        }

        private ActionList parent;
        public ActionList Parent
        {
            get { return parent; }
        }

        protected override void ClearItems()
        {
            foreach (Action action in this)
                action.ActionList = null;

            base.ClearItems();
        }

        protected override void InsertItem(int index, Action item)
        {
            base.InsertItem(index, item);
            item.ActionList = Parent;
        }

        protected override void RemoveItem(int index)
        {
            this[index].ActionList = null;
            base.RemoveItem(index);
        }

        protected override void SetItem(int index, Action item)
        {
            if (this.Count > index)
                this[index].ActionList = null;
            base.SetItem(index, item);

            item.ActionList = Parent;
        }
    }
}
