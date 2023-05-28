using w3dniDoSetki.Entities;

public partial class Car
{
    public int Id { get; set; }

    public string? Make { get; set; }

    public virtual ICollection<Carmodel> Carmodels { get; } = new List<Carmodel>();
}