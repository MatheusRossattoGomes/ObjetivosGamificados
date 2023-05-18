using Contexts;
using ContractContext;
using Domain;
using Dtos;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class UsuarioRepository : IUsuarioRepository
{

    private readonly ObjetivosGamificadosContext _context;
    public UsuarioRepository(ObjetivosGamificadosContext context)
    {
        // var optionsBuilder = new DbContextOptionsBuilder<ObjetivosGamificadosContext>();
        // _context = new ObjetivosGamificadosContext(optionsBuilder.Options);
        _context = context;
    }

    public UsuarioDto GetUsuario(long id)
    {
        using (_context)
        {
            var usuario = (from u in _context.Usuarios.Where(x => x.Id == id)
                           select new UsuarioDto()
                           {
                               Email = u.Email,
                               Id = u.Id,
                               Nome = u.Nome

                           }).FirstOrDefault();

            return usuario;
        }
    }

    public long AddUsuario(Usuario usuario)
    {
        _context.Add(usuario);
        _context.SaveChanges();
        return usuario.Id;
    }

    public void DeleteUsuario(long id)
    {
        using (_context)
        {
            var usuario = (from u in _context.Usuarios.Where(x => x.Id == id)
                           select u).FirstOrDefault();
            _context.Remove(usuario);
            _context.SaveChanges();
        }
    }
}