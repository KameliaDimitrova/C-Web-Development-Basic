using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using PandaWebApp.Models;
using PandaWebApp.Models.Enums;
using PandaWebApp.ViewModels.Reports;
using SIS.HTTP.Responses;
using SIS.MvcFramework;

namespace PandaWebApp.Controllers
{
    public class ReportsController : BaseController
    {
        [Authorize]
        public IHttpResponse Details(int id)
        {
            var dateNow = DateTime.Now;
            var model = this.Db.Tasks
                .Where(x => x.Id == id)
                .Select(x => new ReportsDetailsViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Level = x.AffectedSectors.Count,
                    DueDate = x.DueDate.ToString("DD/MM/yyyy"),
                    AffectedSectors = string.Join(", ", x.AffectedSectors.Select(s => s.Sector.ToString())),
                    Participants = String.Join(", ", x.Participants),
                    ReportedOn = dateNow.ToString("dd/MM/yyyy"),
                    ReporterName = User.Username,

                }).FirstOrDefault();
            if (model == null)
            {
                return this.BadRequestError("Invalid task id.");
            }
          
            var report = new Report
            {
                ReportedOn = dateNow,
                Reporter = this.Db.Users.FirstOrDefault(x => x.Username == User.Username),
                Status = Status.Completed,
                Task = this.Db.Tasks.FirstOrDefault(x => x.Id == model.Id),
            };
            model.Status = report.Status.ToString();
            if (this.Db.Reports.FirstOrDefault(x => x.TaskId == model.Id) == null)
            {
                this.Db.Reports.Add(report);
                this.Db.Tasks.FirstOrDefault(x => x.Id == id).IsReported = true;
                this.Db.SaveChanges();
                return this.View("/Reports/Details", model);
            }
            return this.BadRequestError("You`ve already reported this task!");
        }

        [Authorize("Admin")]
        public IHttpResponse All()
        {
            var reportedTasksList = this.Db.Reports
                .Select(x => new ReportedTaskViewModel
                {
                    Id = x.TaskId,
                    Level = x.Task.AffectedSectors.Count,
                    Status = x.Status.ToString(),
                    Title = x.Task.Title

                }).ToHashSet();
            var model = new AllReportedTasksViewModel
            {
                AllReportedTasks = reportedTasksList
            };

            return this.View("/Reports/All", model);
        }
    }
}
