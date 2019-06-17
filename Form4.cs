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
using System.IO;
using Microsoft.WindowsAPICodePack.Shell;
using System.Configuration;

namespace learning_french
{
    public partial class Listen : Form
    {
        String lang;

        public int Id { get; set; }

        private String Path { get; set; }
        private String Lyric { get; set; }

        private String[] LyricSentences;

        private String[] tracks;
        private int cur_track;

        private int max_track;

        private double[] track_score;

        private Boolean isCheck;
        private String input;

        private int progress = 0;
        int seconds = 0;

        Panel panel2 = new Panel();
        Button currentButton;

        public Listen()
        {
            InitializeComponent();
        }

        private void Listen_Load(object sender, EventArgs e)
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

            int level;
            string name;
            DateTime date;
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-27M8JFH\SQLEXPRESS;Initial Catalog='learning french';Integrated Security=True");
            SqlCommand cmd = new SqlCommand("SELECT lyric, path, level, name, created_at FROM lessons WHERE id = @id", con);
            cmd.Parameters.AddWithValue("@id", this.Id);
            try
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        this.Lyric = (String)reader["lyric"];
                        this.Path = (String)reader["path"];
                        level = (int)reader["level"];
                        name = (String)reader["name"];
                        date = (DateTime)reader["created_at"];
                    }
                    if (lang == "en")
                    {
                       
                    }
                    btnLevel.Text = "Level 1";
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            

            LyricSentences = this.Lyric.Split('#');
            for (int i = 0; i < LyricSentences.Length; i++)
                LyricSentences[i] = RemoveSpecialCharacters(LyricSentences[i]);

            this.tracks = Directory.GetFiles(@"C:\Users\Manh\Desktop\learning french\learning french\audio\" + this.Path, "*.mp3");
            this.cur_track = 0;
            this.max_track = LyricSentences.Length;
            this.isCheck = false;
        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {

        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void CheckValidInput(String[] words)
        {
            int x = 0, y = 0, z = 0;
            for (int i = 0; i < words.Length; i++)
            {
                if (x >= 40)
                {
                    y++;
                    x = 0;
                }
                String word = words[i];
                for (int j = 0; j < word.Length; j++)
                {
                    z++;
                    Button btn = new Button();
                    btn.Name = "BtnChar" + z.ToString();
                    btn.Text = word[j].ToString();

                    btn.Location = new Point(17 + x * 12, 13 + y * 23);
                    x++;
                    btn.Size = new Size(12, 22);
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.BackColor = Color.Transparent;
                    btn.FlatAppearance.MouseDownBackColor = Color.Transparent;
                    btn.FlatAppearance.MouseOverBackColor = Color.Transparent;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.BackColor = System.Drawing.Color.Azure;

                    char[] specials = { 'a', 'c', 'e', 'i', 'u', 'o' };
                    if (specials.Contains(word[j]))
                    {
                        btn.BackColor = System.Drawing.Color.Yellow;
                        btn.Click += new System.EventHandler(this.special_Character_Click);
                    }
                    panel1.Controls.Add(btn);
                }

                z++;
                Button btnSpace = new Button();
                btnSpace.Name = "BtnSpace" + z.ToString();
                btnSpace.Text = " ";
                btnSpace.Location = new Point(17 + x * 12, 13 + y * 23);
                x++;
                btnSpace.Size = new Size(12, 22);
                btnSpace.FlatStyle = FlatStyle.Flat;
                btnSpace.BackColor = Color.Transparent;
                btnSpace.FlatAppearance.MouseDownBackColor = Color.Transparent;
                btnSpace.FlatAppearance.MouseOverBackColor = Color.Transparent;
                btnSpace.FlatAppearance.BorderSize = 0;
                panel1.Controls.Add(btnSpace);
            }
        }

        private void special_Character_Click(object sender, EventArgs e)
        {
            currentButton = (Button)(sender as Button);
            Point point = (Point)(sender as Button).Location;
            panel2.Parent = this;
            panel2.BringToFront();
            panel2.Size = new Size(14, 5 * 24);
            panel2.Location = new Point(point.X, point.Y + 26);
            String Character = (String)(sender as Button).Text;
            String[] specials;

            String[] specialA = { "å", "à" };
            String[] specialC = { "ç" };
            String[] specialE = { "è", "ê" };
            String[] specialI = { "î", "ï" };
            String[] specialU = { "ü", "û" };
            String[] specialO = { "ö" };

            if (Character.Equals("a"))
            {
                specials = specialA;
            }
            else if (Character.Equals("c"))
            {
                specials = specialC;
            }
            else if (Character.Equals("e"))
            {
                specials = specialE;
            }
            else if (Character.Equals("i"))
            {
                specials = specialI;
            }
            else if (Character.Equals("o"))
            {
                specials = specialO;
            }
            else
            {
                specials = specialU;
            }
            panel2.Controls.Clear();

            for (int i = 0; i < specials.Length; i++)
            {
                Button btnSpecial = new Button();
                btnSpecial.Name = "BtnSpecial" + i.ToString();
                btnSpecial.Text = specials[i].ToString();
                btnSpecial.Location = new Point(0, i * 24);
                btnSpecial.Size = new Size(12, 22);
                btnSpecial.FlatStyle = FlatStyle.Flat;
                btnSpecial.BackColor = Color.Transparent;
                btnSpecial.FlatAppearance.MouseDownBackColor = Color.Transparent;
                btnSpecial.FlatAppearance.MouseOverBackColor = Color.Transparent;
                btnSpecial.FlatAppearance.BorderSize = 0;
                btnSpecial.BackColor = System.Drawing.Color.Azure;

                btnSpecial.Click += new System.EventHandler(this.change_Text_Button_Click);
                panel2.Controls.Add(btnSpecial);
            }
            panel2.Visible = true;
            panel1.Controls.Add(panel2);
        }

        private void change_Text_Button_Click(object sender, EventArgs e)
        {
            currentButton.Text = (String)(sender as Button).Text;
            panel2.Visible = false;
        }

        private void critique_Click(object sender, EventArgs e)
        {
            if (isCheck == false)
            {
                isCheck = true;
                this.input = textBox1.Text;
                textBox1.Visible = false;
                panel1.Visible = true;
                CheckValidInput(input.Split(' '));
                return;
            }

            StringBuilder sb = new StringBuilder();

            foreach (Control c in panel1.Controls)
            {
                if (c is Button)
                {
                    sb.Append((c as Button).Text);
                }
            }
            MessageBox.Show(sb.ToString());
            return;
            String[] UserInputWords;
            String[] LyricWords;
            MessageBox.Show(LyricSentences[0]);
            MessageBox.Show(LyricSentences[1]);
            UserInputWords = RemoveSpecialCharacters(textBox1.Text).Split(' ');
            LyricWords = this.LyricSentences[cur_track].Split(' ');
            ComputeScore(LyricWords, UserInputWords);

            // neu curent_track < max_track     curent_track++
            // neu curent_track = max_track     show score
        }

        public String RemoveSpecialCharacters(String str)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append('s');
            sb.Append(' ');
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] != '.' && str[i] != ','
                        && str[i] != '?' && str[i] != ':'
                        && str[i] != '!' && str[i] != ';'
                        && str[i] != ' ' && str[i] != '\t'
                        && str[i] != '\n' && str[i] != '\0')
                {
                    sb.Append(str[i]);
                }
                else
                {
                    sb.Append(' ');
                }
            }

            String tempo = sb.ToString();
            tempo = System.Text.RegularExpressions.Regex.Replace(tempo, @"\s+", " ");

            return tempo;
        }

        public void ComputeScore(String[] lyric, String[] input)
        {
            int[,] Score = new int[lyric.Length, input.Length];

            for (int i = 1; i < lyric.Length; i++)
                for (int j = 1; j < input.Length; j++)
                {
                    if (lyric[i].Equals(input[j]))
                        Score[i, j] = Score[i - 1, j - 1] + 1;
                    else if (Score[i, j - 1] > Score[i - 1, j])
                        Score[i, j] = Score[i, j - 1];
                    else
                        Score[i, j] = Score[i - 1, j];
                }

            int practice = Score[lyric.Length - 1, input.Length - 1];
            int score = (practice * 100) / (lyric.Length - 1);
            //MessageBox.Show(Score[lyric.Length - 1, input.Length - 1].ToString());
            MessageBox.Show("Score: " + practice + "/" + lyric.Length);
        }

        private void btnLevel_Click(object sender, EventArgs e)
        {
            WMPlayer.close();
            this.Hide();
            Lesson Lesson = new Lesson();
            Lesson.MyLevel = (int)btnLevel.Tag;
            Lesson.StartPosition = FormStartPosition.Manual;
            Lesson.Left = 300;
            Lesson.Top = 100;
            Lesson.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void back_Click(object sender, EventArgs e)
        {
            WMPlayer.close();
            this.Hide();
            Form ChooseLesson = new ChooseLesson();
            ChooseLesson.StartPosition = FormStartPosition.Manual;
            ChooseLesson.Left = 300;
            ChooseLesson.Top = 100;
            ChooseLesson.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ShellFile so = ShellFile.FromFilePath(this.tracks[this.cur_track]);
            double nanoseconds;
            double.TryParse(so.Properties.System.Media.Duration.Value.ToString(), out nanoseconds);
            seconds = (int)Convert100NanosecondsToMilliseconds(nanoseconds) / 1000 + 1;

            MessageBox.Show(seconds.ToString());
            progress = 0;
            progressBar1.Value = 0;
            progressBar1.Maximum = seconds;
            timer1.Interval = seconds;
            timer1.Start();
            WMPlayer.URL = this.tracks[this.cur_track];
        }

        public static double Convert100NanosecondsToMilliseconds(double nanoseconds)
        {
            return nanoseconds * 0.0001;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progress += 1;
            if (progress >= seconds)
            {
                timer1.Enabled = false;
                timer1.Stop();
            }
            progressBar1.Value = progress;
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

            label1.Text = "Enter the content you hear in the space below:";
            btnLevel.Text = "Level 1";
            label3.Text = "Create at:";
            critique.Text = "Criticize";
            back.Text = "<< Back";
            btnPlay.Text = "Play";
            backToolStripMenuItem.Text = "<< Back";
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

            label1.Text = "Entrez le contenu que vous entendez dans l'espace ci-dessous:";
            btnLevel.Text = "Jouer 1";
            label3.Text = "Créer à:";
            critique.Text = "Critiquer";
            back.Text = "<< Retour";
            btnPlay.Text = "Jouer";
            backToolStripMenuItem.Text = "<< Retour";
        }

        private void backToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WMPlayer.close();
            this.Hide();
            Lesson Lesson = new Lesson();
            Lesson.MyLevel = (int)btnLevel.Tag;
            Lesson.StartPosition = FormStartPosition.Manual;
            Lesson.Left = 300;
            Lesson.Top = 100;
            Lesson.Show();
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
