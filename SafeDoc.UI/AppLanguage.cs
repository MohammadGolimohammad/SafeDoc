namespace SafeDoc.UI
{
    public enum AppLanguage
    {
        Persian,
        English
    }

    public static class AppSettings
    {
        public static AppLanguage CurrentLanguage
        {
            get; set;
        } = AppLanguage.Persian;
    }
}
