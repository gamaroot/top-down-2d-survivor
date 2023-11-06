namespace Game
{
    internal class URL
    {
#if UNITY_ANDROID
        internal static string STORE => "https://play.google.com/store/apps/details?id=com.package.name";
#else
        internal static string STORE => "https://itunes.apple.com/br/app/id123123123";
#endif

#if UNITY_ANDROID
        internal static string PRIVACY => "https://mobile-game-privacy-policy.web.app/";
#else
        internal static string PRIVACY => "https://mobile-game-privacy-policy.web.app/";
#endif
    }
}