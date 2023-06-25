using Microsoft.AspNetCore.Mvc;
using MyperSacFunctionalTest.Dto.Trabajador;
using MyperSacFunctionalTest.Models;
using MyperSacFunctionalTest.Services.Trabajador;
using System.Net;

namespace MyperSacFunctionalTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrabajadorController : ControllerBase
    {
        private readonly ITrabajadorService _trabajadorService;

        public TrabajadorController(ITrabajadorService trabajadorService)
        {
            _trabajadorService = trabajadorService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<TrabajadorDto>>>> GetAll()
        {
            var response = await _trabajadorService.GetAll();
            if (response.Status == HttpStatusCode.BadRequest) return BadRequest(response);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<TrabajadorDto>>> AddTrabajador(CreateTrabajadorDto createTrabajadorDto)
        {
            var response = await _trabajadorService.AddTrabajador(createTrabajadorDto);
            if (response.Status == HttpStatusCode.NotFound) return NotFound(response);
            if (response.Status == HttpStatusCode.BadRequest) return BadRequest(response);
            return Created("/api/Trabajador/{id}", response);
        }

        [HttpPut("{id_trabajador}")]
        public async Task<ActionResult<ApiResponse<TrabajadorDto>>> UpdateTrabajador(UpdateTrabajadorDto updateTrabajadorDto, int id_trabajador)
        {
            var response = await _trabajadorService.UpdateTrabajador(updateTrabajadorDto, id_trabajador);
            if (response.Status == HttpStatusCode.NotFound) return NotFound(response);
            if (response.Status == HttpStatusCode.BadRequest) return BadRequest(response);
            return Ok(response);
        }

        [HttpDelete("{id_trabajador}")]
        public async Task<ActionResult<ApiResponse<TrabajadorDto>>> DeletTrabajador(int id_trabajador)
        {
            var response = await _trabajadorService.DeleteTrabajador(id_trabajador);
            if (response.Status == HttpStatusCode.NotFound) return NotFound(response);
            if (response.Status == HttpStatusCode.BadRequest) return BadRequest(response);
            return Ok(response);
        }

    }
}
