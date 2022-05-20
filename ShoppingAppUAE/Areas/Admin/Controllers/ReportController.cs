using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report.Mvc;
using Stimulsoft.Report.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReportController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ReportController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Stimulsoft.Base.StiLicense.LoadFromFile("license.key");
            this.configuration = configuration;
            this.webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Invoice([FromQuery]int ID)
        {
            ViewBag.InvoiceId = ID;
            return View();
        }

        public IActionResult GetInvoice([FromQuery]int ID)
        {

        
            StiReport report = new StiReport();
            report.Load("Reports/OrderInvoice.mrt");
            report["@OrderID"] = ID;
            report.Dictionary.Databases.Add(new StiSqlDatabase("Connection", configuration.GetConnectionString("DefaultConnection")));
           
            report.Render();
           
            return StiNetCoreViewer.GetReportResult(this, report);
        }

        public IActionResult InvoicePDF([FromQuery] int OrderID)
        {
            StiReport report = new StiReport();
            report.Load("Reports/OrderInvoice.mrt");
            report["@OrderID"] = OrderID;
            report.Dictionary.Databases.Add(new StiSqlDatabase("Connection", configuration.GetConnectionString("DefaultConnection")));
            return  StiNetCoreReportResponse.PrintAsPdf(report);
        }
        public IActionResult ViewerEvent()
        {
            return StiNetCoreViewer.ViewerEventResult(this);
        }
    }
}
