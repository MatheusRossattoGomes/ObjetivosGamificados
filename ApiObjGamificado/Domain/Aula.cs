
namespace Domain;
public class Aula : BaseEntity
{
    public Aula()
    {
        Objetivos = new List<Objetivos>();
    }


    public Aula(string descricao, string resumo, DateTime dataAula, long idUsuario)
    : this()
    {
        Descricao = descricao;
        Resumo = resumo;
        DataAula = dataAula;
        IdUsuario = idUsuario;
    }

    public long Id { get; set; }
    public string Descricao { get; set; }
    public string Resumo { get; set; }
    public DateTime DataAula { get; set; }
    public long IdUsuario { get; set; }
    public Usuario Usuario { get; set; }
    public List<Objetivos> Objetivos { get; set; }

    public void AddObjetivo(Objetivos objetivo){
        Objetivos.Add(objetivo);
    }

    public void RemoveObjetivo(Objetivos objetivo){
        Objetivos.Remove(objetivo);
    }
}