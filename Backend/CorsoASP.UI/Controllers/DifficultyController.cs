using CorsoASP.Core.Interfaces.IService;
using CorsoASP.Core.Views;
using Microsoft.AspNetCore.Mvc;

namespace CorsoASP.UI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DifficultyController : Controller
{
    private readonly IDifficultyService _difficultyService;

    public DifficultyController(IDifficultyService diffService)
    {
        _difficultyService = diffService;
    }
    
    //api/difficulty
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DifficultyView>>> GetDifficulties()
    {
        var res = await _difficultyService.GetDifficulties();
        if (res == null)
            return NotFound();
        return Ok(res);
    }
    
    // GET: api/difficulty/details?id=10
    [HttpGet("details")]
    public async Task<ActionResult<DifficultyView>> GetDifficulty([FromQuery] int id)
    {
        var res = await _difficultyService.GetDifficulty(id);
        if (res == null)
            return NotFound();
        return Ok(res);
    }
}