using System;
using System.Collections.Generic;

namespace StockMarketAPI.Models;

public partial class Stock
{
    public int StockId { get; set; }

    public string TickerSymbol { get; set; } = null!;

    public string? CompanyName { get; set; }

    public string? Sector { get; set; }

    public string? Exchange { get; set; }

    public virtual ICollection<StockPrice> StockPrices { get; set; } = new List<StockPrice>();

    public virtual ICollection<UserHolding> UserHoldings { get; set; } = new List<UserHolding>();

    public virtual ICollection<Watchlist> Watchlists { get; set; } = new List<Watchlist>();
}
