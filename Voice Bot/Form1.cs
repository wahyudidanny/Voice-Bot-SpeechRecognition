using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;//
using System.Speech.Recognition;//
using System.Diagnostics;//
using System.Xml;
using System.IO;
using System.Data.SqlClient;

namespace Voice_Bot
{


    public partial class Form1 : Form
    {
        SpeechSynthesizer speech = new SpeechSynthesizer();
        Choices list = new Choices();
        Boolean wake = true;
        String temp;
        String condition;
        String name = "danny";
        String namePath = @"E:/name.txt";
        
        Boolean varl = false;



        public Boolean search = false;

        public Form1()
        {
            SpeechRecognitionEngine rec = new SpeechRecognitionEngine();

          list.Add(File.ReadAllLines(@"C:\Users\dannywahyudi\Documents\Visual Studio 2015\Projects\Voice Bot\Voice Bot Command\commands.txt")); 
           // list.Add(File.ReadAllLines(@"C:\Users\dannywahyudi\Documents\Visual Studio 2015\Projects\Voice Bot\Voice Bot Command\commandfullgrammar.txt")); //so full grammar not really good, not recommended

            //list.Add(new String[] { "hello, how are you" ,
            //                        "hi",
            //                        "hello","what time is it",
            //                        "what is today",
            //                        "open firefox",
            //                        "sleep",
            //                        "wake",
            //                        "restart",
            //                        "update",
            //                        "open 9gag",
            //                        "open youtube",
            //                        "are you there",
            //                        "open microsoft word",
            //                        "close word",
            //                        "whats the weather like",
            //                        "whats the temperature",
            //                        "hey amy",
            //                        "minimize",
            //                        "maximize",
            //                        "unminimize",
            //                        "play",
            //                        "pause",
            //                        "spotify",
            //                        "next",
            //                        "last"});

            Grammar gr = new Grammar(new GrammarBuilder(list));

            try
            {
                rec.RequestRecognizerUpdate();
                rec.LoadGrammar(gr);
                rec.SpeechRecognized += rec_speechRecognized;
                rec.SetInputToDefaultAudioDevice();
                rec.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch { return; }

            speech.SelectVoiceByHints(VoiceGender.Female);
            speech.Speak("hello, my name is voice bot");
            InitializeComponent();
        }



        public String GetWeather(String input)
        {
            String query = String.Format("https://query.yahooapis.com/v1/public/yql?q=select * from weather.forecast where woeid in (select woeid from geo.places(1) where text='Jakarta, Indonesia')&format=xml&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys");
            XmlDocument wData = new XmlDocument();


            try
            {
                wData.Load(query);
            }
            catch {

                MessageBox.Show("No Internet");
                return "No internet";

            }



            XmlNamespaceManager manager = new XmlNamespaceManager(wData.NameTable);
            manager.AddNamespace("yweather", "http://xml.weather.yahoo.com/ns/rss/1.0");

            XmlNode channel = wData.SelectSingleNode("query").SelectSingleNode("results").SelectSingleNode("channel");
            XmlNodeList nodes = wData.SelectNodes("query/results/channel");

            try
            {
               int rawtemp = int.Parse(channel.SelectSingleNode("item").SelectSingleNode("yweather:condition", manager).Attributes["temp"].Value);
                temp = (rawtemp - 32) * 5 / 9 + "";
               // temp = channel.SelectSingleNode("item").SelectSingleNode("yweather:condition", manager).Attributes["temp"].Value;
                condition = channel.SelectSingleNode("item").SelectSingleNode("yweather:condition", manager).Attributes["text"].Value;
               
                if (input == "temp")
                {
                    return temp;
                }
               
                if (input == "cond")
                {
                    return condition;
                }
            }
            catch
            {
                return "Error Reciving data";
            }
            return "error";
        }




        public void restart()
        {
            Process.Start(@"C:\Users\dannywahyudi\Documents\Visual Studio 2015\Projects\Voice Bot\Voice Bot\bin\Debug\Voice Bot.exe");
            Environment.Exit(0);
        }

        public void say(String h)
        {

            speech.SpeakAsync(h); // command akan dijalan kan bersamaan

            //if (h == "hey amy")
            //{

            //}
            //else {
            // speech.Speak(h); // command akan dijalan kan satu persatu
            //// wake = false;
            textBox2.AppendText(h + "\n");

            //   }
        }

        public static void killprog(String s) {

              System.Diagnostics.Process[] procs = null;
           // Process[] procs = null;
            try
            {
                procs = Process.GetProcessesByName(s);
                Process prog = procs[0];

                if (!prog.HasExited){
                    prog.Kill();
                }
            }
            finally{
                if (procs != null){
                    foreach (Process p in procs) {
                        p.Dispose();
                    }
                }                        
              }
            procs = null;

        }





        String[] greetings = new String[3] { "hi", "hello", "hi, how are you" };

        public String greetings_action() {
            Random r = new Random();
            return greetings[r.Next(3)];
        }


        private void rec_speechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string r = e.Result.Text;

            //if (r == "hey amy")
            //{
            //    say(greetings_action());
            //    wake = true;
            //}

            //if (r == "wake") { wake = true; label3.Text = "State : Awake";  }
            //if (r == "sleep") { wake = false; label3.Text = "State : Sleep"; }

            if (search)
            {
                Process.Start("https://www.google.com/search?q=" + r);
                search = false;
               // Process.Start("http://www.firefox.com");
            }

            if (wake == true && search == false)
            {

                if (r == "do i like fastfood")
                {
                    if (!varl)
                        say("no "+ name + " , you dont like fast food");

                    if(varl)
                        say("yes " + name + ", you  like fast food");
                }

                if (r == "spotify")
                {
                   Process.Start(@"C:\Users\dannywahyudi\AppData\Roaming\Spotify\Spotify.exe");
                    
                }

                if (r == "close program")
                {
                    say("Thank You for using the bot");
                    this.Close();
                    //Process test;
                    //test = Process.Start(@"C:\Users\dannywahyudi\Documents\Visual Studio 2015\Projects\Voice Bot\Voice Bot\bin\Debug\Voice Bot.exe");
                    //test.CloseMainWindow();
                    //test.Close();
                }


                if (r == "search for")
                {
                    search = true;
                }

                if (r == "next")
                {
                    SendKeys.Send("^{RIGHT}");
                }

                if (r == "what the fuck")
                {
                    say("please , dont swear, its a bad habit");
                }

                if (r == "tell me yo mama joke")
                {
                    say("yo mama is so fat, when thanos snapped his finger, she only losing weight");
                }
                if (r == "stop")
                {
                    speech.SpeakAsyncCancelAll();
                }

                if (r == "mute")
                {
                    SendKeys.Send("^{RIGHT}^{RIGHT}");
                }


                if (r == "last")
                {
                    SendKeys.Send("^{LEFT}");
                }


                if (r == "play" || r == "pause")
                {
                    SendKeys.Send(" ");
                }

                if (r == "whats my name")
                {
                    //  say(name);
                    say(File.ReadAllText(namePath));
                }

                if (r == "whats the weather like")
                {
                    say(" the sky is " + GetWeather("cond") + " ");
                }

                if (r == "minimize")
                {
                    this.WindowState = FormWindowState.Minimized;
                }
                if (r == "maximize")
                {
                    this.WindowState = FormWindowState.Maximized;
                }
                if (r == "unminimize")
                {
                    this.WindowState = FormWindowState.Normal;
                }

                if (r == "do i like fastfood")
                {
                    this.WindowState = FormWindowState.Normal;
                }

                if (r == "whats the temperature")
                {
                    say(" it is " + GetWeather("temp") + " degree ");
                }


                if (r == "restart" || r== "update")
                {
                    restart();
                }

                if (r == "hello")
                {
                    say("Hi");
                }

                if (r == "what time is it")
                {
                    say(DateTime.Now.ToString("h:mm tt"));

                }

                if (r == "open firefox")
                {
                    Process.Start("http://www.firefox.com");

                }

                if (r == "are you there")
                {
                    say("yes , i am here, what can i do for you");

                }

                if (r == "open 9gag")
                { 
                    Process.Start("http://www.9gag.com");

                }
                if (r == "open youtube")
                {
                    Process.Start("http://www.youtube.com");

                }

                if (r == "open microsoft word")
                {
                    Process.Start(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Microsoft Office\Microsoft Office Word 2007");
                  
                }

                if (r == "close word")
                {
                    killprog("WINWORD");

                }


                if (r == "what is today")
                {
                    say(DateTime.Now.ToString("M/d/yyyy"));

                }


                if (r == "how are you")
                {
                    say("great, and how are you");

                }

                if (r == "hello, how are you")
                {
                    say("great, and how are you");

                }

                if (r == "show data from database")
                {

                    testingdatabase();

                    say("data from database is shown");

                }

                if (r == "show data")
                {

                    showdata();

                    say("data from database is shown");

                }


            }


            if (r != "close program")
                textBox1.AppendText(r + "\n");


        }


        public void testingdatabase() {
            SqlConnection con = new SqlConnection("Data Source= DANNYWAHYUDI-PC;Initial Catalog = DotNetCore;Integrated Security=True;Pooling= False");
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Employees", con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        public void showdata()
        {
            SqlConnection con = new SqlConnection("Data Source= DANNYWAHYUDI-PC;Initial Catalog = DotNetCore;Integrated Security=True;Pooling= False");
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM testing", con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

    }
}
