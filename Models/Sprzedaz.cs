using System;
using System.Collections.Generic;

namespace Zadanie7.Models;

public partial class Sprzedaz
{
    public int Id { get; set; }

    public string SamochodVin { get; set; } = null!;

    public int SprzedawcaId { get; set; }

    public int Cena { get; set; }

    public DateTime Data { get; set; }

    public virtual Samochod SamochodVinNavigation { get; set; } = null!;

    public virtual Sprzedawca Sprzedawca { get; set; } = null!;

    public virtual ICollection<Klient> Klients { get; set; } = new List<Klient>();
}
