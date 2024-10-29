
using eITPR.Data;
using Microsoft.AspNetCore.Hosting.Server;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reactive;


namespace eITPR.Models
{
    public class Drawdown
    {
        public string pjkId { get; set; }
        public string? BudgetId { get; set; }
        public Decimal? drawdownCapex { get; set; }
        public Decimal? drawdownOpex { get; set; }
    }
}
