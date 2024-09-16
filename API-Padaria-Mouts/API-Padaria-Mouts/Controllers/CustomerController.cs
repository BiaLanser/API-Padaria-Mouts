using API_Padaria_Mouts.Models;
using API_Padaria_Mouts.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_Padaria_Mouts.Controllers
{
    [ApiController]
    [Route("api/v1/[Controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _service;
        public CustomerController(CustomerService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Customer customer)
        {
            try
            {
                return StatusCode(201, _service.Create(customer));
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
        public IActionResult Put(int id, [FromBody] Customer customer)
        {
            try
            {
                _service.Update(customer);
                return StatusCode(200, "Customer successfully updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
    }
}
