using API_Padaria_Mouts.Models;
using API_Padaria_Mouts.Services;
using Microsoft.AspNetCore.Mvc;

//
using System.Text.RegularExpressions;

namespace API_Padaria_Mouts.Controllers
{
    [ApiController]
    [Route("api/v1/[Controller]")]
    public class SaleController : ControllerBase
    {
        private readonly SaleService _service;
        public SaleController(SaleService service)
        {
            _service = service;
        }
        [HttpPost]
        public IActionResult Post([FromBody] Sale sale)
        {
            try
            {
                return StatusCode(201, _service.Create(sale));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_service.FindAll());
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return StatusCode(200, _service.Get(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Sale sale)
        {
            try
            {
                _service.Update(sale);
                return StatusCode(200, "Sale successfully updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("document/{document}")] 
        public IActionResult GetByDocument(string document)
        {
            try
            {
                var sales = _service.GetSalesByDocument(document);
                return Ok(sales);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
