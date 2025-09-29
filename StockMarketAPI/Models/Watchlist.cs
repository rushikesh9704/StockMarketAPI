using System;
using System.Collections.Generic;

namespace StockMarketAPI.Models;

public partial class Watchlist
{
    public int WatchlistId { get; set; }

    public int? UserId { get; set; }

    public int? StockId { get; set; }

    public DateTime? AddedDate { get; set; }

    public virtual Stock? Stock { get; set; }

    public virtual User? User { get; set; }
}
