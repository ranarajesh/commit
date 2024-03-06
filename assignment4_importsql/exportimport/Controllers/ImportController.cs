using Microsoft.AspNetCore.Mvc;

namespace exportimport;

[Route("api/v1/[controller]")]
[ApiController]
public class ImportController : ControllerBase
{
    private readonly IImportService _importService;
    private readonly ILogger<ImportController> _logger;

    public ImportController(IImportService importService, ILogger<ImportController> logger){
        _importService = importService;
        _logger = logger;
    }
    
    [HttpGet]
    public async Task<ActionResult> Get(){
        return Ok("Test");
    }

    [HttpPost]
    public async Task<IActionResult> ImportExcelFile(IFormFile file){

        _logger.LogInformation($"REquest for import excel file");
        // can create another service to check the file validation. I leave this part for now.
        try
        {
            await _importService.ParseImportFile(file);
            return Ok("Excel file parsed successfully");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error parsing Excel file: {ex.Message}");
        }
    }


}
