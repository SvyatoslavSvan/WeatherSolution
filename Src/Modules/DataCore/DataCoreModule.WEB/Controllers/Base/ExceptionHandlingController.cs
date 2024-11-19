using Microsoft.AspNetCore.Mvc;

namespace DataCoreModule.WEB.Controllers.Base;

public class ExceptionHandlingController : ControllerBase
{
    protected virtual async Task<IActionResult> ExecuteWithExceptionHandlingAsync(Func<Task<IActionResult>> action)
    {
        try
        {
            return await action();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch(InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    protected virtual IActionResult ExecuteWithExceptionHandling(Func<IActionResult> action)
    {
        try
        {
            return action();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }
}