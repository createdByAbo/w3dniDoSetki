namespace w3dniDoSetki.Models.DTOs;

public class JwtDataDTO
{
    public JwtDataDTO(string password, string email, int id)
    {
        Password = password;
        Email = email;
        Id = id;
    }

    public string Password { get; set; }
    public string Email { get; set; }
    public int Id { get; set; }
}