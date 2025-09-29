using System;
using System.Collections.Generic;

namespace StockMarketAPI.Models;

public partial class UserHolding
{
    public int HoldingId { get; set; }

    public int? UserId { get; set; }

    public int? StockId { get; set; }

    public int Quantity { get; set; }

    public decimal PurchasePrice { get; set; }

    public DateOnly PurchaseDate { get; set; }

    public virtual Stock? Stock { get; set; }

    public virtual User? User { get; set; }
}
