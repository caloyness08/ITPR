

using Microsoft.AspNetCore.Hosting.Server;
using System.ComponentModel.DataAnnotations;
using System.Reactive;

namespace eITPR.Models
{
    public class Constants {

        public string lbl_budgetID { get; set; } = "Budget ID:";
        public string lbl_costCentre { get; set; } = "Cost Centre:";
        public string lbl_prjName { get; set; } = "Project Name:";

        //DRAWDOWN
        public string lbl_Capex { get; set; } = "CAPEX:";
        public string lbl_OneTimeOpex { get; set; } = "One Time OPEX:";
        public string lbl_RecOpex { get; set; } = "Recurring OPEX:";

        public string ddCapex { get; set; } = "0";
        public string ddOpex { get; set; } = "0";
        public string ddRecOpex { get; set; } = "0";


        //PURCHASE ORDER
        public string lbl_Opex { get; set; } = "OPEX:";

        public string poCapex { get; set; } = "0";
        public string poOpex { get; set; } = "0";

        //Available Budget

        public string totalCapex { get; set; } = "0";
        public string totalOpex { get; set; } = "0";

        //Notifications
        public string toolTip_budid { get; set; } = "If BUDID has not yet been created in Clarity, please liaise with Clarity Team.";
        public string toolTip_btnNext1 { get; set; } = "Avaible Budget CAPEX / OPEX must be sufficient to proceed.";
        public string toolTip_typeName2 { get; set; } = "Select ITPR Cost Type";
        public string toolTip_vendorType { get; set; } = "NEW = Provide name manually   " +
                                                         "EXISTING = Select from vendor name from list of vendor below.";
        public string toolTip_vendorName { get; set; } = "Select / Search Vendor Name...";
        public string toolTip_contractID { get; set; } = "Contract ID example - (MBB/MY/IT/1111)";
        public string toolTip_vendorTab_1 { get; set; } = "Please fill up required field to proceed.";
    }
}
 