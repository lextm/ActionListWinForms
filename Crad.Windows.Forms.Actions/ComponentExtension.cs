/*
 * Created by SharpDevelop.
 * User: Lex
 * Date: 8/4/2012
 * Time: 2:31 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;

namespace Crad.Windows.Forms.Actions
{
    /// <summary>
    /// Description of ComponentHelper.
    /// </summary>
    public static class ComponentExtension
    {
        internal static bool IsInDesignMode(Component component)
        {
            // TODO: make this function an extension method if upgraded to .NET 3.5+
            string location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            return location.Contains("VisualStudio") || location.Contains("SharpDevelop");
        }
    }
}
