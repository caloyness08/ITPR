
using eITPR.Data;
using Microsoft.AspNetCore.Hosting.Server;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reactive;


namespace eITPR.Models {

    public class PurchaseOrder {

        public string id { get; set; } = "";
        public string budgetId { get; set; } = "";
        public int budgetYear { get; set; }
        public int projectYear { get; set; }
        public string projectName { get; set; } = "";
        public string costCentre { get; set; } = "";
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public Decimal budgetCapex { get; set; }
        public Decimal budgetOpex { get; set; }
        public Decimal budgetRecOpex { get; set; }
        public Decimal ddCapex { get; set; }
        public Decimal ddOpex { get; set; }
        public Decimal ddRecOpex { get; set; }
        public Decimal invoiceCapex { get; set; }
        public Decimal invoiceOpex { get; set; }
        public Decimal prCapex { get; set; }
        public Decimal prOpex { get; set; }
        public Decimal poCapex { get; set; }
        public Decimal poOpex { get; set; }
        public string pjkOwner { get; set; } = "";
        public string projectManager { get; set; } = "";    
        public string projectStatus { get; set; } = "";

    }

}