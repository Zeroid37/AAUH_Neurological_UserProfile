using API_AAUH.BusinessLogic;
using API_AAUH.Models;
using Azure;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace API_AAUH.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase {
        private readonly IConfiguration _configuration;

        public PatientController(IConfiguration configuration) {
            _configuration = configuration;
        }
        // GET api/<PatientController>/5
        [HttpGet("{id}")]
        public string Get(int id) {
            PersonLogic PL = new PersonLogic(_configuration);
            Patient patient = PL.getPatientFromDB(id.ToString());

            return JsonSerializer.Serialize<Patient>(patient);
        }

        // POST api/<PatientController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Patient patient) {
            ActionResult foundReturn;
            PersonLogic PL = new PersonLogic(_configuration);
            bool ok = false;
            if (ok) {
                foundReturn = Ok();
            }
            else {
                foundReturn = new StatusCodeResult(500);
            }
            return foundReturn;
        }

        // PUT api/<PatientController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) {
        }

        // DELETE api/<PatientController>/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
        }
    }
}
