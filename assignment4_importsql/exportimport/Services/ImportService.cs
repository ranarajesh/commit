
using System.Collections;
using System.Data.Common;
using OfficeOpenXml;

namespace exportimport;
public class ImportService : IImportService
{
    public async Task ParseImportFile(IFormFile file)
    {
        var importList = new List<Company>();

        if (file == null || file.Length == 0)
        {
            throw new ArgumentException("File is empty");
        }
         using (var stream = new MemoryStream())
        {
            await file.CopyToAsync(stream);
            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;
                int colCount = worksheet.Dimension.Columns;
                
                for (int row = 1; row <= rowCount; row++)
                {
                    for (int col = 1; col <= colCount; col++)
                    {
                        var id = worksheet.Cells[row, col].Value?.ToString();
                        var name = worksheet.Cells[row, col].Value?.ToString();
                        var location = worksheet.Cells[row, col].Value?.ToString();
                        Console.WriteLine($"id: {id}, name: {name}, location : {location}" );
                        
                        var obj = new Company();
                        obj.Id = id;
                        obj.Name = name;
                        obj.Location = location;
                        importList.Add(obj);

                    }
                }
            }
        }

        //  after getting the parse list we make call to data base to add these information to Company table of MSSQL Database
        System.Console.WriteLine(importList);
    }
}

