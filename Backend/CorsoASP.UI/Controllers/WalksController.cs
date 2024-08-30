using CorsoASP.Core.Interfaces.IService;
using CorsoASP.Core.Views;
using Microsoft.AspNetCore.Mvc;

namespace CorsoASP.UI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WalksController : Controller
{
    private readonly IWalksService _walksService;

    public WalksController(IWalksService walksService)
    {
        _walksService = walksService;
    }

    // GET: api/walks?page=10?limit=50?filters=
    [HttpGet]
    public async Task<ActionResult<PaginatedDataView<WalksDetailView>>> GetPage(
        [FromQuery] int page, 
        [FromQuery] int limit,
        [FromQuery] int? lengthMin,
        [FromQuery] int? lengthMax,
        [FromQuery] int? difficultyFK,
        [FromQuery] int? regionFK)  //maybe: struttura in json? 
    {
        PaginatedDataView<WalksDetailView> result;

        if (!ModelState.IsValid)
            return BadRequest();
        
        if (limit > 50)
            return BadRequest("Limited to 50 rows max");
        
        result = await _walksService.GetPaginatedWalks(page, limit, lengthMin, lengthMax, difficultyFK, regionFK);

        if (!result.Data.Any())
            return NotFound();
        return Ok(result);
    } 
    
    // GET: api/walks/details?id=10
    [HttpGet("details")]
    public async Task<ActionResult<WalksDetailView>> GetWalk([FromQuery] int id)
    {
        if (id < 0)
            return BadRequest();
        
        var walk = await _walksService.GetWalk(id);
        
        if (walk == null)
            return NotFound();
        return Ok(walk);
    }
    
    // GET: api/walks/exists?id=10
    [HttpGet("exists")]
    public async Task<ActionResult<bool>> GetExists([FromQuery] int id)
    {
        if (id < 0)
            return BadRequest();
        
        var walk = await _walksService.ExistsById(id);
        if (!walk)
            return NotFound(false);
        return Ok(true);
    }
    
    [HttpPost("create")]
    public async Task<ActionResult<WalksView>> PostCreate([FromBody] WalksView model)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        
        var createdWalk = await _walksService.Create(model);
        if (createdWalk == null)
            return Conflict();

        return CreatedAtAction(nameof(GetWalk), new { id = createdWalk.ID }, createdWalk);
    }
    
    [HttpPost("update")]
    public async Task<ActionResult<WalksView>> PostUpdate([FromBody] WalksView model)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        
        if (! await _walksService.ExistsById(model.ID))
            return NotFound();
        
        await _walksService.Update(model);
        
        return Ok();
    }
    
    [HttpPost("delete")]
    public async Task<ActionResult<bool>> PostDelete([FromBody] int id)
    {
        if (id < 0)
            return BadRequest();
        
        if (!await _walksService.ExistsById(id))
            return NotFound(false);
        
        await _walksService.Delete(id);
        
        return Ok(true);
    }
}