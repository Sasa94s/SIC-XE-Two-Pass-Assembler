namespace SicAssembler
{
    partial class Assembler
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pathLabel = new System.Windows.Forms.Label();
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.Browse = new System.Windows.Forms.OpenFileDialog();
            this.textBoxPass1 = new System.Windows.Forms.TextBox();
            this.textBoxPass2 = new System.Windows.Forms.TextBox();
            this.labelPass1 = new System.Windows.Forms.Label();
            this.labelPass2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pathLabel
            // 
            this.pathLabel.AutoSize = true;
            this.pathLabel.Location = new System.Drawing.Point(13, 13);
            this.pathLabel.Name = "pathLabel";
            this.pathLabel.Size = new System.Drawing.Size(33, 13);
            this.pathLabel.TabIndex = 0;
            this.pathLabel.Text = "Path:";
            // 
            // pathTextBox
            // 
            this.pathTextBox.Location = new System.Drawing.Point(53, 10);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.Size = new System.Drawing.Size(313, 20);
            this.pathTextBox.TabIndex = 1;
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(372, 8);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(107, 23);
            this.browseButton.TabIndex = 2;
            this.browseButton.Text = "Browse";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(485, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(140, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Generate Object File";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Browse
            // 
            this.Browse.FileName = "src.txt";
            // 
            // textBoxPass1
            // 
            this.textBoxPass1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBoxPass1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPass1.ForeColor = System.Drawing.SystemColors.Info;
            this.textBoxPass1.Location = new System.Drawing.Point(13, 59);
            this.textBoxPass1.Multiline = true;
            this.textBoxPass1.Name = "textBoxPass1";
            this.textBoxPass1.ReadOnly = true;
            this.textBoxPass1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxPass1.Size = new System.Drawing.Size(313, 228);
            this.textBoxPass1.TabIndex = 6;
            this.textBoxPass1.WordWrap = false;
            // 
            // textBoxPass2
            // 
            this.textBoxPass2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBoxPass2.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPass2.ForeColor = System.Drawing.SystemColors.Info;
            this.textBoxPass2.Location = new System.Drawing.Point(332, 59);
            this.textBoxPass2.Multiline = true;
            this.textBoxPass2.Name = "textBoxPass2";
            this.textBoxPass2.ReadOnly = true;
            this.textBoxPass2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxPass2.Size = new System.Drawing.Size(293, 228);
            this.textBoxPass2.TabIndex = 6;
            this.textBoxPass2.WordWrap = false;
            // 
            // labelPass1
            // 
            this.labelPass1.AutoSize = true;
            this.labelPass1.Location = new System.Drawing.Point(143, 43);
            this.labelPass1.Name = "labelPass1";
            this.labelPass1.Size = new System.Drawing.Size(35, 13);
            this.labelPass1.TabIndex = 6;
            this.labelPass1.Text = "Pass1";
            // 
            // labelPass2
            // 
            this.labelPass2.AutoSize = true;
            this.labelPass2.Location = new System.Drawing.Point(469, 43);
            this.labelPass2.Name = "labelPass2";
            this.labelPass2.Size = new System.Drawing.Size(35, 13);
            this.labelPass2.TabIndex = 7;
            this.labelPass2.Text = "Pass2";
            // 
            // Assembler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 299);
            this.Controls.Add(this.labelPass2);
            this.Controls.Add(this.labelPass1);
            this.Controls.Add(this.textBoxPass2);
            this.Controls.Add(this.textBoxPass1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.pathTextBox);
            this.Controls.Add(this.pathLabel);
            this.Name = "Assembler";
            this.Text = "Assembler";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label pathLabel;
        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog Browse;
        private System.Windows.Forms.TextBox textBoxPass1;
        private System.Windows.Forms.TextBox textBoxPass2;
        private System.Windows.Forms.Label labelPass1;
        private System.Windows.Forms.Label labelPass2;
    }
}

