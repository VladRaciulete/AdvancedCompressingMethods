using static System.Net.Mime.MediaTypeNames;

namespace AdvancedCompressingMethods
{
    public partial class Form1 : Form
    {
        //System.Diagnostics.Debug.WriteLine(4);
        //MessageBox.Show("a");
        FileController fileController = new FileController("input.txt", "output.txt");
        ArithmeticCoder arithmeticCoder = new ArithmeticCoder();

        public Form1()
        {
            InitializeComponent();
            start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void start() {
            arithmeticCoder.StartModel();
            arithmeticCoder.UpdateModel();
        }


        ~Form1()
        {
            fileController.close();
        }
    }
}
