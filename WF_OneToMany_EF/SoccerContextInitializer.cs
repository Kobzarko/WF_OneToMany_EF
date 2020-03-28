using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using WF_OneToMany_EF.Models;

namespace WF_OneToMany_EF
{
    class SoccerContextInitializer : DropCreateDatabaseIfModelChanges<SoccerContext>
    {
        protected override void Seed(SoccerContext db)
        {

            List<Team> teams = new List<Team>()
            {
                new Team{ Name ="Man City", Coach = "Gvardiola"},
                new Team{Name ="Barcelona", Coach= "Quique Setién"},
                new Team{Name ="Juventus", Coach= "Maurizio Sarri"}
            };

            List<Player> players = new List<Player>()
            {
                new Player{ Name = "de Brune", Age = 26},
                new Player{ Name = "Messi", Age = 33 },
                new Player{ Name = "Buffon", Age = 37 }
            };

            db.Teams.AddRange(teams);
            db.Players.AddRange(players);
            db.SaveChanges();
        }

    }
}
