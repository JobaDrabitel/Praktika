using System;
using System.Collections.Generic;

namespace Praktika.Models;

public partial class Supply
{
    public int Id { get; set; }

    public int Price { get; set; }

    public int AgentId { get; set; }

    public int ClientId { get; set; }

    public int RealtyId { get; set; }

    public virtual Agent Agent { get; set; } = null!;

    public virtual Client Client { get; set; } = null!;

    public virtual ICollection<Deal> Deals { get; set; } = new List<Deal>();

    public virtual Realty Realty { get; set; } = null!;
}
