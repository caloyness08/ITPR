namespace eITPR.Data {
    public class DapperQueryString {

        public  string BudgetID_ProjectView { get; set; } = $"SELECT * FROM [itpr].BudgetID_ProjectView";

        public string GetSubmissions { get; set; } = $"SELECT * FROM [itpr].GetSubmissionsView ORDER BY id DESC";

        public string GetContractsList { get; set; } = $"SELECT * FROM [itpr].GetVendorMasterView";

        public string GetVendorNameList { get; set; } = $"SELECT DISTINCT UPPER(vendorName) as vendorName FROM [itpr].GetVendorMasterView";

        public string GetVendorMain { get; set; } =  $"SELECT * FROM [itpr].GetVendorMainView";
    }
}
