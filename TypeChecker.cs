using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NapierBank
{
    class TypeChecker
    {
        //variables
        private string header;
        private string body;
        public StoredLists sl = new StoredLists();
        SaveToJson stj = new SaveToJson();
        LoadFromJson lfj = new LoadFromJson();


        /// <summary>
        /// instantiates class variables 
        /// calls checkMessageType method
        /// </summary>
        /// <param name="header"></param>
        /// <param name="body"></param>
        public TypeChecker(string header, string body)
        {
            this.header = header;
            this.body = body;

            checkMessageType();
        }

        public void loadLists()
        {
            try
            {
                sl = lfj.loadLists();
            }
            catch
            {
                MessageBox.Show("No Lists to show");
            }
        }

        /// <summary>
        /// check first character of header
        /// calls corresponding methods depending on result
        /// </summary>
        private void checkMessageType()
        {
            try
            {
                switch (header[0])
                {
                    case 'S':
                    case 's':
                        passToSms();
                        break;
                    case 'T':
                    case 't':
                        passToTweet();
                        break;
                    case 'E':
                    case 'e':
                        passToEmail();
                        break;
                    default:
                        MessageBox.Show("Error");
                        break;
                }
            }
            catch
            {
                MessageBox.Show("Incorrect Header Input");
            }
        }

        /// <summary>
        /// creates sms class
        /// </summary>
        private void passToSms()
        {
            Sms sms = new Sms(header, body);
            sms.callMethods();
            stj.saveSms(sms);
        }

        /// <summary>
        /// creates email class
        /// </summary>
        private void passToEmail()
        {
            Email email = new Email(header, body, sl);
            email.callMethods();
            stj.saveEmail(email);
        }

        /// <summary>
        /// create tweet class
        /// </summary>
        private void passToTweet()
        {
            Tweet tweet = new Tweet(header, body, sl);
            tweet.callMethods();
            stj.saveTweet(tweet);
        }   
        
        private void addToLists()
        {
            StoredLists lists = new StoredLists();            
        }
    }
}
