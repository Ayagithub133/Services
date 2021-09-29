using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Server.Services
{
    public  class SMSCode
    {
        public SMSCode()
        {
            random = GenerateRandomNo();
        }

        private int GenerateRandomNo()
        {
            int _min = 100000;
            int _max = 999999;
            Random random = new Random();
            return random.Next(_min, _max);
        }
        public static int random { set; get; }
    }
}
