using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace learning_french
{
    public partial class Form5 : Form
    {
        String lang;
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void backToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form startForm = new StartedForm();
            startForm.StartPosition = FormStartPosition.Manual;
            startForm.Left = 300;
            startForm.Top = 100;
            startForm.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings["lang"].Value = "en";
            configuration.Save();
            ConfigurationManager.RefreshSection("appSettings");

            label1.Text = "Group of Authors";
            label8.Text = "Copyright 2018";
        }

        private void franchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings["lang"].Value = "fr";
            configuration.Save();
            ConfigurationManager.RefreshSection("appSettings");

            label1.Text = "Groupe d'auteurs";
            label8.Text = "Droits d'auteur";
        }
    }
}
