using System;
using System.Collections.Generic;

namespace Praktika.Models;

public partial class Realty
{
    public int Id { get; set; }

    public string AddressCity { get; set; } = null!;

    public string AddressStreet { get; set; } = null!;

    public int? AddressHouse { get; set; }

    public int? AddressNumber { get; set; }

    public int CoordinateLatitude { get; set; }

    public int CoordinateLongitude { get; set; }

    public double? TotalFloor { get; set; }

    public double? TotalArea { get; set; }

    public int? Rooms { get; set; }

    public int? Floor { get; set; }

    public int? DistrictId { get; set; }

    public virtual District? District { get; set; }

    public virtual ICollection<Supply> Supplies { get; set; } = new List<Supply>();
}
