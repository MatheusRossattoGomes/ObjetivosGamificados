using System.Text.Json;
using AppService;
using ContractAppService;
using Domain;
using Dtos;
using Microsoft.AspNetCore.Mvc;

namespace TodoApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly ILogger<UsuariosController> _logger;
    private readonly IObjetivosGamificadosAppService _appService;

    public UsuariosController(ILogger<UsuariosController> logger, IObjetivosGamificadosAppService appService)
    {
        _logger = logger;
        _appService = appService;
    }

    [HttpGet("GetUsuario/{id}")]
    public IActionResult GetUsuario(long id)
    {
        var dados = _appService.Getusuario(id);
        return Ok(dados);
    }

    [HttpPost("AddUsuario")]
    public IActionResult AddUsuario(string values)
    {
        UsuarioDto dto = JsonSerializer.Deserialize<UsuarioDto>(values);
        var id = _appService.AddUsuario(dto);
        return Ok(id);
    }

    [HttpDelete("DeleteUsuario")]
    public IActionResult DeleteUsuario(long id)
    {
        _appService.DeleteUsuario(id);
        return Ok();
    }

}
