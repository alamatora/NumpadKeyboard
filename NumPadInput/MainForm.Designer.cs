namespace NumPadInput {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.btnGlobalLLKeyboardHook = new System.Windows.Forms.Button();
            this.tbLog = new System.Windows.Forms.TextBox();
            this.tbNum7 = new System.Windows.Forms.TextBox();
            this.tbNum4 = new System.Windows.Forms.TextBox();
            this.tbNum1 = new System.Windows.Forms.TextBox();
            this.tbNum2 = new System.Windows.Forms.TextBox();
            this.tbNum5 = new System.Windows.Forms.TextBox();
            this.tbNum8 = new System.Windows.Forms.TextBox();
            this.tbNum3 = new System.Windows.Forms.TextBox();
            this.tbNum6 = new System.Windows.Forms.TextBox();
            this.tbNum9 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnGlobalLLKeyboardHook
            // 
            this.btnGlobalLLKeyboardHook.Location = new System.Drawing.Point(12, 12);
            this.btnGlobalLLKeyboardHook.Name = "btnGlobalLLKeyboardHook";
            this.btnGlobalLLKeyboardHook.Size = new System.Drawing.Size(75, 23);
            this.btnGlobalLLKeyboardHook.TabIndex = 0;
            this.btnGlobalLLKeyboardHook.Text = "Hook";
            this.btnGlobalLLKeyboardHook.UseVisualStyleBackColor = true;
            this.btnGlobalLLKeyboardHook.Click += new System.EventHandler(this.btnGlobalLLKeyboardHook_Click);
            // 
            // tbLog
            // 
            this.tbLog.Location = new System.Drawing.Point(13, 55);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.Size = new System.Drawing.Size(246, 175);
            this.tbLog.TabIndex = 1;
            // 
            // tbNum7
            // 
            this.tbNum7.Location = new System.Drawing.Point(302, 55);
            this.tbNum7.Name = "tbNum7";
            this.tbNum7.Size = new System.Drawing.Size(49, 20);
            this.tbNum7.TabIndex = 2;
            this.tbNum7.Text = "abc";
            // 
            // tbNum4
            // 
            this.tbNum4.Location = new System.Drawing.Point(302, 81);
            this.tbNum4.Name = "tbNum4";
            this.tbNum4.Size = new System.Drawing.Size(49, 20);
            this.tbNum4.TabIndex = 3;
            this.tbNum4.Text = "jkl";
            // 
            // tbNum1
            // 
            this.tbNum1.Location = new System.Drawing.Point(302, 107);
            this.tbNum1.Name = "tbNum1";
            this.tbNum1.Size = new System.Drawing.Size(49, 20);
            this.tbNum1.TabIndex = 4;
            this.tbNum1.Text = "stu";
            // 
            // tbNum2
            // 
            this.tbNum2.Location = new System.Drawing.Point(357, 107);
            this.tbNum2.Name = "tbNum2";
            this.tbNum2.Size = new System.Drawing.Size(49, 20);
            this.tbNum2.TabIndex = 7;
            this.tbNum2.Text = "vwx";
            // 
            // tbNum5
            // 
            this.tbNum5.Location = new System.Drawing.Point(357, 81);
            this.tbNum5.Name = "tbNum5";
            this.tbNum5.Size = new System.Drawing.Size(49, 20);
            this.tbNum5.TabIndex = 6;
            this.tbNum5.Text = "mno";
            // 
            // tbNum8
            // 
            this.tbNum8.Location = new System.Drawing.Point(357, 55);
            this.tbNum8.Name = "tbNum8";
            this.tbNum8.Size = new System.Drawing.Size(49, 20);
            this.tbNum8.TabIndex = 5;
            this.tbNum8.Text = "def";
            // 
            // tbNum3
            // 
            this.tbNum3.Location = new System.Drawing.Point(412, 107);
            this.tbNum3.Name = "tbNum3";
            this.tbNum3.Size = new System.Drawing.Size(49, 20);
            this.tbNum3.TabIndex = 10;
            this.tbNum3.Text = "yz";
            // 
            // tbNum6
            // 
            this.tbNum6.Location = new System.Drawing.Point(412, 81);
            this.tbNum6.Name = "tbNum6";
            this.tbNum6.Size = new System.Drawing.Size(49, 20);
            this.tbNum6.TabIndex = 9;
            this.tbNum6.Text = "pqr";
            // 
            // tbNum9
            // 
            this.tbNum9.Location = new System.Drawing.Point(412, 55);
            this.tbNum9.Name = "tbNum9";
            this.tbNum9.Size = new System.Drawing.Size(49, 20);
            this.tbNum9.TabIndex = 8;
            this.tbNum9.Text = "ghi";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 287);
            this.Controls.Add(this.tbNum3);
            this.Controls.Add(this.tbNum6);
            this.Controls.Add(this.tbNum9);
            this.Controls.Add(this.tbNum2);
            this.Controls.Add(this.tbNum5);
            this.Controls.Add(this.tbNum8);
            this.Controls.Add(this.tbNum1);
            this.Controls.Add(this.tbNum4);
            this.Controls.Add(this.tbNum7);
            this.Controls.Add(this.tbLog);
            this.Controls.Add(this.btnGlobalLLKeyboardHook);
            this.Name = "MainForm";
            this.Text = "NumPad Input";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGlobalLLKeyboardHook;
        private System.Windows.Forms.TextBox tbLog;
        private System.Windows.Forms.TextBox tbNum7;
        private System.Windows.Forms.TextBox tbNum4;
        private System.Windows.Forms.TextBox tbNum1;
        private System.Windows.Forms.TextBox tbNum2;
        private System.Windows.Forms.TextBox tbNum5;
        private System.Windows.Forms.TextBox tbNum8;
        private System.Windows.Forms.TextBox tbNum3;
        private System.Windows.Forms.TextBox tbNum6;
        private System.Windows.Forms.TextBox tbNum9;
    }
}

