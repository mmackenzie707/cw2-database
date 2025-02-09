using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using trailAPI.Models;
using trailAPI.Services;
using System;

namespace trailAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExplorationsController : ControllerBase
    {
        private readonly ExplorationServices _explorationService;

        public ExplorationsController(ExplorationServices explorationService)
        {
            _explorationService = explorationService;
        }

[Authorize]
[HttpPost]
public IActionResult Post([FromBody] ExplorationDto explorationDto)
{
    if (explorationDto == null)
    {
        return BadRequest("Exploration data is null");
    }

    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    var exploration = new Explorations
    {
        UserID = explorationDto.UserID,
        TrailID = explorationDto.TrailID,
        CompletionStatus = explorationDto.CompletionStatus,
        CompletionTime = explorationDto.CompletionTime,
        CompletionDate = explorationDto.CompletionDate
    };

    _explorationService.AddExploration(exploration);
    return Ok(new { Status = "Exploration added", Exploration = exploration });
}
    }
}