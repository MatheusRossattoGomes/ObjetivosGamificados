
namespace Domain;
public class Objetivos : BaseEntity
{
    public Objetivos(string objetivo, string descricao, DateTime dataCriacao, DateTime dataEntrega, int quantidade, TiposObjetivosEnum tipoObjetivo, long idUsuario, long? idAula)
    {
        Objetivo = objetivo;
        Descricao = descricao;
        DataCriacao = dataCriacao;
        DataEntrega = dataEntrega;
        Quantidade = quantidade;
        TipoObjetivo = tipoObjetivo;
        IdUsuario = idUsuario;
        IdAula = idAula;
    }

    public long Id { get; set; }
    public string Objetivo { get; set; }
    public string Descricao { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime DataEntrega { get; set; }
    public int Quantidade { get; set; }
    public TiposObjetivosEnum TipoObjetivo { get; set; }
    public long? IdAula { get; set; }
    public Aula Aula { get; set; }
    public long IdUsuario { get; set; }
    public Usuario Usuario { get; set; }
}