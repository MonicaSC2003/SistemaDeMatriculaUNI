using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Seccione
{
    public int SeccionId { get; set; }

    public int? UsuarioId { get; set; }

    public int? CursoId { get; set; }

    public int? HorarioId { get; set; }

    public string Grupo { get; set; } = null!;

    public string Periodo { get; set; } = null!;

    public string? Carrera { get; set; }

    public int? CuposMax { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Curso? Curso { get; set; }

    public virtual ICollection<Evaluacione> Evaluaciones { get; set; } = new List<Evaluacione>();

    public virtual Horario? Horario { get; set; }

    public virtual ICollection<Inscripcione> Inscripciones { get; set; } = new List<Inscripcione>();

    public virtual Usuario? Usuario { get; set; }
}
