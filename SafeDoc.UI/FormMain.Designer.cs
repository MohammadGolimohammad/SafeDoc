namespace SafeDoc.UI
{
    partial class FormMain
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Timer connectionWatchTimer;
        private System.Windows.Forms.Timer deviceClockTimer;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.GroupBox groupFingerprint;
        private System.Windows.Forms.GroupBox groupSettings;
        private System.Windows.Forms.GroupBox groupUsers;
        private System.Windows.Forms.StatusStrip operationStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblOperationProgress;
        private System.Windows.Forms.ToolStripProgressBar operationProgressBar;
        private System.Windows.Forms.Label lblConnection;
        private System.Windows.Forms.Label lblCurrentDeviceTime;
        private System.Windows.Forms.Label lblPasswordStrength;
        private System.Windows.Forms.TabControl tabFeatures;
        private System.Windows.Forms.TabPage tabFingerprint, tabPassword, tabDateTime, tabSettings;
        private System.Windows.Forms.TextBox txtFingerprintUserId, txtFingerprintPasswordName, txtFingerprintPassword, txtPasswordUserId, txtPasswordName, txtPasswordValue, txtUsbTimeout;
        private System.Windows.Forms.MaskedTextBox txtDeviceTime, txtExpirationTime;
        private Atf.UI.DateTimeSelector deviceDateSelector, expirationDateSelector;
        private System.Windows.Forms.Button btnEnrollFingerprint, btnCancelUser, btnIdentifyFingerprint, btnDeleteAllFingerprints, btnAddPassword, btnDeletePassword, btnDeleteAllPasswords, btnShowPasswords, btnSetDateTime, btnSetExpiration, btnGetExpiration, btnSetUsb;
        private System.Windows.Forms.Button btnEnterStatusToggle, btnHidAfterExpirationToggle, btnBuzzerStatusToggle;
        private System.Windows.Forms.Button btnToggleFingerprintPassword;
        private System.Windows.Forms.Panel pnlPasswordStrengthWeak, pnlPasswordStrengthMedium, pnlPasswordStrengthStrong;
        private System.Windows.Forms.Label lblUsbUseCount;
        private System.Windows.Forms.NumericUpDown nudUsbUseCount;
        private System.Windows.Forms.DataGridView dgvUsers;
        private System.Windows.Forms.Button btnSaveSelectedUserPassword;
        private System.Windows.Forms.Button btnGetSelectedUserPasswords;
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.connectionWatchTimer = new System.Windows.Forms.Timer(this.components);
            this.deviceClockTimer = new System.Windows.Forms.Timer(this.components);
            this.pnlMain = new System.Windows.Forms.Panel();
            this.connection = new System.Windows.Forms.GroupBox();
            this.operationStatusStrip = new System.Windows.Forms.StatusStrip();
            this.lblOperationProgress = new System.Windows.Forms.ToolStripStatusLabel();
            this.operationProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.lblConnection = new System.Windows.Forms.Label();
            this.chkClearSavedAccessKey = new System.Windows.Forms.CheckBox();
            this.groupFingerprint = new System.Windows.Forms.GroupBox();
            this.lblPasswordStrength = new System.Windows.Forms.Label();
            this.pnlPasswordStrengthWeak = new System.Windows.Forms.Panel();
            this.pnlPasswordStrengthMedium = new System.Windows.Forms.Panel();
            this.pnlPasswordStrengthStrong = new System.Windows.Forms.Panel();
            this.btnToggleFingerprintPassword = new System.Windows.Forms.Button();
            this.chkFlashPermission = new System.Windows.Forms.CheckBox();
            this.lblUsbUseCount = new System.Windows.Forms.Label();
            this.nudUsbUseCount = new System.Windows.Forms.NumericUpDown();
            this.fpPasswordName = new System.Windows.Forms.Label();
            this.fpPassword = new System.Windows.Forms.Label();
            this.txtFingerprintPasswordName = new System.Windows.Forms.TextBox();
            this.txtFingerprintPassword = new System.Windows.Forms.TextBox();
            this.btnCancelUser = new System.Windows.Forms.Button();
            this.expTime = new System.Windows.Forms.Label();
            this.expirationDateSelector = new Atf.UI.DateTimeSelector();
            this.txtExpirationTime = new System.Windows.Forms.MaskedTextBox();
            this.btnEnrollFingerprint = new System.Windows.Forms.Button();
            this.btnIdentifyFingerprint = new System.Windows.Forms.Button();
            this.groupSettings = new System.Windows.Forms.GroupBox();
            this.date = new System.Windows.Forms.Label();
            this.usb = new System.Windows.Forms.Label();
            this.btnSetDateTime = new System.Windows.Forms.Button();
            this.btnEnterStatusToggle = new System.Windows.Forms.Button();
            this.btnDeleteAllFingerprints = new System.Windows.Forms.Button();
            this.txtDeviceTime = new System.Windows.Forms.MaskedTextBox();
            this.txtUsbTimeout = new System.Windows.Forms.TextBox();
            this.btnSetUsb = new System.Windows.Forms.Button();
            this.deviceDateSelector = new Atf.UI.DateTimeSelector();
            this.lblCurrentDeviceTime = new System.Windows.Forms.Label();
            this.btnHidAfterExpirationToggle = new System.Windows.Forms.Button();
            this.btnBuzzerStatusToggle = new System.Windows.Forms.Button();
            this.currentTimeTitle = new System.Windows.Forms.Label();
            this.groupUsers = new System.Windows.Forms.GroupBox();
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.btnSaveSelectedUserPassword = new System.Windows.Forms.Button();
            this.btnGetSelectedUserPasswords = new System.Windows.Forms.Button();
            this.fpId = new System.Windows.Forms.Label();
            this.passId = new System.Windows.Forms.Label();
            this.passName = new System.Windows.Forms.Label();
            this.passValue = new System.Windows.Forms.Label();
            this.passHelp = new System.Windows.Forms.Label();
            this.expId = new System.Windows.Forms.Label();
            this.tabFeatures = new System.Windows.Forms.TabControl();
            this.tabFingerprint = new System.Windows.Forms.TabPage();
            this.tabDateTime = new System.Windows.Forms.TabPage();
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.btnSetExpiration = new System.Windows.Forms.Button();
            this.btnGetExpiration = new System.Windows.Forms.Button();
            this.txtFingerprintUserId = new System.Windows.Forms.TextBox();
            this.tabPassword = new System.Windows.Forms.TabPage();
            this.txtPasswordUserId = new System.Windows.Forms.TextBox();
            this.txtPasswordName = new System.Windows.Forms.TextBox();
            this.txtPasswordValue = new System.Windows.Forms.TextBox();
            this.btnAddPassword = new System.Windows.Forms.Button();
            this.btnDeletePassword = new System.Windows.Forms.Button();
            this.btnShowPasswords = new System.Windows.Forms.Button();
            this.btnDeleteAllPasswords = new System.Windows.Forms.Button();
            this.ColumnUserId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnUserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnExpireDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnExpireTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnFlashPermission = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColumnUsbUseCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnUsbUsedTimes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnEdit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ColumnDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ColumnFingerprint = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ColumnFingerprintStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlMain.SuspendLayout();
            this.connection.SuspendLayout();
            this.operationStatusStrip.SuspendLayout();
            this.groupFingerprint.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudUsbUseCount)).BeginInit();
            this.groupSettings.SuspendLayout();
            this.groupUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            this.tabFeatures.SuspendLayout();
            this.tabPassword.SuspendLayout();
            this.SuspendLayout();
            // 
            // connectionWatchTimer
            // 
            this.connectionWatchTimer.Enabled = true;
            this.connectionWatchTimer.Interval = 2000;
            this.connectionWatchTimer.Tick += new System.EventHandler(this.connectionWatchTimer_Tick);
            // 
            // deviceClockTimer
            // 
            this.deviceClockTimer.Interval = 1000;
            this.deviceClockTimer.Tick += new System.EventHandler(this.deviceClockTimer_Tick);
            // 
            // pnlMain
            // 
            this.pnlMain.AutoScroll = true;
            this.pnlMain.Controls.Add(this.connection);
            this.pnlMain.Controls.Add(this.lblConnection);
            this.pnlMain.Controls.Add(this.chkClearSavedAccessKey);
            this.pnlMain.Controls.Add(this.groupFingerprint);
            this.pnlMain.Controls.Add(this.groupSettings);
            this.pnlMain.Controls.Add(this.groupUsers);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(946, 623);
            this.pnlMain.TabIndex = 0;
            // 
            // connection
            // 
            this.connection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.connection.Controls.Add(this.operationStatusStrip);
            this.connection.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.connection.Location = new System.Drawing.Point(12, 12);
            this.connection.Name = "connection";
            this.connection.Size = new System.Drawing.Size(922, 51);
            this.connection.TabIndex = 1;
            this.connection.TabStop = false;
            this.connection.Text = "اتصال به دستگاه";
            // 
            // operationStatusStrip
            // 
            this.operationStatusStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.operationStatusStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.operationStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblOperationProgress,
            this.operationProgressBar});
            this.operationStatusStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.operationStatusStrip.Location = new System.Drawing.Point(3, 20);
            this.operationStatusStrip.Name = "operationStatusStrip";
            this.operationStatusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.operationStatusStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.operationStatusStrip.Size = new System.Drawing.Size(916, 28);
            this.operationStatusStrip.TabIndex = 1;
            // 
            // lblOperationProgress
            // 
            this.lblOperationProgress.AutoSize = false;
            this.lblOperationProgress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.lblOperationProgress.Name = "lblOperationProgress";
            this.lblOperationProgress.Size = new System.Drawing.Size(430, 17);
            this.lblOperationProgress.Text = "آماده انجام عملیات";
            this.lblOperationProgress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // operationProgressBar
            // 
            this.operationProgressBar.Name = "operationProgressBar";
            this.operationProgressBar.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.operationProgressBar.Size = new System.Drawing.Size(200, 22);
            this.operationProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // lblConnection
            // 
            this.lblConnection.AutoSize = true;
            this.lblConnection.Location = new System.Drawing.Point(737, 577);
            this.lblConnection.Name = "lblConnection";
            this.lblConnection.Size = new System.Drawing.Size(194, 16);
            this.lblConnection.TabIndex = 7;
            this.lblConnection.Text = "وضعیت دستگاه تأییدشده: ● پیدا نشد";
            this.lblConnection.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkClearSavedAccessKey
            // 
            this.chkClearSavedAccessKey.AutoSize = true;
            this.chkClearSavedAccessKey.Location = new System.Drawing.Point(12, 580);
            this.chkClearSavedAccessKey.Name = "chkClearSavedAccessKey";
            this.chkClearSavedAccessKey.Size = new System.Drawing.Size(152, 20);
            this.chkClearSavedAccessKey.TabIndex = 8;
            this.chkClearSavedAccessKey.Text = "حذف کلید ورود ذخیره‌شده";
            this.chkClearSavedAccessKey.UseVisualStyleBackColor = true;
            this.chkClearSavedAccessKey.CheckedChanged += new System.EventHandler(this.chkClearSavedAccessKey_CheckedChanged);
            // 
            // groupFingerprint
            // 
            this.groupFingerprint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.groupFingerprint.Controls.Add(this.lblPasswordStrength);
            this.groupFingerprint.Controls.Add(this.pnlPasswordStrengthWeak);
            this.groupFingerprint.Controls.Add(this.pnlPasswordStrengthMedium);
            this.groupFingerprint.Controls.Add(this.pnlPasswordStrengthStrong);
            this.groupFingerprint.Controls.Add(this.btnToggleFingerprintPassword);
            this.groupFingerprint.Controls.Add(this.chkFlashPermission);
            this.groupFingerprint.Controls.Add(this.lblUsbUseCount);
            this.groupFingerprint.Controls.Add(this.nudUsbUseCount);
            this.groupFingerprint.Controls.Add(this.fpPasswordName);
            this.groupFingerprint.Controls.Add(this.fpPassword);
            this.groupFingerprint.Controls.Add(this.txtFingerprintPasswordName);
            this.groupFingerprint.Controls.Add(this.txtFingerprintPassword);
            this.groupFingerprint.Controls.Add(this.btnCancelUser);
            this.groupFingerprint.Controls.Add(this.expTime);
            this.groupFingerprint.Controls.Add(this.expirationDateSelector);
            this.groupFingerprint.Controls.Add(this.txtExpirationTime);
            this.groupFingerprint.Controls.Add(this.btnEnrollFingerprint);
            this.groupFingerprint.Controls.Add(this.btnIdentifyFingerprint);
            this.groupFingerprint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.groupFingerprint.Location = new System.Drawing.Point(12, 69);
            this.groupFingerprint.Name = "groupFingerprint";
            this.groupFingerprint.Size = new System.Drawing.Size(922, 100);
            this.groupFingerprint.TabIndex = 1;
            this.groupFingerprint.TabStop = false;
            this.groupFingerprint.Text = "اطلاعات";
            // 
            // lblPasswordStrength
            // 
            this.lblPasswordStrength.AutoSize = true;
            this.lblPasswordStrength.Location = new System.Drawing.Point(408, 81);
            this.lblPasswordStrength.Name = "lblPasswordStrength";
            this.lblPasswordStrength.Size = new System.Drawing.Size(65, 16);
            this.lblPasswordStrength.TabIndex = 10;
            this.lblPasswordStrength.Text = "قدرت رمز: —";
            // 
            // pnlPasswordStrengthWeak
            // 
            this.pnlPasswordStrengthWeak.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.pnlPasswordStrengthWeak.Location = new System.Drawing.Point(366, 64);
            this.pnlPasswordStrengthWeak.Name = "pnlPasswordStrengthWeak";
            this.pnlPasswordStrengthWeak.Size = new System.Drawing.Size(32, 7);
            this.pnlPasswordStrengthWeak.TabIndex = 11;
            // 
            // pnlPasswordStrengthMedium
            // 
            this.pnlPasswordStrengthMedium.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.pnlPasswordStrengthMedium.Location = new System.Drawing.Point(402, 64);
            this.pnlPasswordStrengthMedium.Name = "pnlPasswordStrengthMedium";
            this.pnlPasswordStrengthMedium.Size = new System.Drawing.Size(32, 7);
            this.pnlPasswordStrengthMedium.TabIndex = 12;
            // 
            // pnlPasswordStrengthStrong
            // 
            this.pnlPasswordStrengthStrong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.pnlPasswordStrengthStrong.Location = new System.Drawing.Point(438, 64);
            this.pnlPasswordStrengthStrong.Name = "pnlPasswordStrengthStrong";
            this.pnlPasswordStrengthStrong.Size = new System.Drawing.Size(32, 7);
            this.pnlPasswordStrengthStrong.TabIndex = 13;
            // 
            // btnToggleFingerprintPassword
            // 
            this.btnToggleFingerprintPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.btnToggleFingerprintPassword.BackgroundImage = global::SafeDoc.UI.Properties.Resources.showPass;
            this.btnToggleFingerprintPassword.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnToggleFingerprintPassword.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnToggleFingerprintPassword.FlatAppearance.BorderSize = 0;
            this.btnToggleFingerprintPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggleFingerprintPassword.ForeColor = System.Drawing.Color.White;
            this.btnToggleFingerprintPassword.Location = new System.Drawing.Point(252, 25);
            this.btnToggleFingerprintPassword.Name = "btnToggleFingerprintPassword";
            this.btnToggleFingerprintPassword.Size = new System.Drawing.Size(38, 24);
            this.btnToggleFingerprintPassword.TabIndex = 9;
            this.btnToggleFingerprintPassword.UseVisualStyleBackColor = false;
            this.btnToggleFingerprintPassword.Click += new System.EventHandler(this.btnToggleFingerprintPassword_Click);
            // 
            // chkFlashPermission
            // 
            this.chkFlashPermission.AutoSize = true;
            this.chkFlashPermission.Location = new System.Drawing.Point(489, 61);
            this.chkFlashPermission.Name = "chkFlashPermission";
            this.chkFlashPermission.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkFlashPermission.Size = new System.Drawing.Size(76, 20);
            this.chkFlashPermission.TabIndex = 8;
            this.chkFlashPermission.Text = "مجوز فلش";
            this.chkFlashPermission.UseVisualStyleBackColor = true;
            this.chkFlashPermission.CheckedChanged += new System.EventHandler(this.chkFlashPermission_CheckedChanged);
            //
            // lblUsbUseCount
            //
            this.lblUsbUseCount.AutoSize = true;
            this.lblUsbUseCount.Location = new System.Drawing.Point(477, 83);
            this.lblUsbUseCount.Name = "lblUsbUseCount";
            this.lblUsbUseCount.Size = new System.Drawing.Size(109, 16);
            this.lblUsbUseCount.TabIndex = 15;
            this.lblUsbUseCount.Text = "دفعات مجاز روزانه:";
            //
            // nudUsbUseCount
            //
            this.nudUsbUseCount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.nudUsbUseCount.ForeColor = System.Drawing.Color.White;
            this.nudUsbUseCount.Location = new System.Drawing.Point(593, 80);
            this.nudUsbUseCount.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.nudUsbUseCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudUsbUseCount.Name = "nudUsbUseCount";
            this.nudUsbUseCount.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.nudUsbUseCount.Size = new System.Drawing.Size(51, 24);
            this.nudUsbUseCount.TabIndex = 16;
            this.nudUsbUseCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudUsbUseCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // fpPasswordName
            // 
            this.fpPasswordName.AutoSize = true;
            this.fpPasswordName.Location = new System.Drawing.Point(817, 27);
            this.fpPasswordName.Name = "fpPasswordName";
            this.fpPasswordName.Size = new System.Drawing.Size(54, 16);
            this.fpPasswordName.TabIndex = 2;
            this.fpPasswordName.Text = "نام کاربری";
            // 
            // fpPassword
            // 
            this.fpPassword.AutoSize = true;
            this.fpPassword.Location = new System.Drawing.Point(528, 27);
            this.fpPassword.Name = "fpPassword";
            this.fpPassword.Size = new System.Drawing.Size(40, 16);
            this.fpPassword.TabIndex = 2;
            this.fpPassword.Text = "گذرواژه";
            // 
            // txtFingerprintPasswordName
            // 
            this.txtFingerprintPasswordName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtFingerprintPasswordName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFingerprintPasswordName.ForeColor = System.Drawing.Color.White;
            this.txtFingerprintPasswordName.Location = new System.Drawing.Point(579, 25);
            this.txtFingerprintPasswordName.Name = "txtFingerprintPasswordName";
            this.txtFingerprintPasswordName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtFingerprintPasswordName.Size = new System.Drawing.Size(232, 24);
            this.txtFingerprintPasswordName.TabIndex = 3;
            // 
            // txtFingerprintPassword
            // 
            this.txtFingerprintPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtFingerprintPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFingerprintPassword.ForeColor = System.Drawing.Color.White;
            this.txtFingerprintPassword.Location = new System.Drawing.Point(290, 25);
            this.txtFingerprintPassword.MaxLength = 20;
            this.txtFingerprintPassword.Name = "txtFingerprintPassword";
            this.txtFingerprintPassword.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtFingerprintPassword.Size = new System.Drawing.Size(232, 24);
            this.txtFingerprintPassword.TabIndex = 4;
            this.txtFingerprintPassword.UseSystemPasswordChar = true;
            this.txtFingerprintPassword.TextChanged += new System.EventHandler(this.txtFingerprintPassword_TextChanged);
            this.txtFingerprintPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFingerprintPassword_KeyPress);
            // 
            // btnCancelUser
            // 
            this.btnCancelUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnCancelUser.FlatAppearance.BorderSize = 0;
            this.btnCancelUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelUser.ForeColor = System.Drawing.Color.White;
            this.btnCancelUser.Location = new System.Drawing.Point(22, 62);
            this.btnCancelUser.Name = "btnCancelUser";
            this.btnCancelUser.Size = new System.Drawing.Size(117, 24);
            this.btnCancelUser.TabIndex = 6;
            this.btnCancelUser.Text = "انصراف";
            this.btnCancelUser.UseVisualStyleBackColor = false;
            this.btnCancelUser.Click += new System.EventHandler(this.btnCancelUser_Click);
            // 
            // expTime
            // 
            this.expTime.AutoSize = true;
            this.expTime.Location = new System.Drawing.Point(817, 62);
            this.expTime.Name = "expTime";
            this.expTime.Size = new System.Drawing.Size(59, 16);
            this.expTime.TabIndex = 1;
            this.expTime.Text = "تاریخ انقضا";
            // 
            // expirationDateSelector
            // 
            this.expirationDateSelector.Font = new System.Drawing.Font("Shabnam FD", 9F);
            this.expirationDateSelector.Location = new System.Drawing.Point(686, 58);
            this.expirationDateSelector.Name = "expirationDateSelector";
            this.expirationDateSelector.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.expirationDateSelector.Size = new System.Drawing.Size(125, 25);
            this.expirationDateSelector.TabIndex = 4;
            this.expirationDateSelector.UsePersianFormat = true;
            // 
            // txtExpirationTime
            // 
            this.txtExpirationTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtExpirationTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtExpirationTime.ForeColor = System.Drawing.Color.White;
            this.txtExpirationTime.Location = new System.Drawing.Point(623, 58);
            this.txtExpirationTime.Mask = "00:00";
            this.txtExpirationTime.Name = "txtExpirationTime";
            this.txtExpirationTime.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtExpirationTime.Size = new System.Drawing.Size(56, 24);
            this.txtExpirationTime.TabIndex = 5;
            this.txtExpirationTime.Text = "2359";
            this.txtExpirationTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtExpirationTime.ValidatingType = typeof(System.DateTime);
            // 
            // btnEnrollFingerprint
            // 
            this.btnEnrollFingerprint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnEnrollFingerprint.FlatAppearance.BorderSize = 0;
            this.btnEnrollFingerprint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnrollFingerprint.ForeColor = System.Drawing.Color.White;
            this.btnEnrollFingerprint.Location = new System.Drawing.Point(145, 62);
            this.btnEnrollFingerprint.Name = "btnEnrollFingerprint";
            this.btnEnrollFingerprint.Size = new System.Drawing.Size(117, 24);
            this.btnEnrollFingerprint.TabIndex = 5;
            this.btnEnrollFingerprint.Text = "ذخیره";
            this.btnEnrollFingerprint.UseVisualStyleBackColor = false;
            this.btnEnrollFingerprint.Click += new System.EventHandler(this.btnEnrollFingerprint_Click);
            // 
            // btnIdentifyFingerprint
            // 
            this.btnIdentifyFingerprint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnIdentifyFingerprint.FlatAppearance.BorderSize = 0;
            this.btnIdentifyFingerprint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIdentifyFingerprint.ForeColor = System.Drawing.Color.White;
            this.btnIdentifyFingerprint.Location = new System.Drawing.Point(264, 62);
            this.btnIdentifyFingerprint.Name = "btnIdentifyFingerprint";
            this.btnIdentifyFingerprint.Size = new System.Drawing.Size(96, 24);
            this.btnIdentifyFingerprint.TabIndex = 14;
            this.btnIdentifyFingerprint.Text = "شناسایی";
            this.btnIdentifyFingerprint.UseVisualStyleBackColor = false;
            this.btnIdentifyFingerprint.Click += new System.EventHandler(this.btnIdentifyFingerprint_Click);
            // 
            // groupSettings
            // 
            this.groupSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.groupSettings.Controls.Add(this.date);
            this.groupSettings.Controls.Add(this.usb);
            this.groupSettings.Controls.Add(this.btnSetDateTime);
            this.groupSettings.Controls.Add(this.btnEnterStatusToggle);
            this.groupSettings.Controls.Add(this.btnDeleteAllFingerprints);
            this.groupSettings.Controls.Add(this.txtDeviceTime);
            this.groupSettings.Controls.Add(this.txtUsbTimeout);
            this.groupSettings.Controls.Add(this.btnSetUsb);
            this.groupSettings.Controls.Add(this.deviceDateSelector);
            this.groupSettings.Controls.Add(this.lblCurrentDeviceTime);
            this.groupSettings.Controls.Add(this.btnHidAfterExpirationToggle);
            this.groupSettings.Controls.Add(this.btnBuzzerStatusToggle);
            this.groupSettings.Controls.Add(this.currentTimeTitle);
            this.groupSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.groupSettings.Location = new System.Drawing.Point(12, 414);
            this.groupSettings.Name = "groupSettings";
            this.groupSettings.Size = new System.Drawing.Size(922, 160);
            this.groupSettings.TabIndex = 3;
            this.groupSettings.TabStop = false;
            this.groupSettings.Text = "تنظیمات دستگاه";
            // 
            // date
            // 
            this.date.AutoSize = true;
            this.date.Location = new System.Drawing.Point(627, 21);
            this.date.Name = "date";
            this.date.Size = new System.Drawing.Size(59, 16);
            this.date.TabIndex = 0;
            this.date.Text = "تاریخ جدید";
            // 
            // usb
            // 
            this.usb.AutoSize = true;
            this.usb.Location = new System.Drawing.Point(653, 84);
            this.usb.Name = "usb";
            this.usb.Size = new System.Drawing.Size(222, 16);
            this.usb.TabIndex = 2;
            this.usb.Text = "مهلت دسترسی USB به حافظه داخلی (دقیقه)";
            // 
            // btnSetDateTime
            // 
            this.btnSetDateTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnSetDateTime.FlatAppearance.BorderSize = 0;
            this.btnSetDateTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetDateTime.ForeColor = System.Drawing.Color.White;
            this.btnSetDateTime.Location = new System.Drawing.Point(246, 40);
            this.btnSetDateTime.Name = "btnSetDateTime";
            this.btnSetDateTime.Size = new System.Drawing.Size(234, 27);
            this.btnSetDateTime.TabIndex = 7;
            this.btnSetDateTime.Text = "ثبت تاریخ و ساعت جدید";
            this.btnSetDateTime.UseVisualStyleBackColor = false;
            this.btnSetDateTime.Click += new System.EventHandler(this.btnSetDateTime_Click);
            // 
            // btnEnterStatusToggle
            // 
            this.btnEnterStatusToggle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.btnEnterStatusToggle.FlatAppearance.BorderSize = 0;
            this.btnEnterStatusToggle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnterStatusToggle.ForeColor = System.Drawing.Color.White;
            this.btnEnterStatusToggle.Location = new System.Drawing.Point(6, 79);
            this.btnEnterStatusToggle.Name = "btnEnterStatusToggle";
            this.btnEnterStatusToggle.Size = new System.Drawing.Size(234, 27);
            this.btnEnterStatusToggle.TabIndex = 7;
            this.btnEnterStatusToggle.Text = "ارسال Enter پس از رمز (فعال)";
            this.btnEnterStatusToggle.UseVisualStyleBackColor = false;
            this.btnEnterStatusToggle.Click += new System.EventHandler(this.btnEnterStatusToggle_Click);
            // 
            // btnDeleteAllFingerprints
            // 
            this.btnDeleteAllFingerprints.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnDeleteAllFingerprints.FlatAppearance.BorderSize = 0;
            this.btnDeleteAllFingerprints.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteAllFingerprints.ForeColor = System.Drawing.Color.White;
            this.btnDeleteAllFingerprints.Location = new System.Drawing.Point(6, 40);
            this.btnDeleteAllFingerprints.Name = "btnDeleteAllFingerprints";
            this.btnDeleteAllFingerprints.Size = new System.Drawing.Size(234, 27);
            this.btnDeleteAllFingerprints.TabIndex = 6;
            this.btnDeleteAllFingerprints.Text = "پاک کردن همه اطلاعات ثبت ‌شده دستگاه";
            this.btnDeleteAllFingerprints.UseVisualStyleBackColor = false;
            this.btnDeleteAllFingerprints.Click += new System.EventHandler(this.btnDeleteAllFingerprints_Click);
            // 
            // txtDeviceTime
            // 
            this.txtDeviceTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtDeviceTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDeviceTime.ForeColor = System.Drawing.Color.White;
            this.txtDeviceTime.Location = new System.Drawing.Point(489, 41);
            this.txtDeviceTime.Mask = "00:00";
            this.txtDeviceTime.Name = "txtDeviceTime";
            this.txtDeviceTime.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtDeviceTime.Size = new System.Drawing.Size(56, 24);
            this.txtDeviceTime.TabIndex = 6;
            this.txtDeviceTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDeviceTime.ValidatingType = typeof(System.DateTime);
            // 
            // txtUsbTimeout
            // 
            this.txtUsbTimeout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtUsbTimeout.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUsbTimeout.Font = new System.Drawing.Font("Shabnam FD", 12F);
            this.txtUsbTimeout.ForeColor = System.Drawing.Color.White;
            this.txtUsbTimeout.Location = new System.Drawing.Point(601, 79);
            this.txtUsbTimeout.Multiline = true;
            this.txtUsbTimeout.Name = "txtUsbTimeout";
            this.txtUsbTimeout.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtUsbTimeout.Size = new System.Drawing.Size(46, 27);
            this.txtUsbTimeout.TabIndex = 10;
            // 
            // btnSetUsb
            // 
            this.btnSetUsb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnSetUsb.FlatAppearance.BorderSize = 0;
            this.btnSetUsb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetUsb.ForeColor = System.Drawing.Color.White;
            this.btnSetUsb.Location = new System.Drawing.Point(489, 79);
            this.btnSetUsb.Name = "btnSetUsb";
            this.btnSetUsb.Size = new System.Drawing.Size(106, 27);
            this.btnSetUsb.TabIndex = 11;
            this.btnSetUsb.Text = "ذخیره مهلت دسترسی";
            this.btnSetUsb.UseVisualStyleBackColor = false;
            this.btnSetUsb.Click += new System.EventHandler(this.btnSetUsb_Click);
            // 
            // deviceDateSelector
            // 
            this.deviceDateSelector.Font = new System.Drawing.Font("Shabnam FD", 9F);
            this.deviceDateSelector.Location = new System.Drawing.Point(551, 41);
            this.deviceDateSelector.Name = "deviceDateSelector";
            this.deviceDateSelector.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.deviceDateSelector.Size = new System.Drawing.Size(125, 25);
            this.deviceDateSelector.TabIndex = 5;
            this.deviceDateSelector.UsePersianFormat = true;
            // 
            // lblCurrentDeviceTime
            // 
            this.lblCurrentDeviceTime.AutoSize = true;
            this.lblCurrentDeviceTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(165)))), ((int)(((byte)(250)))));
            this.lblCurrentDeviceTime.Location = new System.Drawing.Point(750, 45);
            this.lblCurrentDeviceTime.Name = "lblCurrentDeviceTime";
            this.lblCurrentDeviceTime.Size = new System.Drawing.Size(95, 16);
            this.lblCurrentDeviceTime.TabIndex = 4;
            this.lblCurrentDeviceTime.Text = "هنوز دریافت نشده";
            // 
            // btnHidAfterExpirationToggle
            // 
            this.btnHidAfterExpirationToggle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.btnHidAfterExpirationToggle.FlatAppearance.BorderSize = 0;
            this.btnHidAfterExpirationToggle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHidAfterExpirationToggle.ForeColor = System.Drawing.Color.White;
            this.btnHidAfterExpirationToggle.Location = new System.Drawing.Point(246, 79);
            this.btnHidAfterExpirationToggle.Name = "btnHidAfterExpirationToggle";
            this.btnHidAfterExpirationToggle.Size = new System.Drawing.Size(234, 27);
            this.btnHidAfterExpirationToggle.TabIndex = 13;
            this.btnHidAfterExpirationToggle.Text = "فعال‌بودن HID پس از انقضا (فعال)";
            this.btnHidAfterExpirationToggle.UseVisualStyleBackColor = false;
            this.btnHidAfterExpirationToggle.Click += new System.EventHandler(this.btnHidAfterExpirationToggle_Click);
            // 
            // btnBuzzerStatusToggle
            // 
            this.btnBuzzerStatusToggle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnBuzzerStatusToggle.FlatAppearance.BorderSize = 0;
            this.btnBuzzerStatusToggle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuzzerStatusToggle.ForeColor = System.Drawing.Color.White;
            this.btnBuzzerStatusToggle.Location = new System.Drawing.Point(489, 112);
            this.btnBuzzerStatusToggle.Name = "btnBuzzerStatusToggle";
            this.btnBuzzerStatusToggle.Size = new System.Drawing.Size(234, 27);
            this.btnBuzzerStatusToggle.TabIndex = 14;
            this.btnBuzzerStatusToggle.Text = "صدای بازر (وضعیت نامشخص)";
            this.btnBuzzerStatusToggle.UseVisualStyleBackColor = false;
            this.btnBuzzerStatusToggle.Click += new System.EventHandler(this.btnBuzzerStatusToggle_Click);
            // 
            // currentTimeTitle
            // 
            this.currentTimeTitle.AutoSize = true;
            this.currentTimeTitle.Location = new System.Drawing.Point(750, 21);
            this.currentTimeTitle.Name = "currentTimeTitle";
            this.currentTimeTitle.Size = new System.Drawing.Size(92, 16);
            this.currentTimeTitle.TabIndex = 3;
            this.currentTimeTitle.Text = "زمان فعلی دستگاه";
            // 
            // groupUsers
            // 
            this.groupUsers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.groupUsers.Controls.Add(this.dgvUsers);
            this.groupUsers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.groupUsers.Location = new System.Drawing.Point(12, 175);
            this.groupUsers.Name = "groupUsers";
            this.groupUsers.Size = new System.Drawing.Size(922, 233);
            this.groupUsers.TabIndex = 4;
            this.groupUsers.TabStop = false;
            this.groupUsers.Text = "لیست کاربران";
            // 
            // dgvUsers
            // 
            this.dgvUsers.AllowUserToAddRows = false;
            this.dgvUsers.AllowUserToDeleteRows = false;
            this.dgvUsers.AllowUserToResizeRows = false;
            this.dgvUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUsers.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.dgvUsers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Shabnam FD", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvUsers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvUsers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnUserId,
            this.ColumnUserName,
            this.ColumnPass,
            this.ColumnExpireDate,
            this.ColumnExpireTime,
            this.ColumnFlashPermission,
            this.ColumnUsbUseCount,
            this.ColumnUsbUsedTimes,
            this.ColumnEdit,
            this.ColumnDelete,
            this.ColumnFingerprint,
            this.ColumnFingerprintStatus});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Shabnam FD", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvUsers.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvUsers.Location = new System.Drawing.Point(14, 23);
            this.dgvUsers.MultiSelect = false;
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.ReadOnly = true;
            this.dgvUsers.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dgvUsers.RowHeadersVisible = false;
            this.dgvUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsers.Size = new System.Drawing.Size(902, 186);
            this.dgvUsers.TabIndex = 0;
            this.dgvUsers.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUsers_CellClick);
            this.dgvUsers.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvUsers_CellFormatting);
            this.dgvUsers.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvUsers_CellPainting);
            this.dgvUsers.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUsers_CellValueChanged);
            this.dgvUsers.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvUsers_CurrentCellDirtyStateChanged);
            this.dgvUsers.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvUsers_DataBindingComplete);
            this.dgvUsers.SelectionChanged += new System.EventHandler(this.dgvUsers_SelectionChanged);
            // btnSaveSelectedUserPassword
            // 
            this.btnSaveSelectedUserPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnSaveSelectedUserPassword.FlatAppearance.BorderSize = 0;
            this.btnSaveSelectedUserPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveSelectedUserPassword.ForeColor = System.Drawing.Color.White;
            this.btnSaveSelectedUserPassword.Location = new System.Drawing.Point(333, 22);
            this.btnSaveSelectedUserPassword.Name = "btnSaveSelectedUserPassword";
            this.btnSaveSelectedUserPassword.Size = new System.Drawing.Size(194, 28);
            this.btnSaveSelectedUserPassword.TabIndex = 1;
            this.btnSaveSelectedUserPassword.Text = "ذخیره یا ویرایش رمز کاربر";
            this.btnSaveSelectedUserPassword.UseVisualStyleBackColor = false;
            this.btnSaveSelectedUserPassword.Visible = false;
            this.btnSaveSelectedUserPassword.Click += new System.EventHandler(this.btnSaveSelectedUserPassword_Click);
            // 
            // btnGetSelectedUserPasswords
            // 
            this.btnGetSelectedUserPasswords.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnGetSelectedUserPasswords.FlatAppearance.BorderSize = 0;
            this.btnGetSelectedUserPasswords.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetSelectedUserPasswords.ForeColor = System.Drawing.Color.White;
            this.btnGetSelectedUserPasswords.Location = new System.Drawing.Point(118, 22);
            this.btnGetSelectedUserPasswords.Name = "btnGetSelectedUserPasswords";
            this.btnGetSelectedUserPasswords.Size = new System.Drawing.Size(194, 28);
            this.btnGetSelectedUserPasswords.TabIndex = 2;
            this.btnGetSelectedUserPasswords.Text = "گرفتن وضعیت رمزها";
            this.btnGetSelectedUserPasswords.UseVisualStyleBackColor = false;
            this.btnGetSelectedUserPasswords.Visible = false;
            this.btnGetSelectedUserPasswords.Click += new System.EventHandler(this.btnGetSelectedUserPasswords_Click);
            // 
            // fpId
            // 
            this.fpId.AutoSize = true;
            this.fpId.Location = new System.Drawing.Point(692, 64);
            this.fpId.Name = "fpId";
            this.fpId.Size = new System.Drawing.Size(84, 16);
            this.fpId.TabIndex = 0;
            this.fpId.Text = "شماره پرسنلی";
            // 
            // passId
            // 
            this.passId.AutoSize = true;
            this.passId.Location = new System.Drawing.Point(710, 25);
            this.passId.Name = "passId";
            this.passId.Size = new System.Drawing.Size(74, 13);
            this.passId.TabIndex = 0;
            this.passId.Text = "شماره پرسنلی";
            // 
            // passName
            // 
            this.passName.AutoSize = true;
            this.passName.Location = new System.Drawing.Point(505, 25);
            this.passName.Name = "passName";
            this.passName.Size = new System.Drawing.Size(38, 13);
            this.passName.TabIndex = 1;
            this.passName.Text = "نام رمز";
            // 
            // passValue
            // 
            this.passValue.AutoSize = true;
            this.passValue.Location = new System.Drawing.Point(311, 25);
            this.passValue.Name = "passValue";
            this.passValue.Size = new System.Drawing.Size(22, 13);
            this.passValue.TabIndex = 2;
            this.passValue.Text = "رمز";
            // 
            // passHelp
            // 
            this.passHelp.AutoSize = true;
            this.passHelp.Location = new System.Drawing.Point(582, 90);
            this.passHelp.Name = "passHelp";
            this.passHelp.Size = new System.Drawing.Size(177, 13);
            this.passHelp.TabIndex = 3;
            this.passHelp.Text = "رمز در فایل یا گزارش ذخیره نمی‌شود.";
            // 
            // expId
            // 
            this.expId.AutoSize = true;
            this.expId.Location = new System.Drawing.Point(22, 91);
            this.expId.Name = "expId";
            this.expId.Size = new System.Drawing.Size(262, 16);
            this.expId.TabIndex = 0;
            this.expId.Text = "برای آخرین اثرانگشت ثبت‌شده اعمال می‌شود.";
            // 
            // tabFeatures
            // 
            this.tabFeatures.Controls.Add(this.tabFingerprint);
            this.tabFeatures.Controls.Add(this.tabDateTime);
            this.tabFeatures.Controls.Add(this.tabSettings);
            this.tabFeatures.Enabled = false;
            this.tabFeatures.Font = new System.Drawing.Font("Shabnam FD", 9F, System.Drawing.FontStyle.Bold);
            this.tabFeatures.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.tabFeatures.ItemSize = new System.Drawing.Size(130, 25);
            this.tabFeatures.Location = new System.Drawing.Point(12, 67);
            this.tabFeatures.Margin = new System.Windows.Forms.Padding(1);
            this.tabFeatures.Name = "tabFeatures";
            this.tabFeatures.Padding = new System.Drawing.Point(2, 2);
            this.tabFeatures.RightToLeftLayout = true;
            this.tabFeatures.SelectedIndex = 0;
            this.tabFeatures.Size = new System.Drawing.Size(836, 361);
            this.tabFeatures.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabFeatures.TabIndex = 2;
            // 
            // tabFingerprint
            // 
            this.tabFingerprint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.tabFingerprint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.tabFingerprint.Location = new System.Drawing.Point(4, 29);
            this.tabFingerprint.Name = "tabFingerprint";
            this.tabFingerprint.Padding = new System.Windows.Forms.Padding(10);
            this.tabFingerprint.Size = new System.Drawing.Size(828, 328);
            this.tabFingerprint.TabIndex = 0;
            this.tabFingerprint.Text = "ثبت اثرانگشت و رمز";
            // 
            // tabDateTime
            // 
            this.tabDateTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.tabDateTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.tabDateTime.Location = new System.Drawing.Point(4, 29);
            this.tabDateTime.Name = "tabDateTime";
            this.tabDateTime.Padding = new System.Windows.Forms.Padding(20);
            this.tabDateTime.Size = new System.Drawing.Size(828, 328);
            this.tabDateTime.TabIndex = 2;
            this.tabDateTime.Text = "تاریخ و ساعت";
            // 
            // tabSettings
            // 
            this.tabSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.tabSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.tabSettings.Location = new System.Drawing.Point(4, 29);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Padding = new System.Windows.Forms.Padding(20);
            this.tabSettings.Size = new System.Drawing.Size(828, 328);
            this.tabSettings.TabIndex = 3;
            this.tabSettings.Text = "تنظیمات دستگاه";
            // 
            // btnSetExpiration
            // 
            this.btnSetExpiration.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnSetExpiration.FlatAppearance.BorderSize = 0;
            this.btnSetExpiration.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetExpiration.ForeColor = System.Drawing.Color.White;
            this.btnSetExpiration.Location = new System.Drawing.Point(21, 46);
            this.btnSetExpiration.Name = "btnSetExpiration";
            this.btnSetExpiration.Size = new System.Drawing.Size(142, 36);
            this.btnSetExpiration.TabIndex = 6;
            this.btnSetExpiration.Text = "تنظیم انقضا";
            this.btnSetExpiration.UseVisualStyleBackColor = false;
            // 
            // btnGetExpiration
            // 
            this.btnGetExpiration.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnGetExpiration.FlatAppearance.BorderSize = 0;
            this.btnGetExpiration.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetExpiration.ForeColor = System.Drawing.Color.White;
            this.btnGetExpiration.Location = new System.Drawing.Point(663, 46);
            this.btnGetExpiration.Name = "btnGetExpiration";
            this.btnGetExpiration.Size = new System.Drawing.Size(142, 36);
            this.btnGetExpiration.TabIndex = 7;
            this.btnGetExpiration.Text = "خواندن انقضا";
            this.btnGetExpiration.UseVisualStyleBackColor = false;
            // 
            // txtFingerprintUserId
            // 
            this.txtFingerprintUserId.Location = new System.Drawing.Point(0, 0);
            this.txtFingerprintUserId.Name = "txtFingerprintUserId";
            this.txtFingerprintUserId.Size = new System.Drawing.Size(100, 20);
            this.txtFingerprintUserId.TabIndex = 2;
            this.txtFingerprintUserId.Visible = false;
            // 
            // tabPassword
            // 
            this.tabPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.tabPassword.Controls.Add(this.passId);
            this.tabPassword.Controls.Add(this.passName);
            this.tabPassword.Controls.Add(this.passValue);
            this.tabPassword.Controls.Add(this.passHelp);
            this.tabPassword.Controls.Add(this.txtPasswordUserId);
            this.tabPassword.Controls.Add(this.txtPasswordName);
            this.tabPassword.Controls.Add(this.txtPasswordValue);
            this.tabPassword.Controls.Add(this.btnAddPassword);
            this.tabPassword.Controls.Add(this.btnDeletePassword);
            this.tabPassword.Controls.Add(this.btnShowPasswords);
            this.tabPassword.Controls.Add(this.btnDeleteAllPasswords);
            this.tabPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.tabPassword.Location = new System.Drawing.Point(4, 29);
            this.tabPassword.Name = "tabPassword";
            this.tabPassword.Padding = new System.Windows.Forms.Padding(20);
            this.tabPassword.Size = new System.Drawing.Size(828, 332);
            this.tabPassword.TabIndex = 1;
            this.tabPassword.Text = "رمز عبور";
            // 
            // txtPasswordUserId
            // 
            this.txtPasswordUserId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtPasswordUserId.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPasswordUserId.ForeColor = System.Drawing.Color.White;
            this.txtPasswordUserId.Location = new System.Drawing.Point(582, 48);
            this.txtPasswordUserId.Name = "txtPasswordUserId";
            this.txtPasswordUserId.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtPasswordUserId.Size = new System.Drawing.Size(210, 13);
            this.txtPasswordUserId.TabIndex = 4;
            // 
            // txtPasswordName
            // 
            this.txtPasswordName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtPasswordName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPasswordName.ForeColor = System.Drawing.Color.White;
            this.txtPasswordName.Location = new System.Drawing.Point(372, 48);
            this.txtPasswordName.Name = "txtPasswordName";
            this.txtPasswordName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtPasswordName.Size = new System.Drawing.Size(180, 13);
            this.txtPasswordName.TabIndex = 5;
            // 
            // txtPasswordValue
            // 
            this.txtPasswordValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtPasswordValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPasswordValue.ForeColor = System.Drawing.Color.White;
            this.txtPasswordValue.Location = new System.Drawing.Point(142, 48);
            this.txtPasswordValue.Name = "txtPasswordValue";
            this.txtPasswordValue.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtPasswordValue.Size = new System.Drawing.Size(190, 13);
            this.txtPasswordValue.TabIndex = 6;
            this.txtPasswordValue.UseSystemPasswordChar = true;
            // 
            // btnAddPassword
            // 
            this.btnAddPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnAddPassword.FlatAppearance.BorderSize = 0;
            this.btnAddPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddPassword.ForeColor = System.Drawing.Color.White;
            this.btnAddPassword.Location = new System.Drawing.Point(650, 135);
            this.btnAddPassword.Name = "btnAddPassword";
            this.btnAddPassword.Size = new System.Drawing.Size(142, 36);
            this.btnAddPassword.TabIndex = 7;
            this.btnAddPassword.Text = "ذخیره رمز";
            this.btnAddPassword.UseVisualStyleBackColor = false;
            this.btnAddPassword.Click += new System.EventHandler(this.btnAddPassword_Click);
            // 
            // btnDeletePassword
            // 
            this.btnDeletePassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnDeletePassword.FlatAppearance.BorderSize = 0;
            this.btnDeletePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeletePassword.ForeColor = System.Drawing.Color.White;
            this.btnDeletePassword.Location = new System.Drawing.Point(344, 135);
            this.btnDeletePassword.Name = "btnDeletePassword";
            this.btnDeletePassword.Size = new System.Drawing.Size(142, 36);
            this.btnDeletePassword.TabIndex = 8;
            this.btnDeletePassword.Text = "حذف رمز";
            this.btnDeletePassword.UseVisualStyleBackColor = false;
            this.btnDeletePassword.Click += new System.EventHandler(this.btnDeletePassword_Click);
            // 
            // btnShowPasswords
            // 
            this.btnShowPasswords.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnShowPasswords.FlatAppearance.BorderSize = 0;
            this.btnShowPasswords.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowPasswords.ForeColor = System.Drawing.Color.White;
            this.btnShowPasswords.Location = new System.Drawing.Point(492, 135);
            this.btnShowPasswords.Name = "btnShowPasswords";
            this.btnShowPasswords.Size = new System.Drawing.Size(152, 36);
            this.btnShowPasswords.TabIndex = 9;
            this.btnShowPasswords.Text = "نمایش رمزهای فرد";
            this.btnShowPasswords.UseVisualStyleBackColor = false;
            this.btnShowPasswords.Click += new System.EventHandler(this.btnShowPasswords_Click);
            // 
            // btnDeleteAllPasswords
            // 
            this.btnDeleteAllPasswords.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnDeleteAllPasswords.FlatAppearance.BorderSize = 0;
            this.btnDeleteAllPasswords.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteAllPasswords.ForeColor = System.Drawing.Color.White;
            this.btnDeleteAllPasswords.Location = new System.Drawing.Point(196, 135);
            this.btnDeleteAllPasswords.Name = "btnDeleteAllPasswords";
            this.btnDeleteAllPasswords.Size = new System.Drawing.Size(142, 36);
            this.btnDeleteAllPasswords.TabIndex = 10;
            this.btnDeleteAllPasswords.Text = "حذف همه رمزها";
            this.btnDeleteAllPasswords.UseVisualStyleBackColor = false;
            this.btnDeleteAllPasswords.Click += new System.EventHandler(this.btnDeleteAllPasswords_Click);
            // 
            // ColumnUserId
            // 
            this.ColumnUserId.DataPropertyName = "UserId";
            this.ColumnUserId.FillWeight = 40F;
            this.ColumnUserId.HeaderText = "ID";
            this.ColumnUserId.MinimumWidth = 3;
            this.ColumnUserId.Name = "ColumnUserId";
            this.ColumnUserId.ReadOnly = true;
            this.ColumnUserId.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // ColumnUserName
            // 
            this.ColumnUserName.DataPropertyName = "UserName";
            this.ColumnUserName.HeaderText = "نام کاربری";
            this.ColumnUserName.Name = "ColumnUserName";
            this.ColumnUserName.ReadOnly = true;
            // 
            // ColumnPass
            // 
            this.ColumnPass.DataPropertyName = "Pass";
            this.ColumnPass.FillWeight = 110F;
            this.ColumnPass.HeaderText = "رمز";
            this.ColumnPass.Name = "ColumnPass";
            this.ColumnPass.ReadOnly = true;
            this.ColumnPass.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ColumnExpireDate
            // 
            this.ColumnExpireDate.DataPropertyName = "ExpireDate";
            this.ColumnExpireDate.FillWeight = 89.65736F;
            this.ColumnExpireDate.HeaderText = "تاریخ انقضا";
            this.ColumnExpireDate.Name = "ColumnExpireDate";
            this.ColumnExpireDate.ReadOnly = true;
            // 
            // ColumnExpireTime
            // 
            this.ColumnExpireTime.DataPropertyName = "ExpireTime";
            this.ColumnExpireTime.FillWeight = 89.65736F;
            this.ColumnExpireTime.HeaderText = "زمان انقضا";
            this.ColumnExpireTime.Name = "ColumnExpireTime";
            this.ColumnExpireTime.ReadOnly = true;
            // 
            // ColumnFlashPermission
            // 
            this.ColumnFlashPermission.DataPropertyName = "FlashPermission";
            this.ColumnFlashPermission.FillWeight = 89.65736F;
            this.ColumnFlashPermission.HeaderText = "مجوز فلش";
            this.ColumnFlashPermission.Name = "ColumnFlashPermission";
            this.ColumnFlashPermission.ReadOnly = true;
            // 
            // ColumnUsbUseCount
            // 
            this.ColumnUsbUseCount.DataPropertyName = "UsbUseCount";
            this.ColumnUsbUseCount.FillWeight = 75F;
            this.ColumnUsbUseCount.HeaderText = "تعداد مجاز USB";
            this.ColumnUsbUseCount.Name = "ColumnUsbUseCount";
            this.ColumnUsbUseCount.ReadOnly = true;
            // 
            // ColumnUsbUsedTimes
            // 
            this.ColumnUsbUsedTimes.DataPropertyName = "UsbUsedTimes";
            this.ColumnUsbUsedTimes.FillWeight = 75F;
            this.ColumnUsbUsedTimes.HeaderText = "دفعات استفاده USB";
            this.ColumnUsbUsedTimes.Name = "ColumnUsbUsedTimes";
            this.ColumnUsbUsedTimes.ReadOnly = true;
            // 
            // ColumnEdit
            // 
            this.ColumnEdit.FillWeight = 89.65736F;
            this.ColumnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ColumnEdit.HeaderText = "ویرایش";
            this.ColumnEdit.Name = "ColumnEdit";
            this.ColumnEdit.ReadOnly = true;
            this.ColumnEdit.Text = "✎";
            this.ColumnEdit.UseColumnTextForButtonValue = true;
            // 
            // ColumnDelete
            // 
            this.ColumnDelete.FillWeight = 89.65736F;
            this.ColumnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ColumnDelete.HeaderText = "حذف";
            this.ColumnDelete.Name = "ColumnDelete";
            this.ColumnDelete.ReadOnly = true;
            this.ColumnDelete.Text = "حذف";
            this.ColumnDelete.UseColumnTextForButtonValue = true;
            // 
            // ColumnFingerprint
            // 
            this.ColumnFingerprint.FillWeight = 89.65736F;
            this.ColumnFingerprint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ColumnFingerprint.HeaderText = "انگشت";
            this.ColumnFingerprint.Name = "ColumnFingerprint";
            this.ColumnFingerprint.ReadOnly = true;
            this.ColumnFingerprint.Text = "ثبت";
            this.ColumnFingerprint.UseColumnTextForButtonValue = true;
            // 
            // ColumnFingerprintStatus
            // 
            this.ColumnFingerprintStatus.DataPropertyName = "FingerprintStatus";
            this.ColumnFingerprintStatus.FillWeight = 89.65736F;
            this.ColumnFingerprintStatus.HeaderText = "وضعیت";
            this.ColumnFingerprintStatus.Name = "ColumnFingerprintStatus";
            this.ColumnFingerprintStatus.ReadOnly = true;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.ClientSize = new System.Drawing.Size(946, 623);
            this.Controls.Add(this.pnlMain);
            this.Font = new System.Drawing.Font("Shabnam FD", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SafeDoc | مدیریت دستگاه";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.connection.ResumeLayout(false);
            this.connection.PerformLayout();
            this.operationStatusStrip.ResumeLayout(false);
            this.operationStatusStrip.PerformLayout();
            this.groupFingerprint.ResumeLayout(false);
            this.groupFingerprint.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudUsbUseCount)).EndInit();
            this.groupSettings.ResumeLayout(false);
            this.groupSettings.PerformLayout();
            this.groupUsers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            this.tabFeatures.ResumeLayout(false);
            this.tabPassword.ResumeLayout(false);
            this.tabPassword.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.GroupBox connection;
        private System.Windows.Forms.Label fpId;
        private System.Windows.Forms.Label fpPasswordName;
        private System.Windows.Forms.Label fpPassword;
        private System.Windows.Forms.Label passId;
        private System.Windows.Forms.Label passName;
        private System.Windows.Forms.Label passValue;
        private System.Windows.Forms.Label passHelp;
        private System.Windows.Forms.Label date;
        private System.Windows.Forms.Label currentTimeTitle;
        private System.Windows.Forms.Label expId;
        private System.Windows.Forms.Label expTime;
        private System.Windows.Forms.Label usb;
        private System.Windows.Forms.CheckBox chkFlashPermission;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnUserId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnUserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPass;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnExpireDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnExpireTime;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnFlashPermission;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnUsbUseCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnUsbUsedTimes;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnEdit;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnDelete;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnFingerprint;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFingerprintStatus;
        private System.Windows.Forms.CheckBox chkClearSavedAccessKey;
    }
}
