using System.Windows.Forms;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection.Emit;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdvancedCompressingMethods
{
    public partial class Form1 : Form
    {
        //FileController fileController = new FileController("input.mp4", "output.mp4");
        Wavelet wavelet = new Wavelet();
        private Bitmap loadedImage;
        private Bitmap loadedImageCopy;
        int imageWidth;
        int imageHeight;
        int currentWidth;
        int currentHeight;
        int scale = 1;
        int offset = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
        private void UpdateImageDimensions()
        {
            this.currentWidth = this.currentWidth / 2;
            this.currentHeight = this.currentHeight / 2;
        }


        private int NormalizeIndex(int n, int maxValue)
        {
            if (n < 0)
            {
                return Math.Abs(n);
            }
            else if (n > maxValue)
            {
                return n - (maxValue - n) * (-1);
            }
            return n;
        }

        private int NormalizePixelValue(double n)
        {
            int value = (int)Math.Round(n);

            if (value < 0)
            {
                return 0;
            }

            if (value > 255)
            {
                return 255;
            }

            return value;
        }

        private void ScaleImage()
        {
            labelMinError.Text = "h|w: " + currentHeight + " x " + currentWidth;

            for (int i = 0; i < imageHeight; i++)
            {
                for (int j = 0; j < imageWidth; j++)
                {
                    if (i > currentHeight || j > currentWidth)
                    {
                        Color pixel = loadedImageCopy.GetPixel(i, j);

                        loadedImageCopy.SetPixel(i, j, Color.FromArgb(
                                NormalizePixelValue(pixel.R * this.scale + this.offset),
                                NormalizePixelValue(pixel.G * this.scale + this.offset),
                                NormalizePixelValue(pixel.B * this.scale + this.offset)
                            ));
                    }
                }
            }

            loadedImageCopyPictureBox.Image = loadedImageCopy;
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.bmp; *.png",
                Title = "Select an Image"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    loadedImage = new Bitmap(openFileDialog.FileName);
                    loadedImageCopy = loadedImage;

                    loadedImagePictureBox.Image = loadedImage;
                    loadedImagePictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

                    loadedImageCopyPictureBox.Image = loadedImageCopy;
                    loadedImageCopyPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

                    this.imageWidth = loadedImage.Width;
                    this.imageHeight = loadedImage.Height;

                    this.currentWidth = loadedImage.Width;
                    this.currentHeight = loadedImage.Height;

                    this.textBoxScale.Text = scale.ToString();
                    this.textBoxOffset.Text = offset.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading image: " + ex.Message);
                }
            }
        }

        private void AnalisysHorizontal(int level)
        {
            double[] high = new double[currentHeight];
            double[] low = new double[currentHeight];

            for (int i = 0; i < currentHeight; i++)
            {
                for (int j = 0; j < currentWidth; j++)
                {
                    low[j] =
                    loadedImage.GetPixel(NormalizeIndex(j - 4, currentWidth - 1), i).R * this.wavelet.analysisLow[0] +
                    loadedImage.GetPixel(NormalizeIndex(j - 3, currentWidth - 1), i).R * this.wavelet.analysisLow[1] +
                    loadedImage.GetPixel(NormalizeIndex(j - 2, currentWidth - 1), i).R * this.wavelet.analysisLow[2] +
                    loadedImage.GetPixel(NormalizeIndex(j - 1, currentWidth - 1), i).R * this.wavelet.analysisLow[3] +
                    loadedImage.GetPixel(NormalizeIndex(j, currentWidth - 1), i).R * this.wavelet.analysisLow[4] +
                    loadedImage.GetPixel(NormalizeIndex(j + 1, currentWidth - 1), i).R * this.wavelet.analysisLow[5] +
                    loadedImage.GetPixel(NormalizeIndex(j + 2, currentWidth - 1), i).R * this.wavelet.analysisLow[6] +
                    loadedImage.GetPixel(NormalizeIndex(j + 3, currentWidth - 1), i).R * this.wavelet.analysisLow[7] +
                    loadedImage.GetPixel(NormalizeIndex(j + 4, currentWidth - 1), i).R * this.wavelet.analysisLow[8];

                    high[j] =
                    loadedImage.GetPixel(NormalizeIndex(j - 4, currentWidth - 1), i).R * this.wavelet.analysisHigh[0] +
                    loadedImage.GetPixel(NormalizeIndex(j - 3, currentWidth - 1), i).R * this.wavelet.analysisHigh[1] +
                    loadedImage.GetPixel(NormalizeIndex(j - 2, currentWidth - 1), i).R * this.wavelet.analysisHigh[2] +
                    loadedImage.GetPixel(NormalizeIndex(j - 1, currentWidth - 1), i).R * this.wavelet.analysisHigh[3] +
                    loadedImage.GetPixel(NormalizeIndex(j, currentWidth - 1), i).R * this.wavelet.analysisHigh[4] +
                    loadedImage.GetPixel(NormalizeIndex(j + 1, currentWidth - 1), i).R * this.wavelet.analysisHigh[5] +
                    loadedImage.GetPixel(NormalizeIndex(j + 2, currentWidth - 1), i).R * this.wavelet.analysisHigh[6] +
                    loadedImage.GetPixel(NormalizeIndex(j + 3, currentWidth - 1), i).R * this.wavelet.analysisHigh[7] +
                    loadedImage.GetPixel(NormalizeIndex(j + 4, currentWidth - 1), i).R * this.wavelet.analysisHigh[8];
                }

                int mergedVectorIndex = 0;
                int[] mergedVector = new int[currentHeight];

                for (int x = 0; x < currentHeight; x += 2)
                {
                    mergedVector[mergedVectorIndex] = NormalizePixelValue(low[x]);
                    mergedVectorIndex++;
                }

                for (int x = 1; x < currentHeight; x += 2)
                {
                    mergedVector[mergedVectorIndex] = NormalizePixelValue(high[x]);
                    mergedVectorIndex++;
                }

                for (int x = 0; x < currentWidth; x++)
                {
                    loadedImageCopy.SetPixel(x, i, Color.FromArgb(mergedVector[x], mergedVector[x], mergedVector[x]));
                }

                loadedImageCopyPictureBox.Image = loadedImageCopy;
            }
            loadedImageCopyPictureBox.Image = loadedImageCopy;

            //UpdateImageDimensions();
        }

        private void AnalisysVertical(int level)
        {
            double[] high = new double[currentHeight];
            double[] low = new double[currentHeight];

            for (int i = 0; i < currentHeight; i++)
            {
                for (int j = 0; j < currentWidth; j++)
                {
                    low[j] =
                    loadedImage.GetPixel(i, NormalizeIndex(j - 4, currentWidth - 1)).R * this.wavelet.analysisLow[0] +
                    loadedImage.GetPixel(i, NormalizeIndex(j - 3, currentWidth - 1)).R * this.wavelet.analysisLow[1] +
                    loadedImage.GetPixel(i, NormalizeIndex(j - 2, currentWidth - 1)).R * this.wavelet.analysisLow[2] +
                    loadedImage.GetPixel(i, NormalizeIndex(j - 1, currentWidth - 1)).R * this.wavelet.analysisLow[3] +
                    loadedImage.GetPixel(i, NormalizeIndex(j, currentWidth - 1)).R * this.wavelet.analysisLow[4] +
                    loadedImage.GetPixel(i, NormalizeIndex(j + 1, currentWidth - 1)).R * this.wavelet.analysisLow[5] +
                    loadedImage.GetPixel(i, NormalizeIndex(j + 2, currentWidth - 1)).R * this.wavelet.analysisLow[6] +
                    loadedImage.GetPixel(i, NormalizeIndex(j + 3, currentWidth - 1)).R * this.wavelet.analysisLow[7] +
                    loadedImage.GetPixel(i, NormalizeIndex(j + 4, currentWidth - 1)).R * this.wavelet.analysisLow[8];

                    high[j] =
                    loadedImage.GetPixel(i, NormalizeIndex(j - 4, currentWidth - 1)).R * this.wavelet.analysisHigh[0] +
                    loadedImage.GetPixel(i, NormalizeIndex(j - 3, currentWidth - 1)).R * this.wavelet.analysisHigh[1] +
                    loadedImage.GetPixel(i, NormalizeIndex(j - 2, currentWidth - 1)).R * this.wavelet.analysisHigh[2] +
                    loadedImage.GetPixel(i, NormalizeIndex(j - 1, currentWidth - 1)).R * this.wavelet.analysisHigh[3] +
                    loadedImage.GetPixel(i, NormalizeIndex(j, currentWidth - 1)).R * this.wavelet.analysisHigh[4] +
                    loadedImage.GetPixel(i, NormalizeIndex(j + 1, currentWidth - 1)).R * this.wavelet.analysisHigh[5] +
                    loadedImage.GetPixel(i, NormalizeIndex(j + 2, currentWidth - 1)).R * this.wavelet.analysisHigh[6] +
                    loadedImage.GetPixel(i, NormalizeIndex(j + 3, currentWidth - 1)).R * this.wavelet.analysisHigh[7] +
                    loadedImage.GetPixel(i, NormalizeIndex(j + 4, currentWidth - 1)).R * this.wavelet.analysisHigh[8];
                }

                int mergedVectorIndex = 0;
                int[] mergedVector = new int[currentHeight];

                for (int x = 0; x < currentHeight; x += 2)
                {
                    mergedVector[mergedVectorIndex] = NormalizePixelValue(low[x]);
                    mergedVectorIndex++;
                }

                for (int x = 1; x < currentHeight; x += 2)
                {
                    mergedVector[mergedVectorIndex] = NormalizePixelValue(high[x]);
                    mergedVectorIndex++;
                }

                for (int x = 0; x < currentWidth; x++)
                {
                    loadedImageCopy.SetPixel(i, x, Color.FromArgb(mergedVector[x], mergedVector[x], mergedVector[x]));
                }

                loadedImageCopyPictureBox.Image = loadedImageCopy;
            }
            loadedImageCopyPictureBox.Image = loadedImageCopy;

            UpdateImageDimensions();
        }

        private void AnH1_Click(object sender, EventArgs e)
        {
            AnalisysHorizontal(1);
        }

        private void AnV1_Click(object sender, EventArgs e)
        {
            AnalisysVertical(1);
        }

        private void AnH2_Click(object sender, EventArgs e)
        {
            AnalisysHorizontal(2);
        }

        private void AnV2_Click(object sender, EventArgs e)
        {
            AnalisysVertical(2);
        }

        private void MinMaxError_Click(object sender, EventArgs e)
        {
            int error;
            int minError = 400;
            int maxError = -1;

            for (int i = 0; i < currentHeight; i++)
            {
                for (int j = 0; j < currentWidth; j++)
                {
                    error = (loadedImage.GetPixel(i, j).R + loadedImage.GetPixel(i, j).G + loadedImage.GetPixel(i, j).B) / 3 -
                        (loadedImageCopy.GetPixel(i, j).R + loadedImageCopy.GetPixel(i, j).G + loadedImageCopy.GetPixel(i, j).B) / 3;

                    if (error > maxError)
                    {
                        maxError = error;
                    }
                    if (error < minError)
                    {
                        minError = error;
                    }
                }
            }
            labelMinError.Text = "Min: " + minError;
            labelMaxError.Text = "Max: " + maxError;
        }

        private void RefreshScale_Click(object sender, EventArgs e)
        {
            int number;
            int.TryParse(textBoxScale.Text, out number);
            this.scale = number;

            int.TryParse(textBoxOffset.Text, out number);
            this.offset = number;

            ScaleImage();
        }

        ~Form1()
        {
            //fileController.close();
        }
    }
}
