using System;
using System.Collections.Generic;

namespace StockMarketAPI.Models;

public partial class UserToken
{
    public int TokenId { get; set; }

    public int? UserId { get; set; }

    public string? Token { get; set; }

    public DateTime? Expiry { get; set; }

    public bool? IsRevoked { get; set; }

    public virtual User? User { get; set; }
}
