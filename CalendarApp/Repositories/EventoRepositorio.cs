using CalendarApp.Data;
using CalendarApp.Models;
using CalendarApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CalendarApp.Repositories
{
    public class EventoRepositorio : IEventoRepositorio
    {
        private readonly CalendarDbContext _dbContext;

        public EventoRepositorio(CalendarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<EventoModel>> BuscarTodosEventos()
        {
            return await _dbContext.Eventos
                .Include(e => e.Usuario)
                .ToListAsync();
        }

        public async Task<EventoModel> BuscarPorId(int id)
        {
            return await _dbContext.Eventos
                .Include(e => e.Usuario)                   
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<EventoModel> Adicionar(EventoModel evento)
        {
            await _dbContext.Eventos.AddAsync(evento);
            await _dbContext.SaveChangesAsync();

            return evento;
        }

        public async Task<EventoModel> Atualizar(EventoModel evento, int id)
        {
            EventoModel eventoExistente = await BuscarPorId(id);

            if (eventoExistente == null)
            {
                throw new Exception($"Evento com ID: {id} não foi encontrado no banco de dados.");
            }

            eventoExistente.Nome = evento.Nome;
            eventoExistente.Descricao = evento.Descricao;
            eventoExistente.Date = evento.Date;

            _dbContext.Eventos.Update(eventoExistente);
            await _dbContext.SaveChangesAsync();

            return eventoExistente;
        }

        public async Task<bool> Apagar(int id)
        {
            EventoModel eventoExistente = await BuscarPorId(id);

            if (eventoExistente == null)
            {
                throw new Exception($"Evento com ID: {id} não foi encontrado no banco de dados.");
            }

            _dbContext.Eventos.Remove(eventoExistente);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
