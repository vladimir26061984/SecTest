﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploySet
{
    public class Employ
    {
        public Employ(string fam, string name, string patrName)
        {
            Fam = fam;
            Name = name;
            PatrName = patrName;
        }

        public string Fam { get; set; }
        public string Name { get; set; }
        public string PatrName { get; set; }
    }
}
