using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;

namespace NapierBank
{
    class LoadFromJson
    {
        //file paths for each folder
        private static string basePath = Directory.GetCurrentDirectory();
        private static string saveFolder = basePath + "\\Save Data";
        private static string tweetFolder = saveFolder + "\\Tweets";
        private static string smsFolder = saveFolder + "\\SMS";
        private static string emailFolder = saveFolder + "\\Emails";
        private static string listsFolder = saveFolder + "\\Lists";


        /// <summary>
        /// takes input (id number)
        /// finds id number and serializes to an instance of a class and return the class
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Sms loadSms(string input)
        {
            string file = smsFolder + "\\" + input + ".json";
            using(StreamReader sr = new StreamReader(file))
            {
                string json = sr.ReadToEnd();
                JavaScriptSerializer jss = new JavaScriptSerializer();
                Sms sms = jss.Deserialize<Sms>(json);
                return sms;
            }
        }

        /// <summary>
        /// takes input (id number)
        /// finds id number and serializes to an instance of a class and return the class
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Tweet loadTweet(string input)
        {
            string file = tweetFolder + "\\" + input + ".json";
            using (StreamReader sr = new StreamReader(file))
            {
                string json = sr.ReadToEnd();
                JavaScriptSerializer jss = new JavaScriptSerializer();
                Tweet tweet = jss.Deserialize<Tweet>(json);
                return tweet;
            }
        }

        /// <summary>
        /// takes input (id number)
        /// finds id number and serializes to an instance of a class and return the class
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Email loadEmail(string input)
        {
            string file = emailFolder + "\\" + input + ".json";
            using (StreamReader sr = new StreamReader(file))
            {
                string json = sr.ReadToEnd();
                JavaScriptSerializer jss = new JavaScriptSerializer();
                Email email = jss.Deserialize <Email>(json);
                return email;
            }
        }

        public StoredLists loadLists()
        {
            string file = listsFolder + "\\lists.json";
            using(StreamReader sr = new StreamReader(file))
            {
                string json = sr.ReadToEnd();
                JavaScriptSerializer jss = new JavaScriptSerializer();
                StoredLists list = jss.Deserialize<StoredLists>(json);
                return list;
            }
        }
    }
}
