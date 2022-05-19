using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report.Mvc;
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
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetReport(string id)
        {
            // Create the report object
            StiReport report = new StiReport();

            // Load report
            report.Load("Reports/Report2.mrt");
            report["@OrderID"] = 27;
            report.Dictionary.Databases.Add(new StiSqlDatabase("Connection", configuration.GetConnectionString("DefaultConnection")));
            report.Render();

            return StiNetCoreViewer.GetReportResult(this, report);
        }

        public IActionResult ViewerEvent()
        {
            return StiNetCoreViewer.ViewerEventResult(this);
        }
    }
}
