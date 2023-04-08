namespace UserService.Contracts.V1;

public static class ApiRoutes
{
    private const string Prefix = "api";

    private const string Version = "v1.0";

    private const string Base = $"/{Prefix}/{Version}";
    
    public static class Users
    {
        public const string Login = $"{Base}/Users/Login";

        public const string Register = $"{Base}/Users/Register";

        public const string RefreshToken = $"{Base}/Users/RefreshToken";
    }
}