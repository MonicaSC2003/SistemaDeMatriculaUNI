using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Horario
{
    public int HorarioId { get; set; }

    public TimeOnly HoraInicio { get; set; }

    public TimeOnly HoraFin { get; set; }

    public string Dia { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Seccione> Secciones { get; set; } = new List<Seccione>();
}
