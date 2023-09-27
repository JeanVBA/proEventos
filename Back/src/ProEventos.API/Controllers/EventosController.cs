using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ProEventos.API.Data;
using ProEventos.API.models;

namespace ProEventos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventosController : ControllerBase
{
    public readonly DataContext _context;

    public EventosController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IEnumerable<Evento> Get()
    {
        return _context.Eventos;
    }
    [HttpGet("{tema}")]
    public IEnumerable<Evento> Get(string tema)
    {
        return _context.Eventos.Where(evento => evento.Tema == tema);
    }
    [HttpGet("{id}")]
    public Evento Get(int id)
    {
        return _context.Eventos.FirstOrDefault(evento => evento.Id == id);
    }
    [HttpPost]
    public string Post()
    {
        return "Post Method";
    }
    [HttpPut("{id}")]
    public string Put(int id)
    {
        return "Put Method";
    }
    [HttpDelete("{id}")]
    public string Delete(int id)
    {
        return "Delete Method";
    }
}
