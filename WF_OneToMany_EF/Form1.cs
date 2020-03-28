using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WF_OneToMany_EF.Models;
using System.Data.Entity;

namespace WF_OneToMany_EF
{
    public partial class Form1 : Form
    {

        SoccerContext db;

        public Form1()
        {
            InitializeComponent();

            db = new SoccerContext();
            db.Players.Load();
            dataGridView1.DataSource = db.Players.Local.ToBindingList();

        }

        #region добавление
        private void button1_Click(object sender, EventArgs e)
        {
            PlayerForm plForm = new PlayerForm();

            // из команд в бд формируем список
            List<Team> teams = db.Teams.ToList();
            // присваиваем поле для списка 
            plForm.comboBox3.DataSource = teams;
            plForm.comboBox3.ValueMember = "Id";
            plForm.comboBox3.DisplayMember = "Name";



            DialogResult result = plForm.ShowDialog(this);

            if (result == DialogResult.Cancel)
            {
                return;
            }

            Player player = new Player();
            // добавляем возраст через поле в форме PlayerForm
            player.Age = (int)plForm.numericUpDown1.Value;
            // добавляем имя 
            player.Name = plForm.textBox1.Text;
            // выбираем позицию игрока 
            // добавить позиции в поле
            player.Position = plForm.comboBox1.SelectedItem.ToString();
            // добавляем игрока в БД
            db.Players.Add(player);
            
            // сохраняем изменения
            db.SaveChanges();

            MessageBox.Show(" Новый объект добавлен");
        }
        #endregion

        #region Редактирование

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;
                Player player = db.Players.Find(id);
                PlayerForm plForm = new PlayerForm();

                // выводим наши данные из БД в форму
                plForm.numericUpDown1.Value = player.Age;
                plForm.comboBox1.SelectedItem = player.Position;
                plForm.textBox1.Text = player.Name;

                // редактируем команду
                List<Team> teams = db.Teams.ToList();
                plForm.comboBox3.DataSource = teams;
                plForm.comboBox3.ValueMember = "Id";
                plForm.comboBox3.DisplayMember = "Name";

                // если игрок существует получаем связанную с игроком команду
                if (player.Team!=null)
                {
                    plForm.comboBox3.SelectedValue = player.TeamId;
                }

                DialogResult result = plForm.ShowDialog(this);

                if (result == DialogResult.Cancel)
                {
                    return;
                }
                // заносим наши отредактированные данные в БД
                player.Age = (int)plForm.numericUpDown1.Value;
                player.Position = plForm.comboBox1.SelectedItem.ToString();
                player.Name = plForm.textBox1.Text;

                // ссохраняем изменения
                db.SaveChanges();
                // обновляем таблицу
                dataGridView1.Refresh();
                MessageBox.Show("Объект обновлен");
            }
        }

        #endregion

        #region Удаление

        private void button3_Click(object sender, EventArgs e)
        {
            // если строк больше 0 то выполняем удаление
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                var id = 0;
                // 
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;
                // поиск по id нужного игрока
                Player player = db.Players.Find(id);
                // удаление записи
                db.Players.Remove(player);
                // сохраняем результат
                db.SaveChanges();
                MessageBox.Show("Запись была удалена");
            }


        }



        #endregion

        // открыть форму с командами
        private void button4_Click(object sender, EventArgs e)
        {
            AllTeams teams = new AllTeams();
            teams.Show(this);  // this?
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Приветствую");
        }
    }
}
