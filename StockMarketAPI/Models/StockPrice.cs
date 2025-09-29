using System;
using System.Collections.Generic;

namespace StockMarketAPI.Models;

public partial class StockPrice
{
    public int PriceId { get; set; }

    public int? StockId { get; set; }

    public decimal? Price { get; set; }

    public DateTime? PriceDate { get; set; }

    public virtual Stock? Stock { get; set; }
}
