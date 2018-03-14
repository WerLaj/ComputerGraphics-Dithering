using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ComputerGraphicsDithering
{
    public partial class Form1 : Form
    {
        private Button buttonLoad;
        private PictureBox originalImage;
        private Bitmap originalBitmap;
        private PictureBox filterImage;
        private Bitmap filterBitmap;

        private Button buttonThreshold;
        private TrackBar thresholdTrackbar;
        private int threshold = 100;

        private Button buttonAverageDithering;

        public Form1()
        {
            buttonLoad = new Button();
            buttonLoad.Text = "Load";
            buttonLoad.Left = 3;
            buttonLoad.Top = 3;
            buttonLoad.Width = 150;
            buttonLoad.Click += new EventHandler(this.LoadOnClick);

            buttonThreshold = new Button();
            buttonThreshold.Text = "Threshold";
            buttonThreshold.Left = 3;
            buttonThreshold.Top = 3 + 2 * buttonLoad.Height;
            buttonThreshold.Width = 180;
            buttonThreshold.Click += new EventHandler(this.ThresholdOnClick);

            buttonAverageDithering = new Button();
            buttonAverageDithering.Text = "Average Dithering";
            buttonAverageDithering.Left = 3;
            buttonAverageDithering.Top = 3 + 3 * buttonLoad.Height;
            buttonAverageDithering.Width = 180;
            buttonAverageDithering.Click += new EventHandler(this.AverageDitheringOnClick);

            thresholdTrackbar = new TrackBar();
            thresholdTrackbar.Left = 3 + buttonLoad.Width;
            thresholdTrackbar.Top = 3 + 2 * buttonLoad.Height;
            thresholdTrackbar.Height = 40;
            thresholdTrackbar.Width = 180;
            thresholdTrackbar.Minimum = 0;
            thresholdTrackbar.Maximum = 255;
            thresholdTrackbar.TickFrequency = 5;
            thresholdTrackbar.Orientation = Orientation.Horizontal;
            thresholdTrackbar.Scroll += new System.EventHandler(this.trackbarScroll2);

            originalImage = new PictureBox();
            originalImage.BorderStyle = BorderStyle.Fixed3D;
            originalImage.Width = this.Width * 2;
            originalImage.Height = this.Height * 2;
            originalImage.Left = 3;
            originalImage.Top = 10 * buttonLoad.Height;
            originalImage.SizeMode = PictureBoxSizeMode.StretchImage;

            filterImage = new PictureBox();
            filterImage.BorderStyle = BorderStyle.Fixed3D;
            filterImage.Width = this.Width * 2;
            filterImage.Height = this.Height * 2;
            filterImage.Left = 3 + originalImage.Width;
            filterImage.Top = 10 * buttonLoad.Height;
            filterImage.SizeMode = PictureBoxSizeMode.StretchImage;

            this.Controls.Add(buttonLoad);
            this.Controls.Add(originalImage);
            this.Controls.Add(filterImage);

            this.Controls.Add(thresholdTrackbar);
            this.Controls.Add(buttonThreshold);
            this.Controls.Add(buttonAverageDithering);

            InitializeComponent();
        }

        private void AverageDitheringOnClick(object sender, EventArgs e)
        {
            filterBitmap = averageDithering(originalBitmap, 2);
            filterImage.Image = filterBitmap;
            originalBitmap = SetGrayscale(originalBitmap);
            originalImage.Image = originalBitmap;
        }

        private void ThresholdOnClick(object sender, EventArgs e)
        {
            filterBitmap = SetThreshold(originalBitmap, threshold);
            filterImage.Image = filterBitmap;
            originalBitmap = SetGrayscale(originalBitmap);
            originalImage.Image = originalBitmap;
        }

        public Bitmap SetThreshold(Bitmap orgBitmap, int threshold)
        {
            Bitmap tempBitmap = (Bitmap)orgBitmap;
            Bitmap bitmap = (Bitmap)tempBitmap.Clone();
            Color c;
            byte gray;

            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    c = bitmap.GetPixel(i, j);
                    gray = (byte)(0.299 * c.R + 0.587 * c.G + 0.114 * c.B);

                    if (gray >= threshold)
                    {
                        bitmap.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                    }
                    else
                    {
                        bitmap.SetPixel(i, j, Color.FromArgb(0, 0, 0));
                    }
                }
            }

            return (Bitmap)bitmap.Clone();
        }

        public Bitmap averageDithering(Bitmap orgBitmap, int level)
        {
            Bitmap tempBitmap = (Bitmap)orgBitmap;
            Bitmap bitmap = (Bitmap)tempBitmap.Clone();

            if (level == 2)
            {
                int average = averageIntensity(orgBitmap);
                byte gray;
                Color c;
                for (int i = 0; i < bitmap.Width; i++)
                {
                    for (int j = 0; j < bitmap.Height; j++)
                    {
                        c = bitmap.GetPixel(i, j);
                        gray = (byte)(0.299 * c.R + 0.587 * c.G + 0.114 * c.B);

                        if (gray >= average)
                        {
                            bitmap.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                        }
                        else
                        {
                            bitmap.SetPixel(i, j, Color.FromArgb(0, 0, 0));
                        }
                    }
                }
            }

            return (Bitmap)bitmap.Clone();
        }

        public int averageIntensity(Bitmap orgBitmap)
        {
            int intensity = 0;
            int totalR = 0, totalG = 0, totalB = 0;
            Bitmap temp = (Bitmap)orgBitmap.Clone();
            Color c;

            for (int i = 0; i < temp.Width; i++)
            {
                for (int j = 0; j < temp.Height; j++)
                {
                    c = temp.GetPixel(i, j);
                    totalR += c.R;
                    totalG += c.G;
                    totalB += c.B;
                }
            }

            totalR = totalR / (temp.Width * temp.Height);
            totalG = totalG / (temp.Width * temp.Height);
            totalB = totalB / (temp.Width * temp.Height);

            intensity = (totalR + totalG + totalB) / 3;

            return intensity;
        }

        private void trackbarScroll2(object sender, EventArgs e)
        {
            threshold = Int32.Parse(thresholdTrackbar.Value.ToString());
        }

        protected void LoadOnClick(object sender, EventArgs e)
        {
            OpenFileDialog imageDialog = new OpenFileDialog();

            imageDialog.Title = "Original Image";
            imageDialog.Filter = "jpg files (*.jpg)|*.jpg|All files (*.*)|*.*";

            if (imageDialog.ShowDialog() == DialogResult.OK)
            {
                originalBitmap = new Bitmap(imageDialog.OpenFile());
                originalImage.Image = originalBitmap;
            }

            imageDialog.Dispose();
        }

        public Bitmap SetGrayscale(Bitmap orgBitmap)
        {
            Bitmap tempBitmap = (Bitmap)orgBitmap;
            Bitmap bitmap = (Bitmap)tempBitmap.Clone();
            Color c;
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    c = bitmap.GetPixel(i, j);
                    byte gray = (byte)(.299 * c.R + .587 * c.G + .114 * c.B);

                    bitmap.SetPixel(i, j, Color.FromArgb(gray, gray, gray));
                }
            }
            return (Bitmap)bitmap.Clone();
        }
    }
}

