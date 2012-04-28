using System;
using System.Reflection;
using System.Collections.Generic;

namespace Crad.Windows.Forms.Actions
{
    public class ActionTargetDescriptionInfo
    {
        public ActionTargetDescriptionInfo(Type targetType)
        {
            this.properties = new Dictionary<string,PropertyInfo>();
            this.targetType = targetType;

            foreach (PropertyInfo property in targetType.GetProperties())
                properties.Add(property.Name, property);
        }

        private Dictionary<string, PropertyInfo> properties;

        private Type targetType;
        public Type TargetType
        {
            get { return targetType; }
        }

        internal void SetValue(string propertyName, object target, object value)
        {
            if (properties.ContainsKey(propertyName))
                properties[propertyName].SetValue(target, value, null);
        }

        internal object GetValue(string propertyName, object source)
        {
            if (properties.ContainsKey(propertyName))
                return properties[propertyName].GetValue(source, null);
                
            return null;
        }
    }
}
