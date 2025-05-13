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

        ~Form1()
        {
            fileController.close();
        }
    }
}
