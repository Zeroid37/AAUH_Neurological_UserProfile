using Microsoft.AspNetCore.Mvc;
using FlagAPI.Business;

namespace FlagAPI.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class FlagController : Controller {
        private readonly IConfiguration _configuration;
        public FlagController(IConfiguration configuration) {
            _configuration = configuration;
        }

        [HttpPost]
        public async Task Process() {
            DateOnly lastRead = new DateOnly(2000, 1, 1); //TODO dummy date, needed for example

            FlagLogic fl = new FlagLogic(_configuration);
            fl.processFlags(lastRead);
        }
    }
}
