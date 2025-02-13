using System;
using System.Collections.Generic;

namespace KursTRPO.Models;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string NameSupplier { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string SupplierAdress { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
