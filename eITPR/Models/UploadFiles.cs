namespace eITPR.Models {
    public class UploadFiles {

        public int id { get; set; } = 0;
        public string fileName { get; set; } = "";
        public string fileSize { get; set; } = "";
        public string fileDesc { get; set; } = "";
        public DateTime uploadDate { get; set; } = DateTime.Now;

     

        private static IEnumerable<UploadFiles> _uploadMainList = new List<UploadFiles>();
        private static List<UploadFiles> _uploadMain = new List<UploadFiles>() {
            new UploadFiles {
                id = 1,
                fileName = "ITPR - (RESO).pdf",
                fileSize = "38KB",
                fileDesc = "IT Spend Paper w/ sign-off.",
                uploadDate = DateTime.Now

            },
            new UploadFiles {
                id = 1,
                fileName = "Meeting Minutes.xlsx",
                fileSize = "264KB",
                fileDesc = "Minutes Resolution.",
                uploadDate = DateTime.Now

            }
        };

        public static Task<IEnumerable<UploadFiles>> GetUploadFiles() {
            _uploadMainList = _uploadMain;
            return Task.FromResult(_uploadMainList);
        }
    }
}
