
using DevExpress.Blazor.Internal.TreeListData;
using eITPR.Data;
using eITPR.Services;
using Microsoft.AspNetCore.Hosting.Server;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reactive;
using System.Security.Cryptography.Pkcs;


namespace eITPR.Models {

    public class VendorMain {

        public int id { get; set; } = 0;
        public string glCode { get; set; } = "";
        public string glDesc { get; set; } = "";
        public string glDesc2 { get; set; } = "";
        public string itprDesc { get; set; } = "";
      
        public DateTime startDate { get; set; } = DateTime.Now;
        public DateTime endDate { get; set; } = DateTime.Now;
        public int currencyId { get; set; } = 0;
        public string currencyName { get; set; } = "";
        public Decimal amountMyr { get; set; } = 0.00m;
        public Decimal amountLocal { get; set; } = 0.00m;


        private static IEnumerable<VendorMain> _vendorMainList = new List<VendorMain>();
        private static List<VendorMain> _vendorMain = new List<VendorMain>() {
            new VendorMain {
                id = 1,
                glCode = "1318488",
                glDesc = "EGL FA-CIP ASSET CLG",
                glDesc2 = "EGL FA-CIP ASSET CLEARING AC",
                currencyName = "MYR"

            },
            new VendorMain {
                id = 2,
                glCode = "1318459",
                glDesc = "FA ACQUISITION",
                glDesc2 = "FA ACQUISITION",
                currencyName = "MYR"

            }
        };

        public static Task<IEnumerable<VendorMain>> GetVendorMain() {
            _vendorMainList = _vendorMain;
            return Task.FromResult(_vendorMainList);
        }

    }

 

}