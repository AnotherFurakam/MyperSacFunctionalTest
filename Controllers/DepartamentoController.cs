using Microsoft.AspNetCore.Mvc;
using MyperSacFunctionalTest.Dto.Departamento;
using MyperSacFunctionalTest.Dto.Provincia;
using MyperSacFunctionalTest.Models;
using MyperSacFunctionalTest.Services.Departamento;
using System.Net;

namespace MyperSacFunctionalTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {
        private readonly IDepartamentoService _departamentoService;

        public DepartamentoController(IDepartamentoService departamentoService)
        {
            _departamentoService = departamentoService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<DepartamentoDto>>>> GetAll()
        {
            var response = await _departamentoService.GetAll();
            return Ok(response);
        }

        [HttpGet("{id_departamento}/provincia")]
        public async Task<ActionResult<ApiResponse<List<ProvinciaDto>>>> GetProvinciasByIdDepartamento(int id_departamento)
        {
            var response = await _departamentoService.GetProviciasByIdDepartamento(id_departamento);
            if(response.Status == HttpStatusCode.NotFound)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
