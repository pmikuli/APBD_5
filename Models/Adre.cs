using System;
using System.Collections.Generic;

namespace Zadanie7.Models;

public partial class Adre
{
    public int Id { get; set; }

    public string Miasto { get; set; } = null!;

    public string KodPoczt { get; set; } = null!;

    public string Ulica { get; set; } = null!;

    public string NumerDomu { get; set; } = null!;

    public virtual ICollection<Klient> Klients { get; set; } = new List<Klient>();
}
