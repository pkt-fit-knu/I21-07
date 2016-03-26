using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ImageToHistogram
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

                            pictureBox.Image = image.Resize(pictureBox.Width, pictureBox.Height);

                            Histogram histogram = new Histogram();

                            histogram.SetIntensities(image);

                            histogram.DrawHistogram(chart);

                            chart.Visible = true;

                            this.Controls.Add(chart);
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
