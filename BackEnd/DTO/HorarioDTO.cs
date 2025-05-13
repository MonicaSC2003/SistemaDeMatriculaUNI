namespace BackEnd.DTO
{
    public class HorarioDTO
    {
            public int HorarioId { get; set; }
            public TimeOnly HoraInicio { get; set; }
            public TimeOnly HoraFin { get; set; }
            public string Dia { get; set; }
        
    }
}
