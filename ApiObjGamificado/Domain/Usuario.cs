
namespace Domain;
public class Usuario : BaseEntity
{
    public Usuario(string nome, string email)
    {
        Nome = nome;
        Email = email;
    }
    public long Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public List<Objetivos> Objetivos { get; set; }
}