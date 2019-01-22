using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using PandaWebApp.Models;
using PandaWebApp.Models.Enums;
using PandaWebApp.ViewModels.Tasks;
using SIS.HTTP.Responses;
using SIS.MvcFramework;

namespace PandaWebApp.Controllers
{
    public class TasksController : BaseController
    {
        [Authorize("Admin")]
        [HttpGet]
        public IHttpResponse Create()
        {
            return this.View();
        }

        [Authorize("Admin")]
        [HttpPost]
        public IHttpResponse Create(CreateTaskViewModel inputViewModel)
        {
            var sectorStrings = new string[]
                {
                    inputViewModel.Sector1, inputViewModel.Sector2, inputViewModel.Sector3, inputViewModel.Sector4, inputViewModel.Sector5
                }
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToArray();

            var taskForAdd = new Task
            {
                Description = inputViewModel.Description,
                DueDate = DateTime.ParseExact(inputViewModel.DueDate, "mm/dd/yyyy", CultureInfo.InvariantCulture),
                Participants = inputViewModel.Participants,
                Title = inputViewModel.Title,

            };
           
            foreach (var sectorStr in sectorStrings)
            {
                var validSector = Enum.TryParse(sectorStr, out Sector sector);
                if (validSector)
                {
                    taskForAdd.AffectedSectors.Add(new TaskSector
                    {
                        Sector = sector
                        
                    });
                }
            }
          
            this.Db.Tasks.Add(taskForAdd);
            this.Db.SaveChanges();
            return this.Redirect("/");
        }

        [Authorize]
        public IHttpResponse Details(int id)
        {
            if (this.User.IsLoggedIn)
            {
                var user = this.Db.Users.FirstOrDefault(x => x.Username == this.User.Username);

                var modelView = this.Db.Tasks
                    .Where(x=>id==x.Id)
                   .Select(x => new TaskDetails
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Level = x.AffectedSectors.Count,
                    DueDate = x.DueDate.ToString("dd/mm/yyyy"),
                    AffectedSectors = string.Join(", ", x.AffectedSectors.Select(s => s.Sector.ToString())),
                       Participants = String.Join(", ", x.Participants),
                }).FirstOrDefault();

                if (modelView == null)
                {
                    return this.BadRequestError("Invalid task id.");
                }
           

                return this.View("/Tasks/Details", modelView);
            }
            else
            {
                return this.Redirect("/Users/Login");
            }
        }
    }
}
