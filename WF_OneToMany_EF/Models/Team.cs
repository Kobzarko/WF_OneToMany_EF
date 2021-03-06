﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace WF_OneToMany_EF.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; } // имя команды
        public string Coach { get; set; } // тренер

        // поле для связки таблиц
        public virtual ICollection<Player> Players { get; set; }

        public Team()
        {
            Players = new List<Player>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
