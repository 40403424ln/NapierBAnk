using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace NapierBank
{
    public class Email : Message
    {
        public string subject;
        public string sortCode;
        public string incidentNature;
        public bool sir;
        public List<string> quarantine;
        public List<string> sirList { get; set; }
        public List<string> noiList { get; set; }

        public Email(string header, string body, StoredLists list)
        {
            this.header = header;
            this.body = body;
            this.list = list;
            id = header;
            subject = "";
            sortCode = "";
            incidentNature = "";
            sir = false;
            quarantine = new List<string>();
            sirList = new List<string>();
            noiList = new List<string>();
        }

        public Email()
        {

        }


        /// <summary>
        /// override method that calls other methods in the class
        /// </summary>
        public override void callMethods()
        {
            splitBodyIntoArr();
            if(CheckEmail() == true)
            {
                checkMessageLength();
                checkForSir();
                quarantineUrl();
                addIdToList();
                addToLists();
                prepareText();
                saveListToJson();
            }
            else
            {
                MessageBox.Show("Incorrect email");
            }
        }

        /// <summary>
        /// Regex used to check format of email
        /// returns bool true or false depending on if it is valid format
        /// </summary>
        /// <returns></returns>
        public bool CheckEmail()
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(splitBody[0]);

            if (match.Success)
            {
                //MessageBox.Show(splitBody[0]);
                return true;
            }
            else
            {
                return false;
            }
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

            if (length > 1028)
                MessageBox.Show("Email message too long, must be 1028 characters or less");
        }

        /// <summary>
        /// override method that will add sir to lists class
        /// </summary>
        public override void addToLists()
        {
            foreach(string sir in sirList)
            {
                list.sir.Add(sir);
            }

            foreach(string noi in noiList)
            {
                list.noi.Add(noi);
            }
        }

        /// <summary>
        /// loops through split body and checks for SIR
        /// if found adds subject, sort code and incident nature
        /// </summary>
        private void checkForSir()
        {
            string sir = "SIR";
            for (int i = 1; i < splitBody.Length; i++)
            {
                if (splitBody[i].ToUpper().Contains(sir))
                {
                    subject = splitBody[i] + ' ' + splitBody[i + 1];
                    sortCode = splitBody[i + 2];
                    incidentNature = splitBody[i + 3];
                    switch (splitBody[i+3].ToUpper())
                    {   
                        case "STAFF":
                        case "CUSTOMER":
                            incidentNature += ' ' + splitBody[i + 4];
                            break;
                        case "ATM":
                            incidentNature += ' ' + splitBody[i + 4];
                            break; 
                        case "ABUSE":
                            incidentNature += ' ' + splitBody[i + 4];
                            break;
                        case "SUSPICIOUS":
                            incidentNature += ' ' + splitBody[i + 4];
                            break;
                        case "CASH":
                            incidentNature += ' ' + splitBody[i + 4];
                            break;
                        default:
                            break;
                    }
                }
            }

            sirList.Add(subject);
            noiList.Add(incidentNature);

            //list.sir.Add(subject);
            //list.noi.Add(incidentNature);

            //MessageBox.Show(subject);
            //MessageBox.Show(sortCode);
            //MessageBox.Show(incidentNature);
        }

        /// <summary>
        /// loops through split body list and takes urls out
        /// and replaces them with "<URL Quarentined>"
        /// </summary>
        private void quarantineUrl()
        {
            string url = "http";
            for (int i = 1; i < splitBody.Length; i++)
            {
                if (splitBody[i].Contains(url))
                {
                    quarantine.Add(splitBody[i]);
                    splitBody[i] = "<URL Quarantined>";
                }
            }
        }
    }
}
