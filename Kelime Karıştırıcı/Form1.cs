using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Kelime_Karıştırıcı
{
    public partial class Form1 : Form
    {
        List<string> finalWordList;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string mainWord = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(mainWord))
            {
                return;
            }
            label4.Text = "Kelimeler oluşturuluyor, lütfen bekleyiniz...";

            CreateTheWords(mainWord.Trim());

            PlaySound();
        }

        void CreateTheWords(string mainWord)
        {
            int mainWordLength = mainWord.Length;
            List<string> mainLetterList = new List<string>();
            finalWordList = new List<string>();

            for (int i = 0; i < mainWord.Length; i++)
            {
                mainLetterList.Add(mainWord.Substring(i, 1));
            }

            int factorial = 1;
            for (int i = 1; i <= mainWordLength; i++)
            {
                factorial *= i;
            }



            List<string> letters = new List<string>(mainLetterList);

            Random rnd = new Random();
            dataGridView1.Rows.Clear();
            int totalLoopCount = factorial * 10; //100 

            if (totalLoopCount > 10000)
            {
                totalLoopCount = 10000;
            }
            for (int i = 0; i < totalLoopCount; i++)
            {
                letters = new List<string>(mainLetterList);

                string wordToAdd = "";

                while (letters.Count > 0)
                {
                    int index = rnd.Next(0, letters.Count);

                    wordToAdd = wordToAdd + letters[index];

                    letters.RemoveAt(index);
                }

                if (!finalWordList.Contains(wordToAdd))
                {
                    finalWordList.Add(wordToAdd);
                    //listBox1.Items.Add(wordToAdd); //
                    dataGridView1.Rows.Add(finalWordList.Count, wordToAdd);
                }
            }

            //listBox1.Refresh();
            dataGridView1.Refresh();
            label4.Text = $"{finalWordList.Count} kelime oluşturuldu";
        }

        void SaveWordsToXML()
        {
            List<string> data = new List<string>(finalWordList); // ... // populate the list

            //create the serialiser to create the xml
            XmlSerializer serialiser = new XmlSerializer(typeof(List<string>));

            // Create the TextWriter for the serialiser to use
            TextWriter filestream = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "Kelimeler.xml");

            //write to the file
            serialiser.Serialize(filestream, data);

            label4.Text = $"Dosya {AppDomain.CurrentDomain.BaseDirectory}Kelimeler.xml konumuna kaydedildi.";

            PlaySound();

            // Close the file
            filestream.Close();
        }

        void PlaySound()
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(AppDomain.CurrentDomain.BaseDirectory + "Sound.wav");
            player.Play();
        }

        public string ToXML<T>(T obj)
        {
            using (StringWriter stringWriter = new StringWriter(new StringBuilder()))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                xmlSerializer.Serialize(stringWriter, obj);
                return stringWriter.ToString();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveWordsToXML();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
