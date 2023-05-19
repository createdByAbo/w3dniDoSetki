using System;
using System.Collections.Generic;

namespace w3dniDoSetki.Entities;

public partial class Car
{
    public int Id { get; set; }

    public virtual ICollection<Carmodel> Carmodels { get; } = new List<Carmodel>();
}
