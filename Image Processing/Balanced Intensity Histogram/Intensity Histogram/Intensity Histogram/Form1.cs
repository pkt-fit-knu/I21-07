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

                            pictureBox1.Image = image.Resize(pictureBox1.Width, pictureBox1.Height);

                            Histogram histogram1 = new Histogram();

                            histogram1.SetIntensities(image);

                            histogram1.DrawHistogram(chart1);

                            chart1.Visible = true;

                            this.Controls.Add(chart1);

                            Histogram histogram2 = new Histogram();

                            image = histogram1.BalanceImage(image);

                            pictureBox2.Image = image.Resize(pictureBox2.Width, pictureBox2.Height);

                            histogram2.SetIntensities(image);

                            histogram2.DrawHistogram(chart2);

                            chart2.Visible = true;

                            this.Controls.Add(chart2);
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
