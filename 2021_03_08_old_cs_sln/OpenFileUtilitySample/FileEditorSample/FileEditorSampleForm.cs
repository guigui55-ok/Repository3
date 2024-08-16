using CommonUtility.FileUtility;
using CommonUtility.FileUtility.OpendFileUtility;
using ErrorUtility;
using FileEditorSample.FileEditorSample;
using FileEditorSample.Menu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileEditorSample
{
    public partial class FileEditorSampleForm : Form
    {
        protected ErrorManager _err;
        protected IErrorMessenger _errorMessenger;
        public readonly OpenedFile openedFile;
        public readonly OpenedFileManager openedFileManager;
        public readonly EditControl editControl;
        public readonly FileIOFunction fileIOFunction;
        protected MenuStripManager _menuStripManager;
        protected MenuStripEvent _menuStripEvent;
        

        public FileEditorSampleForm()
        {
            InitializeComponent();
            _err = new ErrorManager(1);
            _errorMessenger = new ErrorMessengerMessageBox(_err);
            openedFile = new OpenedFile(_err);
            openedFileManager = new OpenedFileManager(_err, openedFile);
            editControl = new EditControl(_err, this.richTextBox1);
            openedFile.ChangeFilePathEvent += editControl.ReadFileAfterEvent;
            fileIOFunction = new FileIOFunction(_err, this);
            _menuStripManager = new MenuStripManager(_err,menuStrip1);
            _menuStripEvent = new MenuStripEvent(_err, _menuStripManager, this, this);
            _menuStripEvent.InitializeMenuStrip();
            InitializeControls();
        }

        private void InitializeControls()
        {
            try
            {
                richTextBox1.Anchor = AnchorStyles.Top|AnchorStyles.Left|AnchorStyles.Bottom | AnchorStyles.Right;
                //richTextBox1.Dock = DockStyle.Bottom | DockStyle.Right;
                button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

                this.MinimumSize = new Size(300, 150);
            } catch (Exception ex)
            {
                _err.AddLogAlert(ex,this, "InitializeControls Failed");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fileIOFunction.OpenFile(null,EventArgs.Empty);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            fileIOFunction.CloseForm(null, EventArgs.Empty);
        }
    }
}
