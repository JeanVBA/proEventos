using Microsoft.AspNetCore.Mvc;
using ProEventos.Application;
using ProEventos.Application.Contratos;
using ProEventos.Domain;
using ProEventos.Persistence.Context;

namespace ProEventos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventosController : ControllerBase
{
    private readonly IEventosService _eventosService;

    public EventosController(IEventosService eventosService)
    {
        _eventosService = eventosService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var eventos = await _eventosService.GetAllEventosAsync(true);
            if (eventos == null) return NotFound("Nenhum evento encontrado");
            return Ok(eventos);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {e.Message}");
        }
    }
    [HttpGet("tema/{tema}")]
    public async Task<IActionResult> Get(string tema)
    {
        try
        {
            var eventos = await _eventosService.GetAllEventosByTemaAsync(tema, true);
            if (eventos == null) return NotFound("Nenhum evento encontrado");
            return Ok(eventos);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {e.Message}");
        }
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var evento = await _eventosService.GetEventoByIdAsync(id, true);
            if (evento == null) return NotFound("Nenhum evento encontrado");
            return Ok(evento);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {e.Message}");
        }
    }
    [HttpPost]
    public async Task<IActionResult> Post(Evento model)
    {
        try
        {
            var evento = await _eventosService.AddEventos(model);
            if (evento == null) return BadRequest("Erro ao tentar salvar evento");
            return Ok(evento);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar salvar evento. Erro: {e.Message}");
        }
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Evento model)
    {
        try
        {
            var evento = await _eventosService.UpdateEventos(id, model);
            if (evento == null) return BadRequest("Erro ao tentar atualizar evento");
            return Ok(evento);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar evento. Erro: {e.Message}");
        }
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            return await _eventosService.DeleteEventos(id) ?
                 Ok("Deletado") :
                 BadRequest("Evento não deletado");

        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar salvar evento. Erro: {e.Message}");
        }
    }
}
