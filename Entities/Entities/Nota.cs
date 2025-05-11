using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Nota
{
    public int NotaId { get; set; }

    public int? EvaluacionId { get; set; }

    public int? InscripcionId { get; set; }

    public decimal Total { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Evaluacione? Evaluacion { get; set; }

    public virtual Inscripcione? Inscripcion { get; set; }
}
