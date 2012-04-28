using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Reflection;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Crad.Windows.Forms.Actions.Design
{
    internal sealed class ActionCollectionEditor : CollectionEditor
    {
        private Type[] returnedTypes;

        public ActionCollectionEditor()
            : base(typeof(ActionCollection))
        {}

        protected override Type[] CreateNewItemTypes()
        {
            return returnedTypes;
        }
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (returnedTypes == null)
            {
                returnedTypes = getReturnedTypes(provider);
            }
            return base.EditValue(context, provider, value);
        }

        private Type[] getReturnedTypes(IServiceProvider provider)
        {
            List<Type> res = new List<Type>();

            ITypeDiscoveryService tds = (ITypeDiscoveryService)
                provider.GetService(typeof(ITypeDiscoveryService));
            
            if (tds != null)
                foreach (Type actionType in tds.GetTypes(typeof(Action), false))
                {
                    if (actionType.GetCustomAttributes(typeof(StandardActionAttribute), false).Length > 0 &&
                    !res.Contains(actionType))
                        res.Add(actionType);
                }

            return res.ToArray();
        }
    }
}
