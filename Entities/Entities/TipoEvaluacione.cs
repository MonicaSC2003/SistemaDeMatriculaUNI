using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class TipoEvaluacione
{
    public int TipEvaluacionId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Evaluacione> Evaluaciones { get; set; } = new List<Evaluacione>();
}
