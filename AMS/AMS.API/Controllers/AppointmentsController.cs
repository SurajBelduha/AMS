using AMS.Common.Entities;
using AMS.Providers.IProvider;
using AMS.Repository.Models;
using Microsoft.AspNetCore.Mvc;

namespace AMS.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentProvider _provider;

    public AppointmentsController(IAppointmentProvider provider)
    {
        _provider = provider;
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
    {
        var list =  _provider.GetAll();
        return Ok(list);
    }

    [HttpGet("get/{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = _provider.GetById(id);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(AppointmentModel appointment)
    {
        var data =  _provider.Save(appointment);
        return data.IsSuccess ? Ok(data) : BadRequest(data);
    }

    [HttpDelete("delete/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var data =  _provider.Cancel(id);
        return data.IsSuccess ? Ok() : NotFound();
    }
}
