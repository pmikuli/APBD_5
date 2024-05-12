using System;
using System.Collections.Generic;

namespace Zadanie7.Models;

public partial class ModelSamochodu
{
    public int Id { get; set; }

    public string Marka { get; set; } = null!;

    public string Model { get; set; } = null!;

    public virtual ICollection<Samochod> Samochods { get; set; } = new List<Samochod>();
}
