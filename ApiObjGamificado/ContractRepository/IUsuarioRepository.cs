using Domain;
using Dtos;

namespace ContractContext;
public interface IUsuarioRepository {

UsuarioDto GetUsuario(long id);

long AddUsuario(Usuario usuario);

void DeleteUsuario(long id);

}