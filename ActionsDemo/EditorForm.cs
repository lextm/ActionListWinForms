using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Crad.Windows.Forms.Actions.Sample
{
    public partial class EditorForm : Form
    {
        private bool _modified;
        private bool modified
        {
            get { return _modified; }
            set 
            {
                if (_modified != value)
                {
                    _modified = value;
                    buildFormTitle();
                }
            }
        }

        private string _fileName;
        private string fileName
        {
            get
            {
                if (string.IsNullOrEmpty(_fileName))
                    return "Untitled Document";
                else
                    return _fileName; 
            }
            set 
            {
                if (_fileName != value)
                {
                    _fileName = value;
                    buildFormTitle();
                }
            }
        }

        private void buildFormTitle()
        {
            StringBuilder sb = new StringBuilder(Application.ProductName);
            sb.Append(" - ");
            sb.Append(fileName);
            if (modified)
                sb.Append(" *");
            this.Text = sb.ToString();
        }

        public EditorForm()
        {
            InitializeComponent();
            buildFormTitle();
            actAbout.Text = string.Format("About {0}...", Application.ProductName);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            modified = true;
        }

        private void actNew_Execute(object sender, EventArgs e)
        {
            richTextBox1.Text = string.Empty;
            fileName = string.Empty;
            modified = false;
        }

        private void actNew_BeforeExecute(object sender, CancelEventArgs e)
        {
            askSaveChanges(e);
        }

        private void askSaveChanges(CancelEventArgs e)
        {
            DialogResult toSave = (modified ?
                MessageBox.Show("Save changes?", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) :
                DialogResult.No);
            
            if (toSave == DialogResult.Cancel)
            {
                e.Cancel = true;
                return;
            }

            if (toSave == DialogResult.Yes)
            {
                actSave.DoExecute();
                e.Cancel = modified; // this checks if user cancelled save
            }
        }

        private void actSave_Update(object sender, EventArgs e)
        {
            actSave.Enabled = modified;
        }

        private void actSave_Execute(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_fileName))
                actSaveAs.DoExecute();
            else
                saveRTF(_fileName);
        }

        private void saveRTF(string fileName)
        {
            richTextBox1.SaveFile(fileName);
            this.fileName = fileName;
            modified = false;
        }

        private void actSaveAs_Execute(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = fileName;
            
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                saveRTF(saveFileDialog1.FileName);
        }

        private void actLoad_BeforeExecute(object sender, CancelEventArgs e)
        {
            askSaveChanges(e);
        }

        private void actLoad_Execute(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                loadRTF(openFileDialog1.FileName);
        }

        private void loadRTF(string fileName)
        {
            richTextBox1.LoadFile(fileName);
            this.fileName = fileName;
            modified = false;
        }

        private void actExit_Execute(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void actSelectFont_Execute(object sender, EventArgs e)
        {
            fontDialog1.Font = richTextBox1.SelectionFont;
            if (fontDialog1.ShowDialog() == DialogResult.OK)
                richTextBox1.SelectionFont = fontDialog1.Font;
        }

        private void EditorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CancelEventArgs cea = new CancelEventArgs(false);
            askSaveChanges(cea);
            e.Cancel = cea.Cancel;
        }

        private void actAbout_Execute(object sender, EventArgs e)
        {
            AboutBox f = new AboutBox();
            f.ShowDialog();
        }

    }
}