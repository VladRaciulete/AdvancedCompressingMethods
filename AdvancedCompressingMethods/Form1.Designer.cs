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
            SuspendLayout();
            // 
            // buttonEncode
            // 
            buttonEncode.Location = new Point(194, 293);
            buttonEncode.Name = "buttonEncode";
            buttonEncode.Size = new Size(75, 23);
            buttonEncode.TabIndex = 0;
            buttonEncode.Text = "Encode";
            buttonEncode.UseVisualStyleBackColor = true;
            buttonEncode.Click += buttonEncode_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 461);
            Controls.Add(buttonEncode);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button buttonEncode;
    }
}
