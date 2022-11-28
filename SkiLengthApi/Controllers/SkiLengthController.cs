using Microsoft.AspNetCore.Mvc;
using SkiLengthUtilities;

namespace SkiLengthApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SkiLengthController : ControllerBase
    {
        [HttpPost]
        public ActionResult Post([FromBody] SkiLength input)
        {
            SkiLengthValidator skiLengthValidator = new();
            var validatorResult = skiLengthValidator.Validate(input);

            if (!validatorResult.IsValid)
            {
                return BadRequest(ModelState);
            }

            var skiLength = SkiLengthCalcuation.CalculateSkiLength(input.Age, input.Length, input.SkiType);

            return Ok(skiLength);
        }
    }
}