using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WF_OneToMany_EF.Models;


namespace WF_OneToMany_EF
{
    public partial class AllTeams : Form
    {
        SoccerContext db;
        public AllTeams()
        {
            InitializeComponent();
            // загружаем бд в форму
            db = new SoccerContext();
            db.Teams.Load();
            dataGridView1.DataSource = db.Teams.Local.ToBindingList();

        }

        #region добавление

        private void button1_Click(object sender, EventArgs e)
        {

            // запускаем форму с командами
            TeamForm tmForm = new TeamForm();
            DialogResult dialogResult = tmForm.ShowDialog(this);

            if (dialogResult == DialogResult.Cancel)
            {
                return;
            }

            // создаем команду

            Team team = new Team
            {
                Name = tmForm.textBox1.Text,
                Coach = tmForm.textBox3.Text
            };

            // добавляем команду  в БД 
            db.Teams.Add(team);
            db.SaveChanges();
            // покажем что сохранилось
            MessageBox.Show("Команда добавлена");
        }


        #endregion

        #region просмотр списка игроков команды

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // относительная позиция диапазона в элементе
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);

                if (converted == false)
                {
                    return;
                }
            }
        }

        #endregion




        private void AllTeams_Load(object sender, EventArgs e)
        {

        }

       
    }
}
