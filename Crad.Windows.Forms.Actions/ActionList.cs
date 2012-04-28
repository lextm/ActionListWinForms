using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace Crad.Windows.Forms.Actions
{
    [ProvideProperty("Action", typeof(Component)),
     ToolboxBitmap(typeof(ActionList), "Images.ActionList.bmp"),
     ToolboxItemFilter("System.Windows.Forms")]
    public class ActionList: Component, IExtenderProvider, ISupportInitialize
    {
        public ActionList()
        {
            this.actions = new ActionCollection(this);
            this.targets = new Dictionary<Component, Action>();
            this.typesDescription = new Dictionary<Type, ActionTargetDescriptionInfo>();
            this.enabled = true;
            this.tooltip = new ToolTip();

            if (!DesignMode)
                Application.Idle += new EventHandler(Application_Idle);
        }

        #region ToolTip section
        private ToolTip tooltip;
        public ToolTip ToolTip
        {
            get { return tooltip; }
        }
        #endregion

        #region events and event raisers
        void Application_Idle(object sender, EventArgs e)
        {
            OnUpdate(EventArgs.Empty);
        }
        
        public event EventHandler Update;
        protected virtual void OnUpdate(EventArgs eventArgs)
        {
            // si solleva l'evento Update per l'ActionList
            if (Update != null)
                Update(this, eventArgs);

            foreach (Action action in actions)
            {
                action.DoUpdate();
            }
        }
        #endregion

        #region Properties
        private bool enabled;
        [DefaultValue(true)]
        public bool Enabled
        {
            get { return enabled; }
            set 
            {
                if (enabled != value)
                {
                    enabled = value;
                    refreshActions();
                }
            }
        }

        private ActionCollection actions;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ActionCollection Actions
        {
            get { return actions; }
        }
        #endregion                

        #region IExtenderProvider Members
        private Dictionary<Component, Action> targets;

        [DefaultValue(null)]
        public Action GetAction(Component extendee)
        {
            if (targets.ContainsKey(extendee))
                return targets[extendee];
            else
                return null;
        }

        public void SetAction(Component extendee, Action action)
        {
            if (!initializing)
            {
                if (extendee == null)
                    throw new ArgumentNullException("extendee");
                if (action != null && action.ActionList != this)
                    throw new ArgumentException("The Action you selected is owned by another ActionList");
            }

            /* Se extendee appartiene già alla collection, rimuovo l'handler
             * sul suo evento Click e lo rimuovo dai component associati alla
             * collection */
            if (targets.ContainsKey(extendee))
            {
                targets[extendee].InternalRemoveTarget(extendee);
                targets.Remove(extendee);
            }

            /* Aggiungo extendee alla collection */
            if (action != null)
            {
                // eventualmente aggiungo le informazioni sul tipo
                if (!typesDescription.ContainsKey(extendee.GetType()))
                {
                    typesDescription.Add(extendee.GetType(),
                        new ActionTargetDescriptionInfo(extendee.GetType()));
                }

                targets.Add(extendee, action);
                action.InternalAddTarget(extendee);
            }            
        }        

        bool IExtenderProvider.CanExtend(object extendee)
        {
            Type targetType = extendee.GetType();

            foreach (Type t in GetSupportedTypes())
                if (t.IsAssignableFrom(targetType))
                {
                    return true;
                }

            return false;
        }

        protected virtual Type[] GetSupportedTypes()
        {
            return new Type[] {typeof(ButtonBase), typeof(ToolStripButton),
                typeof(ToolStripMenuItem), typeof(ToolBarButton), typeof(MenuItem)};
        }
        #endregion

        #region types informations
        private Dictionary<Type, ActionTargetDescriptionInfo> typesDescription;
        
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Dictionary<Type, ActionTargetDescriptionInfo> TypesDescription
        {
            get { return typesDescription; }
        }
        #endregion

        #region ISupportInitialize Members
        private bool initializing;

        public void BeginInit()
        {
            initializing = true;
        }

        public void EndInit()
        {
            initializing = false;
            checkInternalCollections();
            refreshActions();
        }

        private void refreshActions()
        {
            /* questo metodo effettua il refresh dello stato Enabled e CheckState
             * di ogni action */
            if (DesignMode)
                return;

            foreach (Action action in actions)
                action.RefreshEnabledCheckState();
        }

        private void checkInternalCollections()
        {
            /* questo metodo verifica che ogni action su targets
             * appartenga a questa actionList e che abbia la proprietà
             * ActionList correttamente impostata */
            foreach (Action action in targets.Values)
            {
                if (!Actions.Contains(action) || action.ActionList != this)
                    throw new InvalidOperationException(
                        "Action owned by another action list or invalid Action.ActionList");
            }
        }

        #endregion

        #region reference to ContainerControl
        private ContainerControl containerControl;
        public ContainerControl ContainerControl
        {
            get { return containerControl; }
            set
            {
                if (containerControl != value)
                {
                    containerControl = value;
                    if (!DesignMode)
                    {
                        Form f = containerControl as Form;
                        f.KeyPreview = true;
                        f.KeyDown += new KeyEventHandler(form_KeyDown);
                    }
                }
            }
        }

        [Browsable(false)]
        public Control ActiveControl
        {
            get { return getActiveControl(this.ContainerControl); }
        }

        private Control getActiveControl(ContainerControl containerControl)
        {
            if (containerControl == null)
                return null;
            else if (containerControl.ActiveControl is ContainerControl)
                return getActiveControl((ContainerControl)containerControl.ActiveControl);
            else
                return containerControl.ActiveControl;
        }

        private void form_KeyDown(object sender, KeyEventArgs e)
        {
            foreach (Action action in actions)
            {
                if (action.ShortcutKeys == (Keys) e.KeyData)
                    action.ExecuteShortcut();
            }
        }

        public override ISite Site
        {
            get
            {
                return base.Site;
            }
            set
            {
                base.Site = value;
                if (value != null)
                {
                    IDesignerHost host1 = value.GetService(typeof(IDesignerHost)) as IDesignerHost;
                    if (host1 != null)
                    {
                        IComponent component1 = host1.RootComponent;
                        if (component1 is ContainerControl)
                        {
                            this.ContainerControl = (ContainerControl)component1;                            
                        }
                    }
                }
            }
        }
        #endregion
    }
}
