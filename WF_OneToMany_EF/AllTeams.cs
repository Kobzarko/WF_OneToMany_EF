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
        // благодаря lazy loading мы можем получить 
        // связанных с командой игроков и загрузить их
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
                    MessageBox.Show("список пуст");
                    return;
                }
                // поиск команды в базе данных
                Team team = db.Teams.Find(id);
                // вкладываем в listbox список игроков
                listBox1.DataSource = team.Players.ToList();
                // отображаем имена
                listBox1.DisplayMember = "name";

            }
        }

        #endregion

        // при удалении мы предварительно очищаем данный список: team.Players.Clear();
        #region Удаление

        private void button3_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.SelectedRows[0].Index;
            int id = 0;
            // возвращает значение указывающее успешно ли выполнено преобразование
            bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);

            if (converted == false)
            {
                return;
            }

            Team team = db.Teams.Find(id);
            // удалить все элементы коллекции из команды
            team.Players.Clear();
            // удалить команду
            db.Teams.Remove(team);
            // сохранить изменения в бд
            db.SaveChanges();

            MessageBox.Show("Объект удален");
        }

        #endregion

        #region редактирование

        private void button2_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.SelectedRows[0].Index;
            int id = 0;
            bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);

            if (converted == false)
            {
                return;
            }
            // поиск команды
            Team team = db.Teams.Find(id);
            // создаем новую форму присваиваем поля
            TeamForm teamForm = new TeamForm();
            teamForm.textBox1.Text = team.Name;
            teamForm.textBox3.Text = team.Coach;

            // выводим значение в форме
            DialogResult dialogResult = teamForm.ShowDialog(this);
            if (dialogResult == DialogResult.Cancel)
            {
                return;
            }
            // присваиваем новое значение
            team.Name = teamForm.textBox1.Text;
            team.Coach = teamForm.textBox3.Text;
            // сохраняем
            db.Entry(team).State = EntityState.Modified;
            db.SaveChanges();
            // сообщаем
            MessageBox.Show("Запись обновлена");


        }

        #endregion



        private void AllTeams_Load(object sender, EventArgs e)
        {

        }

      
    }
}
