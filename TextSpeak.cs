using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NapierBank
{
    class TextSpeak
    {
        private List<string> abbreviations;
        private List<TextAbrvs> textAbrvs;

        /// <summary>
        /// constructor creates a list of abbreviation from .csv file
        /// splits it into a 2d array [abreviation, explenation]
        /// </summary>
        public TextSpeak()
        {
            abbreviations = null;
            textAbrvs = null;
            populateVariables();
        }       


        private void populateVariables()
        {
            textAbrvs = new List<TextAbrvs>();
            abbreviations = File.ReadAllLines("textwords.csv").ToList();
            foreach (string line in abbreviations)
            {
                var item = line.Split(',');
                TextAbrvs text = new TextAbrvs(item[0], item[1]);
                textAbrvs.Add(text);
            }
        }
        /// <summary>
        /// takes in array and checks each element of array against the abreArr
        /// if a match is found the word has explenation apended to it
        /// return changed body array
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public string[] checkForTextSpeak(string[] body)
        {                   

            for (int i = 0; i < body.Length; i++)
            {
                foreach (var txt in textAbrvs)
                {
                    if(body[i].ToUpper() == txt.Abb)
                    {
                        body[i] = body[i] + " <" + txt.Translation + ">";
                    }
                }
            }
            return body;
        }
    }
}
