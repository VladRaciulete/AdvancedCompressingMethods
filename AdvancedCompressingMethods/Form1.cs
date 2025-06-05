using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace AdvancedCompressingMethods
{
    public partial class Form1 : Form
    {
        //System.Diagnostics.Debug.WriteLine(4);
        //MessageBox.Show("a");
        //FileController fileController = new FileController("input.txt", "output.txt");
        ArithmeticCoder arithmeticCoder = new ArithmeticCoder();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text Files|*.txt",
                Title = "Select a text file"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    arithmeticCoder.OpenInputFileStream(openFileDialog.FileName);

                    MessageBox.Show("File loaded!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading file: " + ex.Message);
                }
            }
        }

        private void buttonEncode_Click(object sender, EventArgs e)
        {
            if (!arithmeticCoder.fileLoaded)
            {
                MessageBox.Show("Please load a file to encode!");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt";

            DialogResult result = saveFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                arithmeticCoder.OpenOutputFileStream(saveFileDialog.FileName);
                arithmeticCoder.Encode();

                MessageBox.Show("Encoded file saved!");
            }
        }

        private void buttonDecode_Click(object sender, EventArgs e)
        {
            if (!arithmeticCoder.fileLoaded)
            {
                MessageBox.Show("Please load a file to decode!");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt";

            DialogResult result = saveFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                arithmeticCoder.OpenOutputFileStream(saveFileDialog.FileName);
                arithmeticCoder.Decode();

                MessageBox.Show("Decoded file saved!");
            }
        }

        ~Form1()
        {
            //fileController.close();
        }
    }
}
