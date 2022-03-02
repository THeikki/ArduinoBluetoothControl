
namespace ArduinoBluetoothControl
{
    partial class ArduinoController
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
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.comboBox = new System.Windows.Forms.ComboBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.textBox = new System.Windows.Forms.TextBox();
            this.quitButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.textBoxTemperature = new System.Windows.Forms.TextBox();
            this.stopButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBox
            // 
            this.comboBox.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar;
            this.comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox.FormattingEnabled = true;
            this.comboBox.Location = new System.Drawing.Point(191, 42);
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size(200, 21);
            this.comboBox.TabIndex = 0;
            // 
            // connectButton
            // 
            this.connectButton.BackColor = System.Drawing.SystemColors.Control;
            this.connectButton.Location = new System.Drawing.Point(34, 42);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(90, 23);
            this.connectButton.TabIndex = 1;
            this.connectButton.Text = "Yhdistä ";
            this.connectButton.UseVisualStyleBackColor = false;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // textBox
            // 
            this.textBox.Enabled = false;
            this.textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox.Location = new System.Drawing.Point(34, 138);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.ReadOnly = true;
            this.textBox.Size = new System.Drawing.Size(259, 112);
            this.textBox.TabIndex = 3;
            // 
            // quitButton
            // 
            this.quitButton.BackColor = System.Drawing.Color.Salmon;
            this.quitButton.ForeColor = System.Drawing.Color.Black;
            this.quitButton.Location = new System.Drawing.Point(504, 379);
            this.quitButton.Name = "quitButton";
            this.quitButton.Size = new System.Drawing.Size(75, 40);
            this.quitButton.TabIndex = 4;
            this.quitButton.Text = "Sulje ohjelma";
            this.quitButton.UseVisualStyleBackColor = false;
            this.quitButton.Click += new System.EventHandler(this.quitButton_Click);
            // 
            // startButton
            // 
            this.startButton.BackColor = System.Drawing.SystemColors.Control;
            this.startButton.Location = new System.Drawing.Point(34, 282);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(70, 40);
            this.startButton.TabIndex = 5;
            this.startButton.Text = "Käynnistä";
            this.startButton.UseVisualStyleBackColor = false;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.BackColor = System.Drawing.SystemColors.Control;
            this.saveButton.Location = new System.Drawing.Point(223, 282);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(70, 40);
            this.saveButton.TabIndex = 6;
            this.saveButton.Text = "Tallenna data";
            this.saveButton.UseVisualStyleBackColor = false;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // textBoxTemperature
            // 
            this.textBoxTemperature.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxTemperature.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxTemperature.Enabled = false;
            this.textBoxTemperature.Location = new System.Drawing.Point(111, 112);
            this.textBoxTemperature.Name = "textBoxTemperature";
            this.textBoxTemperature.Size = new System.Drawing.Size(100, 13);
            this.textBoxTemperature.TabIndex = 7;
            this.textBoxTemperature.Text = "Lämpötila";
            this.textBoxTemperature.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // stopButton
            // 
            this.stopButton.BackColor = System.Drawing.SystemColors.Control;
            this.stopButton.Location = new System.Drawing.Point(129, 282);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(70, 40);
            this.stopButton.TabIndex = 8;
            this.stopButton.Text = "Pysäytä tallennus";
            this.stopButton.UseVisualStyleBackColor = false;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // ArduinoController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(613, 457);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.textBoxTemperature);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.quitButton);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.comboBox);
            this.Name = "ArduinoController";
            this.Text = "Arduino Controller 1.0";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ArduinoController_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.ComboBox comboBox;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Button quitButton;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.TextBox textBoxTemperature;
        private System.Windows.Forms.Button stopButton;
    }
}

