using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ProEventos.Domain;
using ProEventos.Persistence.Context;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence
{
    public class EventosPersistence : IEventoPersistence
    {
        private readonly ProEventosContext _context;

        public EventosPersistence(ProEventosContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos
           .Include(x => x.Lotes)
           .Include(x => x.RedeSociais);
            if (includePalestrantes)
                query.Include(x => x.PalestranteEventos)
                     .ThenInclude(x => x.Palestrante);
            query = query.OrderBy(x => x.Id);
            return await query.ToArrayAsync();
        }
        public async Task<Evento> GetEventoByIdAsync(int id, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos
            .Include(x => x.Lotes)
            .Include(x => x.RedeSociais);
            if (includePalestrantes)
                query.Include(x => x.PalestranteEventos)
                     .ThenInclude(x => x.Palestrante);
            query = query.OrderBy(x => x.Id)
                         .Where(x => x.Id.Equals(id));
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos
           .Include(x => x.Lotes)
           .Include(x => x.RedeSociais);
            if (includePalestrantes)
                query.Include(x => x.PalestranteEventos)
                     .ThenInclude(x => x.Palestrante);
            query = query.OrderBy(x => x.Id)
                         .Where(x => x.Tema.ToLower().Contains(tema.ToLower()));
            return await query.ToArrayAsync();
        }
    }
}