using ProEventos.Domain;

namespace ProEventos.Application.Contratos
{
    public interface IEventosService
    {
        Task<Evento> AddEventos(Evento model);
        Task<Evento> UpdateEventos(int id, Evento model);
        Task<bool> DeleteEventos(int id);

        Task<Evento[]> GetAllEventosAsync(bool includePalestrantes);
        Task<Evento> GetEventoByIdAsync(int id, bool includePalestrantes);
        Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes);
    }
}