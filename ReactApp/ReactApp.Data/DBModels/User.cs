using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace ReactApp.Data.DBModels
{
    public class User : IdentityUser
    {
        public User()
        {
            
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int age { get; set; }

        public int? ProductsId { get; set; }

        public virtual HashSet<AllProducts> Products { get; set; } = new HashSet<AllProducts>();
    }
}
