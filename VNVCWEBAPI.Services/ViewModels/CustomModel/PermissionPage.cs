using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.Services.ViewModels.CustomModel
{
    public class PermissionPage
    {
        public PermissionPage()
        {
            PermissionPageDetails = new List<PermissionPageDetails>();
        }
        public string PageName { get; set; }
        public List<PermissionPageDetails> PermissionPageDetails { get; set; }
    }
    public class PermissionPageDetails {
        public string Permission { get; set; }
        public bool isSelect { get; set; }
    }
}
