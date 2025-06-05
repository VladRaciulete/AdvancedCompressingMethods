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
            buttonEncode = new Button();
            buttonLoad = new Button();
            buttonDecode = new Button();
            SuspendLayout();
            // 
            // buttonEncode
            // 
            buttonEncode.Location = new Point(462, 230);
            buttonEncode.Name = "buttonEncode";
            buttonEncode.Size = new Size(74, 24);
            buttonEncode.TabIndex = 0;
            buttonEncode.Text = "Encode";
            buttonEncode.UseVisualStyleBackColor = true;
            buttonEncode.Click += buttonEncode_Click;
            // 
            // buttonLoad
            // 
            buttonLoad.Location = new Point(463, 200);
            buttonLoad.Name = "buttonLoad";
            buttonLoad.Size = new Size(74, 24);
            buttonLoad.TabIndex = 1;
            buttonLoad.Text = "Load";
            buttonLoad.UseVisualStyleBackColor = true;
            buttonLoad.Click += buttonLoad_Click;
            // 
            // buttonDecode
            // 
            buttonDecode.Location = new Point(462, 260);
            buttonDecode.Name = "buttonDecode";
            buttonDecode.Size = new Size(74, 24);
            buttonDecode.TabIndex = 2;
            buttonDecode.Text = "Decode";
            buttonDecode.UseVisualStyleBackColor = true;
            buttonDecode.Click += buttonDecode_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 461);
            Controls.Add(buttonDecode);
            Controls.Add(buttonLoad);
            Controls.Add(buttonEncode);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button buttonEncode;
        private Button buttonLoad;
        private Button buttonDecode;
    }
}
