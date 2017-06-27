namespace Trains2
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
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.fileNameTextBox = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.imageBox1 = new Emgu.CV.UI.ImageBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.statusLabel = new System.Windows.Forms.Label();
            this.contoursImageBox = new Emgu.CV.UI.ImageBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.originalImageBox = new Emgu.CV.UI.ImageBox();
            this.pauseButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.highThTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lowThTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.writeOutputCheckBox = new System.Windows.Forms.CheckBox();
            this.outputFileTextBox = new System.Windows.Forms.TextBox();
            this.OutputFilePanel = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.browseSaveButton = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.stopButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.contoursImageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.originalImageBox)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.OutputFilePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 38);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 26);
            this.button1.TabIndex = 3;
            this.button1.Text = "Выбрать файл";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // fileNameTextBox
            // 
            this.fileNameTextBox.Location = new System.Drawing.Point(3, 4);
            this.fileNameTextBox.Name = "fileNameTextBox";
            this.fileNameTextBox.Size = new System.Drawing.Size(122, 20);
            this.fileNameTextBox.TabIndex = 4;
            this.fileNameTextBox.TextChanged += new System.EventHandler(this.fileNameTextBox_TextChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 1);
            this.label1.TabIndex = 5;
            this.label1.Text = "Видео для анализа";
            // 
            // imageBox1
            // 
            this.imageBox1.Location = new System.Drawing.Point(12, 9);
            this.imageBox1.Name = "imageBox1";
            this.imageBox1.Size = new System.Drawing.Size(287, 262);
            this.imageBox1.TabIndex = 2;
            this.imageBox1.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.6791F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.3209F));
            this.tableLayoutPanel1.Controls.Add(this.contoursImageBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.statusLabel, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.originalImageBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pauseButton, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.stopButton, 0, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(16, 11);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(579, 314);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(3, 299);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(19, 13);
            this.statusLabel.TabIndex = 12;
            this.statusLabel.Text = "__";
            // 
            // contoursImageBox
            // 
            this.contoursImageBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.contoursImageBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.contoursImageBox.Location = new System.Drawing.Point(302, 23);
            this.contoursImageBox.Name = "contoursImageBox";
            this.contoursImageBox.Size = new System.Drawing.Size(274, 213);
            this.contoursImageBox.TabIndex = 6;
            this.contoursImageBox.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Видео";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(302, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Контуры";
            // 
            // originalImageBox
            // 
            this.originalImageBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.originalImageBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.originalImageBox.Location = new System.Drawing.Point(3, 23);
            this.originalImageBox.Name = "originalImageBox";
            this.originalImageBox.Size = new System.Drawing.Size(293, 213);
            this.originalImageBox.TabIndex = 2;
            this.originalImageBox.TabStop = false;
            // 
            // pauseButton
            // 
            this.pauseButton.Enabled = false;
            this.pauseButton.Location = new System.Drawing.Point(3, 242);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(92, 23);
            this.pauseButton.TabIndex = 7;
            this.pauseButton.Text = "Пауза";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Click += new System.EventHandler(this.button3_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.groupBox1, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.button1, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.fileNameTextBox, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.startButton, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.groupBox2, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.button2, 0, 6);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(602, 9);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 8;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 114F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(185, 378);
            this.tableLayoutPanel2.TabIndex = 11;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(3, 234);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(179, 68);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Параметры Хафа";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(7, 37);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(121, 20);
            this.textBox2.TabIndex = 1;
            this.textBox2.Text = "30";
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Порог";
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(3, 75);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(148, 23);
            this.startButton.TabIndex = 6;
            this.startButton.Text = "Анализировать файл";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.highThTextBox);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.lowThTextBox);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(3, 120);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(179, 108);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Параметры Canny";
            // 
            // highThTextBox
            // 
            this.highThTextBox.Location = new System.Drawing.Point(7, 77);
            this.highThTextBox.Name = "highThTextBox";
            this.highThTextBox.Size = new System.Drawing.Size(121, 20);
            this.highThTextBox.TabIndex = 3;
            this.highThTextBox.Text = "120";
            this.highThTextBox.TextChanged += new System.EventHandler(this.highThTextBox_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Верхний порог";
            // 
            // lowThTextBox
            // 
            this.lowThTextBox.Location = new System.Drawing.Point(7, 37);
            this.lowThTextBox.Name = "lowThTextBox";
            this.lowThTextBox.Size = new System.Drawing.Size(121, 20);
            this.lowThTextBox.TabIndex = 1;
            this.lowThTextBox.Text = "50";
            this.lowThTextBox.TextChanged += new System.EventHandler(this.lowThTextBox_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Нижний порог";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(3, 308);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(113, 23);
            this.button2.TabIndex = 10;
            this.button2.Text = "С камеры";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // writeOutputCheckBox
            // 
            this.writeOutputCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.writeOutputCheckBox.AutoSize = true;
            this.writeOutputCheckBox.Location = new System.Drawing.Point(3, 36);
            this.writeOutputCheckBox.Name = "writeOutputCheckBox";
            this.writeOutputCheckBox.Size = new System.Drawing.Size(169, 17);
            this.writeOutputCheckBox.TabIndex = 12;
            this.writeOutputCheckBox.Text = "Записывать выходной файл";
            this.writeOutputCheckBox.UseVisualStyleBackColor = true;
            // 
            // outputFileTextBox
            // 
            this.outputFileTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.outputFileTextBox.Location = new System.Drawing.Point(200, 33);
            this.outputFileTextBox.Name = "outputFileTextBox";
            this.outputFileTextBox.Size = new System.Drawing.Size(152, 20);
            this.outputFileTextBox.TabIndex = 13;
            // 
            // OutputFilePanel
            // 
            this.OutputFilePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.OutputFilePanel.ColumnCount = 2;
            this.OutputFilePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.OutputFilePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 253F));
            this.OutputFilePanel.Controls.Add(this.outputFileTextBox, 1, 1);
            this.OutputFilePanel.Controls.Add(this.writeOutputCheckBox, 0, 1);
            this.OutputFilePanel.Controls.Add(this.label4, 1, 0);
            this.OutputFilePanel.Controls.Add(this.browseSaveButton, 1, 2);
            this.OutputFilePanel.Location = new System.Drawing.Point(19, 331);
            this.OutputFilePanel.Name = "OutputFilePanel";
            this.OutputFilePanel.RowCount = 3;
            this.OutputFilePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.OutputFilePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.OutputFilePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.OutputFilePanel.Size = new System.Drawing.Size(450, 88);
            this.OutputFilePanel.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(200, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Путь к выходному файлу";
            // 
            // browseSaveButton
            // 
            this.browseSaveButton.Location = new System.Drawing.Point(200, 59);
            this.browseSaveButton.Name = "browseSaveButton";
            this.browseSaveButton.Size = new System.Drawing.Size(96, 23);
            this.browseSaveButton.TabIndex = 15;
            this.browseSaveButton.Text = "Выбрать путь";
            this.browseSaveButton.UseVisualStyleBackColor = true;
            this.browseSaveButton.Click += new System.EventHandler(this.browseSaveButton_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "avi";
            this.saveFileDialog1.Filter = "Video Files (*.avi)|*.avi;";
            // 
            // stopButton
            // 
            this.stopButton.Enabled = false;
            this.stopButton.Location = new System.Drawing.Point(3, 273);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 15;
            this.stopButton.Text = "Стоп";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 417);
            this.Controls.Add(this.OutputFilePanel);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.imageBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.contoursImageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.originalImageBox)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.OutputFilePanel.ResumeLayout(false);
            this.OutputFilePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox fileNameTextBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label1;
        private Emgu.CV.UI.ImageBox imageBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button startButton;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Emgu.CV.UI.ImageBox contoursImageBox;
        private Emgu.CV.UI.ImageBox originalImageBox;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox highThTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox lowThTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button pauseButton;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.CheckBox writeOutputCheckBox;
        private System.Windows.Forms.TextBox outputFileTextBox;
        private System.Windows.Forms.TableLayoutPanel OutputFilePanel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button browseSaveButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button stopButton;
    }
}

