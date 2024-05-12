using System;
using System.Collections.Generic;

namespace Zadanie7.Models;

public partial class ElementWyposazenium
{
    public int Id { get; set; }

    public string Nazwa { get; set; } = null!;

    public virtual ICollection<Samochod> SamochodVins { get; set; } = new List<Samochod>();
}
