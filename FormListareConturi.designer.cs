
namespace ProiectBRC
{
    partial class FormListareConturi
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button4 = new System.Windows.Forms.Button();
            this.label35 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.textBox28 = new System.Windows.Forms.TextBox();
            this.textBox27 = new System.Windows.Forms.TextBox();
            this.textBox26 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(50, 223);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(426, 150);
            this.dataGridView1.TabIndex = 0;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(378, 70);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 46);
            this.button4.TabIndex = 18;
            this.button4.Text = "Inapoi";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(48, 142);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(49, 13);
            this.label35.TabIndex = 17;
            this.label35.Text = "Prenume";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(48, 106);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(35, 13);
            this.label34.TabIndex = 16;
            this.label34.Text = "Nume";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(47, 70);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(29, 13);
            this.label33.TabIndex = 15;
            this.label33.Text = "CNP";
            // 
            // textBox28
            // 
            this.textBox28.Location = new System.Drawing.Point(106, 142);
            this.textBox28.Multiline = true;
            this.textBox28.Name = "textBox28";
            this.textBox28.Size = new System.Drawing.Size(200, 30);
            this.textBox28.TabIndex = 14;
            // 
            // textBox27
            // 
            this.textBox27.Location = new System.Drawing.Point(106, 106);
            this.textBox27.Multiline = true;
            this.textBox27.Name = "textBox27";
            this.textBox27.Size = new System.Drawing.Size(200, 30);
            this.textBox27.TabIndex = 13;
            // 
            // textBox26
            // 
            this.textBox26.Location = new System.Drawing.Point(106, 70);
            this.textBox26.Multiline = true;
            this.textBox26.Name = "textBox26";
            this.textBox26.Size = new System.Drawing.Size(200, 30);
            this.textBox26.TabIndex = 12;
            // 
            // FormListareConturi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 450);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.textBox28);
            this.Controls.Add(this.textBox27);
            this.Controls.Add(this.textBox26);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FormListareConturi";
            this.Text = "FormListareConturi";
            this.Load += new System.EventHandler(this.FormListareConturi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.TextBox textBox28;
        private System.Windows.Forms.TextBox textBox27;
        private System.Windows.Forms.TextBox textBox26;
    }
}