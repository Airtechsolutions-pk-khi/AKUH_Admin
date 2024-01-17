using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKU_Admin._Models
{
    public class appSettingViewModel
    {
    }

    public class AppSetingBLL
    {
        public int SettingID { get; set; }

        public string About { get; set; }

        public string PrivacyPolicy { get; set; }
        public string? SplashScreen { get; set; } = "";

        public string AppName { get; set; }
        public string FacebookUrl { get; set; }
        public string InstagramUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string YoutubeUrl { get; set; }

        public string AppVersion { get; set; }

        public int? StatusID { get; set; }

        public DateTime? Createdon { get; set; }

        public DateTime? Updatedon { get; set; }
    }

}
