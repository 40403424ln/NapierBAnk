using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NapierBank
{
    public abstract class Message
    {
        public string id;
        public string header;
        public string body;
        public string text;
        public string sender;
        public string[] splitBody;
        public string lenghtCheck;
        public StoredLists list;

        public Message()
        {
            header = "";
            body = "";
            id = "";
            text = "";
            sender = "";
            splitBody = new string[0];
            lenghtCheck = "";
            list = new StoredLists();
        }

        /// <summary>
        /// virtual method that calls other methods in the class
        /// </summary>
        public virtual void callMethods()
        {
            splitBodyIntoArr();
            checkMessageLength();
            prepareText();
        }

        /// <summary>
        /// method to seperate each word in body to an element of an array
        /// sets first word in the array to sender
        /// </summary>
        public void splitBodyIntoArr()
        {
            splitBody = body.Split(' ');
            sender = splitBody[0];            
        }

        /// <summary>
        /// virtual method to check length, this one returns a message
        /// </summary>
        public virtual void checkMessageLength()
        {
            MessageBox.Show("Message Lenght Not Checked");
        }

        /// <summary>
        /// create an sintance of text speak
        /// and calls check for text speak mehtod
        /// </summary>
        public void checkForTextspeak()
        {
            TextSpeak ts = new TextSpeak();
            splitBody = ts.checkForTextSpeak(splitBody);            
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void addToLists()
        {
            
        }

        /// <summary>
        /// displays message
        /// </summary>
        public void prepareText()
        {
            text = "";
            for (int i = 1; i < splitBody.Length; i++)
            {
                text += (splitBody[i] + ' ');
            }
            
            //MessageBox.Show(sender);
            //MessageBox.Show(message);
        }

        public void saveListToJson()
        {
            SaveToJson stj = new SaveToJson();
            stj.saveLists(list);
        }

        public string getId()
        {
            return id;
        }

        public void addIdToList()
        {
            list.ids.Add(id);
        }
    }
}
