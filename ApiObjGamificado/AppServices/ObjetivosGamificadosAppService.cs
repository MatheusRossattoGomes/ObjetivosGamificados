
using System.Reflection;
using ContractAppService;
using ContractContext;
using Domain;
using Dtos;
using Repository;

namespace AppService;
public class ObjetivosGamificadosAppService : IObjetivosGamificadosAppService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IObjetivosGamificadosRepository _objetivosGamificadosRepository;
    public ObjetivosGamificadosAppService(IUsuarioRepository usuarioRepository, IObjetivosGamificadosRepository objetivosGamificadosRepository)
    {
        _usuarioRepository = usuarioRepository;
        _objetivosGamificadosRepository = objetivosGamificadosRepository;
    }

    #region Usuario
    public UsuarioDto Getusuario(long id)
    {
        var dados = _usuarioRepository.GetUsuario(id);
        return dados;
    }

    public long AddUsuario(UsuarioDto dto)
    {
        var usuario = new Usuario(dto.Nome, dto.Email);
        var id = _usuarioRepository.AddUsuario(usuario);
        return id;
    }

    public void DeleteUsuario(long id)
    {
        _usuarioRepository.DeleteUsuario(id);
    }

    #endregion Usuario



    #region Objetivo
    public IList<TiposObjetivosEnum> GetTiposDeObjetivo()
    {
        var dados = _objetivosGamificadosRepository.GetTiposDeObjetivo();
        return dados;
    }

    public ObjetivoDto GetObjetivo(long id)
    {
        var dados = _objetivosGamificadosRepository.GetObjetivoDto(id);
        return dados;
    }

    public string GetViewObjetivos(long id)
    {
        var dados = _objetivosGamificadosRepository.GetViewObjetivos(id);
        return dados;
    }

    public List<ObjetivoDto> GetObjetivos(long idUsuario)
    {
        var dados = _objetivosGamificadosRepository.GetObjetivosDto(idUsuario);
        return dados;
    }

    public long AddObjetivo(ObjetivoDto dto)
    {
        dto.DataCriacao = DateTime.Now;
        var objetivoString = GetObjetivoString(dto);
        var Objetivo = new Objetivos(objetivoString, dto.Descricao, dto.DataCriacao.Value, dto.DataEntrega, dto.Quantidade, dto.TipoObjetivo, dto.IdUsuario, dto.IdAula);
        var id = _objetivosGamificadosRepository.AddObjetivo(Objetivo);
        return id;
    }

    public long AlterarObjetivo(ObjetivoDto dto)
    {
        var objetivo = _objetivosGamificadosRepository.GetObjetivo(dto.Id.Value);
        objetivo.DataEntrega = dto.DataEntrega;
        objetivo.Descricao = dto.Descricao;
        objetivo.Quantidade = dto.Quantidade;
        objetivo.TipoObjetivo = dto.TipoObjetivo;
        dto.DataCriacao = objetivo.DataCriacao;
        objetivo.Objetivo = GetObjetivoString(dto);

        var id = _objetivosGamificadosRepository.AlterarObjetivo(objetivo);
        return id;
    }

    private string GetObjetivoString(ObjetivoDto dto)
    {
        var texto = "";
        int periodoEmDias = (dto.DataEntrega - dto.DataCriacao.Value).Days + 2;
        int quantidadePorDia = dto.Quantidade / periodoEmDias;
        int restoQuantidade = dto.Quantidade - (periodoEmDias * quantidadePorDia);

        for (int aux = 0; aux < periodoEmDias; aux++)
        {
            if (aux == periodoEmDias - 1) quantidadePorDia += restoQuantidade;
            if (aux > 0) texto += "\n";

            switch (dto.TipoObjetivo)
            {
                case TiposObjetivosEnum.Questionario:
                    texto += @$"<p>{aux + 1} - Resolva {quantidadePorDia} questões do questionário {dto.Descricao} no dia '{dto.DataCriacao.Value.AddDays(aux).ToString("dd/MM/yyyy")}'</p>";
                    break;
                case TiposObjetivosEnum.Texto:
                    texto += @$"<p>{aux + 1} - Leia {quantidadePorDia} páginas do texto {dto.Descricao} no dia '{dto.DataCriacao.Value.AddDays(aux).ToString("dd/MM/yyyy")}'</p>";
                    break;
                case TiposObjetivosEnum.Video:
                    texto += @$"<p>{aux + 1} - Assista {quantidadePorDia} minutos do vídeo {dto.Descricao} no dia '{dto.DataCriacao.Value.AddDays(aux).ToString("dd/MM/yyyy")}'</p>";
                    break;
            }
        }

        return texto;
    }

    public void DeleteObjetivo(long id)
    {
        _objetivosGamificadosRepository.DeleteObjetivo(id);
    }

    #endregion Objetivo

    #region Aulas

    public List<AulaGridDto> GetAulas(long idUsuario)
    {
        var dados = _objetivosGamificadosRepository.GetAulasDto(idUsuario);
        return dados;
    }

    public AulaDto GetAula(long idAula)
    {
        var dados = _objetivosGamificadosRepository.GetAulaDto(idAula);
        return dados;
    }

    public List<ObjetivoDto> GetObjetivosAula(long idAula)
    {
        var dados = _objetivosGamificadosRepository.GetObjetivosAula(idAula);
        return dados;
    }

    public long AddAula(AulaDto dto)
    {
        var Aula = new Aula(dto.Descricao, dto.Resumo, dto.DataAula, dto.IdUsuario);
        var objetivos = dto.Objetivos?.Select(x => new Objetivos("", x.Descricao, DateTime.Now, x.DataEntrega, x.Quantidade, x.TipoObjetivo, dto.IdUsuario, x.IdAula)).ToList();
        objetivos?.ForEach(x => Aula.AddObjetivo(x));
        var id = _objetivosGamificadosRepository.AddAula(Aula);
        return id;
    }

    public void DeleteAula(long id)
    {
        _objetivosGamificadosRepository.DeleteAula(id);
    }

    public long AlterarAula(AulaDto dto)
    {
        var aula = _objetivosGamificadosRepository.GetAula(dto.Id.Value);
        aula.DataAula = dto.DataAula;
        aula.Descricao = dto.Descricao;
        aula.Resumo = dto.Resumo;

        GerenciarObjetivosAula(aula, dto.Objetivos, aula.Objetivos);

        var id = _objetivosGamificadosRepository.AlterarAula(aula);
        return id;
    }

    private void GerenciarObjetivosAula(Aula aula, List<ObjetivoDto> objetivosDto, List<Objetivos> objetivosEntidade)
    {
        // Find objetivos to insert
        var objetivosToInsert = objetivosDto?.Where(o => !objetivosEntidade.Any(e => e.Id == o.Id))
        .Select(x => new Objetivos("", x.Descricao, DateTime.Now, x.DataEntrega, x.Quantidade, x.TipoObjetivo, aula.IdUsuario, aula.Id)).ToList();
        // Find objetivos to update
        var objetivosToUpdate = objetivosEntidade?.Where(o => objetivosDto.Any(e => e.Id == o.Id)).Select(x => x).ToList();
        // Find objetivos to delete
        var objetivosToDelete = objetivosEntidade?.Where(e => !objetivosDto.Any(o => o.Id == e.Id)).Select(x => x).ToList();

        objetivosToInsert.ForEach(x => aula.AddObjetivo(x));
        AlterarObjetivosAula(objetivosToUpdate, objetivosDto);
        objetivosToDelete.ForEach(x => aula.RemoveObjetivo(x));

    }

    public void AlterarObjetivosAula(List<Objetivos> objetivos, List<ObjetivoDto> objetivosDto)
    {
        if (objetivos.Count() <= 0) return;

        objetivos.ForEach(x =>
        {
            var dto = objetivosDto.FirstOrDefault(y => y.Id == x.Id);
            AlterarObjetivoAula(dto, x);
        });

        _objetivosGamificadosRepository.AlterarObjetivosAula(objetivos);
    }

    public void AlterarObjetivoAula(ObjetivoDto dto, Objetivos objetivo)
    {
        objetivo.DataEntrega = dto.DataEntrega;
        objetivo.Descricao = dto.Descricao;
        objetivo.Quantidade = dto.Quantidade;
        objetivo.TipoObjetivo = dto.TipoObjetivo;
    }

    #endregion Aulas

}