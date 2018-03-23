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

        private ComboBox greyLevelComboBox;
        private int greyLevel = 2;

        private ComboBox ditherMatrixSizeComboBox;
        private int ditherMatrixSize = 2;

        private TextBox colorsNumberTextbox;
        private int colorsNumber = 5;

        private Label greyLevelLabel;
        private Label ditherMatrixSizeLabel;
        private Label colorsNumberLabel;

        //private Button buttonThreshold;
        //private TrackBar thresholdTrackbar;
        //private int threshold = 100;

        private Button buttonAverageDithering;
        private Button buttonOrderedDitheing;
        private Button buttonkMeansQuantization;

        public Form1()
        {
            buttonLoad = new Button();
            buttonLoad.Text = "Load";
            buttonLoad.Left = 3;
            buttonLoad.Top = 3;
            buttonLoad.Width = 180;
            buttonLoad.Click += new EventHandler(this.LoadOnClick);

            //buttonThreshold = new Button();
            //buttonThreshold.Text = "Threshold";
            //buttonThreshold.Left = 3;
            //buttonThreshold.Top = 3 + 2 * buttonLoad.Height;
            //buttonThreshold.Width = 180;
            //buttonThreshold.Click += new EventHandler(this.ThresholdOnClick);

            buttonAverageDithering = new Button();
            buttonAverageDithering.Text = "Average Dithering";
            buttonAverageDithering.Left = 3;
            buttonAverageDithering.Top = 3 + 3 * buttonLoad.Height;
            buttonAverageDithering.Width = 180;
            buttonAverageDithering.Click += new EventHandler(this.AverageDitheringOnClick);

            buttonOrderedDitheing = new Button();
            buttonOrderedDitheing.Text = "Ordered Dithering";
            buttonOrderedDitheing.Left = 3;
            buttonOrderedDitheing.Top = 3 + 4 * buttonLoad.Height;
            buttonOrderedDitheing.Width = 180;
            buttonOrderedDitheing.Click += new EventHandler(this.OrderedDitheringOnClick);

            buttonkMeansQuantization = new Button();
            buttonkMeansQuantization.Text = "K means quantization";
            buttonkMeansQuantization.Left = 3;
            buttonkMeansQuantization.Top = 3 + 5 * buttonLoad.Height;
            buttonkMeansQuantization.Width = 180;
            buttonkMeansQuantization.Click += new EventHandler(this.kMeansQuantizationOnClick);

            //thresholdTrackbar = new TrackBar();
            //thresholdTrackbar.Left = 3 + buttonLoad.Width;
            //thresholdTrackbar.Top = 3 + 2 * buttonLoad.Height;
            //thresholdTrackbar.Height = 40;
            //thresholdTrackbar.Width = 180;
            //thresholdTrackbar.Minimum = 0;
            //thresholdTrackbar.Maximum = 255;
            //thresholdTrackbar.TickFrequency = 5;
            //thresholdTrackbar.Orientation = Orientation.Horizontal;
            //thresholdTrackbar.Scroll += new System.EventHandler(this.trackbarScroll2);

            greyLevelComboBox = new ComboBox();
            greyLevelComboBox.Text = "Grey level";
            greyLevelComboBox.Left = 3 + 2 * buttonLoad.Width;
            greyLevelComboBox.Top = 3 + 3 * buttonLoad.Height;
            greyLevelComboBox.SelectedIndexChanged += new System.EventHandler(this.greyLevelChanged);
            greyLevelComboBox.Items.AddRange(new object[] { "2", "4", "8", "16" });

            ditherMatrixSizeComboBox = new ComboBox();
            ditherMatrixSizeComboBox.Text = "Dither Matrix Size";
            ditherMatrixSizeComboBox.Left = 3 + 2 * buttonLoad.Width;
            ditherMatrixSizeComboBox.Top = 3 + 4 * buttonLoad.Height;
            ditherMatrixSizeComboBox.SelectedIndexChanged += new System.EventHandler(this.ditherMatrixSizeChanged);
            ditherMatrixSizeComboBox.Items.AddRange(new object[] { "2", "3", "4", "6" });

            colorsNumberTextbox = new TextBox();
            colorsNumberTextbox.Text = "5";
            colorsNumberTextbox.Left = 3 + 2 * buttonLoad.Width;
            colorsNumberTextbox.Top = 3 + 5 * buttonLoad.Height;
            colorsNumberTextbox.TextChanged += new System.EventHandler(this.colorsNumberChanged);

            greyLevelLabel = new Label();
            greyLevelLabel.Text = "Grey level";
            greyLevelLabel.Top = 3 + 3 * buttonLoad.Height;
            greyLevelLabel.Left = 53 + buttonLoad.Width;

            ditherMatrixSizeLabel = new Label();
            ditherMatrixSizeLabel.Text = "Dither matrix size";
            ditherMatrixSizeLabel.Top = 3 + 4 * buttonLoad.Height;
            ditherMatrixSizeLabel.Left = 53 + buttonLoad.Width;

            colorsNumberLabel = new Label();
            colorsNumberLabel.Text = "Colors number";
            colorsNumberLabel.Top = 3 + 5 * buttonLoad.Height;
            colorsNumberLabel.Left = 53 + buttonLoad.Width;

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
            this.Controls.Add(greyLevelComboBox);
            this.Controls.Add(ditherMatrixSizeComboBox);
            this.Controls.Add(colorsNumberTextbox);
            //this.Controls.Add(buttonThreshold);
            this.Controls.Add(buttonAverageDithering);
            this.Controls.Add(buttonOrderedDitheing);
            this.Controls.Add(buttonkMeansQuantization);
            this.Controls.Add(greyLevelLabel);
            this.Controls.Add(ditherMatrixSizeLabel);
            this.Controls.Add(colorsNumberLabel);

            InitializeComponent();
        }

        private void colorsNumberChanged(object sender, EventArgs e)
        {
            colorsNumber = Int32.Parse(colorsNumberTextbox.Text.ToString());
        }

        private void kMeansQuantizationOnClick(object sender, EventArgs e)
        {
            filterBitmap = kMeansQuantization(originalBitmap, colorsNumber);
            filterImage.Image = filterBitmap;
        }

        public Bitmap kMeansQuantization(Bitmap orgBitmap, int colorNumber)
        {
            Bitmap tempBitmap = (Bitmap)orgBitmap;
            Bitmap bitmap = (Bitmap)tempBitmap.Clone();

            double minDist = 442;
            int closestColor = 0;
            Color c;
            Color[] colors = getRandomColors(colorNumber);
            List<Color>[] colorGroups = putColorsInProperGroup(colors, colorNumber, bitmap);

            colors = createNewColorsGroupAndCompare(colorGroups, colorNumber, bitmap);

            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    c = bitmap.GetPixel(i, j);

                    for (int k = 0; k < colorNumber; k++)
                    {
                        double dist = getEuclideanDistance(c, colors[k]);
                        if (dist < minDist)
                        {
                            closestColor = k;
                            minDist = dist;
                        }
                    }
                    minDist = 442;

                    bitmap.SetPixel(i, j, colors[closestColor]);
                }
            }

            return (Bitmap)bitmap.Clone();
        }

        public Color[] createNewColorsGroupAndCompare(List<Color>[] oldColorGroup, int colorNumber, Bitmap bitmap)
        {
            bool theSame = false;
            Color[] colors = new Color[colorNumber];
            List<Color>[] colorGroups1 = oldColorGroup;
            List<Color>[] colorGroups2 = new List<Color>[colorNumber];

            for (int i = 0; i < colorNumber; i++)
            {
                colorGroups2[i] = new List<Color>();
            }

            while (theSame == false)
            {
                for (int k = 0; k < colorNumber; k++)
                {
                    Color c4 = getAvarageColorOfColorsGroup(colorGroups1[k]);
                    colors[k] = c4;
                }

                colorGroups2 = putColorsInProperGroup(colors, colorNumber, bitmap);

                for(int i = 0; i < colorNumber; i++)
                {
                    if(colorGroups1[i].Count != colorGroups2[i].Count)
                    {
                        break;
                    }
                    else
                    {
                        theSame = true;
                    }
                }

                colorGroups1 = colorGroups2;
            }

            return colors;
        }

        //public Bitmap kMeansQuantization(Bitmap orgBitmap, int colorNumber)
        //{
        //    Bitmap tempBitmap = (Bitmap)orgBitmap;
        //    Bitmap bitmap = (Bitmap)tempBitmap.Clone();
        //    Color c;
        //    Color[] colors = getRandomColors(colorNumber);
        //    double minDist = 442;
        //    int closestColor = 0;
        //    List<Color>[] colorGroups = new List<Color>[colorNumber];

        //    for (int i = 0; i < colorNumber; i++)
        //    {
        //        colorGroups[i] = new List<Color>();
        //    }

        //    for (int i = 0; i < bitmap.Width; i++)
        //    {
        //        for (int j = 0; j < bitmap.Height; j++)
        //        {
        //            c = bitmap.GetPixel(i, j);

        //            for (int k = 0; k < colorNumber; k++)
        //            {
        //                double dist = getEuclideanDistance(c, colors[k]);
        //                if (dist < minDist)
        //                {
        //                    closestColor = k;
        //                    minDist = dist;
        //                }
        //            }
        //            minDist = 442;
        //            colorGroups[closestColor].Add(c);
        //        }
        //    }

        //    for (int k = 0; k < colorNumber; k++)
        //    {
        //        Color c4 = getAvarageColorOfColorsGroup(colorGroups[k]);
        //    }

        //    return (Bitmap)bitmap.Clone();
        //}

        public List<Color>[] putColorsInProperGroup(Color[] colors, int colorNumber, Bitmap bitmap)
        {
            double minDist = 442;
            int closestColor = 0;
            Color c;
            List<Color>[] colorGroups = new List<Color>[colorNumber];

            for (int i = 0; i < colorNumber; i++)
            {
                colorGroups[i] = new List<Color>();
            }

            for (int i = 0; i < colorNumber; i++)
            {
                colorGroups[i] = new List<Color>();
            }

            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    c = bitmap.GetPixel(i, j);

                    for (int k = 0; k < colorNumber; k++)
                    {
                        double dist = getEuclideanDistance(c, colors[k]);
                        if (dist < minDist)
                        {
                            closestColor = k;
                            minDist = dist;
                        }
                    }
                    minDist = 442;
                    colorGroups[closestColor].Add(c);
                }
            }
            return colorGroups;
        }

        public Color[] getRandomColors(int n)
        {
            Color[] colors = new Color[n];
            Random rn = new Random();

            for (int i = 0; i < n; i++)
            {
                int r = rn.Next(0, 255);
                int g = rn.Next(0, 255);
                int b = rn.Next(0, 255);
                colors[i] = Color.FromArgb(r, g, b);
            }

            return colors;
        }

        public double getEuclideanDistance(Color c1, Color c2)
        {
            double dist = 0;

            dist = Math.Sqrt(Math.Pow(c1.R - c2.R, 2) + Math.Pow(c1.G - c2.G, 2) + Math.Pow(c1.B - c2.B, 2));

            return dist;
        }

        public Color getAvarageColorOfColorsGroup(List<Color> colors)
        {
            Color average;
            int totalR = 0, totalG = 0, totalB = 0;

            for (int i = 0; i < colors.Count; i ++)
            {
                Color c = colors.ElementAt(i);
                totalR += c.R;
                totalG += c.G;
                totalB += c.B;
            }

            totalR = (int)totalR / colors.Count;
            totalG = (int)totalG / colors.Count;
            totalB = (int)totalB / colors.Count;

            average = Color.FromArgb(totalR, totalG, totalB);

            return average;
        }

        private void OrderedDitheringOnClick(object sender, EventArgs e)
        {
            filterBitmap = orderedDithering(originalBitmap, greyLevel, ditherMatrixSize);
            filterImage.Image = filterBitmap;
        }

        public Bitmap orderedDithering(Bitmap orgBitmap, int level, int size)
        {
            Bitmap tempBitmap = (Bitmap)orgBitmap;
            Bitmap bitmap = (Bitmap)tempBitmap.Clone();
            Color c;
            Color[] levels = generateGreyLevels(level);
            //Color[] levels = new Color[] { Color.FromArgb(0, 0, 0), Color.FromArgb(255, 255, 255) };
            double[,] ditherMatrix = generateDitherMatrix(size);
            
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    c = bitmap.GetPixel(i, j);
                    double intensity = (double)(0.299 * c.R + 0.587 * c.G + 0.114 * c.B)/255;
                    int col = (int)((level - 1) * intensity);
                    double re = (level - 1) * intensity - col;
                    if (re >= ditherMatrix[i % size, j % size])
                    {
                        ++col;
                    }
                    bitmap.SetPixel(i, j, levels[col]);
                }
            }
            

            return (Bitmap)bitmap.Clone();
        }

        public Color[] generateGreyLevels(int n)
        {
            Color[] levels = new Color[n];
            Color c;
            double level = (double) 1 / (n-1);

            c = Color.FromArgb(0, 0, 0);
            levels[0] = c;

            for (int i = 1; i < n - 1; i++)
            {
                int d = (int)(i * level * 255);
                c = Color.FromArgb(d,d,d);
                levels[i] = c;
            }

            c = Color.FromArgb(255, 255, 255);
            levels[n - 1] = c;

            return levels;
        }

        private void ditherMatrixSizeChanged(object sender, EventArgs e)
        {
            int index = ditherMatrixSizeComboBox.SelectedIndex;
            if(index == 0)
            {
                ditherMatrixSize = 2;
            }
            else if(index == 1)
            {
                ditherMatrixSize = 3;
            }
            else if(index == 2)
            {
                ditherMatrixSize = 4;
            }
            else if(index == 3)
            {
                ditherMatrixSize = 6;
            }
        }

        public double[,] generateDitherMatrix(int size)
        {
            double n = (double) 1 / (size * size + 1);
            double[,] m2 = new double[2, 2] { { (double)(1 * n), (double)(3 * n) }, { (double)(4 * n), (double)(2 * n) } };
            double[,] m3 = new double[3, 3] { { (double)(3 * n),  (double)(7 * n), (double)(4 * n) }, { (double) (6 * n),  (double)(1 * n), (double) (9 * n) }, { (double)( 2 * n), (double) (8 * n),  (double)(5 * n) } };
            double[,] matrix = new double[size, size];

            if(size == 2)
            {
                return m2;
            }
            else if(size == 3)
            {
                return m3;
            }
            else if (size == 4)
            {
                matrix = bayerMatrix(m2, 2);
            }
            else if(size == 6)
            {
                matrix = bayerMatrix(m3, 3);
            }

            return matrix;
        }

        public double[,] bayerMatrix(double[,] m, int size)
        {
            double[,] matrix = new double[2*size, 2*size];
            double n = (double) 1 / (size * size + 1);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = (double)((4 * m[i, j] + 1) * n);
                    matrix[i + size, j] = (double)((4 * m[i, j] + 4) * n);
                    matrix[i, j + size] = (double)((4 * m[i, j] + 3) * n);
                    matrix[i + size, j + size] = (double)((4 * m[i, j] + 2) * n);
                }              
            }

            return matrix;
        }

        private void greyLevelChanged(object sender, EventArgs e)
        {
            double gl = Math.Pow(2,(greyLevelComboBox.SelectedIndex + 1));
            greyLevel = (int)(gl);
        }

        private void AverageDitheringOnClick(object sender, EventArgs e)
        {
            filterBitmap = averageDithering(originalBitmap, greyLevel);
            filterImage.Image = filterBitmap;
            originalBitmap = SetGrayscale(originalBitmap);
            originalImage.Image = originalBitmap;
        }

        //private void ThresholdOnClick(object sender, EventArgs e)
        //{
        //    filterBitmap = SetThreshold(originalBitmap, greyLevel);
        //    filterImage.Image = filterBitmap;
        //    originalBitmap = SetGrayscale(originalBitmap);
        //    originalImage.Image = originalBitmap;
        //}

        //public Bitmap SetThreshold(Bitmap orgBitmap, int threshold)
        //{
        //    Bitmap tempBitmap = (Bitmap)orgBitmap;
        //    Bitmap bitmap = (Bitmap)tempBitmap.Clone();
        //    Color c;
        //    byte gray;

        //    for (int i = 0; i < bitmap.Width; i++)
        //    {
        //        for (int j = 0; j < bitmap.Height; j++)
        //        {
        //            c = bitmap.GetPixel(i, j);
        //            gray = (byte)(0.299 * c.R + 0.587 * c.G + 0.114 * c.B);

        //            if (gray >= threshold)
        //            {
        //                bitmap.SetPixel(i, j, Color.FromArgb(255, 255, 255));
        //            }
        //            else
        //            {
        //                bitmap.SetPixel(i, j, Color.FromArgb(0, 0, 0));
        //            }
        //        }
        //    }

        //    return (Bitmap)bitmap.Clone();
        //}

        public Bitmap averageDithering(Bitmap orgBitmap, int level)
        {
            Bitmap tempBitmap = (Bitmap)orgBitmap;
            Bitmap bitmap = (Bitmap)tempBitmap.Clone();

            int average = averageIntensity(orgBitmap);
            byte gray;
            Color c;

            if (level == 2)
            {
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
            else
            {
                int[] levels = new int[level + 1];
                levels[0] = 0;
                for (int i = 1; i < level; i++)
                {
                    levels[i] = (average * i) / (level / 2);
                }
                levels[level] = 255;
                for (int i = 0; i < bitmap.Width; i++)
                {
                    for (int j = 0; j < bitmap.Height; j++)
                    {
                        c = bitmap.GetPixel(i, j);
                        gray = (byte)(0.299 * c.R + 0.587 * c.G + 0.114 * c.B);

                        for (int k = 0; k < level; k++)
                        {
                            if(gray >= levels[k] && gray <= levels[k+1])
                            {
                                bitmap.SetPixel(i, j, Color.FromArgb(levels[k + 1], levels[k + 1], levels[k + 1]));
                            }
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

        //private void trackbarScroll2(object sender, EventArgs e)
        //{
        //    threshold = Int32.Parse(thresholdTrackbar.Value.ToString());
        //}

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

