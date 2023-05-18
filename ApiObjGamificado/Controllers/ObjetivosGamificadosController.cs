using System.Text.Json;
using AppService;
using ContractAppService;
using Domain;
using Dtos;
using Microsoft.AspNetCore.Mvc;

namespace TodoApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ObjetivosGamificadosController : ControllerBase
{
    private readonly ILogger<ObjetivosGamificadosController> _logger;
    private readonly IObjetivosGamificadosAppService _appService;

    public ObjetivosGamificadosController(ILogger<ObjetivosGamificadosController> logger, IObjetivosGamificadosAppService appService)
    {
        _logger = logger;
        _appService = appService;
    }

    // [HttpGet(Name = "GetTiposDeObjetivo")]
    [HttpGet("GetTiposDeObjetivo")]
    public IList<TiposObjetivosEnum> GetTiposDeObjetivo()
    {
        var dados = _appService.GetTiposDeObjetivo();
        return dados;
    }

    [HttpGet("GetObjetivo/{id}")]
    public IActionResult GetObjetivo(long id)
    {
        var dados = _appService.GetObjetivo(id);
        return Ok(dados);
    }

    [HttpGet("GetViewObjetivos/{id}")]
    public IActionResult GetViewObjetivos(long id)
    {
        var dados = _appService.GetViewObjetivos(id);
        return Ok(dados);
    }

    [HttpGet("GetObjetivos/{idUsuario}")]
    public IActionResult GetObjetivos(long idUsuario)
    {
        var dados = _appService.GetObjetivos(idUsuario);
        return Ok(dados);
    }

    [HttpPost("AddObjetivo")]
    public IActionResult AddObjetivo([FromBody] ObjetivoDto dto)
    {
        // ObjetivoDto dto = JsonSerializer.Deserialize<ObjetivoDto>(values);
        var idRetorno = _appService.AddObjetivo(dto);
        return Ok(idRetorno);
    }

    [HttpPut("AlterarObjetivo")]
    public IActionResult AlterarObjetivo([FromBody] ObjetivoDto dto)
    {
        var idRetorno = _appService.AlterarObjetivo(dto);
        return Ok(idRetorno);
    }

    [HttpDelete("DeleteObjetivo")]
    public IActionResult DeleteObjetivo(long id)
    {
        _appService.DeleteObjetivo(id);
        return Ok();
    }

}
