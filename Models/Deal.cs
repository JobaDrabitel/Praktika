using System;
using System.Collections.Generic;

namespace Praktika.Models;

public partial class Deal
{
    public int Id { get; set; }

    public int DemandId { get; set; }

    public int SupplyId { get; set; }

    public virtual Demand Demand { get; set; } = null!;

    public virtual Supply Supply { get; set; } = null!;
}
