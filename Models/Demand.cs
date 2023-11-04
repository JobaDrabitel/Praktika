using System;
using System.Collections.Generic;

namespace Praktika.Models;

public partial class Demand
{
    public int Id { get; set; }

    public string? AddressCity { get; set; }

    public string? AddressStreet { get; set; }

    public string? AddressHouse { get; set; }

    public int? AddressNumber { get; set; }

    public int? MinPrice { get; set; }

    public int? MaxPrice { get; set; }

    public int AgentId { get; set; }

    public int ClientId { get; set; }

    public int? MinArea { get; set; }

    public int? MaxArea { get; set; }

    public int? MinRooms { get; set; }

    public int? MaxRooms { get; set; }

    public int? MinFloor { get; set; }

    public int? MaxFloor { get; set; }

    public virtual Agent Agent { get; set; } = null!;

    public virtual Client Client { get; set; } = null!;

    public virtual ICollection<Deal> Deals { get; set; } = new List<Deal>();
}
