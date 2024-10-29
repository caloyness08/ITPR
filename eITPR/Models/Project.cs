
using eITPR.Data;
using Microsoft.AspNetCore.Hosting.Server;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reactive;




namespace eITPR.Models
{
    public class Project
    {
  
        [Required(ErrorMessage = "Budget ID is required.")]
        public string? BudgetId { get; set; }
        [Required]
        public string? CostCentre { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? pjkId { get; set; }
        public int? currencyId { get; set; } = 0;
        public int? budgetYear { get; set; } = 0;

        private static IEnumerable<Project> _projects = new List<Project>();
        private static List<Project> projects = new List<Project>() {
        new Project {  BudgetId = "BUD012955", CostCentre = " 383_88511", Name = " Additional New Hiring of 7 Agency Contractors under D&O CFS & Core Banking" },
        new Project {  BudgetId = "BUD022222", CostCentre = " Cost Centre 2", Name = "Project Name 2" },
        new Project {  BudgetId = "BUD033333", CostCentre = " Cost Centre 3", Name = "Project Name 3" },
        new Project {  BudgetId = "BUD044444", CostCentre = " Cost Centre 4", Name = "Project Name 4" },
        new Project {  BudgetId = "BUD055555", CostCentre = " Cost Centre 5", Name = "Project Name 5" },
        new Project {  BudgetId = "BUD066666", CostCentre = " Cost Centre 6", Name = "Project Name 6" },

        };

        public static List<Project> GetProjects() {
            return projects;
        }

        public static Project? GetProjectsById(string id) {

            var project = projects.FirstOrDefault(s => s.BudgetId == id);

            if (project != null) {
                return new Project {
                    BudgetId = project.BudgetId,
                    CostCentre = project.CostCentre,
                    Name = project.Name,
                };
            }

            return null;
        }


        //public Task<IEnumerable<Project>> GetProjectsAsync(CancellationToken ct = default) {
        //    return projects;
        //}
    }
}
