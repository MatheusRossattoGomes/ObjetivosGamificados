namespace Dtos;

public class AulaDto
{
    public AulaDto()
    {
        Objetivos = new List<ObjetivoDto>();
    }

    public long? Id { get; set; }
    public string Descricao { get; set; }
    public string Resumo { get; set; }
    public DateTime DataAula { get; set; }
    public long IdUsuario { get; set; }

    public List<ObjetivoDto> Objetivos { get; set; }

}