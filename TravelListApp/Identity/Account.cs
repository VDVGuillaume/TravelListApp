using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelListApp.Identity
{
    class Account
    {
        String UserId { get; set; }
        public String UserName { get; set;}
        String GivenName { get; set; }


        public Account(String userId,String userName, String givenName)
        {
            UserId = userId;
            UserName = userName;
            GivenName = givenName;

        }

    }
}
