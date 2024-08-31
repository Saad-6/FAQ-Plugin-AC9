using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAQPlugin.Models
{
    public class FAQSettings
    {
        public bool AllowAnonymousUsers {  get; set; }
        public string DefaultResponderName { get; set; }
        public FAQSettings()
        {
        AllowAnonymousUsers = true;
        DefaultResponderName = "Admin";
        }
    }
}
