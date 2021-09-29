using Microsoft.AspNetCore.Http;
using Services.Server.DataAccessLayer;
using Services.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Server.Helper
{
    public static class CurrentUser
    {
        public static string Id { set; get; }
        public static string UserData { set; get; }
    }
}
