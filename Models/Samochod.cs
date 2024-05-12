using System;
using System.Collections.Generic;

namespace Zadanie7.Models;

public partial class Samochod
{
    public string Vin { get; set; } = null!;

    public int RokProdukcji { get; set; }

    public int CenaZakupu { get; set; }

    public DateTime DataZakupu { get; set; }

    public int PojSilnika { get; set; }

    public int Przebieg { get; set; }

    public int ModelSamochoduId { get; set; }

    public virtual ModelSamochodu ModelSamochodu { get; set; } = null!;

    public virtual ICollection<Sprzedaz> Sprzedazs { get; set; } = new List<Sprzedaz>();

    public virtual ICollection<ElementWyposazenium> ElementWyposazenia { get; set; } = new List<ElementWyposazenium>();
}
