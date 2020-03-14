using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace WF_OneToMany_EF.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public int Age { get; set; }

        // поля связывающие таблицы
        public int? TeamId { get; set; }
        public virtual Team Team { get; set; }
    }
}
