namespace LearnWellUniversity.Infrastructure.Constants
{
    public static class AppSettingValues
    {
        public static bool IsRunningOnConatiner = false;
        
        public static string DefaultConnectionString = string.Empty;

        public static string JwtKey = string.Empty;
        public static string JwtIssuer = "LearnWellUniversity";
        public static string JwtAudience = "LearnWellUniversityAudience";
    }
}
