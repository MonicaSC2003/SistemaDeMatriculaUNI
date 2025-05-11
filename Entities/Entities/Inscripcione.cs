using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Inscripcione
{
    public int InscripcionId { get; set; }

    public int? UsuarioId { get; set; }

    public int? SeccionId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Nota> Nota { get; set; } = new List<Nota>();

    public virtual Seccione? Seccion { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
