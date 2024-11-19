using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Exception;

namespace WeatherForecast.Controllers.Base
{
    public abstract class ExceptionHandlingController : ControllerBase
    {
        protected virtual async Task<IActionResult> ExecuteWithExceptionHandling(Func<Task<IActionResult>> action)
        {
            try
            {
                return await action();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (HttpRequestException e)
            {
                return StatusCode(500, e.Message);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (System.Exception e)
            {
                return StatusCode(500);
            }
        }
    }
}
