namespace DG_Engineering
{
    partial class LoginWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginWindow));
            this.SaveLoginInfo = new System.Windows.Forms.CheckBox();
            this.ResetPasswordLink = new System.Windows.Forms.LinkLabel();
            this.Login_Button = new System.Windows.Forms.Button();
            this.Password_TextBox = new System.Windows.Forms.TextBox();
            this.Password_Label = new System.Windows.Forms.Label();
            this.Username_TextBox = new System.Windows.Forms.TextBox();
            this.Username_Label = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.DebugMode = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // SaveLoginInfo
            // 
            this.SaveLoginInfo.AutoSize = true;
            this.SaveLoginInfo.Location = new System.Drawing.Point(183, 516);
            this.SaveLoginInfo.Name = "SaveLoginInfo";
            this.SaveLoginInfo.Size = new System.Drawing.Size(132, 17);
            this.SaveLoginInfo.TabIndex = 18;
            this.SaveLoginInfo.Text = "Save My Login Details";
            this.SaveLoginInfo.UseVisualStyleBackColor = true;
            // 
            // ResetPasswordLink
            // 
            this.ResetPasswordLink.AutoSize = true;
            this.ResetPasswordLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResetPasswordLink.Location = new System.Drawing.Point(170, 484);
            this.ResetPasswordLink.Name = "ResetPasswordLink";
            this.ResetPasswordLink.Size = new System.Drawing.Size(158, 20);
            this.ResetPasswordLink.TabIndex = 16;
            this.ResetPasswordLink.TabStop = true;
            this.ResetPasswordLink.Text = "Reset your password";
            this.ResetPasswordLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ResetPasswordLink_LinkClicked);
            // 
            // Login_Button
            // 
            this.Login_Button.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Login_Button.Location = new System.Drawing.Point(187, 434);
            this.Login_Button.Name = "Login_Button";
            this.Login_Button.Size = new System.Drawing.Size(121, 41);
            this.Login_Button.TabIndex = 14;
            this.Login_Button.Text = "Login";
            this.Login_Button.UseVisualStyleBackColor = true;
            this.Login_Button.Click += new System.EventHandler(this.Login_Button_Click);
            // 
            // Password_TextBox
            // 
            this.Password_TextBox.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Password_TextBox.Location = new System.Drawing.Point(75, 395);
            this.Password_TextBox.Name = "Password_TextBox";
            this.Password_TextBox.PasswordChar = '*';
            this.Password_TextBox.Size = new System.Drawing.Size(341, 33);
            this.Password_TextBox.TabIndex = 13;
            this.Password_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Password_TextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Password_TextBox_KeyDown);
            // 
            // Password_Label
            // 
            this.Password_Label.AutoSize = true;
            this.Password_Label.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Password_Label.Location = new System.Drawing.Point(189, 359);
            this.Password_Label.Name = "Password_Label";
            this.Password_Label.Size = new System.Drawing.Size(119, 33);
            this.Password_Label.TabIndex = 15;
            this.Password_Label.Text = "Password";
            this.Password_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Username_TextBox
            // 
            this.Username_TextBox.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Username_TextBox.Location = new System.Drawing.Point(75, 323);
            this.Username_TextBox.Name = "Username_TextBox";
            this.Username_TextBox.Size = new System.Drawing.Size(341, 33);
            this.Username_TextBox.TabIndex = 11;
            this.Username_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Username_Label
            // 
            this.Username_Label.AutoSize = true;
            this.Username_Label.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Username_Label.Location = new System.Drawing.Point(185, 287);
            this.Username_Label.Name = "Username_Label";
            this.Username_Label.Size = new System.Drawing.Size(127, 33);
            this.Username_Label.TabIndex = 12;
            this.Username_Label.Text = "Username";
            this.Username_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DG_Engineering.Properties.Resources.Standard_Logo;
            this.pictureBox1.Location = new System.Drawing.Point(75, 15);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(341, 185);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // DebugMode
            // 
            this.DebugMode.AutoSize = true;
            this.DebugMode.Location = new System.Drawing.Point(380, 536);
            this.DebugMode.Name = "DebugMode";
            this.DebugMode.Size = new System.Drawing.Size(99, 17);
            this.DebugMode.TabIndex = 19;
            this.DebugMode.Text = "DEBUG MODE";
            this.DebugMode.UseVisualStyleBackColor = true;
            // 
            // LoginWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 565);
            this.Controls.Add(this.DebugMode);
            this.Controls.Add(this.SaveLoginInfo);
            this.Controls.Add(this.ResetPasswordLink);
            this.Controls.Add(this.Login_Button);
            this.Controls.Add(this.Password_TextBox);
            this.Controls.Add(this.Password_Label);
            this.Controls.Add(this.Username_TextBox);
            this.Controls.Add(this.Username_Label);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoginWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DG Engineering Automation";
            this.Shown += new System.EventHandler(this.LoginForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox SaveLoginInfo;
        private System.Windows.Forms.LinkLabel ResetPasswordLink;
        private System.Windows.Forms.Button Login_Button;
        private System.Windows.Forms.TextBox Password_TextBox;
        private System.Windows.Forms.Label Password_Label;
        private System.Windows.Forms.TextBox Username_TextBox;
        private System.Windows.Forms.Label Username_Label;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox DebugMode;
    }
}

