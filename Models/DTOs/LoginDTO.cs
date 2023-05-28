namespace w3dniDoSetki.Models.DTOs;

public class LoginDto
{
    public string Password { get; set; }
    public string Email { get; set; }

    public LoginDto(string email, string password)
    {
        Password = password;
        Email = email;
    }
}