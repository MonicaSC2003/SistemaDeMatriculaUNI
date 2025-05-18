namespace BackEnd.DTO
{
    public class CambiarContrasenaDTO
    {
        public int UsuarioId { get; set; }
        public string ContrasenaActual { get; set; } = string.Empty;
        public string ContrasenaNueva { get; set; } = string.Empty;
    }
}
