using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NapierBank
{
    public class Tweet : Message
    {
        public List<string> hashtag { get; set; }
        public List<string> mentions { get; set; }

        public Tweet(string header, string body, StoredLists list)
        {
            this.header = header;
            this.body = body;
            this.list = list;
            id = header;
            hashtag = new List<string>();
            mentions = new List<string>();
        }

        public Tweet()
        {

        }

        /// <summary>
        /// virtual method that calls other methods in the class
        /// </summary>
        public override void callMethods()
        {
            splitBodyIntoArr();
            checkMessageLength();
            checkForTextspeak();
            checkForHashtags();
            checkForMentions();
            addIdToList();
            addToLists();
            prepareText();
            saveListToJson();
        }

        /// <summary>
        /// override method that checks the character length of the message
        /// loops all elements of the array into a single string and counts the length
        /// </summary>
        public override void checkMessageLength()
        {
            int length = 0;
            for (int i = 1; i < splitBody.Length; i++)
            {
                lenghtCheck += splitBody[i] + ' ';
                length = lenghtCheck.Length;
            }

            if (length > 140)
                MessageBox.Show("Tweet message too long, must be 140 characters or less");
        }

        /// <summary>
        /// ovveride method that will add the hashtags and menthons to the list class
        /// </summary>
        public override void addToLists()
        {
            foreach(string mention in mentions)
            {
                list.mentions.Add(mention);
            }

            foreach(string tag in hashtag)
            {
                list.trending.Add(tag);
            }
        }

        /// <summary>
        /// method to check the split body array and finds hastags
        /// strings that start with '#'
        /// </summary>
        private void checkForHashtags()
        {
            for (int i = 1; i < splitBody.Length; i++)
            {
                if(splitBody[i][0] == '#')
                    hashtag.Add(splitBody[i]);              
            }

            //foreach(string tag in hashtag)
            //{
            //    MessageBox.Show(tag);
            //}
        }

        /// <summary>
        /// /// method to check the split body array and finds mentions
        /// strings that start with '@'
        /// </summary>
        private void checkForMentions()
        {
            for (int i = 1; i < splitBody.Length; i++)
            {
                if (splitBody[i][0] == '@')
                    mentions.Add(splitBody[i]);
            }

            //foreach (string item in mentions)
            //{
            //    MessageBox.Show(item);
            //}
        }
    }
}
