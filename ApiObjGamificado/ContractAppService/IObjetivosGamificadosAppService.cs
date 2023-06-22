
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

#region Aulas

List<AulaGridDto> GetAulas(long id);

AulaDto GetAula(long idAula);

List<ObjetivoDto> GetObjetivosAula(long id);

long AlterarAula(AulaDto dto);

long AddAula(AulaDto dto);

void DeleteAula(long id);

#endregion Aulas 

}