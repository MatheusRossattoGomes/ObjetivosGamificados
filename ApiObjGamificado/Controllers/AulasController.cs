using System.Text.Json;
using AppService;
using ContractAppService;
using Domain;
using Dtos;
using Microsoft.AspNetCore.Mvc;

namespace TodoApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AulasController : ControllerBase
{
    private readonly ILogger<AulasController> _logger;
    private readonly IObjetivosGamificadosAppService _appService;

    public AulasController(ILogger<AulasController> logger, IObjetivosGamificadosAppService appService)
    {
        _logger = logger;
        _appService = appService;
    }
    
    [HttpGet("GetAulas/{idUsuario}")]
    public IActionResult GetAulas(long idUsuario)
    {
        var dados = _appService.GetAulas(idUsuario);
        return Ok(dados);
    }
    
    [HttpGet("GetAula/{idAula}")]
    public IActionResult GetAula(long idAula)
    {
        var dados = _appService.GetAula(idAula);
        return Ok(dados);
    }
    
    [HttpGet("GetObjetivosAula/{idAula}")]
    public IActionResult GetObjetivosAula(long idAula)
    {
        var dados = _appService.GetObjetivosAula(idAula);
        return Ok(dados);
    } 
    
    [HttpPost("AddAula")]
    public IActionResult AddAula([FromBody] AulaDto dto)
    {
        var idRetorno = _appService.AddAula(dto);
        return Ok(idRetorno);
    }

    [HttpPut("AlterarAula")]
    public IActionResult AlterarAula([FromBody] AulaDto dto)
    {
        var idRetorno = _appService.AlterarAula(dto);
        return Ok(idRetorno);
    }

    [HttpDelete("DeleteAula")]
    public IActionResult DeleteAula(long id)
    {
        _appService.DeleteAula(id);
        return Ok();
    }

}
