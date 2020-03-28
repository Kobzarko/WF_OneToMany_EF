using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF_OneToMany_EF.Models
{
    class SoccerContext:DbContext
    {
        static SoccerContext()
        {
            Database.SetInitializer(new SoccerContextInitializer());
        }

        public SoccerContext():base("SoccerDb")
        {

        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
    }
}
