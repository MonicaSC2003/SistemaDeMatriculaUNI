namespace BackEnd.DTO
{
    public class HorarioDTO
    {
            public int HorarioId { get; set; }
            public TimeSpan HoraInicio { get; set; }
            public TimeSpan HoraFin { get; set; }
            public string Dia { get; set; }
        
    }
}
