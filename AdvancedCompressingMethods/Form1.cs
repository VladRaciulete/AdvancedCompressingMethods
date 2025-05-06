namespace AdvancedCompressingMethods
{
    public partial class Form1 : Form
    {
        FileController fileController = new FileController("input.bin", "output.bin");
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 101; i++)
            {
                byte test = fileController.ReadByte();
                fileController.WriteByte(test);
            }

            fileController.close();
        }
    }
}
