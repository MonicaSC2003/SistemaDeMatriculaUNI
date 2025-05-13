using BackEnd.DTO;
using BackEnd.Servicios.Interfaces;
using DAL.Interfaces;
using Entities.Entities;

namespace BackEnd.Servicios.Implementaciones
{
    public class HorarioService : IHorarioService
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public HorarioService(IUnidadDeTrabajo unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public List<HorarioDTO> GetAllHorarios()
        {
            try
            {
                var horarios = _unidadDeTrabajo.HorarioDAL.Get();
                var horariosDTO = horarios.Select(h => ConvertToDTO(h)).ToList();
                return horariosDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Métodos Privados de Conversión
        private HorarioDTO ConvertToDTO(Horario horario)
        {
            return new HorarioDTO
            {
                HorarioId = horario.HorarioId,
                HoraInicio = horario.HoraInicio,
                HoraFin = horario.HoraFin,
                Dia = horario.Dia
            };
        }

        private Horario ConvertToEntity(HorarioDTO horarioDTO)
        {
            return new Horario
            {
                HorarioId = horarioDTO.HorarioId,
                HoraInicio = horarioDTO.HoraInicio,
                HoraFin = horarioDTO.HoraFin,
                Dia = horarioDTO.Dia
            };
        }
        #endregion
    }
}
