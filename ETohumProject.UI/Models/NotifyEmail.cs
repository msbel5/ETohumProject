using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.ApplicationInsights.Web;
using Postal;

namespace ETohumProject.UI.Models
{
    public class NotifyEmail:Email
    {
        //Email gönderme aracı olan "postal" için gerekli class, aynı zaman "Notify" adında view oluşturulması gerekiyor. 
        public string To { get; set; }

        public string Message { get; set; }
    }
}