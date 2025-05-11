namespace BackEnd.DTO
{
    public class UsuarioDTO
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Identificacion { get; set; }
        public string Rol { get; set; }
        public string Carrera { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public int? NumeroVerificacion { get; set; }
        public bool Activo { get; set; }
    }
}
