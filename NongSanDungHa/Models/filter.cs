using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NongSanDungHa.Models
{
    public class filter
    {
       
            public string id { get; set; }
            public string name { get; set; }
            public filter(string ma, string ten)
            {
                this.id = ma;
                this.name = ten;
            }

        
    }
}