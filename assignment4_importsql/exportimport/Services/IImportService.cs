namespace exportimport;
public interface IImportService
{
    Task ParseImportFile(IFormFile file);
}
