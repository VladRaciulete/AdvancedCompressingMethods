namespace AdvancedCompressingMethods
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            AnH1 = new Button();
            AnV1 = new Button();
            AnH2 = new Button();
            AnV2 = new Button();
            AnH3 = new Button();
            SyH1 = new Button();
            SyV1 = new Button();
            SyH2 = new Button();
            SyV2 = new Button();
            SyH3 = new Button();
            LoadButton = new Button();
            loadedImagePictureBox = new PictureBox();
            loadedImageCopyPictureBox = new PictureBox();
            labelMinError = new Label();
            labelMaxError = new Label();
            AnV3 = new Button();
            AnH4 = new Button();
            AnV4 = new Button();
            AnH5 = new Button();
            AnV5 = new Button();
            SyV3 = new Button();
            SyH4 = new Button();
            SyV4 = new Button();
            SyH5 = new Button();
            SyV5 = new Button();
            MinMaxError = new Button();
            textBoxScale = new TextBox();
            RefreshScale = new Button();
            textBoxOffset = new TextBox();
            ((System.ComponentModel.ISupportInitialize)loadedImagePictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)loadedImageCopyPictureBox).BeginInit();
            SuspendLayout();
            // 
            // AnH1
            // 
            AnH1.Location = new Point(1410, 16);
            AnH1.Margin = new Padding(3, 4, 3, 4);
            AnH1.Name = "AnH1";
            AnH1.Size = new Size(86, 31);
            AnH1.TabIndex = 0;
            AnH1.Text = "AnH1";
            AnH1.UseVisualStyleBackColor = true;
            AnH1.Click += AnH1_Click;
            // 
            // AnV1
            // 
            AnV1.Location = new Point(1410, 55);
            AnV1.Margin = new Padding(3, 4, 3, 4);
            AnV1.Name = "AnV1";
            AnV1.Size = new Size(86, 31);
            AnV1.TabIndex = 1;
            AnV1.Text = "AnV1";
            AnV1.UseVisualStyleBackColor = true;
            AnV1.Click += AnV1_Click;
            // 
            // AnH2
            // 
            AnH2.Location = new Point(1410, 93);
            AnH2.Margin = new Padding(3, 4, 3, 4);
            AnH2.Name = "AnH2";
            AnH2.Size = new Size(86, 31);
            AnH2.TabIndex = 2;
            AnH2.Text = "AnH2";
            AnH2.UseVisualStyleBackColor = true;
            AnH2.Click += AnH2_Click;
            // 
            // AnV2
            // 
            AnV2.Location = new Point(1410, 132);
            AnV2.Margin = new Padding(3, 4, 3, 4);
            AnV2.Name = "AnV2";
            AnV2.Size = new Size(86, 31);
            AnV2.TabIndex = 3;
            AnV2.Text = "AnV2";
            AnV2.UseVisualStyleBackColor = true;
            AnV2.Click += AnV2_Click;
            // 
            // AnH3
            // 
            AnH3.Location = new Point(1410, 171);
            AnH3.Margin = new Padding(3, 4, 3, 4);
            AnH3.Name = "AnH3";
            AnH3.Size = new Size(86, 31);
            AnH3.TabIndex = 4;
            AnH3.Text = "AnH3";
            AnH3.UseVisualStyleBackColor = true;
            // 
            // SyH1
            // 
            SyH1.Location = new Point(1527, 16);
            SyH1.Margin = new Padding(3, 4, 3, 4);
            SyH1.Name = "SyH1";
            SyH1.Size = new Size(86, 31);
            SyH1.TabIndex = 5;
            SyH1.Text = "SyH1";
            SyH1.UseVisualStyleBackColor = true;
            // 
            // SyV1
            // 
            SyV1.Location = new Point(1527, 55);
            SyV1.Margin = new Padding(3, 4, 3, 4);
            SyV1.Name = "SyV1";
            SyV1.Size = new Size(86, 31);
            SyV1.TabIndex = 6;
            SyV1.Text = "SyV1";
            SyV1.UseVisualStyleBackColor = true;
            // 
            // SyH2
            // 
            SyH2.Location = new Point(1527, 93);
            SyH2.Margin = new Padding(3, 4, 3, 4);
            SyH2.Name = "SyH2";
            SyH2.Size = new Size(86, 31);
            SyH2.TabIndex = 7;
            SyH2.Text = "SyH2";
            SyH2.UseVisualStyleBackColor = true;
            // 
            // SyV2
            // 
            SyV2.Location = new Point(1527, 132);
            SyV2.Margin = new Padding(3, 4, 3, 4);
            SyV2.Name = "SyV2";
            SyV2.Size = new Size(86, 31);
            SyV2.TabIndex = 8;
            SyV2.Text = "SyV2";
            SyV2.UseVisualStyleBackColor = true;
            // 
            // SyH3
            // 
            SyH3.Location = new Point(1527, 171);
            SyH3.Margin = new Padding(3, 4, 3, 4);
            SyH3.Name = "SyH3";
            SyH3.Size = new Size(86, 31);
            SyH3.TabIndex = 9;
            SyH3.Text = "SyH3";
            SyH3.UseVisualStyleBackColor = true;
            // 
            // LoadButton
            // 
            LoadButton.Location = new Point(14, 707);
            LoadButton.Margin = new Padding(3, 4, 3, 4);
            LoadButton.Name = "LoadButton";
            LoadButton.Size = new Size(86, 31);
            LoadButton.TabIndex = 10;
            LoadButton.Text = "Load";
            LoadButton.UseVisualStyleBackColor = true;
            LoadButton.Click += LoadButton_Click;
            // 
            // loadedImagePictureBox
            // 
            loadedImagePictureBox.Location = new Point(14, 16);
            loadedImagePictureBox.Margin = new Padding(3, 4, 3, 4);
            loadedImagePictureBox.Name = "loadedImagePictureBox";
            loadedImagePictureBox.Size = new Size(585, 683);
            loadedImagePictureBox.TabIndex = 11;
            loadedImagePictureBox.TabStop = false;
            // 
            // loadedImageCopyPictureBox
            // 
            loadedImageCopyPictureBox.Location = new Point(606, 16);
            loadedImageCopyPictureBox.Margin = new Padding(3, 4, 3, 4);
            loadedImageCopyPictureBox.Name = "loadedImageCopyPictureBox";
            loadedImageCopyPictureBox.Size = new Size(585, 683);
            loadedImageCopyPictureBox.TabIndex = 12;
            loadedImageCopyPictureBox.TabStop = false;
            // 
            // labelMinError
            // 
            labelMinError.AutoSize = true;
            labelMinError.Location = new Point(96, 777);
            labelMinError.Name = "labelMinError";
            labelMinError.Size = new Size(34, 20);
            labelMinError.TabIndex = 14;
            labelMinError.Text = "Min";
            // 
            // labelMaxError
            // 
            labelMaxError.AutoSize = true;
            labelMaxError.Location = new Point(95, 815);
            labelMaxError.Name = "labelMaxError";
            labelMaxError.Size = new Size(37, 20);
            labelMaxError.TabIndex = 15;
            labelMaxError.Text = "Max";
            // 
            // AnV3
            // 
            AnV3.Location = new Point(1410, 209);
            AnV3.Margin = new Padding(3, 4, 3, 4);
            AnV3.Name = "AnV3";
            AnV3.Size = new Size(86, 31);
            AnV3.TabIndex = 16;
            AnV3.Text = "AnV3";
            AnV3.UseVisualStyleBackColor = true;
            // 
            // AnH4
            // 
            AnH4.Location = new Point(1410, 248);
            AnH4.Margin = new Padding(3, 4, 3, 4);
            AnH4.Name = "AnH4";
            AnH4.Size = new Size(86, 31);
            AnH4.TabIndex = 17;
            AnH4.Text = "AnH4";
            AnH4.UseVisualStyleBackColor = true;
            // 
            // AnV4
            // 
            AnV4.Location = new Point(1410, 287);
            AnV4.Margin = new Padding(3, 4, 3, 4);
            AnV4.Name = "AnV4";
            AnV4.Size = new Size(86, 31);
            AnV4.TabIndex = 18;
            AnV4.Text = "AnV4";
            AnV4.UseVisualStyleBackColor = true;
            // 
            // AnH5
            // 
            AnH5.Location = new Point(1410, 325);
            AnH5.Margin = new Padding(3, 4, 3, 4);
            AnH5.Name = "AnH5";
            AnH5.Size = new Size(86, 31);
            AnH5.TabIndex = 19;
            AnH5.Text = "AnH5";
            AnH5.UseVisualStyleBackColor = true;
            // 
            // AnV5
            // 
            AnV5.Location = new Point(1410, 364);
            AnV5.Margin = new Padding(3, 4, 3, 4);
            AnV5.Name = "AnV5";
            AnV5.Size = new Size(86, 31);
            AnV5.TabIndex = 20;
            AnV5.Text = "AnV5";
            AnV5.UseVisualStyleBackColor = true;
            // 
            // SyV3
            // 
            SyV3.Location = new Point(1527, 209);
            SyV3.Margin = new Padding(3, 4, 3, 4);
            SyV3.Name = "SyV3";
            SyV3.Size = new Size(86, 31);
            SyV3.TabIndex = 21;
            SyV3.Text = "SyV3";
            SyV3.UseVisualStyleBackColor = true;
            // 
            // SyH4
            // 
            SyH4.Location = new Point(1527, 248);
            SyH4.Margin = new Padding(3, 4, 3, 4);
            SyH4.Name = "SyH4";
            SyH4.Size = new Size(86, 31);
            SyH4.TabIndex = 22;
            SyH4.Text = "SyH4";
            SyH4.UseVisualStyleBackColor = true;
            // 
            // SyV4
            // 
            SyV4.Location = new Point(1527, 287);
            SyV4.Margin = new Padding(3, 4, 3, 4);
            SyV4.Name = "SyV4";
            SyV4.Size = new Size(86, 31);
            SyV4.TabIndex = 23;
            SyV4.Text = "SyV4";
            SyV4.UseVisualStyleBackColor = true;
            // 
            // SyH5
            // 
            SyH5.Location = new Point(1527, 325);
            SyH5.Margin = new Padding(3, 4, 3, 4);
            SyH5.Name = "SyH5";
            SyH5.Size = new Size(86, 31);
            SyH5.TabIndex = 24;
            SyH5.Text = "SyH5";
            SyH5.UseVisualStyleBackColor = true;
            // 
            // SyV5
            // 
            SyV5.Location = new Point(1527, 364);
            SyV5.Margin = new Padding(3, 4, 3, 4);
            SyV5.Name = "SyV5";
            SyV5.Size = new Size(86, 31);
            SyV5.TabIndex = 25;
            SyV5.Text = "SyV5";
            SyV5.UseVisualStyleBackColor = true;
            // 
            // MinMaxError
            // 
            MinMaxError.Location = new Point(14, 777);
            MinMaxError.Margin = new Padding(3, 4, 3, 4);
            MinMaxError.Name = "MinMaxError";
            MinMaxError.Size = new Size(74, 57);
            MinMaxError.TabIndex = 26;
            MinMaxError.Text = "Min Max Error";
            MinMaxError.UseVisualStyleBackColor = true;
            MinMaxError.Click += MinMaxError_Click;
            // 
            // textBoxScale
            // 
            textBoxScale.Location = new Point(1265, 17);
            textBoxScale.Margin = new Padding(3, 4, 3, 4);
            textBoxScale.Name = "textBoxScale";
            textBoxScale.Size = new Size(45, 27);
            textBoxScale.TabIndex = 27;
            textBoxScale.Text = "1";
            // 
            // RefreshScale
            // 
            RefreshScale.Location = new Point(14, 841);
            RefreshScale.Name = "RefreshScale";
            RefreshScale.Size = new Size(118, 29);
            RefreshScale.TabIndex = 28;
            RefreshScale.Text = "RefreshScale";
            RefreshScale.UseVisualStyleBackColor = true;
            RefreshScale.Click += RefreshScale_Click;
            // 
            // textBoxOffset
            // 
            textBoxOffset.Location = new Point(1265, 57);
            textBoxOffset.Name = "textBoxOffset";
            textBoxOffset.Size = new Size(45, 27);
            textBoxOffset.TabIndex = 29;
            textBoxOffset.Text = "0";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1629, 973);
            Controls.Add(textBoxOffset);
            Controls.Add(RefreshScale);
            Controls.Add(textBoxScale);
            Controls.Add(MinMaxError);
            Controls.Add(SyV5);
            Controls.Add(SyH5);
            Controls.Add(SyV4);
            Controls.Add(SyH4);
            Controls.Add(SyV3);
            Controls.Add(AnV5);
            Controls.Add(AnH5);
            Controls.Add(AnV4);
            Controls.Add(AnH4);
            Controls.Add(AnV3);
            Controls.Add(labelMaxError);
            Controls.Add(labelMinError);
            Controls.Add(loadedImageCopyPictureBox);
            Controls.Add(loadedImagePictureBox);
            Controls.Add(LoadButton);
            Controls.Add(SyH3);
            Controls.Add(SyV2);
            Controls.Add(SyH2);
            Controls.Add(SyV1);
            Controls.Add(SyH1);
            Controls.Add(AnH3);
            Controls.Add(AnV2);
            Controls.Add(AnH2);
            Controls.Add(AnV1);
            Controls.Add(AnH1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)loadedImagePictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)loadedImageCopyPictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button AnH1;
        private Button AnV1;
        private Button AnH2;
        private Button AnV2;
        private Button AnH3;
        private Button SyH1;
        private Button SyV1;
        private Button SyH2;
        private Button SyV2;
        private Button SyH3;
        private Button LoadButton;
        private PictureBox loadedImagePictureBox;
        private PictureBox loadedImageCopyPictureBox;
        private Label labelMinError;
        private Label labelMaxError;
        private Button AnV3;
        private Button AnH4;
        private Button AnV4;
        private Button AnH5;
        private Button AnV5;
        private Button SyV3;
        private Button SyH4;
        private Button SyV4;
        private Button SyH5;
        private Button SyV5;
        private Button MinMaxError;
        private TextBox textBoxScale;
        private Button RefreshScale;
        private TextBox textBoxOffset;
    }
}
