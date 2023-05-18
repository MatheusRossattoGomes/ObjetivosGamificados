
namespace ContractAppService;

using Domain;
using Dtos;

public interface IObjetivosGamificadosAppService {

IList<TiposObjetivosEnum> GetTiposDeObjetivo();

#region Usuario
UsuarioDto Getusuario(long id);

long AddUsuario(UsuarioDto dto);

void DeleteUsuario(long id);

#endregion Usuario

#region Objetivo
ObjetivoDto GetObjetivo(long id);

string GetViewObjetivos(long id);

List<ObjetivoDto> GetObjetivos(long id);

long AlterarObjetivo(ObjetivoDto dto);

long AddObjetivo(ObjetivoDto dto);

void DeleteObjetivo(long id);

#endregion Objetivo

}