using System;
using System.Collections.Generic;

namespace Zadanie7.Models;

public partial class Klient
{
    public int OsobaId { get; set; }

    public string Pesel { get; set; } = null!;

    public string NrTel { get; set; } = null!;

    public int AdresId { get; set; }

    public virtual Adre Adres { get; set; } = null!;

    public virtual Osoba Osoba { get; set; } = null!;

    public virtual ICollection<Sprzedaz> Sprzedazs { get; set; } = new List<Sprzedaz>();
}
