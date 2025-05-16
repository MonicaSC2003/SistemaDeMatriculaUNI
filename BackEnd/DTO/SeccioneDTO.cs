namespace BackEnd.DTO
{
    public class SeccioneDTO
    {
            public int SeccionId { get; set; }
            public int? UsuarioId { get; set; }
            public int? CursoId { get; set; }
            public int? HorarioId { get; set; }
            public string Grupo { get; set; }
            public string Periodo { get; set; }
            public string Carrera { get; set; }
            public int? CuposMax { get; set; }
        
    }
}
