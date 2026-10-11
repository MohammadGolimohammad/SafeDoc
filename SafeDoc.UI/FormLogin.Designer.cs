namespace SafeDoc.UI
{
    partial class FormLogin
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblActiveKey;
        private System.Windows.Forms.TextBox txtAccessKey;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnLanguage;
        private System.Windows.Forms.Label lblLoading;
        private System.Windows.Forms.ProgressBar loginProgressBar;
        private System.Windows.Forms.Timer loginLoadingTimer;
        private System.Windows.Forms.CheckBox chkRememberAccessKey;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblActiveKey = new System.Windows.Forms.Label();
            this.txtAccessKey = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnLanguage = new System.Windows.Forms.Button();
            this.lblLoading = new System.Windows.Forms.Label();
            this.loginProgressBar = new System.Windows.Forms.ProgressBar();
            this.loginLoadingTimer = new System.Windows.Forms.Timer(this.components);
            this.chkRememberAccessKey = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Shabnam FD", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.lblTitle.Location = new System.Drawing.Point(25, 28);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(390, 43);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "SafeDoc";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.Font = new System.Drawing.Font("Shabnam FD", 9F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblSubtitle.Location = new System.Drawing.Point(25, 70);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(390, 31);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "مدیریت دستگاه امنیتی";
            this.lblSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblActiveKey
            // 
            this.lblActiveKey.AutoSize = true;
            this.lblActiveKey.Font = new System.Drawing.Font("Shabnam FD", 9F);
            this.lblActiveKey.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.lblActiveKey.Location = new System.Drawing.Point(45, 132);
            this.lblActiveKey.Name = "lblActiveKey";
            this.lblActiveKey.Size = new System.Drawing.Size(54, 16);
            this.lblActiveKey.TabIndex = 2;
            this.lblActiveKey.Text = "کلید ورود:";
            // 
            // txtAccessKey
            // 
            this.txtAccessKey.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtAccessKey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAccessKey.Font = new System.Drawing.Font("Shabnam FD", 10F);
            this.txtAccessKey.ForeColor = System.Drawing.Color.White;
            this.txtAccessKey.Location = new System.Drawing.Point(45, 151);
            this.txtAccessKey.Name = "txtAccessKey";
            this.txtAccessKey.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtAccessKey.Size = new System.Drawing.Size(350, 26);
            this.txtAccessKey.TabIndex = 3;
            this.txtAccessKey.UseSystemPasswordChar = true;
            //
            // chkRememberAccessKey
            //
            this.chkRememberAccessKey.AutoSize = true;
            this.chkRememberAccessKey.Font = new System.Drawing.Font("Shabnam FD", 8.5F);
            this.chkRememberAccessKey.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.chkRememberAccessKey.Location = new System.Drawing.Point(45, 179);
            this.chkRememberAccessKey.Name = "chkRememberAccessKey";
            this.chkRememberAccessKey.Size = new System.Drawing.Size(124, 20);
            this.chkRememberAccessKey.TabIndex = 4;
            this.chkRememberAccessKey.Text = "یادآوری کلید ورود";
            this.chkRememberAccessKey.UseVisualStyleBackColor = true;
            //
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Shabnam FD", 9F, System.Drawing.FontStyle.Bold);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(45, 204);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(350, 39);
            this.btnLogin.TabIndex = 5;
            this.btnLogin.Text = "ورود به مدیریت دستگاه";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnLanguage
            // 
            this.btnLanguage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnLanguage.FlatAppearance.BorderSize = 0;
            this.btnLanguage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLanguage.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnLanguage.ForeColor = System.Drawing.Color.White;
            this.btnLanguage.Location = new System.Drawing.Point(25, 25);
            this.btnLanguage.Name = "btnLanguage";
            this.btnLanguage.Size = new System.Drawing.Size(45, 28);
            this.btnLanguage.TabIndex = 6;
            this.btnLanguage.Text = "EN";
            this.btnLanguage.UseVisualStyleBackColor = false;
            this.btnLanguage.Click += new System.EventHandler(this.btnLanguage_Click);
            // 
            // lblLoading
            // 
            this.lblLoading.Font = new System.Drawing.Font("Shabnam FD", 8.5F);
            this.lblLoading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblLoading.Location = new System.Drawing.Point(45, 251);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(350, 20);
            this.lblLoading.TabIndex = 7;
            this.lblLoading.Text = "در حال آماده‌سازی مدیریت دستگاه...";
            this.lblLoading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLoading.Visible = false;
            // 
            // loginProgressBar
            // 
            this.loginProgressBar.Location = new System.Drawing.Point(45, 274);
            this.loginProgressBar.Name = "loginProgressBar";
            this.loginProgressBar.Size = new System.Drawing.Size(350, 8);
            this.loginProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.loginProgressBar.TabIndex = 8;
            this.loginProgressBar.Visible = false;
            // 
            // loginLoadingTimer
            // 
            this.loginLoadingTimer.Interval = 120;
            this.loginLoadingTimer.Tick += new System.EventHandler(this.loginLoadingTimer_Tick);
            // 
            // FormLogin
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.ClientSize = new System.Drawing.Size(440, 305);
            this.Controls.Add(this.loginProgressBar);
            this.Controls.Add(this.lblLoading);
            this.Controls.Add(this.btnLanguage);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.chkRememberAccessKey);
            this.Controls.Add(this.txtAccessKey);
            this.Controls.Add(this.lblActiveKey);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Shabnam FD", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormLogin";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SafeDoc | ورود";
            this.Load += new System.EventHandler(this.FormLogin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
