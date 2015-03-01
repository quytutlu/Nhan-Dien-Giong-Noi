using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Threading;
using System.IO;
using System.Net;
using DieuKhienNgoiNhaThongMinh;

namespace Trier
{
    public partial class Form1 : Form
    {
        private SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox9.Hide();
            LoadGrammars();
            StartRecognition();
        }


        private void LoadGrammars()
        {
            Choices choices = new Choices(new string[] { "on", "off", "Goout" });
            GrammarBuilder grammarBuilder = new GrammarBuilder(choices);
            Grammar grammar = new Grammar(grammarBuilder);
            recognizer.LoadGrammar(grammar);

            /*GrammarBuilder grammarBuilder = new GrammarBuilder();
            grammarBuilder.Append("Turn");
            grammarBuilder.Append(new Choices("On","Off"));
            recognizer.RequestRecognizerUpdate();
            recognizer.LoadGrammar(new Grammar(grammarBuilder));*/
        }

        private void StartRecognition()
        {
            recognizer.SpeechDetected += new EventHandler<SpeechDetectedEventArgs>(recognizer_SpeechDetected);
            recognizer.SpeechRecognitionRejected += new EventHandler<SpeechRecognitionRejectedEventArgs>(recognizer_SpeechRecognitionRejected);
            recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(recognizer_SpeechRecognized);
            recognizer.RecognizeCompleted += new EventHandler<RecognizeCompletedEventArgs>(recognizer_RecognizeCompleted);


            Thread t1 = new Thread(delegate()
            {
                recognizer.SetInputToDefaultAudioDevice();
                recognizer.RecognizeAsync(RecognizeMode.Single);
            });
            t1.Start();
        }

        private void recognizer_SpeechDetected(object sender, SpeechDetectedEventArgs e)
        {
            label1.Text = "Đang chờ nhận diện...";
            pictureBox9.Show();
        }

        private void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            try
            {
                MessageBox.Show(e.Result.Text);
                /*if (e.Result.Text == "Turn On")
                {
                    Thread t = new Thread(HieuUng);
                    t.IsBackground=true;
                    t.Start(new Load("Đang bật bóng đèn..."));
                    HttpWebRequest web = (HttpWebRequest)WebRequest.Create("http://104.154.38.105/Arduino/?cmd=batbongden");
                    HttpWebResponse webs = (HttpWebResponse)web.GetResponse();
                    t.Abort();
                }
                else if (e.Result.Text == "Turn Off")
                {
                    Thread t = new Thread(HieuUng);
                    t.IsBackground = true;
                    t.Start(new Load("Đang tắt bóng đèn..."));
                    HttpWebRequest web = (HttpWebRequest)WebRequest.Create("http://104.154.38.105/Arduino/?cmd=tatbongden");
                    HttpWebResponse webs = (HttpWebResponse)web.GetResponse();
                    t.Abort();
                }
                else if (e.Result.Text == "RaNgoai")
                {
                    MessageBox.Show("Đi ra ngoài");
                }*/
            }
            catch
            {
                label1.Text = "Không nhận dạng được";
            }
        }

        public void HieuUng(Object t)
        {
            Application.Run((Load)t);
        }

        private void recognizer_SpeechRecognitionRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            pictureBox9.Hide();
            label1.Text = "Không nhận dạng được";
        }

        private void recognizer_RecognizeCompleted(object sender, RecognizeCompletedEventArgs e)
        {
            recognizer.RecognizeAsync();
        }
    }
}