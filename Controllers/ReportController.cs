//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Reporting.WebForms;
//using System;
//using System.Data;
//using System.IO;

//namespace YourNamespace.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ReportController : ControllerBase
//    {
//        [HttpGet("GenerateReport")]
//        public IActionResult GenerateReport()
//        {
//            string reportPath = Path.Combine(Directory.GetCurrentDirectory(), "Reports", "SampleReport.rdlc");

//            var reportViewer = new ReportViewer
//            {
//                ProcessingMode = ProcessingMode.Local
//            };
//            reportViewer.LocalReport.ReportPath = reportPath;

//            DataTable dataTable = GetData(); 
//            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dataTable));

//            byte[] bytes = reportViewer.LocalReport.Render("PDF");

//            return File(bytes, "application/pdf", "SampleReport.pdf");
//        }

//        private DataTable GetData()
//        {
//            DataTable dataTable = new DataTable();
//            return dataTable;
//        }
//    }
//}