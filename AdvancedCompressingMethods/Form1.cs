using static System.Net.Mime.MediaTypeNames;

namespace AdvancedCompressingMethods
{
    public partial class Form1 : Form
    {
        FileController fileController = new FileController("input.mp4", "output.mp4");
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            int randomNumber;

            this.label1.Text = "started";
            await Task.Delay(100);

            while (true) {
                randomNumber = random.Next(30, 33);

                long bitsLeft = fileController.getRemainingBitsLeft();

                if (randomNumber <= bitsLeft)
                {
                    int[] test = fileController.ReadNBits(randomNumber);
                    fileController.WriteNBits(test, randomNumber);
                }
                else {
                    int[] test = fileController.ReadNBits((int)bitsLeft);
                    fileController.WriteNBits(test, (int)bitsLeft);
                    break;
                }
            }
            this.label1.Text = "done";
        }


        ~Form1()
        {
            fileController.close();
        }
    }
}
