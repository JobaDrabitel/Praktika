using System;
using System.Collections.Generic;

namespace Praktika.Models;

public partial class District
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Area { get; set; } = null!;

    public virtual ICollection<Realty> Realties { get; set; } = new List<Realty>();
}
