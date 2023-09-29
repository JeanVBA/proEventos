using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProEventos.Application.Contratos;
using ProEventos.Domain;
using ProEventos.Persistence;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Application
{
    public class EventoService : IEventosService
    {
        private readonly IGeralPersistence _geralPersistence;
        private readonly IEventoPersistence _eventoPersistence;
        public EventoService(IGeralPersistence geralPersistence, IEventoPersistence eventoPersistence)
        {
            _eventoPersistence = eventoPersistence;
            _geralPersistence = geralPersistence;

        }
        public async Task<Evento> AddEventos(Evento model)
        {
            try
            {
                _geralPersistence.Add<Evento>(model);
                if (await _geralPersistence.SaveChangesAsync())
                {
                    return await _eventoPersistence.GetEventoByIdAsync(model.Id, false);
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception("Error this save Evento:" + e.Message);
            }
        }
        public async Task<Evento> UpdateEventos(int id, Evento model)
        {
            try
            {
                var evento = await _eventoPersistence.GetEventoByIdAsync(id, false);
                if (evento == null) return null;
                model.Id = id;
                _geralPersistence.Update(model);
                if (await _geralPersistence.SaveChangesAsync())
                {
                    return await _eventoPersistence.GetEventoByIdAsync(model.Id, false);
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception("Error this update Evento:" + e.Message);

            }
        }

        public async Task<bool> DeleteEventos(int id)
        {
            try
            {
                var evento = await _eventoPersistence.GetEventoByIdAsync(id, false);
                if (evento == null)
                    throw new Exception("Não encontrado");
                _geralPersistence.Delete<Evento>(evento);
                return await _geralPersistence.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception("Error this delete Evento:" + e.Message);

            }
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes)
        {
            try
            {
                var eventos = await _eventoPersistence.GetAllEventosAsync(includePalestrantes);
                if (eventos == null) return null;
                return eventos;
            }
            catch (Exception e)
            {
                throw new Exception("Error getAll Evento:" + e.Message);

            }
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes)
        {
            try
            {
                var eventos = await _eventoPersistence.GetAllEventosByTemaAsync(tema, includePalestrantes);
                if (eventos == null) return null;
                return eventos;
            }
            catch (Exception e)
            {
                throw new Exception("Error getTema Evento:" + e.Message);

            }
        }

        public async Task<Evento> GetEventoByIdAsync(int id, bool includePalestrantes)
        {
            try
            {
                var evento = await _eventoPersistence.GetEventoByIdAsync(id, includePalestrantes);
                if (evento == null) return null;
                return evento;
            }
            catch (Exception e)
            {
                throw new Exception("Error getId Evento:" + e.Message);

            }
        }
    }
}