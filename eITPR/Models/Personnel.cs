
using eITPR.Data;
using Microsoft.AspNetCore.Hosting.Server;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reactive;


namespace eITPR.Models
{
    public class Personnel
    {
        public int id { get; set; }
        public int roleId { get; set; }
        public string roleDesc { get; set; } = "";
        public string name { get; set; } = "";
        public string email { get; set; } = "";


        private static IEnumerable<Personnel> _personnel = new List<Personnel>();
        private static List<Personnel> _personnelList = new List<Personnel>() {
            new Personnel {
                id = 1,
                roleDesc= "Preparer",
                name = "John Carlo Gabuya",
                email = "johncg.gabuya@maybank.com"
            }
        };

        public static Task<IEnumerable<Personnel>> GetPersonnel() {
            _personnel = _personnelList;
            return Task.FromResult(_personnel);
        }
    }
}
