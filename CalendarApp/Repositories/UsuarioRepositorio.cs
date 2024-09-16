using Microsoft.EntityFrameworkCore;
using CalendarApp.Data;
using CalendarApp.Models;
using CalendarApp.Repositories.Interfaces;

namespace CalendarApp.Repositories
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly CalendarDbContext _dbContext;
        public UsuarioRepositorio(CalendarDbContext calendarDbContext)
        {
            _dbContext = calendarDbContext;
        } 
        public async Task<UsuarioModel> BuscarPorId(int id)
        {
            return await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.Id == id);  
        }

        public async Task<List<UsuarioModel>> BuscarTodosUsuarios()
        {
            return await _dbContext.Usuarios.ToListAsync();
        }

        public async Task<List<UsuarioModel>> BuscarUsuariosPorIds(List<int> ids)
        {
            return await _dbContext.Usuarios.Where(u => ids.Contains(u.Id)).ToListAsync();
        }

        public async Task<List<UsuarioModel>> BuscarUsuariosPorEmails(List<string> emails)
        {
            return await _dbContext.Usuarios.Where(u => emails.Contains(u.Email)).ToListAsync();
        }

        public async Task<UsuarioModel> BuscarPorEmail(string email)
        {
            return await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<UsuarioModel> Adicionar(UsuarioModel usuario)
        {
            await _dbContext.Usuarios.AddAsync(usuario);
            await _dbContext.SaveChangesAsync();

            return usuario;
        }

        public async Task<UsuarioModel> Atualizar(UsuarioModel usuario, int id)
        {
            UsuarioModel usuarioPorId = await BuscarPorId(id);

            if(usuarioPorId == null)
            {
                throw new Exception($"Usuário com ID: {id} não foi encontrado no banco de dados.");    
            }

            usuarioPorId.Nome = usuario.Nome;
            usuarioPorId.Email = usuario.Email;
            usuarioPorId.SenhaHash = usuario.SenhaHash;

            _dbContext.Usuarios.Update(usuarioPorId);   
            await _dbContext.SaveChangesAsync();

            return usuarioPorId;
        }

        public async Task<bool> Apagar(int id)
        {
            UsuarioModel usuarioPorId = await BuscarPorId(id);

            if (usuarioPorId == null)
            {
                throw new Exception($"Usuário com ID: {id} não foi encontrado no banco de dados.");
            }

            _dbContext.Usuarios.Remove(usuarioPorId);
            await _dbContext.SaveChangesAsync();

            return true;
        }

    }
}