using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido1 { get; set; } = null!;

    public string Apellido2 { get; set; } = null!;

    public string Identificacion { get; set; } = null!;

    public string Rol { get; set; } = null!;

    public string Carrera { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public int? NumeroVerificacion { get; set; }

    public bool Activo { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Inscripcione> Inscripciones { get; set; } = new List<Inscripcione>();

    public virtual ICollection<Seccione> Secciones { get; set; } = new List<Seccione>();
}
