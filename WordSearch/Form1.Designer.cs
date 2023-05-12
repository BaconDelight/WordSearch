namespace WordSearch
{
    partial class Form1
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
            this.btnMake = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.numAcross = new System.Windows.Forms.NumericUpDown();
            this.numDown = new System.Windows.Forms.NumericUpDown();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtWords = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numAcross)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDown)).BeginInit();
            this.SuspendLayout();
            // 
            // btnMake
            // 
            this.btnMake.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.900001F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMake.Location = new System.Drawing.Point(989, 401);
            this.btnMake.Name = "btnMake";
            this.btnMake.Size = new System.Drawing.Size(283, 105);
            this.btnMake.TabIndex = 1;
            this.btnMake.Text = "Make";
            this.btnMake.UseVisualStyleBackColor = true;
            this.btnMake.Click += new System.EventHandler(this.btnMake_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.900001F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.Location = new System.Drawing.Point(1048, 107);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(195, 56);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtFile
            // 
            this.txtFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.900001F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFile.Location = new System.Drawing.Point(279, 107);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(733, 45);
            this.txtFile.TabIndex = 3;
            // 
            // numAcross
            // 
            this.numAcross.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.900001F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numAcross.Location = new System.Drawing.Point(1092, 241);
            this.numAcross.Name = "numAcross";
            this.numAcross.Size = new System.Drawing.Size(151, 45);
            this.numAcross.TabIndex = 4;
            this.numAcross.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // numDown
            // 
            this.numDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.900001F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numDown.Location = new System.Drawing.Point(1092, 309);
            this.numDown.Name = "numDown";
            this.numDown.Size = new System.Drawing.Size(151, 45);
            this.numDown.TabIndex = 5;
            this.numDown.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // txtTitle
            // 
            this.txtTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.900001F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTitle.Location = new System.Drawing.Point(279, 47);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(732, 45);
            this.txtTitle.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.900001F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(162, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 39);
            this.label1.TabIndex = 8;
            this.label1.Text = "Title";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.900001F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(81, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(163, 39);
            this.label2.TabIndex = 9;
            this.label2.Text = "Word File";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.900001F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(952, 243);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 39);
            this.label3.TabIndex = 10;
            this.label3.Text = "Across";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.900001F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(968, 309);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 39);
            this.label4.TabIndex = 11;
            this.label4.Text = "Down";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.900001F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(129, 221);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 39);
            this.label5.TabIndex = 12;
            this.label5.Text = "Words";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.900001F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(989, 545);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(283, 105);
            this.button1.TabIndex = 14;
            this.button1.Text = "Exit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtWords
            // 
            this.txtWords.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.900001F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWords.Location = new System.Drawing.Point(279, 221);
            this.txtWords.Multiline = true;
            this.txtWords.Name = "txtWords";
            this.txtWords.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtWords.Size = new System.Drawing.Size(602, 479);
            this.txtWords.TabIndex = 15;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1473, 755);
            this.Controls.Add(this.txtWords);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.numDown);
            this.Controls.Add(this.numAcross);
            this.Controls.Add(this.txtFile);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.btnMake);
            this.Name = "Form1";
            this.Text = "WordSearch v1.0";
            ((System.ComponentModel.ISupportInitialize)(this.numAcross)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnMake;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.NumericUpDown numAcross;
        private System.Windows.Forms.NumericUpDown numDown;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtWords;
    }
}

