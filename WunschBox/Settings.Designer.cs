namespace WunschBox
{
    partial class Settings
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
            trackBar1 = new TrackBar();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            buttonSave = new Button();
            label4 = new Label();
            textBoxAppName = new TextBox();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            SuspendLayout();
            // 
            // trackBar1
            // 
            trackBar1.Location = new Point(12, 29);
            trackBar1.Maximum = 100;
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(542, 45);
            trackBar1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 11);
            label1.Name = "label1";
            label1.Size = new Size(69, 15);
            label1.TabIndex = 1;
            label1.Text = "Transparenz";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 59);
            label2.Name = "label2";
            label2.Size = new Size(35, 15);
            label2.TabIndex = 2;
            label2.Text = "100%";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(516, 59);
            label3.Name = "label3";
            label3.Size = new Size(23, 15);
            label3.TabIndex = 3;
            label3.Text = "0%";
            // 
            // buttonSave
            // 
            buttonSave.Location = new Point(393, 100);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(157, 23);
            buttonSave.TabIndex = 4;
            buttonSave.Text = "Speichern und schließen";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += buttonSave_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 77);
            label4.Name = "label4";
            label4.Size = new Size(103, 15);
            label4.TabIndex = 5;
            label4.Text = "Applikationsname";
            // 
            // textBoxAppName
            // 
            textBoxAppName.Location = new Point(15, 100);
            textBoxAppName.Name = "textBoxAppName";
            textBoxAppName.Size = new Size(300, 23);
            textBoxAppName.TabIndex = 6;
            // 
            // Settings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(562, 135);
            Controls.Add(textBoxAppName);
            Controls.Add(label4);
            Controls.Add(buttonSave);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(trackBar1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Settings";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "Einstellungen";
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TrackBar trackBar1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button buttonSave;
        private Label label4;
        private TextBox textBoxAppName;
    }
}