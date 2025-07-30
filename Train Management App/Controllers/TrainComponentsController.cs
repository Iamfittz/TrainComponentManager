using Microsoft.AspNetCore.Mvc;
using Train_Management_App.Services;
using Microsoft.AspNetCore.Authorization;
using Train_Management_App.Data;

namespace Train_Management_App.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TrainComponentsController : ControllerBase {
    private readonly ITrainComponentService _service;

    public TrainComponentsController(ITrainComponentService service) {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) {
        var result = await _service.GetByIdAsync(id);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(TrainComponent component) {
        try {
            var created = await _service.CreateAsync(component);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        } catch (ArgumentException ex) {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, TrainComponent component) {
        try {
            var updated = await _service.UpdateAsync(id, component);
            return updated ? NoContent() : NotFound();
        } catch (ArgumentException ex) {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
        var deleted = await _service.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string name, [FromQuery] string uniqueNumber) {
        var result = await _service.SearchAsync(name, uniqueNumber);
        return Ok(result);
    }

}
