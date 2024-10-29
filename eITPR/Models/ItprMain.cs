
using DevExpress.Blazor.Internal.TreeListData;
using eITPR.Data;
using eITPR.Services;
using Microsoft.AspNetCore.Hosting.Server;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reactive;
using System.Security.Cryptography.Pkcs;


namespace eITPR.Models {

    public class ItprMain {

        public int id { get; set; } = 0;
        public string itprId { get; set; } = "";
        public string budgetId { get; set; } = "";
        public string warrantId { get; set; } = "";
        public string projectName { get; set; } = "";
        public string costCentre { get; set; } = "";
        public string entity { get; set; } = "";
        public int? currencyId { get; set; } = 0;
        public Decimal capexAmount { get; set; } = 0.0m;
        public Decimal opexAmount { get; set; } = 0.0m;
        public Decimal totalAmount { get; set; } = 0.0m;
        public int typeId { get; set; } = 0;
        public int statusId { get; set; } = 0;
        public DateTime createdDate { get; set; } = DateTime.Now;
        public string createdBy { get; set; } = "";
        public DateTime updatedDate { get; set; } = DateTime.Now;
        public string updatedBy { get; set; } = "";
        public int? budgetYear { get; set; } = 0;

      
    }

    public class ItprMainView {

        public int id { get; set; } = 0;
        public string itprId { get; set; } = "";
        public string budgetId { get; set; } = "";
        public string warrantId { get; set; } = "";
        public string projectName { get; set; } = "";
        public string costCentre { get; set; } = "";
        public string entity { get; set; } = "";
        public string entityName { get; set; } = "";
        public string vendorName { get; set; } = "";
        public string currencyName { get; set; } = "";
        public Decimal capexAmount { get; set; } = 0.0m;
        public Decimal opexAmount { get; set; } = 0.0m;
        public Decimal totalAmount { get; set; } = 0.0m;
        public string typeName { get; set; } = "";
        public string statusName { get; set; } = "";
        public int statusID { get; set; } = 0;
        public DateTime createdDate { get; set; } = DateTime.Now;
        public string createdBy { get; set; } = "";
        public DateTime updatedDate { get; set; } = DateTime.Now;
        public string updatedBy { get; set; } = "";
        public int? budgetYear { get; set; } = 0;


    }
    public class ItprMain_State {

        public event Action OnStateChange;
        public ItprMain mainVal { get; set; }


        public void SetValue(ItprMain value) {
            this.mainVal = value;
            NotifyStateChanged();
        }
        private void NotifyStateChanged() => OnStateChange?.Invoke();
    }

    public class ItprType {
        public int id { get; set; } = 1;
        public string desc { get; set; } = "";
        public int activeFlag { get; set; } = 1;

        private static IEnumerable<ItprType> _itprType = new List<ItprType>();
        private static List<ItprType> _itprTypeList = new List<ItprType>() {
            new ItprType {
                id = 1,
                desc = "CAPEX",
                activeFlag = 1,

            },
            new ItprType {
                id = 2,
                desc = "ONE-TIME OPEX",
                activeFlag = 1,

            },
               new ItprType {
                id = 3,
                desc = "RECURRING OPEX",
                activeFlag = 1,

            }
        };

        public static Task<IEnumerable<ItprType>> GetItprType() {
            _itprType = _itprTypeList;
            return Task.FromResult(_itprType);
        }
    }



}