using Domain;

namespace Dtos;

public class ObjetivoDto
{
    public long? Id { get; set; }
    public string Objetivo { get; set; }
    public string Descricao { get; set; }
    public DateTime? DataCriacao { get; set; }
    public DateTime DataEntrega { get; set; }
    public string DataCriacaoString => DataCriacao.HasValue ? DataCriacao.Value.ToString("dd/MM/yyyy hh-mm-ss") : "";
    public string DataEntregaString => DataEntrega.ToString("dd/MM/yyyy");
    public int Quantidade { get; set; }
    public TiposObjetivosEnum TipoObjetivo { get; set; }
    public string TipoObjetivoString => TipoObjetivo.ToString();
    public long IdUsuario { get; set; }

}