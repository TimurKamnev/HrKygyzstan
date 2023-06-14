using HrKyrgyzstan.Areas.Identity.Data;

namespace HrKyrgyzstan.Interfaces
{
    public static class Constants
    {
        public static class Roles
        {
            public const string Administrator = "Administrator";
            public const string Companion = "Companion";
            public const string User = "Freelancer";
        }

        public static class Policies
        {
            public const string RequireAdmin = "RequireAdmin";
            public const string RequireCompanion = "RequireCompanion";
        }
    }
}
