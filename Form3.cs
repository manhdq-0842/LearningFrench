using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace learning_french
{
    public partial class Lesson : Form
    {
        String lang;
        public int MyLevel { get; set; }

        public Lesson()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            lang = ConfigurationManager.AppSettings.Get("lang");
            if (lang == "en")
            {
                englishToolStripMenuItem_Click(sender, e);
            }
            else
            {
                franchToolStripMenuItem_Click_1(sender, e);
            }
            labelLevel.Text = "Level " + MyLevel;

            int id;
            String name;
            String path;
            DateTime datecr;
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-27M8JFH\SQLEXPRESS;Initial Catalog='learning french';Integrated Security=True");
            SqlCommand cmd = new SqlCommand("SELECT id, name, path, created_at FROM lessons WHERE level=@level", con);
            cmd.Parameters.AddWithValue("@level", this.MyLevel);
            try
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    int i = 0;
                    while (reader.Read())
                    {
                        id = (int)reader["id"];
                        name = (String)reader["name"];
                        path = (String)reader["path"];
                        datecr = (DateTime)reader["created_at"];
                        i++;

                        Button label = new Button();
                        label.Name = "Lesson" + id;
                        label.Text = "Lesson " + id;
                        label.Width = 80;
                        label.Height = 25;
                        label.Tag = id;
                        label.Location = new Point(3, 14 + 25 * i);
                        label.BackColor = System.Drawing.Color.White;
                        label.ForeColor = System.Drawing.Color.Black;
                        label.Click += new System.EventHandler(this.lesson_Play_Click);
                        lessonlevel.Controls.Add(label);

                        Button btn = new Button();
                        btn.Name = id.ToString();
                        btn.Tag = id;
                        btn.Text = name;
                        btn.Location = new Point(3 + 80, 14 + 25 * i);
                        btn.Width = 200;
                        btn.Height = 25;
                        btn.BackColor = System.Drawing.Color.White;
                        //Hook our button up to our generic button handler
                        btn.Click += new System.EventHandler(this.lesson_Play_Click);
                        lessonlevel.Controls.Add(btn);

                        Button date = new Button();
                        date.Name = "date" + id;
                        date.Text  = "Date Created: " + datecr.ToString("dd/MM/yyyy");
                        date.Width = 200;
                        date.Height = 25;
                        date.Tag = id;
                        date.Location = new Point(3 + 80 + 200, 14 + 25 * i);
                        date.BackColor = System.Drawing.Color.White;
                        date.ForeColor = System.Drawing.Color.Black;
                        date.Click += new System.EventHandler(this.lesson_Play_Click);
                        lessonlevel.Controls.Add(date);
                    }
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lesson_Play_Click(object sender, EventArgs e)
        {
            this.Hide();
            Listen Listen = new Listen();
            Listen.Id = (int)(sender as Button).Tag;
            Listen.StartPosition = FormStartPosition.Manual;
            Listen.Left = 300;
            Listen.Top = 100;
            Listen.Show();
        }

        private void back_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form ChooseLesson = new ChooseLesson();
            ChooseLesson.StartPosition = FormStartPosition.Manual;
            ChooseLesson.Left = 300;
            ChooseLesson.Top = 100;
            ChooseLesson.Show();
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings["lang"].Value = "en";
            configuration.Save();
            ConfigurationManager.RefreshSection("appSettings");

            chooseToolStripMenuItem.Text = "Open";
            lessonToolStripMenuItem.Text = "Lesson";
            loadToolStripMenuItem.Text = "Load";
            helpToolStripMenuItem.Text = "Help";
            stastisticToolStripMenuItem.Text = "Statistic";
            languageToolStripMenuItem.Text = "Language";
            englishToolStripMenuItem.Text = "English";
            franchToolStripMenuItem.Text = "French";
            addFileToolStripMenuItem.Text = "Add Lesson";
            addToolStripMenuItem.Text = "Add Level";
            aboutToolStripMenuItem.Text = "Sur";

            backToolStripMenuItem.Text = "<< Back";
            labelLevel.Text = "Level";
            back.Text = "<< Back";
        }

        private void franchToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings["lang"].Value = "fr";
            configuration.Save();
            ConfigurationManager.RefreshSection("appSettings");

            chooseToolStripMenuItem.Text = "Ouvrir";
            lessonToolStripMenuItem.Text = "Leçon";
            loadToolStripMenuItem.Text = "Charge";
            helpToolStripMenuItem.Text = "Aidez-moi";
            stastisticToolStripMenuItem.Text = "Statistique";
            languageToolStripMenuItem.Text = "La langue";
            englishToolStripMenuItem.Text = "Anglais";
            franchToolStripMenuItem.Text = "Français";
            addFileToolStripMenuItem.Text = "Ajouter une leçon";
            addToolStripMenuItem.Text = "Ajouter un niveau";
            aboutToolStripMenuItem.Text = "Sur";

            backToolStripMenuItem.Text = "<< Retour";
            labelLevel.Text = "Niveau";
            back.Text = "<< Retour";
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void backToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form ChooseLesson = new ChooseLesson();
            ChooseLesson.StartPosition = FormStartPosition.Manual;
            ChooseLesson.Left = 300;
            ChooseLesson.Top = 100;
            ChooseLesson.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form5 Listen = new Form5();
            Listen.StartPosition = FormStartPosition.Manual;
            Listen.Left = 300;
            Listen.Top = 100;
            Listen.Show();
        }
    }
}
