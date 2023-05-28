using System;
using System.Collections.Generic;

namespace w3dniDoSetki.Entities;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public bool IsAdmin { get; set; }

    public bool IsVerified { get; set; }

    public string PasswordHash { get; set; } = null!;

    public virtual ICollection<Car1> Car1s { get; } = new List<Car1>();
}