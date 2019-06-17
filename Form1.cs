using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Media;
using WMPLib;
using System.Configuration;

namespace learning_french
{
    public partial class StartedForm : Form
    {
        String lang;

        public StartedForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Left = 300;
            this.Top = 100;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lang = ConfigurationManager.AppSettings.Get("lang");
            if (lang == "en")
            {
                englishToolStripMenuItem_Click(sender, e);
            }
            else
            {
                franchToolStripMenuItem_Click(sender, e);
            }
        }

        private void play_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form ChooseLesson = new ChooseLesson();
            ChooseLesson.StartPosition = FormStartPosition.Manual;
            ChooseLesson.Left = 300;
            ChooseLesson.Top = 100;
            ChooseLesson.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lessonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form ChooseLesson = new ChooseLesson();
            ChooseLesson.StartPosition = FormStartPosition.Manual;
            ChooseLesson.Left = 300;
            ChooseLesson.Top = 100;
            ChooseLesson.Show();
        }

        private void chooseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings["lang"].Value = "en";
            configuration.Save();
            ConfigurationManager.RefreshSection("appSettings");

            chooseToolStripMenuItem.Text = "Open";
            lessonToolStripMenuItem.Text = "Play";
            loadToolStripMenuItem.Text = "Load";
            helpToolStripMenuItem.Text = "Help";
            stastisticToolStripMenuItem.Text = "Statistic";
            languageToolStripMenuItem.Text = "Language";
            englishToolStripMenuItem.Text = "English";
            franchToolStripMenuItem.Text = "French";
            addFileToolStripMenuItem.Text = "Add Lesson";
            addToolStripMenuItem.Text = "Add Level";
            aboutToolStripMenuItem.Text = "Sur";

            label1.Text = "Learning French";
            play.Text = "Play";
        }

        private void franchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings["lang"].Value = "fr";
            configuration.Save();
            ConfigurationManager.RefreshSection("appSettings");

            chooseToolStripMenuItem.Text = "Ouvrir";
            lessonToolStripMenuItem.Text = "Jouer";
            loadToolStripMenuItem.Text = "Charge";
            helpToolStripMenuItem.Text = "Aidez-moi";
            stastisticToolStripMenuItem.Text = "Statistique";
            languageToolStripMenuItem.Text = "La langue";
            englishToolStripMenuItem.Text = "Anglais";
            franchToolStripMenuItem.Text = "Français";
            addFileToolStripMenuItem.Text = "Ajouter une leçon";
            addToolStripMenuItem.Text = "Ajouter un niveau";
            aboutToolStripMenuItem.Text = "Sur";

            label1.Text = "Apprendre le français";
            play.Text = "Jouer";
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

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
