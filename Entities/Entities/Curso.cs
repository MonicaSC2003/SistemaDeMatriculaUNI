using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Curso
{
    public int CursoId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public int Creditos { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Seccione> Secciones { get; set; } = new List<Seccione>();
}
