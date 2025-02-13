﻿using System;
using System.Collections.Generic;

namespace KursTRPO.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string NameProduct { get; set; } = null!;

    public string TypeProduct { get; set; } = null!;

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public int? SupplierId { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Supplier? Supplier { get; set; }
}
