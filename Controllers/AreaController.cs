using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using ProyectoModeradores.Models.Connection;

namespace ProyectoModeradores.Controllers
{
    public class AreaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult  Import( IFormFile FileData)
        {
            try
            {
                if (FileData != null)
                {
                    using (var content = new MemoryStream())
                    {


                        FileData.CopyTo(content);

                        String lPath = "C:\\Users\\danch\\OneDrive\\Escritorio\\9°\\Auditoria\\Proyecto\\ProyectoModeradores\\.xlsx";
                        
                        System.IO.File.WriteAllBytes(lPath, content.ToArray());
                        FileStream fileStream = new FileStream(lPath, FileMode.Open, FileAccess.Read);
                        IExcelDataReader reader = ExcelReaderFactory.CreateReader(fileStream);

                        System.Data.DataSet dtsTablas = reader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = true,
                            }

                        });

                        foreach(System.Data.DataTable table in dtsTablas.Tables) { 
                            AreaDB.ImportArea(table);
                        }
                    }
                }
                else
                {
                    
                }
            }
            catch (System.Exception ex)
            {
                
            }
            

            return Redirect("Index");
        }
    }
}
