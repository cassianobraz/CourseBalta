using Microsoft.AspNetCore.Mvc;
using ProjetoDBZ.Data;
using ProjetoDBZ.Models;

namespace ProjetoDBZ.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PersonagensController : ControllerBase
{
    private readonly AppDbContext _context;

    public PersonagensController(AppDbContext context) => _context = context;


    [HttpPost]
    public async Task<IActionResult> AddPersonagem(Personagem personagem)
    {
        _context.DBZ.Add(personagem);
        await _context.SaveChangesAsync();

        return Ok(personagem);
    }
}
