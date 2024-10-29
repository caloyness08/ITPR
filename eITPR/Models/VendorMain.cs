
using DevExpress.Blazor.Internal.TreeListData;
using eITPR.Data;
using eITPR.Services;
using Microsoft.AspNetCore.Hosting.Server;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reactive;
using System.Security.Cryptography.Pkcs;


namespace eITPR.Models {

    public class vendorMain {
        public int id { get; set; } = 0;
        public int itprId { get; set; } = 0;
        public int amountTypeId { get; set; } = 0;
        public string vendorType { get; set; } = "";
        public string vendorName { get; set; } = "";
        public string contractNo { get; set; } = "";
        public string whtFlag { get; set; } = "";
        public string whtOwner { get; set; } = "";
        public decimal whtRate { get; set; } = 0.0m; 

    }

    public class vendorType {
        public int id { get; set; } = 1;
        public string desc { get; set; } = "";
        public int activeFlag { get; set; } = 1;

        private static IEnumerable<vendorType> _vendorType = new List<vendorType>();
        private static List<vendorType> _vendorTypeList = new List<vendorType>() {
            new vendorType {
                id = 1,
                desc = "NEW",
                activeFlag = 1,

            },
            new vendorType {
                id = 2,
                desc = "EXISTING",
                activeFlag = 1,

            }
        };

        public static Task<IEnumerable<vendorType>> GetVendorType() {
            _vendorType = _vendorTypeList;
            return Task.FromResult(_vendorType);
        }
    }

}