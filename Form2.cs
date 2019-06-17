using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace learning_french
{
    public partial class ChooseLesson : Form
    {
        String lang;

        public ChooseLesson()
        {
            InitializeComponent();
        }

        private void ChooseLesson_Load(object sender, EventArgs e)
        {
            lang = ConfigurationManager.AppSettings.Get("lang");
            if (lang == "en")
            {
                englishToolStripMenuItem_Click_1(sender, e);
            }
            else
            {
                franchToolStripMenuItem_Click(sender, e);
            }
            LoadLevelLesson();
            LoadLatestLesson();
        }

        private void LoadLevelLesson()
        {
            int level;
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-27M8JFH\SQLEXPRESS;Initial Catalog='learning french';Integrated Security=True");
            SqlCommand cmd = new SqlCommand("SELECT DISTINCT level FROM lessons", con);
            //cmd.Parameters.AddWithValue("@id", 1);
            try
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    int i = 0;
                    while (reader.Read())
                    {
                        level = (int)reader["level"];
                        i++;
                        Button btn = new Button();
                        btn.Name = level.ToString();
                        btn.Tag = level;
                        btn.Text = level.ToString();
                        btn.Location = new Point(3, 14 + 35 * i);
                        btn.BackColor = System.Drawing.Color.White;
                        btn.Width = 100;
                        btn.Height = 35;
                        //Hook our button up to our generic button handler
                        btn.Click += new System.EventHandler(this.level_Play_Click);
                        LevelLesson.Controls.Add(btn);
                    }
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadLatestLesson()
        {
            int id;
            String name;
            String path;
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-27M8JFH\SQLEXPRESS;Initial Catalog='learning french';Integrated Security=True");
            SqlCommand cmd = new SqlCommand("SELECT TOP 10 id, name, path FROM lessons ORDER BY id DESC ", con);
            //cmd.Parameters.AddWithValue("@id", 1);
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
                        i++;
                        Button btn = new Button();
                        btn.Name = id.ToString();
                        btn.Tag = id;
                        btn.Text = name;
                        btn.Location = new Point(3, 14 + 25 * i);
                        btn.Width = 200;
                        btn.Height = 25;
                        btn.BackColor = System.Drawing.Color.White;
                        //Hook our button up to our generic button handler
                        btn.Click += new System.EventHandler(this.lesson_Play_Click);
                        LatestLesson.Controls.Add(btn);
                    }
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void level_group_Paint(object sender, PaintEventArgs e)
        {

        }

        private void level_Play_Click(object sender, EventArgs e)
        {
            this.Hide();
            Lesson Lesson = new Lesson();
            Lesson.MyLevel = (int)(sender as Button).Tag;
            Lesson.StartPosition = FormStartPosition.Manual;
            Lesson.Left = 300;
            Lesson.Top = 100;
            Lesson.Show();
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

        private void LatestLesson_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void englishToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings["lang"].Value = "en";
            configuration.Save();
            ConfigurationManager.RefreshSection("appSettings");

            chooseToolStripMenuItem.Text = "Open";
            lessonToolStripMenuItem.Text = "Lesson";
            levelToolStripMenuItem.Text = "Level";
            loadToolStripMenuItem.Text = "Load";
            helpToolStripMenuItem.Text = "Help";
            stastisticToolStripMenuItem.Text = "Statistic";
            languageToolStripMenuItem.Text = "Language";
            englishToolStripMenuItem.Text = "English";
            franchToolStripMenuItem.Text = "French";
            addFileToolStripMenuItem.Text = "Add Lesson";
            addToolStripMenuItem.Text = "Add Level";
            aboutToolStripMenuItem.Text = "Sur";

            label1.Text = "Level";
            label3.Text = "Lessons Latest";
            back.Text = "<< Back";
        }

        private void franchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings["lang"].Value = "fr";
            configuration.Save();
            ConfigurationManager.RefreshSection("appSettings");

            chooseToolStripMenuItem.Text = "Ouvrir";
            lessonToolStripMenuItem.Text = "Leçon";
            levelToolStripMenuItem.Text = "Niveau";
            loadToolStripMenuItem.Text = "Charge";
            helpToolStripMenuItem.Text = "Aidez-moi";
            stastisticToolStripMenuItem.Text = "Statistique";
            languageToolStripMenuItem.Text = "La langue";
            englishToolStripMenuItem.Text = "Anglais";
            franchToolStripMenuItem.Text = "Français";
            addFileToolStripMenuItem.Text = "Ajouter une leçon";
            addToolStripMenuItem.Text = "Ajouter un niveau";
            aboutToolStripMenuItem.Text = "Sur";

            label1.Text = "Niveau";
            label3.Text = "Dernières leçons";
            back.Text = "<< Retour";
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form startForm = new StartedForm();
            startForm.StartPosition = FormStartPosition.Manual;
            startForm.Left = 300;
            startForm.Top = 100;
            startForm.Show();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
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

        private void back_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form startForm = new StartedForm();
            startForm.StartPosition = FormStartPosition.Manual;
            startForm.Left = 300;
            startForm.Top = 100;
            startForm.Show();
        }
    }
}
