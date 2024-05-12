using System;
using System.Collections.Generic;

namespace Zadanie7.Models;

public partial class Osoba
{
    public int Id { get; set; }

    public string Imie { get; set; } = null!;

    public string Nazwisko { get; set; } = null!;

    public virtual Klient? Klient { get; set; }

    public virtual Sprzedawca? Sprzedawca { get; set; }
}
