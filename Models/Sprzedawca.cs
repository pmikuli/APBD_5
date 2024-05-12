using System;
using System.Collections.Generic;

namespace Zadanie7.Models;

public partial class Sprzedawca
{
    public int OsobaId { get; set; }

    public int Prowizja { get; set; }

    public virtual Osoba Osoba { get; set; } = null!;

    public virtual ICollection<Sprzedaz> Sprzedazs { get; set; } = new List<Sprzedaz>();
}
