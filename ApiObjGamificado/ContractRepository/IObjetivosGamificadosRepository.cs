using Domain;
using Dtos;

namespace ContractContext;

public interface IObjetivosGamificadosRepository
{

    IList<TiposObjetivosEnum> GetTiposDeObjetivo();

    Objetivos GetObjetivo(long id);
    
    ObjetivoDto GetObjetivoDto(long id);

    string GetViewObjetivos(long id);

    List<ObjetivoDto> GetObjetivosDto(long idUsuario);

    long AddObjetivo(Objetivos entity);

    long AlterarObjetivo(Objetivos entity);

    void DeleteObjetivo(long id);

}