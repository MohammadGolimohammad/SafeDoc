using System;
using System.Text;
using Microsoft.Win32;

namespace SafeDoc.UI
{
    internal static class AccessKeyStorage
    {
        private const string RegistryPath = @"Software\SafeDoc";
        private const string ValueName = "AccessKey";
        private const string StorageKey = "www.kara2000.ir";

        internal static void Save(string accessKey)
        {
            if (string.IsNullOrWhiteSpace(accessKey))
            {
                Clear();
                return;
            }

            string textToSave = StorageKey + "|" + accessKey;
            string encodedText = Convert.ToBase64String(Encoding.UTF8.GetBytes(textToSave));

            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(RegistryPath))
            {
                key.SetValue(ValueName, encodedText, RegistryValueKind.String);
            }
        }

        internal static string Load()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryPath))
            {
                string encodedText = key == null ? string.Empty : key.GetValue(ValueName) as string;
                if (string.IsNullOrWhiteSpace(encodedText))
                {
                    return string.Empty;
                }

                try
                {
                    string savedText = Encoding.UTF8.GetString(Convert.FromBase64String(encodedText));
                    string savedKey = StorageKey + "|";
                    if (savedText.StartsWith(savedKey, StringComparison.Ordinal) == false)
                    {
                        return string.Empty;
                    }

                    return savedText.Substring(savedKey.Length);
                }
                catch (FormatException)
                {
                    return string.Empty;
                }
            }
        }

        internal static void Clear()
        {
            Registry.CurrentUser.DeleteSubKeyTree(RegistryPath, false);
        }
    }
}
