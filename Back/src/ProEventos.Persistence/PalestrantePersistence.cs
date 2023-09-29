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
    public class PalestrantePersistence : IPalestrantePersistence
    {
        private readonly ProEventosContext _context;

        public PalestrantePersistence(ProEventosContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        }


        public async Task<Palestrante[]> GetAllPalestranteAsync(bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
           .Include(x => x.RedeSociais);
            if (includeEventos)
                query.Include(x => x.PalestrantesEventos)
                     .ThenInclude(x => x.Evento);
            query = query.OrderBy(x => x.Id);
            return await query.ToArrayAsync();
        }

        public async Task<Palestrante> GetPalestranteByIdAsync(int id, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
            .Include(x => x.RedeSociais);
            if (includeEventos)
                query.Include(x => x.PalestrantesEventos)
                      .ThenInclude(x => x.Evento);
            query = query.OrderBy(x => x.Id)
                         .Where(x => x.Id.Equals(id));
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Palestrante[]> GetAllPalestranteByNomeAsync(string nome, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
          .Include(x => x.RedeSociais);
            if (includeEventos)
                query.Include(x => x.PalestrantesEventos)
                      .ThenInclude(x => x.Evento);
            query = query.OrderBy(x => x.Id)
                         .Where(x => x.Nome.ToLower().Contains(nome.ToLower()));
            return await query.ToArrayAsync();
        }
    }
}