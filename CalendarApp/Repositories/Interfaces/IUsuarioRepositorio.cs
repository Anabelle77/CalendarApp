using CalendarApp.Models;

namespace CalendarApp.Repositories.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Task<List<UsuarioModel>> BuscarTodosUsuarios(); 
        Task<UsuarioModel> BuscarPorId(int id);
        Task<List<UsuarioModel>> BuscarUsuariosPorIds(List<int> ids);
        Task<UsuarioModel> BuscarPorEmail(string email);
        Task<List<UsuarioModel>> BuscarUsuariosPorEmails(List<string> emails);
        Task<UsuarioModel> Adicionar(UsuarioModel usuario);
        Task<UsuarioModel> Atualizar(UsuarioModel usuario, int id);
        Task<bool> Apagar(int id);
    }
}
