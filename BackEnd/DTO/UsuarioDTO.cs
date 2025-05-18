namespace BackEnd.DTO
{
    public class UsuarioDTO
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido1 { get; set; } = string.Empty;
        public string Apellido2 { get; set; } = string.Empty;
        public string Identificacion { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public string Carrera { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string? Contrasena { get; set; }
        public int? NumeroVerificacion { get; set; }
        public bool Activo { get; set; }
    }
}
