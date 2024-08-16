using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ErrorLog;

namespace ImageViewer2
{
    public partial class TestViewImageForm : Form
    {
        public TestViewImageForm()
        {
            InitializeComponent();
            this.Size = new Size(480,720);
            this.Location = new Point(150,150);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GlobalErrloLog.ErrorLog = new ErrorLog.ErrorLog();
            //TestPictureBox_Now();
            TestPictureBoxImage();
        }

        private void TestPictureBoxImage()
        {
            TestPictureBoxImage test2 = new TestPictureBoxImage();
            test2.testImageView(this,this.pictureBox1);
        }
        private void TestPictureBox_Now()
        {
            ErrorLog.IErrorLog _errorLog = new ErrorLog.ErrorLog();

            TestPictureBox testPictureBox = new TestPictureBox();
            testPictureBox.setErrorLog(_errorLog);
            // 表示するだけ　210204OK
            //testPictureBox.test_paint(this, this.pictureBox1);
            // 表示して、左上、親コントロールに合わせる　210204OK
            testPictureBox.test_paint_fit(this,this.pictureBox1);
        }
    }
}
