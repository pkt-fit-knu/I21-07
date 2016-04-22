using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LaplacianSharper
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, EventArgs e)
        {
            Stream stream = null;

            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((stream = openFileDialog.OpenFile()) != null)
                    {
                        using (stream)
                        {
                            string pathName = openFileDialog.FileName;

                            Bitmap image = new Bitmap(pathName);

                            pictureBox1.Image = image.Resize(pictureBox1.Width, pictureBox1.Height);

                            Mask mask = new Mask(new int[,] { { 1, 1, 1 }, { 1, -8, 1 }, { 1, 1, 1 } });

                            image = LaplacianSharper.SharpImage(image, mask);

                            pictureBox2.Image = image.Resize(pictureBox2.Width, pictureBox2.Height);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }
    }
}
