using System;
using System.Windows.Forms;

namespace SafeDoc.UI
{
    public partial class FormLogin : Form
    {
        private bool _isEnglish;
        private bool _isLoginLoading;
        private string _accessToken;

        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            AppSettings.CurrentLanguage = AppLanguage.Persian;
        }

        private void ToggleLanguage()
        {
            _isEnglish = _isEnglish == false;
            if (_isEnglish)
            {
                AppSettings.CurrentLanguage = AppLanguage.English;
                ApplyEnglishText();
            }
            else
            {
                AppSettings.CurrentLanguage = AppLanguage.Persian;
                ApplyPersianText();
            }
        }

        private void StartLoginLoading()
        {
            if (_isLoginLoading)
            {
                return;
            }

            _isLoginLoading = true;
            btnLogin.Enabled = false;
            btnLanguage.Enabled = false;
            lblLoading.Visible = true;
            loginProgressBar.Visible = true;
            loginProgressBar.Value = 0;
            loginLoadingTimer.Start();
        }

        private void ContinueLoginLoading()
        {
            int nextValue = loginProgressBar.Value + 20;
            if (nextValue < 100)
            {
                loginProgressBar.Value = nextValue;
                return;
            }

            loginProgressBar.Value = 100;
            loginLoadingTimer.Stop();
            Hide();

            using (FormMain mainForm = new FormMain(_accessToken))
            {
                mainForm.ShowDialog();
            }

            Close();
        }

        private void ApplyEnglishText()
        {
            Text = "SafeDoc | Sign in";
            btnLanguage.Text = "FA";
            lblSubtitle.Text = "Secure device management";
            lblActiveKey.Text = "Access key:";
            btnLogin.Text = "Open device management";
            lblLoading.Text = "Preparing device management...";
            RightToLeft = RightToLeft.No;
            RightToLeftLayout = false;
        }

        private void ApplyPersianText()
        {
            Text = "SafeDoc | ورود";
            btnLanguage.Text = "EN";
            lblSubtitle.Text = "مدیریت دستگاه امنیتی";
            lblActiveKey.Text = "کلید ورود:";
            btnLogin.Text = "ورود به مدیریت دستگاه";
            lblLoading.Text = "در حال آماده‌سازی مدیریت دستگاه...";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
        }

        #region رویدادهای کنترل‌ها

        private void btnLanguage_Click(object sender, EventArgs e)
        {
            ToggleLanguage();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            _accessToken = txtAccessKey.Text.Trim();
            if (string.IsNullOrWhiteSpace(_accessToken))
            {
                lblLoading.Text = "کلید ورود را وارد کنید.";
                lblLoading.Visible = true;
                return;
            }

            StartLoginLoading();
        }

        private void loginLoadingTimer_Tick(object sender, EventArgs e)
        {
            ContinueLoginLoading();
        }

        #endregion
    }
}
