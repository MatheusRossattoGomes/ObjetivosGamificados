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

    long AddAula(Aula entity);

    List<AulaGridDto> GetAulasDto(long idUsuario);
    
    List<ObjetivoDto> GetObjetivosAula(long idAula);
    
    Aula GetAula(long idAula);
    
    AulaDto GetAulaDto(long idAula);

    public void AlterarObjetivosAula(List<Objetivos> entities);

    long AlterarAula(Aula entity);

    void DeleteAula(long id);

}