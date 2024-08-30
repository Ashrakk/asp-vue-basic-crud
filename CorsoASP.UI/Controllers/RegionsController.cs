using CorsoASP.Core.Interfaces.IService;
using CorsoASP.Core.Views;
using Microsoft.AspNetCore.Mvc;

namespace CorsoASP.UI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegionsController : Controller
{
    private readonly IRegionsService _regionsService;

    public RegionsController(IRegionsService reg)
    {
        _regionsService = reg;
    }
    
    //api/regions
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RegionsView>>> GetRegions()
    {
        var res = await _regionsService.GetRegions();
        if (res == null)
            return NotFound();
        return Ok(res);
    }
    
    // GET: api/regions/details?id=10
    [HttpGet("details")]
    public async Task<ActionResult<RegionsView>> GetRegion([FromQuery] int id)
    {
        var res = await _regionsService.GetRegion(id);
        if (res == null)
            return NotFound();
        return Ok(res);
    }
}