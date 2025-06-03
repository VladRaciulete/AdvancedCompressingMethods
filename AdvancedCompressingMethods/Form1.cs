using System.Windows.Forms;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection.Emit;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdvancedCompressingMethods
{
    public partial class Form1 : Form
    {
        //System.Diagnostics.Debug.WriteLine(4);
        FileController fileController = new FileController("waveletInput.wvt", "waveletOutput.wvt");
        Wavelet wavelet = new Wavelet();
        private Bitmap loadedImage;
        private Bitmap loadedImageCopy;
        int imageWidth;
        int imageHeight;
        int currentWidth;
        int currentHeight;
        int scale = 1;
        int offset = 0;
        double[,] imageMatrix;

        public Form1()
        {
            InitializeComponent();

            loadedImagePictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            loadedImageCopyPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void UpdateImageDimensions(bool reversed = false)
        {
            if (reversed)
            {
                this.currentWidth = this.currentWidth * 2;
                this.currentHeight = this.currentHeight * 2;
                ScaleImage();
                UpdateWidthHeightLabels();
                return;
            }
            this.currentWidth = this.currentWidth / 2;
            this.currentHeight = this.currentHeight / 2;
            ScaleImage();
            UpdateWidthHeightLabels();
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
            int number;
            int.TryParse(textBoxScale.Text, out number);
            this.scale = number;

            int.TryParse(textBoxOffset.Text, out number);
            this.offset = number;

            labelMinError.Text = "h|w: " + currentHeight + " x " + currentWidth;

            for (int i = 0; i < imageHeight; i++)
            {
                for (int j = 0; j < imageWidth; j++)
                {
                    if (i >= currentHeight || j >= currentWidth)
                    {
                        int value = NormalizePixelValue(this.imageMatrix[i, j]);
                        value = NormalizePixelValue(value * this.scale + this.offset);

                        loadedImageCopy.SetPixel(j, i, Color.FromArgb(value, value, value));
                    }
                }
            }

            loadedImageCopyPictureBox.Image = loadedImageCopy;
        }

        private void UpdateWidthHeightLabels() {
            labelWidth.Text = "w: " + this.currentWidth;
            labelHeight.Text = "h: " + this.currentHeight;
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
                    loadedImageCopyPictureBox.Image = loadedImageCopy;

                    this.imageWidth = loadedImage.Width;
                    this.imageHeight = loadedImage.Height;

                    this.currentWidth = loadedImage.Width;
                    this.currentHeight = loadedImage.Height;

                    UpdateWidthHeightLabels();

                    this.textBoxScale.Text = scale.ToString();
                    this.textBoxOffset.Text = offset.ToString();

                    this.imageMatrix = new double[this.imageHeight, this.imageWidth];

                    for (int i = 0; i < currentHeight; i++)
                    {
                        for (int j = 0; j < currentWidth; j++)
                        {
                            Color pixelColor = loadedImage.GetPixel(j, i);

                            this.imageMatrix[i, j] = NormalizePixelValue((pixelColor.R + pixelColor.G + pixelColor.B) / 3);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading image: " + ex.Message);
                }
            }
        }

        private void SaveFileButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.DefaultExt = "wvt";

            saveFileDialog.Filter = "Wavelet Files (*.wvt)|*.wvt";

            DialogResult result = saveFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                fileController.OpenOutputFileStream(filePath);

                //System.IO.File.WriteAllText(filePath, "This is some text that will be saved in the file.");

                //MessageBox.Show(filePath);
            }

            for (int i = 0; i < imageHeight; i++)
            {
                for (int j = 0; j < imageWidth; j++)
                {
                    fileController.WriteDouble(this.imageMatrix[i, j]);
                }
            }

            MessageBox.Show("File saved!");
            fileController.closeWriter();
        }

        private void LoadFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Wavelet Files|*.wvt",
                Title = "Select a wavelet file"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    fileController.OpenInputFileStream(openFileDialog.FileName);

                    this.imageMatrix = new double[512, 512];
                    loadedImageCopy = new Bitmap(512, 512);

                    for (int i = 0; i < 512; i++)
                    {
                        for (int j = 0; j < 512; j++)
                        {
                            this.imageMatrix[i, j] = fileController.ReadDouble();
                        }
                    }

                    fileController.closeReader();

                    for (int i = 0; i < 512; i++)
                    {
                        for (int j = 0; j < 512; j++)
                        {
                            int value = NormalizePixelValue(this.imageMatrix[i, j]);
                            loadedImageCopy.SetPixel(j, i, Color.FromArgb(value, value, value));
                        }
                    }

                    loadedImageCopyPictureBox.Image = loadedImageCopy;

                    this.imageWidth = loadedImageCopy.Width;
                    this.imageHeight = loadedImageCopy.Height;
                    this.currentWidth = 32;
                    this.currentHeight = 32;

                    MessageBox.Show("File loaded!");

                    //labelMinError.Text = "h|w: " + imageHeight + " x " + imageWidth + " |||| " + currentHeight + " x " + currentWidth;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading image: " + ex.Message);
                }
            }
        }

        private void AnalisysHorizontal(int level)
        {
            double[] high = new double[currentWidth];
            double[] low = new double[currentWidth];

            for (int i = 0; i < currentHeight; i++)
            {
                for (int j = 0; j < currentWidth; j++)
                {
                    low[j] =
                    this.imageMatrix[i, NormalizeIndex(j - 4, currentWidth - 1)] * this.wavelet.analysisLow[0] +
                    this.imageMatrix[i, NormalizeIndex(j - 3, currentWidth - 1)] * this.wavelet.analysisLow[1] +
                    this.imageMatrix[i, NormalizeIndex(j - 2, currentWidth - 1)] * this.wavelet.analysisLow[2] +
                    this.imageMatrix[i, NormalizeIndex(j - 1, currentWidth - 1)] * this.wavelet.analysisLow[3] +
                    this.imageMatrix[i, NormalizeIndex(j, currentWidth - 1)] * this.wavelet.analysisLow[4] +
                    this.imageMatrix[i, NormalizeIndex(j + 1, currentWidth - 1)] * this.wavelet.analysisLow[5] +
                    this.imageMatrix[i, NormalizeIndex(j + 2, currentWidth - 1)] * this.wavelet.analysisLow[6] +
                    this.imageMatrix[i, NormalizeIndex(j + 3, currentWidth - 1)] * this.wavelet.analysisLow[7] +
                    this.imageMatrix[i, NormalizeIndex(j + 4, currentWidth - 1)] * this.wavelet.analysisLow[8];


                    high[j] =
                    this.imageMatrix[i, NormalizeIndex(j - 4, currentWidth - 1)] * this.wavelet.analysisHigh[0] +
                    this.imageMatrix[i, NormalizeIndex(j - 3, currentWidth - 1)] * this.wavelet.analysisHigh[1] +
                    this.imageMatrix[i, NormalizeIndex(j - 2, currentWidth - 1)] * this.wavelet.analysisHigh[2] +
                    this.imageMatrix[i, NormalizeIndex(j - 1, currentWidth - 1)] * this.wavelet.analysisHigh[3] +
                    this.imageMatrix[i, NormalizeIndex(j, currentWidth - 1)] * this.wavelet.analysisHigh[4] +
                    this.imageMatrix[i, NormalizeIndex(j + 1, currentWidth - 1)] * this.wavelet.analysisHigh[5] +
                    this.imageMatrix[i, NormalizeIndex(j + 2, currentWidth - 1)] * this.wavelet.analysisHigh[6] +
                    this.imageMatrix[i, NormalizeIndex(j + 3, currentWidth - 1)] * this.wavelet.analysisHigh[7] +
                    this.imageMatrix[i, NormalizeIndex(j + 4, currentWidth - 1)] * this.wavelet.analysisHigh[8];
                }

                int mergedVectorIndex = 0;
                double[] mergedVector = new double[currentHeight];

                for (int x = 0; x < currentHeight; x += 2)
                {
                    mergedVector[mergedVectorIndex] = low[x];
                    mergedVectorIndex++;
                }

                for (int x = 1; x < currentHeight; x += 2)
                {
                    mergedVector[mergedVectorIndex] = high[x];
                    mergedVectorIndex++;
                }

                for (int x = 0; x < currentWidth; x++)
                {
                    this.imageMatrix[i, x] = mergedVector[x];

                    int value = NormalizePixelValue(mergedVector[x]);

                    loadedImageCopy.SetPixel(x, i, Color.FromArgb(value, value, value));
                }
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
                    this.imageMatrix[NormalizeIndex(j - 4, currentHeight - 1), i] * this.wavelet.analysisLow[0] +
                    this.imageMatrix[NormalizeIndex(j - 3, currentHeight - 1), i] * this.wavelet.analysisLow[1] +
                    this.imageMatrix[NormalizeIndex(j - 2, currentHeight - 1), i] * this.wavelet.analysisLow[2] +
                    this.imageMatrix[NormalizeIndex(j - 1, currentHeight - 1), i] * this.wavelet.analysisLow[3] +
                    this.imageMatrix[NormalizeIndex(j, currentHeight - 1), i] * this.wavelet.analysisLow[4] +
                    this.imageMatrix[NormalizeIndex(j + 1, currentHeight - 1), i] * this.wavelet.analysisLow[5] +
                    this.imageMatrix[NormalizeIndex(j + 2, currentHeight - 1), i] * this.wavelet.analysisLow[6] +
                    this.imageMatrix[NormalizeIndex(j + 3, currentHeight - 1), i] * this.wavelet.analysisLow[7] +
                    this.imageMatrix[NormalizeIndex(j + 4, currentHeight - 1), i] * this.wavelet.analysisLow[8];

                    high[j] =
                    this.imageMatrix[NormalizeIndex(j - 4, currentHeight - 1), i] * this.wavelet.analysisHigh[0] +
                    this.imageMatrix[NormalizeIndex(j - 3, currentHeight - 1), i] * this.wavelet.analysisHigh[1] +
                    this.imageMatrix[NormalizeIndex(j - 2, currentHeight - 1), i] * this.wavelet.analysisHigh[2] +
                    this.imageMatrix[NormalizeIndex(j - 1, currentHeight - 1), i] * this.wavelet.analysisHigh[3] +
                    this.imageMatrix[NormalizeIndex(j, currentHeight - 1), i] * this.wavelet.analysisHigh[4] +
                    this.imageMatrix[NormalizeIndex(j + 1, currentHeight - 1), i] * this.wavelet.analysisHigh[5] +
                    this.imageMatrix[NormalizeIndex(j + 2, currentHeight - 1), i] * this.wavelet.analysisHigh[6] +
                    this.imageMatrix[NormalizeIndex(j + 3, currentHeight - 1), i] * this.wavelet.analysisHigh[7] +
                    this.imageMatrix[NormalizeIndex(j + 4, currentHeight - 1), i] * this.wavelet.analysisHigh[8];
                }

                int mergedVectorIndex = 0;
                double[] mergedVector = new double[currentHeight];

                for (int x = 0; x < currentHeight; x += 2)
                {
                    mergedVector[mergedVectorIndex] = low[x];
                    mergedVectorIndex++;
                }

                for (int x = 1; x < currentHeight; x += 2)
                {
                    mergedVector[mergedVectorIndex] = high[x];
                    mergedVectorIndex++;
                }

                for (int x = 0; x < currentWidth; x++)
                {
                    this.imageMatrix[x, i] = mergedVector[x];

                    int value = NormalizePixelValue(mergedVector[x]);

                    loadedImageCopy.SetPixel(i, x, Color.FromArgb(value, value, value));
                }

                //string asdasd = "";

                //if (level == 5)
                //{
                //    for (int x = 0; x < currentWidth / 2; x++)
                //    {
                //        //System.Diagnostics.Debug.WriteLine("w: " + currentWidth / 2);
                //        //System.Diagnostics.Debug.WriteLine("h: " + currentHeight / 2);

                //        //System.Diagnostics.Debug.WriteLine(i + " |||| " + x);

                //        if (i > 15)
                //        {
                //            continue;
                //        }

                //        this.imageMatrix[i, x] = mergedVector[x];

                //        asdasd += mergedVector[x].ToString() + " ";
                //    }

                //    System.Diagnostics.Debug.WriteLine(asdasd);
                //}
            }
            loadedImageCopyPictureBox.Image = loadedImageCopy;

            UpdateImageDimensions();
        }

        private void SynthesisHorizontal(int level)
        {
            double[] high = new double[currentWidth];
            double[] low = new double[currentWidth];

            double[] highVar = new double[currentWidth];
            double[] lowVar = new double[currentWidth];

            for (int i = 0; i < currentHeight; i++)
            {
                int highIndex = 0;
                int lowIndex = 0;
                for (int j = 0; j < currentWidth; j++)
                {
                    if (j < currentWidth / 2)
                    {
                        lowVar[lowIndex] = this.imageMatrix[i, j];
                        lowIndex += 1;
                        lowVar[lowIndex] = 0;
                        lowIndex += 1;
                    }
                    else
                    {
                        highVar[highIndex] = this.imageMatrix[i, j];
                        highIndex += 1;
                        highVar[highIndex] = 0;
                        highIndex += 1;
                    }
                }

                for (int j = 0; j < currentWidth; j++)
                {
                    low[j] =
                    lowVar[NormalizeIndex(j - 4, currentWidth - 1)] * this.wavelet.synthesisLow[0] +
                    lowVar[NormalizeIndex(j - 3, currentWidth - 1)] * this.wavelet.synthesisLow[1] +
                    lowVar[NormalizeIndex(j - 2, currentWidth - 1)] * this.wavelet.synthesisLow[2] +
                    lowVar[NormalizeIndex(j - 1, currentWidth - 1)] * this.wavelet.synthesisLow[3] +
                    lowVar[NormalizeIndex(j, currentWidth - 1)] * this.wavelet.synthesisLow[4] +
                    lowVar[NormalizeIndex(j + 1, currentWidth - 1)] * this.wavelet.synthesisLow[5] +
                    lowVar[NormalizeIndex(j + 2, currentWidth - 1)] * this.wavelet.synthesisLow[6] +
                    lowVar[NormalizeIndex(j + 3, currentWidth - 1)] * this.wavelet.synthesisLow[7] +
                    lowVar[NormalizeIndex(j + 4, currentWidth - 1)] * this.wavelet.synthesisLow[8];

                    high[j] =
                    highVar[NormalizeIndex(j - 4, currentWidth - 1)] * this.wavelet.synthesisHigh[0] +
                    highVar[NormalizeIndex(j - 3, currentWidth - 1)] * this.wavelet.synthesisHigh[1] +
                    highVar[NormalizeIndex(j - 2, currentWidth - 1)] * this.wavelet.synthesisHigh[2] +
                    highVar[NormalizeIndex(j - 1, currentWidth - 1)] * this.wavelet.synthesisHigh[3] +
                    highVar[NormalizeIndex(j, currentWidth - 1)] * this.wavelet.synthesisHigh[4] +
                    highVar[NormalizeIndex(j + 1, currentWidth - 1)] * this.wavelet.synthesisHigh[5] +
                    highVar[NormalizeIndex(j + 2, currentWidth - 1)] * this.wavelet.synthesisHigh[6] +
                    highVar[NormalizeIndex(j + 3, currentWidth - 1)] * this.wavelet.synthesisHigh[7] +
                    highVar[NormalizeIndex(j + 4, currentWidth - 1)] * this.wavelet.synthesisHigh[8];
                }

                for (int x = 0; x < currentWidth; x++)
                {
                    this.imageMatrix[i, x] = low[x] + high[x];

                    int value = NormalizePixelValue(this.imageMatrix[i, x]);

                    loadedImageCopy.SetPixel(x, i, Color.FromArgb(value, value, value));
                }
            }
            loadedImageCopyPictureBox.Image = loadedImageCopy;

            if (level > 1) {
                UpdateImageDimensions(true);
            }
        }

        private void SynthesisVertical(int level)
        {
            double[] high = new double[currentWidth];
            double[] low = new double[currentWidth];

            double[] highVar = new double[currentWidth];
            double[] lowVar = new double[currentWidth];

            for (int i = 0; i < currentHeight; i++)
            {
                int highIndex = 0;
                int lowIndex = 0;
                for (int j = 0; j < currentWidth; j++)
                {
                    if (j < currentWidth / 2)
                    {
                        lowVar[lowIndex] = this.imageMatrix[j, i];
                        lowIndex += 1;
                        lowVar[lowIndex] = 0;
                        lowIndex += 1;
                    }
                    else
                    {
                        highVar[highIndex] = this.imageMatrix[j, i];
                        highIndex += 1;
                        highVar[highIndex] = 0;
                        highIndex += 1;
                    }
                }

                for (int j = 0; j < currentWidth; j++)
                {
                    low[j] =
                    lowVar[NormalizeIndex(j - 4, currentWidth - 1)] * this.wavelet.synthesisLow[0] +
                    lowVar[NormalizeIndex(j - 3, currentWidth - 1)] * this.wavelet.synthesisLow[1] +
                    lowVar[NormalizeIndex(j - 2, currentWidth - 1)] * this.wavelet.synthesisLow[2] +
                    lowVar[NormalizeIndex(j - 1, currentWidth - 1)] * this.wavelet.synthesisLow[3] +
                    lowVar[NormalizeIndex(j, currentWidth - 1)] * this.wavelet.synthesisLow[4] +
                    lowVar[NormalizeIndex(j + 1, currentWidth - 1)] * this.wavelet.synthesisLow[5] +
                    lowVar[NormalizeIndex(j + 2, currentWidth - 1)] * this.wavelet.synthesisLow[6] +
                    lowVar[NormalizeIndex(j + 3, currentWidth - 1)] * this.wavelet.synthesisLow[7] +
                    lowVar[NormalizeIndex(j + 4, currentWidth - 1)] * this.wavelet.synthesisLow[8];

                    high[j] =
                    highVar[NormalizeIndex(j - 4, currentWidth - 1)] * this.wavelet.synthesisHigh[0] +
                    highVar[NormalizeIndex(j - 3, currentWidth - 1)] * this.wavelet.synthesisHigh[1] +
                    highVar[NormalizeIndex(j - 2, currentWidth - 1)] * this.wavelet.synthesisHigh[2] +
                    highVar[NormalizeIndex(j - 1, currentWidth - 1)] * this.wavelet.synthesisHigh[3] +
                    highVar[NormalizeIndex(j, currentWidth - 1)] * this.wavelet.synthesisHigh[4] +
                    highVar[NormalizeIndex(j + 1, currentWidth - 1)] * this.wavelet.synthesisHigh[5] +
                    highVar[NormalizeIndex(j + 2, currentWidth - 1)] * this.wavelet.synthesisHigh[6] +
                    highVar[NormalizeIndex(j + 3, currentWidth - 1)] * this.wavelet.synthesisHigh[7] +
                    highVar[NormalizeIndex(j + 4, currentWidth - 1)] * this.wavelet.synthesisHigh[8];
                }

                for (int x = 0; x < currentWidth; x++)
                {
                    this.imageMatrix[x, i] = low[x] + high[x];

                    int value = NormalizePixelValue(this.imageMatrix[x, i]);

                    loadedImageCopy.SetPixel(i, x, Color.FromArgb(value, value, value));
                }
            }
            loadedImageCopyPictureBox.Image = loadedImageCopy;

            //UpdateImageDimensions(true);
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
            ScaleImage();
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

        private void AnH3_Click(object sender, EventArgs e)
        {
            AnalisysHorizontal(3);
        }

        private void AnV3_Click(object sender, EventArgs e)
        {
            AnalisysVertical(3);
        }

        private void AnH4_Click(object sender, EventArgs e)
        {
            AnalisysHorizontal(4);
        }

        private void AnV4_Click(object sender, EventArgs e)
        {
            AnalisysVertical(4);
        }

        private void AnH5_Click(object sender, EventArgs e)
        {
            AnalisysHorizontal(5);
        }

        private void AnV5_Click(object sender, EventArgs e)
        {
            AnalisysVertical(5);
        }

        private void SyH5_Click(object sender, EventArgs e)
        {
            SynthesisHorizontal(5);
        }

        private void SyV5_Click(object sender, EventArgs e)
        {
            SynthesisVertical(5);
        }

        private void SyH4_Click(object sender, EventArgs e)
        {
            SynthesisHorizontal(4);
        }

        private void SyV4_Click(object sender, EventArgs e)
        {
            SynthesisVertical(4);
        }

        private void SyH3_Click(object sender, EventArgs e)
        {
            SynthesisHorizontal(3);
        }

        private void SyV3_Click(object sender, EventArgs e)
        {
            SynthesisVertical(3);
        }

        private void SyH2_Click(object sender, EventArgs e)
        {
            SynthesisHorizontal(2);
        }

        private void SyV2_Click(object sender, EventArgs e)
        {
            SynthesisVertical(2);
        }

        private void SyH1_Click(object sender, EventArgs e)
        {
            SynthesisHorizontal(1);
        }

        private void SyV1_Click(object sender, EventArgs e)
        {
            SynthesisVertical(1);
        }

        ~Form1()
        {
            fileController.close();
        }
    }
}
