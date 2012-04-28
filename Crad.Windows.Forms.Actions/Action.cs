using System;
using System.Diagnostics;
using System.Reflection;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace Crad.Windows.Forms.Actions
{
    [ToolboxBitmap(typeof(Action), "Images.Action.bmp"),
     DefaultEvent("Execute"), StandardAction]
    public class Action: Component
    {
        protected enum ActionWorkingState
        { 
            Listening,
            Driving
        }

        public Action()
        {
            targets = new List<Component>();
            this._enabled = true;
            this._text = string.Empty;
            this.WorkingState = ActionWorkingState.Listening;
            this.shortcutKeys = Keys.None;
            this.toolTipText = string.Empty;
            this.visible = true;

            this.clickEventHandler = new EventHandler(target_Click);
            this.checkStateChangedEventHandler = new EventHandler(target_CheckStateChanged);
        }

        private ActionWorkingState workingState;
        protected ActionWorkingState WorkingState
        {
            get { return workingState; }
            set { workingState = value; }
        }

        #region Events and event handlers
        public event CancelEventHandler BeforeExecute;
        protected virtual void OnBeforeExecute(CancelEventArgs e)
        {
            if (BeforeExecute != null)
                BeforeExecute(this, e);
        }

        public event EventHandler Execute;
        protected virtual void OnExecute(EventArgs e)
        {
            if (Execute != null)
                Execute(this, e);
        }

        public event EventHandler AfterExecute;
        protected virtual void OnAfterExecute(EventArgs e)
        {
            if (AfterExecute != null)
                AfterExecute(this, e);
        }

        public event EventHandler Update;
        protected virtual void OnUpdate(EventArgs e)
        {
            if (Update != null)
                Update(this, e);
        }

        public void DoUpdate()
        {
            OnUpdate(EventArgs.Empty);
        }
        #endregion

        #region Gestione di collection di oggetti associati
        private List<Component> targets;
        internal void InternalRemoveTarget(Component extendee)
        {
            targets.Remove(extendee);
            RemoveHandler(extendee);
            OnRemovingTarget(extendee);            
        }

        internal void InternalAddTarget(Component extendee)
        {
            targets.Add(extendee);
            refreshState(extendee);
            AddHandler(extendee);
            OnAddingTarget(extendee);
        }

        protected virtual void OnRemovingTarget(Component extendee)
        {
        }

        protected virtual void OnAddingTarget(Component extendee)
        {
        }
        #endregion

        private ActionList actionList;
        protected internal ActionList ActionList
        {
            get { return actionList; }
            set 
            {
                if (actionList != value)
                {
                    actionList = value;
                }
            }
        }
        
        #region common properties
        private string _text;
        [DefaultValue(""), UpdatableProperty(), Localizable(true)]
        public string Text
        {
            get { return _text; }
            set 
            {
                if (_text != value)
                {
                    _text = value;
                    UpdateAllTargets("Text", value);
                }
            }
        }
        
        [DefaultValue(false)]
        public bool Checked
        {
            get
            {
                return (this.checkState != CheckState.Unchecked);
            }
            set
            {
                if (value != this.Checked)
                {
                    this.CheckState = value ? CheckState.Checked : CheckState.Unchecked;
                }
            }
        }

        private CheckState checkState;
        [DefaultValue(CheckState.Unchecked), UpdatableProperty()]
        public CheckState CheckState
        {
            get { return checkState; }
            set
            {
                if (checkState != value)
                {
                    checkState = value;
                    UpdateAllTargets("CheckState", value);
                }
            }
        }

        private bool _enabled;
        [DefaultValue(true), UpdatableProperty]
        public bool Enabled
        {
            get
            {
                if (ActionList != null)
                    return _enabled && ActionList.Enabled;
                else
                    return _enabled;
            }
            set 
            {
                if (_enabled != value)
                {
                    _enabled = value;
                    UpdateAllTargets("Enabled", value);
                }
            }
        }

        private Image image;
        [DefaultValue(null), UpdatableProperty]
        public Image Image
        {
            get { return image; }
            set 
            {
                if (image != value)
                {
                    image = value;
                    UpdateAllTargets("Image", value);
                }
            }
        }

        private bool checkOnClick;
        [DefaultValue(false), UpdatableProperty]
        public bool CheckOnClick
        {
            get { return checkOnClick; }
            set 
            {
                if (checkOnClick != value)
                {
                    checkOnClick = value;
                    UpdateAllTargets("CheckOnClick", value);
                }
            }
        }

        private Keys shortcutKeys;
        [DefaultValue(Keys.None), UpdatableProperty, Localizable(true)]
        public Keys ShortcutKeys
        {
            get { return shortcutKeys; }
            set 
            {
                if (shortcutKeys != value)
                {
                    shortcutKeys = value;
                    KeysConverter kc = new KeysConverter();
                    string s = (string) kc.ConvertTo(value, typeof(string));
                    UpdateAllTargets("ShortcutKeyDisplayString", s);
                }
            }
        }

        private bool visible;
        [DefaultValue(true), UpdatableProperty]
        public bool Visible
        {
            get { return visible; }
            set
            {
                if (visible != value)
                {
                    visible = value;
                    UpdateAllTargets("Visible", value);
                }
            }
        }

        private string toolTipText;
        [DefaultValue(""), UpdatableProperty, Localizable(true)]
        public string ToolTipText
        {
            get { return toolTipText; }
            set 
            {
                if (toolTipText != value)
                {
                    toolTipText = value;
                    UpdateAllTargets("ToolTipText", value);
                }
            }
        }
        #endregion

        #region updating targets
        internal void RefreshEnabledCheckState()
        {
            UpdateAllTargets("Enabled", this.Enabled);
            UpdateAllTargets("CheckState", this.CheckState);
        }

        protected void UpdateAllTargets(string propertyName, object value)
        {
            foreach (Component c in targets)
            {
                updateProperty(c, propertyName, value);
            }
        }
        
        private void updateProperty(Component target, string propertyName, object value)
        {
            WorkingState = ActionWorkingState.Driving;
            try
            {
                if (ActionList != null)
                {
                    if (!SpecialUpdateProperty(target, propertyName, value))
                        ActionList.TypesDescription[target.GetType()].SetValue(
                            propertyName, target, value);
                }
            }
            finally
            {
                WorkingState = ActionWorkingState.Listening;
            }            
        }

        protected virtual bool SpecialUpdateProperty(
            Component target, string propertyName, object value)
        {
            if (propertyName == "ToolTipText")
            {
                Control c = target as Control;

                if (c != null && ActionList.ToolTip.CanExtend(c))
                    ActionList.ToolTip.SetToolTip(c, (string)value);
                return true;
            }
            return false;
        }

        private void refreshState(Component target)
        {
            PropertyDescriptorCollection properties =
            TypeDescriptor.GetProperties(this, new Attribute[] { new UpdatablePropertyAttribute() });

            foreach (PropertyDescriptor property in properties)
            {
                updateProperty(target, property.Name, property.GetValue(this));
            }
        }
        #endregion

        #region Hook su eventi target
        private EventHandler clickEventHandler;
        private EventHandler checkStateChangedEventHandler;

        protected virtual void AddHandler(Component extendee)
        {
            // verifico se extendee possiede l'evento click
            EventInfo clickEvent = extendee.GetType().GetEvent("Click");
            if (clickEvent != null)
            {
                clickEvent.AddEventHandler(extendee, clickEventHandler);
            }

            // verifico se extendee possiede l'evento CheckStateChanged
            EventInfo checkStateChangedEvent = extendee.GetType().GetEvent("CheckStateChanged");
            if (checkStateChangedEvent != null)
            {
                checkStateChangedEvent.AddEventHandler(extendee, checkStateChangedEventHandler);
            }

            // Casi particolari
            // verifico se extendee è un ToolbarButton
            ToolBarButton button = extendee as ToolBarButton;
            if (button != null)
            {
                button.Parent.ButtonClick += new ToolBarButtonClickEventHandler(toolbar_ButtonClick);
            }
        }

        protected virtual void RemoveHandler(Component extendee)
        {
            // verifico se extendee possiede l'evento click
            EventInfo clickEvent = extendee.GetType().GetEvent("Click");
            if (clickEvent != null)
            {
                clickEvent.RemoveEventHandler(extendee, clickEventHandler);
            }

            // verifico se extendee possiede l'evento CheckStateChanged
            EventInfo checkStateChangedEvent = extendee.GetType().GetEvent("CheckStateChanged");
            if (checkStateChangedEvent != null)
            {
                checkStateChangedEvent.RemoveEventHandler(extendee, checkStateChangedEventHandler);
            }

            // Casi particolari
            // verifico se extendee è un ToolbarButton
            ToolBarButton button = extendee as ToolBarButton;
            if (button != null)
            {
                button.Parent.ButtonClick -= new ToolBarButtonClickEventHandler(toolbar_ButtonClick);
            }
        }

        #endregion

        #region Handling eventi target
        #region Click
        private void toolbar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            // called if sender is ToolBarButton
            if (this.targets.Contains(e.Button))
                handleClick(e.Button, e);
        }

        private void target_Click(object sender, EventArgs e)
        {
            // called if sender is Control
            handleClick(sender, e);
        }

        private void handleClick(object sender, EventArgs e)
        {
            if (WorkingState == ActionWorkingState.Listening)
            {
                Component target = sender as Component;
                Debug.Assert(target != null, "Target non è un component su handleClick");
                Debug.Assert(targets.Contains(target), "Target non esiste su collection targets su handleClick");

                DoExecute();
            }
        }
        #endregion

        #region CheckStateChanged
        private void target_CheckStateChanged(object sender, EventArgs e)
        {
            handleCheckStateChanged(sender, e);
        }

        private bool interceptingCheckStateChanged;
        internal bool InterceptingCheckStateChanged
        {
            get { return interceptingCheckStateChanged; }
            set { interceptingCheckStateChanged = value; }
        }
        private void handleCheckStateChanged(object sender, EventArgs e)
        {
            if (WorkingState == ActionWorkingState.Listening)
            {
                Component target = sender as Component;
                CheckState = (CheckState)ActionList.
                    TypesDescription[sender.GetType()].GetValue("CheckState", sender);
                    
            }
        }
        #endregion
        #endregion

        #region Action execution
        public void DoExecute()
        {
            if (!Enabled)
                return;

            CancelEventArgs e = new CancelEventArgs();
            OnBeforeExecute(e);
            if (e.Cancel)
                return;
            OnExecute(EventArgs.Empty);
            OnAfterExecute(EventArgs.Empty);
        }

        internal void ExecuteShortcut()
        {
            if (!Enabled)
                return;

            if (CheckOnClick)
                this.Checked = !this.Checked;
            DoExecute();
        }
        #endregion        
    }
}
