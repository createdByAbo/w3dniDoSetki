using System;
using System.Collections.Generic;

namespace w3dniDoSetki.Entities;

public partial class Carmodel
{
    public int Id { get; set; }

    public int? Brandid { get; set; }

    public string? Model { get; set; }

    public virtual Car? Brand { get; set; }
}
