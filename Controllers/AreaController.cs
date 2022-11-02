using DocumentFormat.OpenXml.Spreadsheet;
using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using ProyectoModeradores.Models;
using ProyectoModeradores.Models.Connection;

namespace ProyectoModeradores.Controllers
{
    public class AreaController : Controller
    {
        List<Area> _areas = new List<Area>();   
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Import(IFormFile FileData)
        {
           
            Stream stream = FileData.OpenReadStream();
            IWorkbook Excel = null;
            if (Path.GetExtension(FileData.FileName) == ".xlsx")
            {
                Excel = new XSSFWorkbook(stream);
            }


            ISheet Hoja = Excel.GetSheetAt(0);
            int CantidadFilas = Hoja.LastRowNum;
            for (int i = 1; i <= CantidadFilas; i++)
            {
                IRow fila = Hoja.GetRow(i);
                _areas.Add(new Area()
                {

                    Code = fila.GetCell(0).ToString(),
                    Description = fila.GetCell(1).ToString(),
                }); ;

            }
            foreach (Area A in _areas)
            {
                AreaDB.SaveArea(A);
            }


            return Redirect("/");
        }
    }
}
