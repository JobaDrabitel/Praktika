using System;
using System.Collections.Generic;

namespace Praktika.Models;

public partial class Client
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string MiddleName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Demand> Demands { get; set; } = new List<Demand>();

    public virtual ICollection<Supply> Supplies { get; set; } = new List<Supply>();
}
