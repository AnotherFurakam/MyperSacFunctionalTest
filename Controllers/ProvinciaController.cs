using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyperSacFunctionalTest.Dto.Departamento;
using MyperSacFunctionalTest.Dto.Distrito;
using MyperSacFunctionalTest.Models;
using MyperSacFunctionalTest.Services.Provincia;
using System.Net;

namespace MyperSacFunctionalTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinciaController : ControllerBase
    {
        private readonly IProvinciaService _provinciaService;

        public ProvinciaController(IProvinciaService provinciaService)
        {
            _provinciaService = provinciaService;
        }

        [HttpGet("{id_provincia}/distrito")]
        public async Task<ActionResult<ApiResponse<List<DistritoDto>>>> GetDistritosByProvinciaId(int id_provincia)
        {
            var response = await _provinciaService.GetDistritosByProvinciaId(id_provincia);

            if (response.Status == HttpStatusCode.NotFound) return NotFound(response);
            return Ok(response);
        }

    }
}
