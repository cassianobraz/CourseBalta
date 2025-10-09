namespace Blog;

public static class Configuration
{
    public static string JwtKey = "ueLfb0F6bG27d+ON7sf30Lcm8I/cuhO8RsMTfInWm+E=";
    public static string ApiKeyName = "api-key";
    public static string ApiKey = "curso_api_cuhO8RsMTfInWm+E=";
    public static SmtpConfiguration Smtp = new();
    public class SmtpConfiguration
    {
        public string Host { get; set; }
        public int Port { get; set; } = 25;
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}