using AltaBancaApi.Data;
using AltaBancaApi.Models.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
#pragma warning disable CS8603
#pragma warning disable CS8600
namespace AltaBancaApi.Services
{
    public class UsuarioService : IUsuariosService
    {
        AltaBancaContext _altaBancaContext;

        public UsuarioService(AltaBancaContext context)
        {
            _altaBancaContext = context;
        }
        public bool ValidarModelo(Usuario usuario)
        {
            return !(usuario.Nombre == null || usuario.Apellido == null || usuario.Celular == null ||
                usuario.Cuit == null || usuario.Domicilio == null || usuario.Email == null || usuario.FechaNacimiento == new DateTime());
        }
        public async Task<Usuario> CreateUsuario(Usuario usuario)
        {
            _altaBancaContext.Usuarios.Add(usuario);
            await _altaBancaContext.SaveChangesAsync();
            Usuario usuarioRet = await _altaBancaContext.Usuarios.OrderBy(u => u.Id).LastOrDefaultAsync();
            return usuarioRet;
        }

        public async Task<IEnumerable<Usuario>> GetUsuarios()
        {
            var usuarios = await _altaBancaContext.Usuarios.ToListAsync();

            return usuarios;
        }

        public async Task<Usuario> GetUsuario(int id)
        {
            Usuario usuario = await _altaBancaContext.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

            return usuario;
        }

        public async Task<IEnumerable<Usuario>> GetUsuariosPorNombre(string nombre)
        {
            var usuarios = await _altaBancaContext.Usuarios.Where(u =>
                                EF.Functions.Like(u.Nombre, $"%{nombre}%")).ToListAsync();
            return usuarios;
        }

        public async Task DeleteUsuario(int id)
        {
            var usuario = GetUsuario(id);
            if (usuario != null)
                _altaBancaContext.Remove(usuario);
            await _altaBancaContext.SaveChangesAsync();
        }

        public async Task UpdateUsuario(int id, Usuario usuario)
        {
            Usuario usuarioDeDB = await GetUsuario(id);
            usuarioDeDB.Nombre = usuario.Nombre;
            usuarioDeDB.Apellido = usuario.Apellido;
            usuarioDeDB.Celular = usuario.Celular;
            usuarioDeDB.Cuit = usuario.Cuit;
            usuarioDeDB.Domicilio = usuario.Domicilio;
            usuarioDeDB.FechaNacimiento = usuario.FechaNacimiento;
            usuarioDeDB.Email = usuario.Email;

            _altaBancaContext.Usuarios.Update(usuarioDeDB);
            await _altaBancaContext.SaveChangesAsync();

        }
    }

    public interface IUsuariosService
    {
        public bool ValidarModelo(Usuario usuario);
        public Task<IEnumerable<Usuario>> GetUsuarios();

        public Task<Usuario> GetUsuario(int id);

        public Task<IEnumerable<Usuario>> GetUsuariosPorNombre(string nombre);

        public Task<Usuario> CreateUsuario(Usuario usuario);

        public Task DeleteUsuario(int id);

        public Task UpdateUsuario(int id, Usuario usuario);
    }
}
