using System;
using System.Collections.Generic;

namespace w3dniDoSetki.Entities;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public bool IsAdmin { get; set; }

    public bool IsVerified { get; set; }
}
