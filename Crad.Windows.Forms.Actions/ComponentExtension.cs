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
using System.Collections.Generic;
using System.Windows.Forms;

namespace Crad.Windows.Forms.Actions
{
    /// <summary>
    /// Description of ComponentHelper.
    /// </summary>
    public static class ComponentExtension
    {
        private static bool _designMode;
        
        static ComponentExtension()
        {
            var designerHosts = new List<string>() { "devenv", "vcsexpress", "vbexpress", "vcexpress", "wdexpress", "sharpdevelop" };
            var processName = System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToLowerInvariant();
            _designMode = designerHosts.Contains(processName);
        }
        
        internal static bool IsInDesignMode(Component component)
        {
            // TODO: make this function an extension method if upgraded to .NET 3.5+
            return _designMode;
        }
    }
}
