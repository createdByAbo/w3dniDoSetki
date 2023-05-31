namespace w3dniDoSetki.JWT;

public class AuthSettings
{
    public string? JwtKey { get; set; }
    public int ExpireDays { get; set; }
    public string? JwtIssuer { get; set; }
}