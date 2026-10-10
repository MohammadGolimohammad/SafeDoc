using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using SafeDoc.Business;
using SafeDoc.Models;

namespace SafeDoc.UI
{
    public partial class FormMain : Form
    {
        private DeviceConnectionService _connection;
        private SafeDocOperations _operations;
        private string _lastRegisteredFingerprintId;
        private string _connectedApprovedPort;
        private bool _approvedDeviceWasPresent;
        private bool _sendEnterAfterPassword;
        private bool _keepHidAfterExpiration;
        private bool _enterStatusIsKnown;
        private bool _hidStatusIsKnown;
        private bool _deviceIsConnected;
        private bool _operationInProgress;
        private bool _isConnecting;
        private bool _isEditMode;
        private bool _isNewUserEntryMode;
        private int _newUserId;
        private BindingList<SafeDocUserRow> _deviceUsers;
        private SafeDocUserRow _selectedUser;
        private SafeDocUserRow _visiblePasswordUser;
        private SafeDocDeviceSettings _lastDeviceSettings;
        private DateTime? _deviceClockAtRead;
        private DateTime _deviceClockLocalReadAt;
        private bool _isFilteringFingerprintPassword;

        private const int ApprovedDeviceBaudRate = 115200;
        private const int DeviceReadyDelayMilliseconds = 1000;
        private const int DefaultUsbUseCount = 1;

        public FormMain()
        {
            InitializeComponent();
        }

        // اتصال دستگاه
        private void FormMain_Load(object sender, EventArgs e)
        {
            UpdateToggleButtons();
            InitializeUsersGrid();
            ClearUserInputs();
            SetConnected(false);
            deviceDateSelector.Value = DateTime.Now;
            txtDeviceTime.Text = DateTime.Now.ToString("HH:mm");
            txtDeviceTime.Validating += OnTimeControlValidating;
        }

        private void UpdateToggleButtons()
        {
            UpdateToggleButton(
                btnEnterStatusToggle,
                _sendEnterAfterPassword,
                _enterStatusIsKnown,
                "ارسال Enter پس از رمز"
            );
            UpdateToggleButton(
                btnHidAfterExpirationToggle,
                _keepHidAfterExpiration,
                _hidStatusIsKnown,
                "فعال‌بودن HID پس از انقضا"
            );
        }
        private static void UpdateToggleButton(Button button, bool enabled, bool statusIsKnown, string operationName)
        {
            if (statusIsKnown == false)
            {
                button.Text = operationName + " (وضعیت نامشخص)";
                button.BackColor = Color.FromArgb(71, 85, 105);
                return;
            }
            string state = enabled ? "فعال" : "غیرفعال";
            button.Text = operationName + " (" + state + ")";
            button.BackColor = enabled ? Color.FromArgb(22, 101, 52) : Color.FromArgb(153, 27, 27);
        }
        // 1
        private void FormMain_Shown(object sender, EventArgs e)
        {
            ConnectApprovedDevice();
        }

        private void connectionWatchTimer_Tick(object sender, EventArgs e)
        {
            if (_isConnecting)
            {
                return;
            }

            string approvedPort = DevicePortFinder.FindApprovedDevicePort();
            if (string.IsNullOrEmpty(approvedPort))
            {
                if (_connection != null || _approvedDeviceWasPresent)
                {
                    CloseConnection();
                    SetConnected(false);
                    Status("کابل دستگاه جدا شد یا دستگاه دیگر در دسترس نیست.", false);
                }

                _approvedDeviceWasPresent = false;
                _connectedApprovedPort = string.Empty;
                return;
            }

            _approvedDeviceWasPresent = true;
            if (_connection == null || _connection.IsConnected == false)
            {
                ConnectApprovedDevice(approvedPort);
                return;
            }

            if (string.Equals(_connectedApprovedPort, approvedPort, StringComparison.OrdinalIgnoreCase) == false)
            {
                CloseConnection();
                SetConnected(false);
                Status("پورت دستگاه تغییر کرد؛ اتصال دوباره برقرار می‌شود.", false);
                ConnectApprovedDevice(approvedPort);
            }
        }

        private void ConnectApprovedDevice()
        {
            string approvedPort = DevicePortFinder.FindApprovedDevicePort();
            if (string.IsNullOrEmpty(approvedPort))
            {
                CloseConnection();
                SetConnected(false);
                Status("دستگاه مورد نظر پیدا نشد .", false);
                return;
            }

            ConnectApprovedDevice(approvedPort);
        }

        private void ConnectApprovedDevice(string approvedPort)
        {
            if (_isConnecting)
            {
                return;
            }

            _isConnecting = true;
            try
            {
                BeginOperationLoading("در حال برقراری اتصال با دستگاه...");
                CloseConnection();
                DeviceConnectionService newConnection = new DeviceConnectionService(
                    approvedPort,
                    ApprovedDeviceBaudRate
                );
                _connection = newConnection;
                newConnection.Connect();

                Thread.Sleep(DeviceReadyDelayMilliseconds);

                if (ReferenceEquals(_connection, newConnection) == false)
                {
                    Status("اتصال هنگام آماده‌سازی قطع شد.", false);
                    return;
                }

                if (newConnection.IsConnected == false)
                {
                    Status("پورت دستگاه پس از اتصال باز نماند.", false);
                    return;
                }

                _operations = new SafeDocOperations(newConnection);
                _connectedApprovedPort = approvedPort;
                _approvedDeviceWasPresent = true;
                _enterStatusIsKnown = false;
                _hidStatusIsKnown = false;
                UpdateToggleButtons();
                SetConnected(false);
                Status("دستگاه با موفقیت متصل شد.", true);
                Thread.Sleep(DeviceReadyDelayMilliseconds);
                ReadDeviceSettings();
                Thread.Sleep(DeviceReadyDelayMilliseconds);
                ReadUsersFromDevice();
                SetConnected(true);
            }
            catch (Exception ex)
            {
                CloseConnection();
                _connectedApprovedPort = string.Empty;
                Status("اتصال برقرار نشد: " + ex.Message, false);
            }
            finally
            {
                _isConnecting = false;
            }
        }

        // وضعیت فرم
        private void SetConnected(bool ok)
        {
            _deviceIsConnected = ok;
            if (ok == false)
            {
                _enterStatusIsKnown = false;
                _hidStatusIsKnown = false;
                UpdateToggleButtons();
            }

            bool controlsEnabled = ok && _operationInProgress == false;
            groupFingerprint.Enabled = controlsEnabled;
            groupSettings.Enabled = controlsEnabled;
            groupUsers.Enabled = controlsEnabled;
            UpdateUserEditorState();
            lblConnection.Text = ok ? "وضعیت دستگاه تأییدشده: ● متصل" : "وضعیت دستگاه تأییدشده: ● پیدا نشد";
            lblConnection.ForeColor = ok ? System.Drawing.Color.ForestGreen : System.Drawing.Color.Firebrick;
        }
        private void Status(string text, bool ok)
        {
            lblOperationProgress.Text = text;
            lblOperationProgress.ToolTipText = text;
            lblOperationProgress.ForeColor = ok ? System.Drawing.Color.FromArgb(74, 222, 128) : System.Drawing.Color.FromArgb(251, 113, 133);

            if (_operationInProgress == false)
            {
                operationProgressBar.Style = ProgressBarStyle.Continuous;
                operationProgressBar.Value = 100;
            }

            if (ok == false)
            {
                SetOperationProgress(100, text);
                EndOperationLoading();
            }
            operationStatusStrip.Refresh();
        }
        private void BeginOperationLoading(string message)
        {
            _operationInProgress = true;
            groupFingerprint.Enabled = false;
            groupSettings.Enabled = _deviceIsConnected;
            groupUsers.Enabled = false;
            UseWaitCursor = true;
            operationProgressBar.Style = ProgressBarStyle.Marquee;
            lblOperationProgress.Text = message;
            lblOperationProgress.ToolTipText = message;
            lblOperationProgress.ForeColor = System.Drawing.Color.FromArgb(226, 232, 240);
            operationStatusStrip.Refresh();
        }

        private void EndOperationLoading()
        {
            _operationInProgress = false;
            UseWaitCursor = false;
            operationProgressBar.Style = ProgressBarStyle.Continuous;
            groupFingerprint.Enabled = _deviceIsConnected;
            groupSettings.Enabled = _deviceIsConnected;
            groupUsers.Enabled = _deviceIsConnected;
            UpdateUserEditorState();
        }

        private void SetOperationProgress(int value, string message)
        {
            if (value < 0)
            {
                value = 0;
            }

            if (value > 100)
            {
                value = 100;
            }

            operationProgressBar.Value = value;
            lblOperationProgress.Text = message;
            lblOperationProgress.ToolTipText = message;
            operationStatusStrip.Refresh();
        }

        private static string DescribeStatusCode(int code)
        {
            if (code == 0)
            {
                return "موفق";
            }

            if (code == 1)
            {
                return "شناسه کاربر تکراری است";
            }

            if (code == 2)
            {
                return "شناسه کاربر در دستگاه پیدا نشد";
            }

            if (code == 3)
            {
                return "زمان ثبت اثرانگشت تمام شد";
            }

            if (code == 4)
            {
                return "خطای دستگاه";
            }

            if (code == 7)
            {
                return "ظرفیت رمز این اثرانگشت پر است";
            }

            if (code == 8)
            {
                return "نام رمز پیدا نشد";
            }

            if (code == 9)
            {
                return "شناسه واردشده با اثر انگشت دستگاه مطابقت ندارد";
            }

            if (code == 10)
            {
                return "زمان انقضا ثبت نشده است";
            }

            if (code == 11)
            {
                return "رمزی ثبت نشده است";
            }

            return "کد ناشناخته دستگاه: " + code;
        }

        private bool Ready()
        {
            if (_operations != null && _connection.IsConnected)
            {
                BeginOperationLoading("در حال ارسال فرمان به دستگاه و دریافت پاسخ...");
                return true;
            }

            Status("ابتدا از بالای صفحه به دستگاه متصل شوید.", false);
            SetOperationProgress(0, "دستگاه متصل نیست");
            return false;
        }

        private void ShowDeviceResponse(SafeDocResponse response, bool showMessage = true, bool completeOperation = true)
        {
            try
            {
                string deviceResult = response.isSuccess ? "پاسخ دستگاه: عملیات موفق بود." : "پاسخ دستگاه: " + DescribeStatusCode(response.responseStatusCode);
                SetOperationProgress(100,deviceResult);
                Status(deviceResult,response.isSuccess);
                if (completeOperation)
                {
                    EndOperationLoading();
                }
            }
            catch (Exception ex)
            {
                SetOperationProgress(100, "نمایش پاسخ با خطا روبه‌رو شد");
                Status("عملیات انجام نشد: " + ex.Message, false);
                if (completeOperation)
                {
                    EndOperationLoading();
                }
            }
        }

        private bool Confirm(string text)
        {
            if (
                MessageBox.Show(
                    "⚠ توجه: این عملیات قابل بازگشت نیست."
                        + Environment.NewLine
                        + Environment.NewLine
                        + text
                        + Environment.NewLine
                        + Environment.NewLine
                        + "آیا ادامه می‌دهید؟",
                        "تأیید عملیات",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                ) == DialogResult.Yes
            )
            {
                return true;
            }

            return false;
        }

        // کاربران دستگاه
        private void InitializeUsersGrid()
        {
            _deviceUsers = new BindingList<SafeDocUserRow>();
            dgvUsers.DataSource = _deviceUsers;
            _selectedUser = null;
            UpdateSelectedUserLabel();
        }

        private int CountRealDeviceUsers()
        {
            int count = 0;

            foreach (SafeDocUserRow user in _deviceUsers)
            {
                if (user.UserId.HasValue)
                {
                    count++;
                }
            }

            return count;
        }

        // 3
        private bool ReadUsersFromDevice(bool completeOperation = true)
        {
            if (_operations == null || _connection == null || _connection.IsConnected == false)
            {
                Status("اتصال دستگاه برای خواندن فهرست کاربران برقرار نیست.", false);
                return false;
            }

            try
            {
                List<SafeDocDeviceUser> deviceUsers;
                SafeDocResponse response = _operations.GetAllUsers(out deviceUsers);
                if (response == null)
                {
                    Status("دستگاه برای فهرست کاربران پاسخ معتبری برنگرداند.", false);
                    return false;
                }
                else
                {
                    ShowDeviceResponse(response, false, completeOperation);
                    _deviceUsers.Clear();
                    _selectedUser = null;
                    AddDeviceUsersToGrid(deviceUsers);

                    dgvUsers.Refresh();
                    ApplyFingerprintRowColors();
                    SelectFirstUserGridRow();
                    int deviceUserCount = CountRealDeviceUsers();
                    bool hasDeviceData = deviceUserCount >= 0;
                    bool listWasRead = response.isSuccess || hasDeviceData;
                    Status("فهرست کاربران از دستگاه خوانده شد. تعداد کاربران ثبت‌شده: " + deviceUserCount,listWasRead);

                    if (completeOperation == false && listWasRead == false)
                    {
                        EndOperationLoading();
                    }

                    return listWasRead;
                }
            }
            catch (Exception exception)
            {
                Status("خواندن فهرست کاربران از دستگاه انجام نشد: " + exception.Message, false);
                return false;
            }
        }

        private void AddDeviceUsersToGrid(List<SafeDocDeviceUser> deviceUsers)
        {
            foreach (SafeDocDeviceUser deviceUser in deviceUsers)
            {
                if (deviceUser.UserId < 1 || deviceUser.UserId > 10)
                {
                    continue;
                }

                SafeDocUserRow user = new SafeDocUserRow
                {
                    UserId = deviceUser.UserId,
                    UserName = deviceUser.UserName,
                    Pass = deviceUser.Pass,
                    FingerprintStatus = "ثبت شده",
                    FlashPermission = deviceUser.IsUseFlash,
                    UsbUseCount = deviceUser.UseUsbFlashCount,
                    UsbUsedTimes = deviceUser.UsbUsedTimes
                };

                SetUserExpirationFromDevice(user, deviceUser.Expire);
                _deviceUsers.Add(user);
            }
        }

        private static void SetUserExpirationFromDevice(SafeDocUserRow user, long timestamp)
        {
            if (timestamp <= 0)
            {
                user.ExpireDate = "بدون انقضا";
                user.ExpireTime = "—";
                return;
            }

            DateTime expiration = DateTimeOffset.FromUnixTimeSeconds(timestamp).LocalDateTime;
            PersianCalendar calendar = new PersianCalendar();
            user.ExpireDate = string.Format(
                "{0:0000}/{1:00}/{2:00}",
                calendar.GetYear(expiration),
                calendar.GetMonth(expiration),
                calendar.GetDayOfMonth(expiration)
            );
            user.ExpireTime = expiration.ToString("HH:mm");
        }

        private bool TryGetSelectedUser(out SafeDocUserRow selectedUser)
        {
            selectedUser = _selectedUser;
            if (selectedUser == null || selectedUser.UserId.HasValue == false)
            {
                Status("ابتدا یک کاربر را از گرید انتخاب کنید.", false);
                return false;
            }

            return true;
        }

        private void UpdateSelectedUser()
        {
            SafeDocUserRow selectedUser = dgvUsers.CurrentRow == null
                ? null
                : dgvUsers.CurrentRow.DataBoundItem as SafeDocUserRow;

            _selectedUser = selectedUser;
            if (_isEditMode)
            {
                _isEditMode = false;
                ClearUserInputs();
            }

            UpdateSelectedUserLabel();
            UpdateUserEditorState();
        }

        private void BeginEditSelectedUser(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= dgvUsers.Rows.Count)
            {
                return;
            }

            SafeDocUserRow selectedUser = dgvUsers.Rows[rowIndex].DataBoundItem as SafeDocUserRow;
            if (selectedUser == null || selectedUser.UserId.HasValue == false)
            {
                return;
            }

            _selectedUser = selectedUser;
            _isEditMode = true;
            _isNewUserEntryMode = false;
            LoadSelectedUserIntoInputs();
            UpdateSelectedUserLabel();
            UpdateUserEditorState();
        }

        private void StartNewUserRegistration()
        {
            if (Ready() == false)
            {
                return;
            }

            if (ReadUsersFromDevice() == false)
            {
                return;
            }

            int userId = GetNextAvailableUserId();
            if (userId == 0)
            {
                Status("ظرفیت ۱۰ کاربر دستگاه تکمیل شده است.", false);
                return;
            }

            _newUserId = userId;
            _isEditMode = false;
            _isNewUserEntryMode = true;
            ClearUserInputs();
            UpdateUserEditorState();
            Status("شناسه کاربر " + userId + " برای ثبت جدید انتخاب شد.", true);
        }

        private void CancelUserOperation()
        {
            _isEditMode = false;
            _isNewUserEntryMode = false;
            _newUserId = 0;
            ClearUserInputs();
            UpdateUserEditorState();
        }

        private void ClearUserInputs()
        {
            txtFingerprintPasswordName.Clear();
            txtFingerprintPassword.Clear();
            DateTime defaultExpiration = DateTime.Now.AddYears(1);
            expirationDateSelector.Value = defaultExpiration;
            txtExpirationTime.Text = defaultExpiration.ToString("HH:mm");
            chkFlashPermission.Checked = false;
            Status("عملیات لغو شد", true);
        }

        private void UpdateUserEditorState()
        {
            bool canUseEditor = _deviceIsConnected && _operationInProgress == false;
            bool canEditInputs = canUseEditor && (_isEditMode || _isNewUserEntryMode);

            txtFingerprintPasswordName.Enabled = canEditInputs;
            txtFingerprintPassword.Enabled = canEditInputs;
            btnToggleFingerprintPassword.Enabled = canEditInputs;
            expirationDateSelector.Enabled = canEditInputs;
            txtExpirationTime.Enabled = canEditInputs;
            chkFlashPermission.Enabled = canEditInputs;
            btnEnrollFingerprint.Enabled = canUseEditor;
            btnCancelUser.Enabled = canEditInputs;
            btnEnrollFingerprint.Text = _isEditMode
                ? "ذخیره ویرایش"
                : _isNewUserEntryMode ? "ذخیره کاربر جدید" : "ثبت کاربر جدید";
        }

        private void LoadSelectedUserIntoInputs()
        {
            if (_selectedUser == null || _selectedUser.UserId.HasValue == false)
            {
                ClearUserInputs();
                return;
            }

            txtFingerprintPasswordName.Text = _selectedUser.UserName == "—"
                ? string.Empty
                : _selectedUser.UserName;
            txtFingerprintPassword.Text = _selectedUser.Pass == "—"
                ? string.Empty
                : _selectedUser.Pass;
            txtExpirationTime.Text = _selectedUser.ExpireTime == "—"
                ? string.Empty
                : _selectedUser.ExpireTime;
            chkFlashPermission.Checked = _selectedUser.FlashPermission;
            SetExpirationDateInput(_selectedUser.ExpireDate);
        }

        private void SetExpirationDateInput(string expireDate)
        {
            if (string.IsNullOrWhiteSpace(expireDate) || expireDate == "—")
            {
                return;
            }

            string[] parts = NormalizeDigits(expireDate).Replace('-', '/').Split('/');
            int year;
            int month;
            int day;
            if (
                parts.Length != 3
                || int.TryParse(parts[0], out year) == false
                || int.TryParse(parts[1], out month) == false
                || int.TryParse(parts[2], out day) == false
            )
            {
                return;
            }

            try
            {
                PersianCalendar calendar = new PersianCalendar();
                expirationDateSelector.Value = calendar.ToDateTime(year, month, day, 0, 0, 0, 0);
            }
            catch (ArgumentOutOfRangeException)
            {
            }
        }

        private void UpdateSelectedUserLabel()
        {
        }

        private void SelectFirstUserGridRow()
        {
            if (dgvUsers.Rows.Count == 0)
            {
                return;
            }

            dgvUsers.ClearSelection();
            dgvUsers.Rows[0].Selected = true;
            dgvUsers.CurrentCell = dgvUsers.Rows[0].Cells[0];
            UpdateSelectedUser();
        }



        private int GetNextAvailableUserId()
        {
            HashSet<int> usedUserIds = new HashSet<int>();
            foreach (SafeDocUserRow user in _deviceUsers)
            {
                if (user.UserId.HasValue && user.UserId.Value >= 1 && user.UserId.Value <= 10)
                {
                    usedUserIds.Add(user.UserId.Value);
                }
            }

            for (int userId = 1; userId <= 10; userId++)
            {
                if (usedUserIds.Contains(userId) == false)
                {
                    return userId;
                }
            }

            return 0;
        }

        private static HashSet<int> LoadFlashPermissionUserIds()
        {
            HashSet<int> flashPermissionUserIds = new HashSet<int>();
            string savedValue = global::UI.Properties.Settings.Default.FlashPermissionUserIds;
            if (string.IsNullOrWhiteSpace(savedValue))
            {
                return flashPermissionUserIds;
            }

            foreach (string savedUserId in savedValue.Split(','))
            {
                int userId;
                if (int.TryParse(savedUserId, out userId) && userId >= 1 && userId <= 10)
                {
                    flashPermissionUserIds.Add(userId);
                }
            }

            return flashPermissionUserIds;
        }

        private void SaveFlashPermissionUserIds()
        {
            List<string> flashPermissionUserIds = new List<string>();
            foreach (SafeDocUserRow user in _deviceUsers)
            {
                if (user.UserId.HasValue && user.FlashPermission)
                {
                    flashPermissionUserIds.Add(user.UserId.Value.ToString());
                }
            }

            global::UI.Properties.Settings.Default.FlashPermissionUserIds = string.Join(",", flashPermissionUserIds);
            global::UI.Properties.Settings.Default.Save();
        }

        private void SaveSelectedUserPassword()
        {
            if (Ready() == false)
            {
                return;
            }

            SafeDocUserRow selectedUser;
            if (TryGetSelectedUser(out selectedUser) == false)
            {
                return;
            }

            string passwordName = txtFingerprintPasswordName.Text.Trim();
            if (passwordName.Length == 0 || passwordName.Length > 10)
            {
                Status("نام رمز باید بین ۱ تا ۱۰ کاراکتر باشد.", false);
                return;
            }

            string password = txtFingerprintPassword.Text.Trim();
            if (password.Length == 0 || password.Length > 20)
            {
                Status("رمز باید بین ۱ تا ۲۰ کاراکتر باشد.", false);
                return;
            }

            try
            {
                SafeDocResponse response = _operations.AddPassword(
                    selectedUser.UserId.ToString(),
                    passwordName,
                    password
                );
                ShowDeviceResponse(response);
                if (response.isSuccess)
                {
                    selectedUser.UserName = passwordName;
                    selectedUser.Pass = password;
                    selectedUser.FlashPermission = chkFlashPermission.Checked;
                    dgvUsers.Refresh();
                    SaveFlashPermissionUserIds();
                    txtFingerprintPassword.Clear();
                    ReadUsersFromDevice();
                    Status("رمز کاربر " + selectedUser.UserId + " ذخیره شد.", true);
                }
            }
            catch (Exception exception)
            {
                Status("ذخیره رمز کاربر انتخاب‌شده انجام نشد: " + exception.Message, false);
            }
        }

        private void GetSelectedUserPasswords()
        {
            if (Ready() == false)
            {
                return;
            }

            SafeDocUserRow selectedUser;
            if (TryGetSelectedUser(out selectedUser) == false)
            {
                return;
            }

            try
            {
                SafeDocResponse response = _operations.GetPasswordNames(selectedUser.UserId.ToString());
                ShowDeviceResponse(response, false);
                if (response.isSuccess)
                {
                    int passwordCount = response.resultData == null ? 0 : response.resultData.Count;
                    Status("رمزهای کاربر " + selectedUser.UserId + " دریافت شدند. تعداد رمزها: " + passwordCount, true);
                }
            }
            catch (Exception exception)
            {
                Status("گرفتن وضعیت رمزهای کاربر انتخاب‌شده انجام نشد: " + exception.Message, false);
            }
        }

        // ثبت اثرانگشت
        private void SaveUser()
        {
            string passName = txtFingerprintPasswordName.Text.Trim();
            string pass = txtFingerprintPassword.Text.Trim();
            long expirationTimestamp;
            if (passName.Length == 0 || passName.Length > 10)
            {
                Status("نام کاربری باید بین ۱ تا ۱۰ کاراکتر باشد.", false);
                return;
            }

            if (pass.Length == 0 || pass.Length > 20)
            {
                Status("رمز باید بین ۱ تا ۲۰ کاراکتر باشد.", false);
                return;
            }

            if (IsAllowedPassword(pass) == false)
            {
                Status("رمز فقط می‌تواند شامل حروف، عدد و علامت‌های انگلیسی باشد.", false);
                return;
            }

            if (TryCreateExpirationTimestamp(out expirationTimestamp) == false)
            {
                return;
            }

            int userId;
            bool isEditing = _isEditMode && _selectedUser != null && _selectedUser.UserId.HasValue;
            if (isEditing)
            {
                userId = _selectedUser.UserId.Value;
                _selectedUser.UserName = passName;
                _selectedUser.Pass = pass;
                _selectedUser.ExpireDate = GetSelectedExpirationDate();
                _selectedUser.ExpireTime = NormalizeDigits(txtExpirationTime.Text);
                _selectedUser.FlashPermission = chkFlashPermission.Checked;
                _selectedUser.UsbUseCount = GetUsbUseCountForSave(_selectedUser);
            }
            else
            {
                userId = _newUserId;
                if (userId < 1 || userId > 10)
                {
                    Status("ابتدا ثبت کاربر جدید را بزنید تا شناسه از دستگاه گرفته شود.", false);
                    return;
                }

                _deviceUsers.Add(
                    new SafeDocUserRow
                    {
                        UserId = userId,
                        UserName = passName,
                        Pass = pass,
                        ExpireDate = GetSelectedExpirationDate(),
                        ExpireTime = NormalizeDigits(txtExpirationTime.Text),
                        FingerprintStatus = "ثبت نشده",
                        FlashPermission = chkFlashPermission.Checked,
                        UsbUseCount = chkFlashPermission.Checked ? DefaultUsbUseCount : 0
                    }
                );
            }

            SaveFlashPermissionUserIds();
            dgvUsers.Refresh();
            ApplyFingerprintRowColors();
            CancelUserOperation();
            Status(isEditing ? "ویرایش کاربر در گرید ذخیره شد." : "کاربر جدید در گرید ذخیره شد.", true);
        }

        private int GetUsbUseCountForSave(SafeDocUserRow user)
        {
            if (user.FlashPermission == false)
            {
                return 0;
            }

            if (user.UsbUseCount > 0)
            {
                return user.UsbUseCount;
            }

            return DefaultUsbUseCount;
        }

        private string GetSelectedExpirationDate()
        {
            DateTime selectedDate = expirationDateSelector.Value.Value;
            PersianCalendar calendar = new PersianCalendar();
            return string.Format(
                "{0:0000}/{1:00}/{2:00}",
                calendar.GetYear(selectedDate),
                calendar.GetMonth(selectedDate),
                calendar.GetDayOfMonth(selectedDate)
            );
        }

        private void RegisterFingerprint(SafeDocUserRow user)
        {
            if (user == null || user.UserId.HasValue == false)
            {
                return;
            }

            if (Ready() == false)
            {
                return;
            }

            long expirationTimestamp;
            _selectedUser = user;
            LoadSelectedUserIntoInputs();
            if (TryCreateExpirationTimestamp(out expirationTimestamp) == false)
            {
                return;
            }

            try
            {
                if (user.FingerprintStatus == "ثبت شده")
                {
                    SafeDocResponse deletePasswordsResponse = _operations.DeleteAllPasswords(
                        user.UserId.Value.ToString()
                    );
                    ShowDeviceResponse(deletePasswordsResponse, true, false);
                    if (deletePasswordsResponse.isSuccess == false)
                    {
                        EndOperationLoading();
                        return;
                    }
                    SafeDocResponse deleteFingerprintResponse = _operations.DeleteFingerprint(
                        user.UserId.Value.ToString()
                    );
                    ShowDeviceResponse(deleteFingerprintResponse, true, false);
                    if (deleteFingerprintResponse.isSuccess == false)
                    {
                        EndOperationLoading();
                        return;
                    }
                }
                Status("اثر انگشت کاربر " + user.UserId + " را روی دستگاه قرار دهید.", true);
                SafeDocResponse enrollResponse = _operations.EnrollFingerprint(user.UserId.Value.ToString());
                ShowDeviceResponse(enrollResponse, true, false);
                if (enrollResponse.isSuccess == false)
                {
                    EndOperationLoading();
                    return;
                }
                SafeDocResponse savePersonResponse = _operations.SavePerson(
                    user.UserId.Value.ToString(),
                    user.UserName,
                    user.Pass,
                    expirationTimestamp,
                    user.FlashPermission,
                    GetUsbUseCountForSave(user)
                );
                ShowDeviceResponse(savePersonResponse, true, false);
                if (savePersonResponse.isSuccess == false)
                {
                    DeleteIncompleteFingerprint(user.UserId.Value.ToString());
                    EndOperationLoading();
                    return;
                }

                user.FingerprintStatus = "ثبت شده";
                dgvUsers.Refresh();
                ApplyFingerprintRowColors();
                EndOperationLoading();
                Status("اثر انگشت ثبت شد و اطلاعات کامل کاربر با یک درخواست ذخیره شد.", true);
            }
            catch (Exception exception)
            {
                EndOperationLoading();
                Status("ثبت اثر انگشت انجام نشد: " + exception.Message, false);
            }
        }

        // 4
        private static void SaveFlashPermission(int userId, bool isAllowed)
        {
            HashSet<int> userIds = LoadFlashPermissionUserIds();
            if (isAllowed)
            {
                userIds.Add(userId);
            }
            else
            {
                userIds.Remove(userId);
            }

            global::UI.Properties.Settings.Default.FlashPermissionUserIds = string.Join(",", userIds);
            global::UI.Properties.Settings.Default.Save();
        }
        private void DeleteIncompleteFingerprint(string automaticUserId)
        {
            try
            {
                SafeDocResponse deleteResponse = _operations.DeleteFingerprint(automaticUserId);
                Status("به علت خطای رمز، اثرانگشت تازه از دستگاه پاک شد.", false);
            }
            catch (Exception exception)
            {
                Status(
                    "رمز ثبت نشد و پاک‌کردن خودکار اثرانگشت هم ناموفق بود: " + exception.Message,
                    false
                );
            }
        }

        private void DeleteFingerprint()
        {
            SafeDocUserRow selectedUser;
            if (TryGetSelectedUser(out selectedUser) == false)
            {
                return;
            }

            if (Confirm("اثرانگشت کاربر " + selectedUser.UserId + " حذف شود؟") == false)
            {
                return;
            }

            if (selectedUser.FingerprintStatus != "ثبت شده")
            {
                _deviceUsers.Remove(selectedUser);
                CancelUserOperation();
                dgvUsers.Refresh();
                ApplyFingerprintRowColors();
                Status("کاربر محلی از گرید حذف شد.", true);
                return;
            }

            if (Ready() == false)
            {
                return;
            }

            try
            {
                SafeDocResponse deletePasswordsResponse = _operations.DeleteAllPasswords(selectedUser.UserId.ToString());
                ShowDeviceResponse(deletePasswordsResponse);
                if (deletePasswordsResponse.isSuccess == false)
                {
                    return;
                }
                Thread.Sleep(DeviceReadyDelayMilliseconds);
                SafeDocResponse response = _operations.DeleteFingerprint(selectedUser.UserId.ToString());
                ShowDeviceResponse(response);
                if (response.isSuccess)
                {
                    _deviceUsers.Remove(selectedUser);
                    _selectedUser = null;
                    SaveFlashPermissionUserIds();
                    CancelUserOperation();
                    dgvUsers.Refresh();
                    ApplyFingerprintRowColors();
                    Status("اثرانگشت کاربر " + selectedUser.UserId + " حذف شد.", true);
                }
            }
            catch (Exception exception)
            {
                Status("حذف اثرانگشت انجام نشد: " + exception.Message, false);
            }
        }

        private void DeleteAllFingerprints()
        {
            if (Ready() == false)
            {
                return;
            }

            if (Confirm("همه اثرانگشت‌های دستگاه حذف شوند؟") == false)
            {
                return;
            }

            try
            {
                SafeDocResponse response = _operations.DeleteAllFingerprints();
                ShowDeviceResponse(response);
                if (response.isSuccess)
                {
                    _lastRegisteredFingerprintId = string.Empty;
                    //ResetUserGridStatuses();
                    Status("همه اثرانگشت‌ها حذف شدند و ۱۰ کاربر برای ثبت مجدد آماده‌اند.", true);
                }
            }
            catch (Exception exception)
            {
                Status("حذف همه اثرانگشت‌ها انجام نشد: " + exception.Message, false);
            }
        }

        // رمز عبور
        private void AddPassword()
        {
            if (Ready() == false)
            {
                return;
            }

            try
            {
                SafeDocResponse response = _operations.AddPassword(
                    txtPasswordUserId.Text,
                    txtPasswordName.Text,
                    txtPasswordValue.Text
                );
                ShowDeviceResponse(response);
            }
            catch (Exception exception)
            {
                Status("ثبت رمز انجام نشد: " + exception.Message, false);
            }
        }

        // 5
        private void DeletePassword()
        {
            if (Ready() == false)
            {
                return;
            }

            try
            {
                SafeDocResponse response = _operations.DeletePassword(
                    txtPasswordUserId.Text,
                    txtPasswordName.Text
                );
                ShowDeviceResponse(response);
            }
            catch (Exception exception)
            {
                Status("حذف رمز انجام نشد: " + exception.Message, false);
            }
        }

        private void DeleteAllPasswords()
        {
            if (Ready() == false)
            {
                return;
            }

            if (Confirm("همه رمزهای این فرد حذف شوند؟") == false)
            {
                return;
            }

            try
            {
                SafeDocResponse response = _operations.DeleteAllPasswords(txtPasswordUserId.Text);
                ShowDeviceResponse(response);
            }
            catch (Exception exception)
            {
                Status("حذف رمزها انجام نشد: " + exception.Message, false);
            }
        }

        private void ShowPasswords()
        {
            if (Ready() == false)
            {
                return;
            }

            try
            {
                IList<PasswordInfo> data = _operations.GetPasswordList(txtPasswordUserId.Text);
                var grid = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    AutoGenerateColumns = true,
                    ReadOnly = true,
                    DataSource = data,
                };
                var form = new Form
                {
                    Text = "رمزهای ثبت‌شده",
                    StartPosition = FormStartPosition.CenterParent,
                    Size = new System.Drawing.Size(480, 330),
                    RightToLeft = RightToLeft.Yes,
                    RightToLeftLayout = true,
                };
                form.Controls.Add(grid);
                form.ShowDialog(this);
                Status("رمزهای فرد نمایش داده شد.", true);
            }
            catch (Exception ex)
            {
                Status("خواندن رمزها انجام نشد: " + ex.Message, false);
            }
        }

        // زمان و انقضا
        private void SetDeviceDateTime()
        {
            if (Ready() == false)
            {
                return;
            }

            if (deviceDateSelector.Value == null)
            {
                Status("تاریخ جدید را انتخاب کنید.", false);
                ShowValidationMessage("تاریخ جدید را از تقویم انتخاب کنید.");
                return;
            }

            DateTime selectedDate = deviceDateSelector.Value.Value;
            PersianCalendar persianCalendar = new PersianCalendar();
            string deviceDate = string.Format("{0:0000}-{1:00}-{2:00}", persianCalendar.GetYear(selectedDate), persianCalendar.GetMonth(selectedDate), persianCalendar.GetDayOfMonth(selectedDate));
            string deviceTime;
            if (TryGetValidTime(txtDeviceTime.Text, out deviceTime) == false)
            {
                Status("ساعت دستگاه معتبر نیست.", false);
                ShowValidationMessage("ساعت را به شکل ۲۳:۵۹ وارد کنید.");
                return;
            }

            try
            {
                Status(
                    "در حال ارسال تاریخ و ساعت جدید به دستگاه: "
                        + deviceDate
                        + " "
                        + deviceTime,
                    true
                );
                SafeDocResponse response = _operations.SetDeviceDateTime(
                    deviceDate,
                    deviceTime
                );
                ShowDeviceResponse(response);

                if (response.isSuccess)
                {
                    Thread.Sleep(DeviceReadyDelayMilliseconds);
                    ReadCurrentDeviceDateTime();
                }
            }
            catch (Exception exception)
            {
                Status("تنظیم تاریخ و ساعت انجام نشد: " + exception.Message, false);
            }
        }
        // 6
        private void ReadCurrentDeviceDateTime()
        {
            if (Ready() == false)
            {
                return;
            }

            try
            {
                SafeDocDeviceSettings settings;
                SafeDocResponse response = _operations.GetDeviceDateTime(out settings);
                if (response.isSuccess == false)
                {
                    ShowDeviceResponse(response, false);
                    return;
                }

                ShowDeviceResponse(response, false);
                ShowCurrentDeviceDateTime(settings);
            }
            catch (Exception ex)
            {
                Status("خواندن ساعت انجام نشد: " + ex.Message, false);
            }
        }
        private void ShowCurrentDeviceDateTime(SafeDocDeviceSettings settings)
        {
            string deviceDate = settings.Date;
            string deviceTime = settings.Time;

            if (string.IsNullOrEmpty(deviceDate) || string.IsNullOrEmpty(deviceTime))
            {
                lblCurrentDeviceTime.Text = "پاسخ تاریخ و ساعت قابل‌خواندن نبود.";
                return;
            }

            string normalizedDate = NormalizeDigits(deviceDate).Replace('-', '/');
            string[] dateParts = normalizedDate.Split('/');
            int year;
            int month;
            int day;
            if (
                dateParts.Length != 3
                || int.TryParse(dateParts[0], out year) == false
                || int.TryParse(dateParts[1], out month) == false
                || int.TryParse(dateParts[2], out day) == false
            )
            {
                lblCurrentDeviceTime.Text = deviceDate + " " + FormatDeviceTimeWithSeconds(deviceTime);
                return;
            }

            int hour;
            int minute;
            int second;
            if (TryReadDeviceTime(deviceTime, out hour, out minute, out second) == false)
            {
                lblCurrentDeviceTime.Text = deviceDate + " " + FormatDeviceTimeWithSeconds(deviceTime);
                return;
            }

            try
            {
                PersianCalendar persianCalendar = new PersianCalendar();
                _deviceClockAtRead = persianCalendar.ToDateTime(year, month, day, hour, minute, second, 0);
                _deviceClockLocalReadAt = DateTime.Now;
                UpdateCurrentDeviceTimeLabel();
                deviceClockTimer.Start();
            }
            catch (ArgumentOutOfRangeException)
            {
                lblCurrentDeviceTime.Text = deviceDate + " " + FormatDeviceTimeWithSeconds(deviceTime);
                return;
            }

            Status("ساعت فعلی دستگاه خوانده شد و در لیبل نمایش داده شد.", true);
        }

        private static bool TryReadDeviceTime(string deviceTime, out int hour, out int minute, out int second)
        {
            hour = 0;
            minute = 0;
            second = 0;

            string[] timeParts = NormalizeDigits(deviceTime).Split(':');
            if (timeParts.Length < 2 || timeParts.Length > 3)
            {
                return false;
            }

            if (int.TryParse(timeParts[0], out hour) == false || int.TryParse(timeParts[1], out minute) == false)
            {
                return false;
            }

            if (timeParts.Length == 3 && int.TryParse(timeParts[2], out second) == false)
            {
                return false;
            }

            return hour >= 0 && hour <= 23 && minute >= 0 && minute <= 59 && second >= 0 && second <= 59;
        }

        private void UpdateCurrentDeviceTimeLabel()
        {
            if (_deviceClockAtRead.HasValue == false)
            {
                return;
            }

            DateTime deviceTimeNow = _deviceClockAtRead.Value.Add(DateTime.Now - _deviceClockLocalReadAt);
            PersianCalendar persianCalendar = new PersianCalendar();
            lblCurrentDeviceTime.Text = string.Format(
                "{0:0000}/{1:00}/{2:00} {3:00}:{4:00}:{5:00}",
                persianCalendar.GetYear(deviceTimeNow),
                persianCalendar.GetMonth(deviceTimeNow),
                persianCalendar.GetDayOfMonth(deviceTimeNow),
                deviceTimeNow.Hour,
                deviceTimeNow.Minute,
                deviceTimeNow.Second
            );
        }

        private static string FormatDeviceTimeWithSeconds(string deviceTime)
        {
            string normalizedTime = NormalizeDigits(deviceTime);
            string[] timeParts = normalizedTime.Split(':');
            if (timeParts.Length == 2)
            {
                return normalizedTime + ":00";
            }

            return normalizedTime;
        }

        private bool TryCreateExpirationTimestamp(out long timestamp)
        {
            timestamp = 0;
            string timeText = NormalizeDigits(txtExpirationTime.Text);

            if (expirationDateSelector.Value == null)
            {
                Status("تاریخ انقضا و ساعت انقضا را کامل انتخاب کنید.", false);
                ShowValidationMessage("تاریخ انقضا را از تقویم انتخاب کنید.");
                return false;
            }

            string validTime;
            if (TryGetValidTime(timeText, out validTime) == false)
            {
                Status("ساعت انقضا را به شکل ۲۳:۵۹ وارد کنید.", false);
                ShowValidationMessage("ساعت انقضا را به شکل ۲۳:۵۹ وارد کنید.");
                return false;
            }

            string[] timeParts = validTime.Split(':');
            int hour = int.Parse(timeParts[0]);
            int minute = int.Parse(timeParts[1]);

            try
            {
                DateTime selectedDate = expirationDateSelector.Value.Value;
                DateTime localExpiration = new DateTime(
                    selectedDate.Year,
                    selectedDate.Month,
                    selectedDate.Day,
                    hour,
                    minute,
                    0
                );
                DateTimeOffset expirationWithOffset = new DateTimeOffset(
                    localExpiration,
                    TimeZoneInfo.Local.GetUtcOffset(localExpiration)
                );
                timestamp = expirationWithOffset.ToUnixTimeSeconds();
                Status("تاریخ و ساعت انقضا بررسی شد.", true);
                return true;
            }
            catch (ArgumentOutOfRangeException)
            {
                Status("تاریخ یا ساعت معتبر نیست.", false);
                return false;
            }
        }
        private static bool TryGetValidTime(string text, out string validTime)
        {
            validTime = NormalizeDigits(text).Trim();
            DateTime parsedTime;
            if (
                DateTime.TryParseExact(
                    validTime,
                    "HH:mm",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out parsedTime
                ) == false
            )
            {
                return false;
            }

            validTime = parsedTime.ToString("HH:mm");
            return true;
        }

        private void ValidateTimeControl(object sender, CancelEventArgs e)
        {
            MaskedTextBox timeControl = sender as MaskedTextBox;
            string validTime;
            if (timeControl == null || TryGetValidTime(timeControl.Text, out validTime))
            {
                return;
            }

            e.Cancel = true;
            ShowValidationMessage("ساعت را به شکل ۲۳:۵۹ وارد کنید.");
        }
        private void ShowValidationMessage(string instruction)
        {
            SetOperationProgress(100, "مقدار واردشده معتبر نیست: " + instruction);
        }
        private static string NormalizeDigits(string value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            return value
                .Replace('۰', '0')
                .Replace('۱', '1')
                .Replace('۲', '2')
                .Replace('۳', '3')
                .Replace('۴', '4')
                .Replace('۵', '5')
                .Replace('۶', '6')
                .Replace('۷', '7')
                .Replace('۸', '8')
                .Replace('۹', '9')
                .Replace('٠', '0')
                .Replace('١', '1')
                .Replace('٢', '2')
                .Replace('٣', '3')
                .Replace('٤', '4')
                .Replace('٥', '5')
                .Replace('٦', '6')
                .Replace('٧', '7')
                .Replace('٨', '8')
                .Replace('٩', '9');
        }

        // تنظیمات دستگاه
        private void SaveEnterStatus()
        {
            if (Ready() == false)
            {
                return;
            }

            try
            {
                SafeDocResponse response = _operations.SetEnterStatus(_sendEnterAfterPassword);
                ShowDeviceResponse(response);
                _enterStatusIsKnown = response.isSuccess;
                UpdateToggleButtons();
            }
            catch (Exception exception)
            {
                Status("ذخیره وضعیت Enter انجام نشد: " + exception.Message, false);
            }
        }

        // 7
        private void ToggleEnterStatus()
        {
            _sendEnterAfterPassword = _sendEnterAfterPassword == false;
            UpdateToggleButtons();
            SaveEnterStatus();
        }

        private void SetUsbTimeout()
        {
            if (Ready() == false)
            {
                return;
            }

            int timeout;
            if (int.TryParse(txtUsbTimeout.Text, out timeout) == false)
            {
                Status("مهلت دسترسی USB به حافظه داخلی را فقط به صورت عدد وارد کنید.", false);
                return;
            }

            try
            {
                SafeDocResponse response = _operations.SetUsbTimeout(timeout);
                ShowDeviceResponse(response);
            }
            catch (Exception exception)
            {
                Status("ذخیره مهلت دسترسی USB به حافظه داخلی انجام نشد: " + exception.Message, false);
            }
        }

        private void SaveHidStatus()
        {
            if (Ready() == false)
            {
                return;
            }

            try
            {
                SafeDocResponse response = _operations.SetHidAfterExpiration(
                    _keepHidAfterExpiration
                );
                ShowDeviceResponse(response);
                _hidStatusIsKnown = response.isSuccess;
                UpdateToggleButtons();
            }
            catch (Exception exception)
            {
                Status("ذخیره وضعیت HID انجام نشد: " + exception.Message, false);
            }
        }

        private void ToggleHidAfterExpiration()
        {
            _keepHidAfterExpiration = _keepHidAfterExpiration == false;
            UpdateToggleButtons();
            SaveHidStatus();
        }

        private void ReadDeviceSettings()
        {
            try
            {
                SafeDocResponse response = ReadSettingsFromDevice();
                SafeDocDeviceSettings settings = _lastDeviceSettings;
                ShowDeviceResponse(response, false, false);
                if (response == null || response.isSuccess == false)
                {
                    return;
                }

                _sendEnterAfterPassword = settings.EnterStatus;
                _enterStatusIsKnown = true;
                _keepHidAfterExpiration = settings.HidStatusAfterExpiration;
                _hidStatusIsKnown = true;
                txtUsbTimeout.Text = settings.UsbConnectionTimeoutValue.ToString();
                ShowCurrentDeviceDateTime(settings);
                UpdateToggleButtons();
                Status("تنظیمات دستگاه خوانده شد.", true);
            }
            catch (Exception exception)
            {
                Status("خواندن تنظیمات دستگاه انجام نشد: " + exception.Message, false);
            }
        }

        private SafeDocResponse ReadSettingsFromDevice()
        {
            return _operations.GetAllSettings(out _lastDeviceSettings);
        }

        private void CloseConnection()
        {
            if (_connection == null)
            {
                return;
            }

            _connection.Dispose();
            _connection = null;
            _operations = null;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            CloseConnection();
            base.OnFormClosed(e);
        }

        // رویدادهای کنترل‌ها

        private void btnEnrollFingerprint_Click(object sender, EventArgs e)
        {
            if (_isEditMode || _isNewUserEntryMode)
            {
                SaveUser();
                return;
            }

            StartNewUserRegistration();
        }

        // 8
        private void btnDeleteFingerprint_Click(object sender, EventArgs e)
        {
            DeleteFingerprint();
        }

        private void btnCancelUser_Click(object sender, EventArgs e)
        {
            CancelUserOperation();
        }

        private void btnDeleteAllFingerprints_Click(object sender, EventArgs e)
        {
            DeleteAllFingerprints();
        }

        private void btnAddPassword_Click(object sender, EventArgs e)
        {
            AddPassword();
        }

        private void btnDeletePassword_Click(object sender, EventArgs e)
        {
            DeletePassword();
        }

        private void btnDeleteAllPasswords_Click(object sender, EventArgs e)
        {
            DeleteAllPasswords();
        }

        private void btnShowPasswords_Click(object sender, EventArgs e)
        {
            ShowPasswords();
        }

        private void btnSetDateTime_Click(object sender, EventArgs e)
        {
            SetDeviceDateTime();
        }

        private void btnEnterStatusToggle_Click(object sender, EventArgs e)
        {
            ToggleEnterStatus();
        }

        private void btnSetUsb_Click(object sender, EventArgs e)
        {
            SetUsbTimeout();
        }

        private void btnHidAfterExpirationToggle_Click(object sender, EventArgs e)
        {
            ToggleHidAfterExpiration();
        }

        private void deviceClockTimer_Tick(object sender, EventArgs e)
        {
            UpdateCurrentDeviceTimeLabel();
        }

        private void btnToggleFingerprintPassword_Click(object sender, EventArgs e)
        {
            if (txtFingerprintPassword.UseSystemPasswordChar)
            {
                txtFingerprintPassword.UseSystemPasswordChar = false;
                btnToggleFingerprintPassword.BackgroundImage = global::SafeDoc.UI.Properties.Resources.hidePass;
                return;
            }

            txtFingerprintPassword.UseSystemPasswordChar = true;
            btnToggleFingerprintPassword.BackgroundImage = global::SafeDoc.UI.Properties.Resources.showPass;
        }

        private void txtFingerprintPassword_TextChanged(object sender, EventArgs e)
        {
            FilterFingerprintPassword();
            UpdatePasswordStrength();
        }

        private void txtFingerprintPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || IsAllowedPasswordCharacter(e.KeyChar))
            {
                return;
            }

            e.Handled = true;
        }

        private void FilterFingerprintPassword()
        {
            if (_isFilteringFingerprintPassword)
            {
                return;
            }

            string password = txtFingerprintPassword.Text;
            string validPassword = GetAllowedPassword(password);
            if (password == validPassword)
            {
                return;
            }

            int cursorPosition = txtFingerprintPassword.SelectionStart;
            _isFilteringFingerprintPassword = true;
            txtFingerprintPassword.Text = validPassword;
            txtFingerprintPassword.SelectionStart = Math.Min(cursorPosition, validPassword.Length);
            _isFilteringFingerprintPassword = false;
        }

        private static string GetAllowedPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return string.Empty;
            }

            var validCharacters = new System.Text.StringBuilder();
            foreach (char character in password)
            {
                if (IsAllowedPasswordCharacter(character))
                {
                    validCharacters.Append(character);
                }

                if (validCharacters.Length == 20)
                {
                    break;
                }
            }

            return validCharacters.ToString();
        }

        private static bool IsAllowedPassword(string password)
        {
            return password == GetAllowedPassword(password);
        }

        private static bool IsAllowedPasswordCharacter(char character)
        {
            return character >= '!' && character <= '~';
        }

        private void UpdatePasswordStrength()
        {
            string password = txtFingerprintPassword.Text;
            int score = GetPasswordStrengthScore(password);

            pnlPasswordStrengthWeak.BackColor = Color.FromArgb(71, 85, 105);
            pnlPasswordStrengthMedium.BackColor = Color.FromArgb(71, 85, 105);
            pnlPasswordStrengthStrong.BackColor = Color.FromArgb(71, 85, 105);

            if (score == 0)
            {
                lblPasswordStrength.Text = "قدرت رمز: —";
                return;
            }

            pnlPasswordStrengthWeak.BackColor = Color.FromArgb(220, 38, 38);
            if (score <= 2)
            {
                lblPasswordStrength.Text = "قدرت رمز: ضعیف";
                return;
            }

            pnlPasswordStrengthMedium.BackColor = Color.FromArgb(234, 179, 8);
            if (score <= 3)
            {
                lblPasswordStrength.Text = "قدرت رمز: متوسط";
                return;
            }

            pnlPasswordStrengthStrong.BackColor = Color.FromArgb(22, 163, 74);
            lblPasswordStrength.Text = "قدرت رمز: قوی";
        }

        private static int GetPasswordStrengthScore(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return 0;
            }

            int score = password.Length >= 8 ? 1 : 0;
            bool hasLowerCase = false;
            bool hasUpperCase = false;
            bool hasNumber = false;
            bool hasSymbol = false;

            foreach (char character in password)
            {
                if (char.IsLower(character))
                {
                    hasLowerCase = true;
                }
                else if (char.IsUpper(character))
                {
                    hasUpperCase = true;
                }
                else if (char.IsDigit(character))
                {
                    hasNumber = true;
                }
                else
                {
                    hasSymbol = true;
                }
            }

            if (hasLowerCase)
            {
                score++;
            }

            if (hasUpperCase)
            {
                score++;
            }

            if (hasNumber)
            {
                score++;
            }

            if (hasSymbol)
            {
                score++;
            }

            return score;
        }

        private void dgvUsers_SelectionChanged(object sender, EventArgs e)
        {
            UpdateSelectedUser();
        }

        private void dgvUsers_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ApplyFingerprintRowColors();
            SelectFirstUserGridRow();
        }

        private void ApplyFingerprintRowColors()
        {
            foreach (DataGridViewRow row in dgvUsers.Rows)
            {
                SafeDocUserRow user = row.DataBoundItem as SafeDocUserRow;
                if (user == null)
                {
                    continue;
                }

                if (user.FingerprintStatus == "ثبت شده")
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(20, 83, 45);
                    row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(22, 101, 52);
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(120, 53, 15);
                    row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(146, 64, 14);
                }

                row.DefaultCellStyle.ForeColor = Color.White;
                row.DefaultCellStyle.SelectionForeColor = Color.White;
            }
        }

        private void dgvUsers_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvUsers.IsCurrentCellDirty)
            {
                dgvUsers.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvUsers_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvUsers.Columns[e.ColumnIndex].Name != "ColumnFlashPermission")
            {
                return;
            }

            SaveFlashPermissionUserIds();

        }

        private void dgvUsers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            SafeDocUserRow user = dgvUsers.Rows[e.RowIndex].DataBoundItem as SafeDocUserRow;
            if (user == null)
            {
                return;
            }

            if (dgvUsers.Columns[e.ColumnIndex].Name == "ColumnPass" && ReferenceEquals(user, _visiblePasswordUser) == false)
            {
                e.Value = string.Empty;
                e.FormattingApplied = true;
            }

        }

        private void dgvUsers_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || dgvUsers.Columns[e.ColumnIndex].Name != "ColumnPass")
            {
                return;
            }

            SafeDocUserRow user = dgvUsers.Rows[e.RowIndex].DataBoundItem as SafeDocUserRow;
            if (user == null || ReferenceEquals(user, _visiblePasswordUser))
            {
                return;
            }

            e.Paint(e.CellBounds, DataGridViewPaintParts.Background | DataGridViewPaintParts.Border);
            Image image = global::SafeDoc.UI.Properties.Resources.showPass;
            int imageSize = Math.Min(18, Math.Min(e.CellBounds.Width - 6, e.CellBounds.Height - 6));
            if (imageSize > 0)
            {
                int imageLeft = e.CellBounds.Left + (e.CellBounds.Width - imageSize) / 2;
                int imageTop = e.CellBounds.Top + (e.CellBounds.Height - imageSize) / 2;
                e.Graphics.DrawImage(image, new Rectangle(imageLeft, imageTop, imageSize, imageSize));
            }

            e.Handled = true;
        }

        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            string columnName = dgvUsers.Columns[e.ColumnIndex].Name;
            if (columnName == "ColumnPass")
            {
                ToggleGridPasswordVisibility(e.RowIndex);
                return;
            }

            if (columnName == "ColumnEdit")
            {
                BeginEditSelectedUser(e.RowIndex);
                return;
            }

            if (columnName == "ColumnDelete")
            {
                BeginEditSelectedUser(e.RowIndex);
                DeleteFingerprint();
                return;
            }

            if (columnName == "ColumnFingerprint")
            {
                SafeDocUserRow selectedUser = dgvUsers.Rows[e.RowIndex].DataBoundItem as SafeDocUserRow;
                RegisterFingerprint(selectedUser);
            }
        }

        private void ToggleGridPasswordVisibility(int rowIndex)
        {
            SafeDocUserRow passwordUser = dgvUsers.Rows[rowIndex].DataBoundItem as SafeDocUserRow;
            if (passwordUser == null)
            {
                return;
            }

            if (ReferenceEquals(_visiblePasswordUser, passwordUser))
            {
                _visiblePasswordUser = null;
            }
            else
            {
                _visiblePasswordUser = passwordUser;
            }

            dgvUsers.Refresh();
        }

        private void btnSaveSelectedUserPassword_Click(object sender, EventArgs e)
        {
            SaveSelectedUserPassword();
        }

        private void btnGetSelectedUserPasswords_Click(object sender, EventArgs e)
        {
            GetSelectedUserPasswords();
        }

        private void OnTimeControlValidating(object sender, CancelEventArgs e)
        {
            ValidateTimeControl(sender, e);
        }

        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }
    }
}
