using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment2
{
    
    public partial class FormEditor : Form
    {
        public FormEditor current;
        public User currentUser;
        public OpenFileDialog openFile1;
        public FormEditor()
        {
            InitializeComponent();
            current = this;
            currentUser = FormLogin.current.currentUser;
            toolStripTxtCurrentUser.Text = "Username: " + currentUser.username;
            if (currentUser.right == "View") richTextBox.ReadOnly = true; else richTextBox.ReadOnly = false;
        }

        public SaveFileDialog SaveFileDialog1;
        public class FontProperty
        {
            public Font a { get; set; }
            public FontStyle fontStyle { get; set; }
            public bool isBold { get; set; }
            public bool isItalic { get; set; }
            public bool isUnderline { get; set; }
            public float fontSize { get; set; }

            public FontFamily fontFamily { get; set; }
        }



        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveMyFile(false);
        }
        private void toolStripBtnSave_Click(object sender, EventArgs e)
        {
            saveMyFile(false);
        }

        private void toolStripBtnSaveAs_Click(object sender, EventArgs e)
        {
            saveMyFile(true);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveMyFile(true);
        }

        public void saveMyFile(bool isSaveAs) {
            SaveFileDialog1 = new SaveFileDialog();
            // Set the properties on SaveFileDialog1 so the user is 
            // prompted to create the file if it doesn't exist 
            // or overwrite the file if it does exist.
            if (this.openFile1 != null && !isSaveAs)
            {
                if (Path.GetExtension(openFile1.FileName) == ".txt") File.WriteAllText(openFile1.FileName, this.richTextBox.Text);
                else File.WriteAllText(openFile1.FileName, this.richTextBox.Rtf);
                return;
            }
            SaveFileDialog1.CreatePrompt = true;
            SaveFileDialog1.OverwritePrompt = true;

            // Set the file name to myText.txt, set the type filter
            // to text files, and set the initial directory to the 
            // MyDocuments folder.
            SaveFileDialog1.FileName = "NewDocument1";
            // DefaultExt is only used when "All files" is selected from 
            // the filter box and no extension is specified by the user.
            SaveFileDialog1.DefaultExt = "txt";
            SaveFileDialog1.Filter =
                "Text Files (*.txt)|*.txt|RTF Files (*.rtf)|*.rtf";
            SaveFileDialog1.InitialDirectory =
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Call ShowDialog and check for a return value of DialogResult.OK,
            // which indicates that the file was saved. 
            DialogResult result = SaveFileDialog1.ShowDialog();


            if (result == DialogResult.OK)
            {
                richTextBox.SaveFile(SaveFileDialog1.FileName);
            }
        }


        private void toolStripBtnOpen_Click(object sender, EventArgs e)
        {
            LoadMyFile();
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadMyFile();
        }

        public void LoadMyFile()
        {
            // Create an OpenFileDialog to request a file to open.
            openFile1 = new OpenFileDialog();

            // Initialize the OpenFileDialog to look for RTF files.
            openFile1.DefaultExt = "*.rtf";
            openFile1.Filter = "Text Files (*.txt)|*.txt|RTF Files (*.rtf)|*.rtf";

            // Determine whether the user selected a file from the OpenFileDialog.
            if (openFile1.ShowDialog() == System.Windows.Forms.DialogResult.OK &&
               openFile1.FileName.Length > 0)
            {
                // Load the contents of the file into the RichTextBox.
                if (Path.GetExtension(openFile1.FileName) == ".txt") richTextBox.LoadFile(openFile1.FileName, RichTextBoxStreamType.PlainText);
                else richTextBox.LoadFile(openFile1.FileName, RichTextBoxStreamType.RichText);
            }
        }



        private void toolStripBtnNew_Click(object sender, EventArgs e)
        {
            richTextBox.Text = "";
            openFile1 = null;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Text = "";
        }

        public void cut() {
            Clipboard.Clear();
            Clipboard.SetData(DataFormats.Rtf, richTextBox.SelectedRtf);
            richTextBox.Cut();
        }
        private void toolStripBtnCut_Click(object sender, EventArgs e)
        {
            cut();
        }
        private void cutCtrlXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cut();
        }
        //Perform Copy
        public void copy() {
            Clipboard.Clear();
            Clipboard.SetData(DataFormats.Rtf, richTextBox.SelectedRtf);
        }
        private void toolStripBtnCopy_Click(object sender, EventArgs e)
        {
            copy();
            
        }
        private void copyCtrlCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            copy();
        }

        //Perform paste
        private void paste() {
            if (Clipboard.ContainsText(TextDataFormat.Rtf))
            {
                //richTextBox.Text = Clipboard.GetText(TextDataFormat.Rtf);

                richTextBox.SelectedRtf = Clipboard.GetData(DataFormats.Rtf).ToString();
            }
        }
        private void toolStripBtnPaste_Click(object sender, EventArgs e)
        {
            paste();
        }
        private void pasteCtrlVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            paste();
        }

        private void toolStripBtnBold_Click(object sender, EventArgs e)
        {
            FontProperty lastProperty = getCurrentProperty();
            if (lastProperty.isBold == false)
            {
                toolStripBtnBold.BackColor = Color.Brown;
                richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, richTextBox.SelectionFont.Style | FontStyle.Bold);

            }

            else
            {
                toolStripBtnBold.BackColor = Color.White;
                richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, richTextBox.SelectionFont.Style & ~(FontStyle.Bold)); 
            }


        }

        private void toolStripBtnItalic_Click(object sender, EventArgs e)
        {
            FontProperty lastProperty = getCurrentProperty();
            if (lastProperty.isItalic == false)
            {
                toolStripBtnItalic.BackColor = Color.Brown;
                richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, richTextBox.SelectionFont.Style | FontStyle.Italic);
            }

            else
            {
                toolStripBtnItalic.BackColor = Color.White;
                richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, richTextBox.SelectionFont.Style & ~(FontStyle.Italic));
            }
        }

        private void toolStripBtnUnderline_Click(object sender, EventArgs e)
        {
            FontProperty lastProperty = getCurrentProperty();
            if (lastProperty.isUnderline == false)
            {
                toolStripBtnUnderline.BackColor = Color.Brown;
                richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, richTextBox.SelectionFont.Style | FontStyle.Underline);
            }

            else
            {
                toolStripBtnUnderline.BackColor = Color.White;
                richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, richTextBox.SelectionFont.Style & ~(FontStyle.Underline));
            }
        
        }
        private void toolStripComboBoxTextSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            FontProperty lastProperty = getCurrentProperty();
            richTextBox.SelectionFont = new Font(lastProperty.fontFamily, Convert.ToInt32(toolStripComboBoxTextSize.Text), richTextBox.SelectionFont.Style);
        }


        public FontProperty getCurrentProperty() {

            FontProperty item = new FontProperty();
            item.fontSize = richTextBox.SelectionFont.Size;
            item.isBold = richTextBox.SelectionFont.Bold;
            item.isItalic = richTextBox.SelectionFont.Italic;
            item.isUnderline = richTextBox.SelectionFont.Underline;
            item.fontFamily = richTextBox.SelectionFont.FontFamily;
            return item;
        }


        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Clear();
            this.Close();
            FormLogin.current.Show();
        }

        private void richTextBox_TextChanged(object sender, EventArgs e)
        {

        }
        private void richTextBox_SelectionChanged(Object sender, EventArgs e)
        {
            FontProperty lastProperty = getCurrentProperty();
            if (lastProperty.isBold) toolStripBtnBold.BackColor = Color.Brown; else toolStripBtnBold.BackColor = Color.White;
            if (lastProperty.isItalic) toolStripBtnItalic.BackColor = Color.Brown; else toolStripBtnItalic.BackColor = Color.White;
            if (lastProperty.isUnderline) toolStripBtnUnderline.BackColor = Color.Brown; else toolStripBtnUnderline.BackColor = Color.White;
        }

        private void toolStripBtnAbout_Click(object sender, EventArgs e)
        {
            FormAbout formAbout = new FormAbout();
            formAbout.ShowDialog();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAbout formAbout = new FormAbout();
            formAbout.ShowDialog();
        }
    }
}
