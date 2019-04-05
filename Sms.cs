using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace NapierBank
{
    public class Sms: Message
    {
        public Sms(string header, string body)
        {
            this.header = header;
            this.body = body;
            id = header;
        }

        public Sms()
        {

        }


        /// <summary>
        /// override method that calls other methods in the class
        /// </summary>
        public override void callMethods()
        {
            splitBodyIntoArr();
            if (checkPhoneNumber() == true)
            {
                checkMessageLength();
                checkForTextspeak();
                addIdToList();
                addToLists();
                prepareText();
                saveListToJson();
            }
            else
            {
                MessageBox.Show("Incorrect phone number");
            }
        }

        /// <summary>
        /// Regex used to check format of international phone numbers
        /// returns bool true or false depending on if it is valid format
        /// </summary>
        /// <returns></returns>
        public bool checkPhoneNumber()
        {
            Regex regex = new Regex(@"^((\+\d{1,3}(-| )?\(?\d\)?(-| )?\d{1,5})|(\(?\d{2,6}\)?))(-| )?(\d{3,4})(-| )?(\d{4})(( x| ext)\d{1,5}){0,1}$");
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

            if (length > 140)
                MessageBox.Show("Sms message too long, must be 140 characters or less");
        }
    }
}
