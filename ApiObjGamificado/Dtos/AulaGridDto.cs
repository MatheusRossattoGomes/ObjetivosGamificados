namespace Dtos;

public class AulaGridDto
{
    public long? Id { get; set; }
    public string Descricao { get; set; }
    public string Resumo { get; set; }
    public DateTime DataAula { get; set; }
    public string DataAulaString => DataAula.ToString("dd/MM/yyyy");

}