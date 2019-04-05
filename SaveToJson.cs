using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace NapierBank
{
    class SaveToJson
    {
        //file paths for each folder
        private static string basePath = Directory.GetCurrentDirectory();
        private static string saveFolder = basePath + "\\Save Data";
        private static string tweetFolder = saveFolder + "\\Tweets";
        private static string smsFolder = saveFolder + "\\SMS";
        private static string emailFolder = saveFolder + "\\Emails";
        private static string listsFolder = saveFolder + "\\Lists";

        /// <summary>
        /// checks if file path exists and if not created them
        /// </summary>
        public SaveToJson()
        {
            if (!Directory.Exists(saveFolder))
                Directory.CreateDirectory(saveFolder);
            if (!Directory.Exists(tweetFolder))
                Directory.CreateDirectory(tweetFolder);
            if (!Directory.Exists(smsFolder))
                Directory.CreateDirectory(smsFolder);
            if (!Directory.Exists(emailFolder))
                Directory.CreateDirectory(emailFolder);
            if (!Directory.Exists(listsFolder))
                Directory.CreateDirectory(listsFolder);
        }

        /// <summary>
        /// creates filename with file path
        /// uses streamwriter to write to file
        /// </summary>
        /// <param name="tweet"></param>
        public void saveTweet(Tweet tweet)
        {
            string file = tweetFolder + "\\" + tweet.getId() + ".json";

            using(StreamWriter sw = new StreamWriter(file))
            {
                JavaScriptSerializer jss = new JavaScriptSerializer();
                sw.Write(jss.Serialize(tweet));
                sw.Close();
            }
        }

        /// <summary>
        /// creates filename with file path
        /// uses streamwriter to write to file
        /// </summary>
        /// <param name="sms"></param>
        public void saveSms(Sms sms)
        {
            string file = smsFolder + "\\" + sms.getId() + ".json";

            using (StreamWriter sw = new StreamWriter(file))
            {
                JavaScriptSerializer jss = new JavaScriptSerializer();
                sw.Write(jss.Serialize(sms));
                sw.Close();
            }
        }

        /// <summary>
        /// creates filename with file path
        /// uses streamwriter to write to file
        /// </summary>
        /// <param name="email"></param>
        public void saveEmail(Email email)
        {
            string file = emailFolder + "\\" + email.getId() + ".json";

            using (StreamWriter sw = new StreamWriter(file))
            {
                JavaScriptSerializer jss = new JavaScriptSerializer();
                sw.Write(jss.Serialize(email));
                sw.Close();
            }
        }

        /// <summary>
        /// creates filename with file path
        /// reads in file and adds to list class
        /// adds to the list
        /// uses streamwriter to write to file with new lists
        /// </summary>
        /// <param name="lists"></param>
        public void saveLists(StoredLists lists)
        {
            string file = listsFolder + "\\lists.json";
            StoredLists readList = new StoredLists();
            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    string json = sr.ReadToEnd();
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    readList = jss.Deserialize<StoredLists>(json);                
                }

                foreach(string m in lists.mentions)
                {
                    readList.mentions.Add(m);
                }
                foreach(string t in lists.trending)
                {
                    readList.trending.Add(t);
                }
                foreach(string s in lists.sir)
                {
                    readList.sir.Add(s);
                }
                foreach(string id in lists.ids)
                {
                    readList.ids.Add(id);
                }
            }
            catch
            {
                readList = lists;
            }

            using (StreamWriter sw = new StreamWriter(file))
            {
                JavaScriptSerializer jss = new JavaScriptSerializer();
                sw.Write(jss.Serialize(readList));
                sw.Close();
            }
        }

    }
}
