using System;
using System.Collections.Generic;

namespace KursTRPO.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public DateOnly OrderDate { get; set; }

    public int? ProductId { get; set; }

    public int Quantyti { get; set; }

    public decimal PriceOrder { get; set; }

    public decimal TotalPrice { get; set; }

    public virtual Product? Product { get; set; }
}
