using System;
using System.Windows.Forms;
using SafeDoc.Business;
using SafeDoc.Models;

namespace SafeDoc.UI
{
    public partial class FormLogin : Form
    {
        private bool _isEnglish;
        private bool _isLoginLoading;
        private string _accessToken;
        private DeviceConnectionService _verifiedConnection;

        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            AppSettings.CurrentLanguage = AppLanguage.Persian;
            txtAccessKey.Text = AccessKeyStorage.Load();
            chkRememberAccessKey.Checked = string.IsNullOrWhiteSpace(txtAccessKey.Text) == false;
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
            int nextValue = loginProgressBar.Value + 5;
            if (nextValue > 85)
            {
                nextValue = 85;
            }

            loginProgressBar.Value = nextValue;
        }

        private void OpenMainForm()
        {
            loginProgressBar.Value = 100;
            loginLoadingTimer.Stop();
            Hide();

            DeviceConnectionService verifiedConnection = _verifiedConnection;
            _verifiedConnection = null;
            using (FormMain mainForm = new FormMain(verifiedConnection))
            {
                mainForm.ShowDialog();
            }

            Close();
        }

        private void StopLoginLoading(string message)
        {
            loginLoadingTimer.Stop();
            _isLoginLoading = false;
            btnLogin.Enabled = true;
            btnLanguage.Enabled = true;
            loginProgressBar.Visible = false;
            lblLoading.Text = message;
            lblLoading.Visible = true;
        }

        private string CheckDeviceToken()
        {
            string approvedPort = DevicePortFinder.FindApprovedDevicePort();
            if (string.IsNullOrWhiteSpace(approvedPort))
            {
                return "دستگاه مورد نظر پیدا نشد.";
            }

            DeviceConnectionService connection = null;
            try
            {
                connection = new DeviceConnectionService(approvedPort, DeviceConfiguration.BaudRate);
                connection.Connect();

                SafeDocOperations operations = new SafeDocOperations(
                    connection,
                    DeviceConfiguration.CommandTimeoutMilliseconds,
                    DeviceConfiguration.SettingsTimeoutMilliseconds,
                    DeviceConfiguration.FingerprintEnrollTimeoutMilliseconds
                );
                SafeDocResponse response = operations.CheckToken(_accessToken);
                if (response.isSuccess)
                {
                    _verifiedConnection = connection;
                    connection = null;
                    return string.Empty;
                }

                if (response.responseStatusCode == 4)
                {
                    return "توکن دستگاه نادرست است.";
                }

                return "بررسی توکن ناموفق بود. " + response.responseStatusCode;
            }
            catch (Exception exception)
            {
                return "بررسی توکن انجام نشد: " + exception.Message;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
            }
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
            //string tokenError = CheckDeviceToken();
            //if (string.IsNullOrWhiteSpace(tokenError) == false)
            //{
            //    StopLoginLoading(tokenError);
            //    return;
            //}

            //if (chkRememberAccessKey.Checked)
            //{
            //    AccessKeyStorage.Save(_accessToken);
            //}
            //else
            //{
            //    AccessKeyStorage.Clear();
            //}

            OpenMainForm();
        }

        private void loginLoadingTimer_Tick(object sender, EventArgs e)
        {
            ContinueLoginLoading();
        }

        #endregion
    }
}
