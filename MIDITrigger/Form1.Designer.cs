namespace MIDITrigger
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.lbInputDevices = new System.Windows.Forms.ListBox();
            this.btnRecordSave = new System.Windows.Forms.Button();
            this.tbPlayed = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.lbOutputDevices = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbInputDevices
            // 
            this.lbInputDevices.FormattingEnabled = true;
            this.lbInputDevices.Location = new System.Drawing.Point(12, 12);
            this.lbInputDevices.Name = "lbInputDevices";
            this.lbInputDevices.Size = new System.Drawing.Size(190, 69);
            this.lbInputDevices.TabIndex = 0;
            this.lbInputDevices.SelectedIndexChanged += new System.EventHandler(this.lbInputDevices_SelectedIndexChanged);
            // 
            // btnRecordSave
            // 
            this.btnRecordSave.Location = new System.Drawing.Point(221, 171);
            this.btnRecordSave.Name = "btnRecordSave";
            this.btnRecordSave.Size = new System.Drawing.Size(189, 30);
            this.btnRecordSave.TabIndex = 1;
            this.btnRecordSave.Text = "Record Password";
            this.btnRecordSave.UseVisualStyleBackColor = true;
            this.btnRecordSave.Click += new System.EventHandler(this.btnRecordSave_Click);
            // 
            // tbPlayed
            // 
            this.tbPlayed.Location = new System.Drawing.Point(12, 171);
            this.tbPlayed.Multiline = true;
            this.tbPlayed.Name = "tbPlayed";
            this.tbPlayed.Size = new System.Drawing.Size(189, 254);
            this.tbPlayed.TabIndex = 2;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(221, 393);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(190, 32);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lbOutputDevices
            // 
            this.lbOutputDevices.FormattingEnabled = true;
            this.lbOutputDevices.Location = new System.Drawing.Point(221, 12);
            this.lbOutputDevices.Name = "lbOutputDevices";
            this.lbOutputDevices.Size = new System.Drawing.Size(190, 69);
            this.lbOutputDevices.TabIndex = 4;
            this.lbOutputDevices.SelectedIndexChanged += new System.EventHandler(this.lbOutputDevices_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(9, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(401, 44);
            this.label1.TabIndex = 5;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 440);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbOutputDevices);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.tbPlayed);
            this.Controls.Add(this.btnRecordSave);
            this.Controls.Add(this.lbInputDevices);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MIDI Password";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbInputDevices;
        private System.Windows.Forms.Button btnRecordSave;
        private System.Windows.Forms.TextBox tbPlayed;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ListBox lbOutputDevices;
        private System.Windows.Forms.Label label1;
    }
}

