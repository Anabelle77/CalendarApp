using CalendarApp.Models;

namespace CalendarApp.Repositories.Interfaces
{
    public interface IEventoRepositorio
    {
        Task<List<EventoModel>> BuscarTodosEventos();
        Task<EventoModel> BuscarPorId(int id);
        Task<EventoModel> Adicionar(EventoModel evento);
        Task<EventoModel> Atualizar(EventoModel evento, int id);
        Task<bool> Apagar(int id);
    }
}
