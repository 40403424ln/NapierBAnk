using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NapierBank
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int displayCount;
        private StoredLists l;
        

        public MainWindow()
        {
            InitializeComponent();

            displayCount = 0;

            try
            {
                l = new StoredLists();
                LoadFromJson lfj = new LoadFromJson();
                l = lfj.loadLists();
                orderTrending();
                lblTrendingOutput.Content = createListsForPrint(orderTrending());
                lblMentionsOutput.Content = createListsForPrint(l.mentions);
                lblSirOutput1.Content = createListsForPrint(l.sir);
                lblNoiOutput1.Content = createListsForPrint(l.noi);
            }
            catch
            {

            }
        }

        public string createListsForPrint(List<string> list)
        {
            string message = "";

            foreach(string l in list)
            {
                message += l + "\n";                
            }

            return message;
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            string header = txtHeader.Text;
            string body = txtBody.Text;
            TypeChecker tc = new TypeChecker(header, body);
        }


        /// <summary>
        /// checks if text box is empty and tries inout text
        /// if a matching id is found it displays message content on form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(txtLoad.Text))
            {
                string input = txtLoad.Text;

                try
                {
                    display(input);
                }
                catch
                {
                    MessageBox.Show("No matching file to load");
                }
            }
            else
            {
                MessageBox.Show("Load text box is empty");
            }
        }


        private void display(string input)
        {
            LoadFromJson lfj = new LoadFromJson();            

            switch (input[0])
            {
                case 'S':
                case 's':
                    Sms sms = lfj.loadSms(input);
                    lblHeaderOutput.Content = sms.header;
                    lblTypeOutput.Content = "SMS";
                    lblSenderOutput.Content = sms.sender;
                    lblBodyOutput.Text = sms.text;
                    break;
                case 'T':
                case 't':
                    Tweet tweet = lfj.loadTweet(input);
                    lblHeaderOutput.Content = tweet.header;
                    lblTypeOutput.Content = "Tweet";
                    lblSenderOutput.Content = tweet.sender;
                    lblBodyOutput.Text = tweet.text;
                    break;
                case 'E':
                case 'e':
                    Email email = lfj.loadEmail(input);
                    lblNoiOutput.Content = email.incidentNature;
                    lblSirOutput.Content = email.subject;
                    lblSortCodeOutput.Content = email.sortCode;
                    lblHeaderOutput.Content = email.header;
                    lblTypeOutput.Content = "Email";
                    lblSenderOutput.Content = email.sender;
                    lblBodyOutput.Text = email.text;
                    break;
                default:
                    MessageBox.Show("Incorrect Input");
                    break;
            }
        }

        private List<string> orderTrending()
        {            
                var sortedList = l.trending
                .GroupBy(s => s)
                .OrderByDescending(g => g.Count())
                .SelectMany(g => g).ToList();
                sortedList = sortedList.Distinct().ToList();               
                return sortedList;
        }
        
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                display(l.ids[displayCount]);
                displayCount++;
            }
            catch
            {
                MessageBox.Show("No More to Display");
                displayCount = 0;
            }
        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                displayCount--;
                display(l.ids[displayCount]);
            }
            catch
            {
                MessageBox.Show("No More to Display");
                displayCount = 0;
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            l = new StoredLists();
            LoadFromJson lfj = new LoadFromJson();
            l = lfj.loadLists();            
            lblTrendingOutput.Content = createListsForPrint(orderTrending());
            lblMentionsOutput.Content = createListsForPrint(l.mentions);
            lblSirOutput1.Content = createListsForPrint(l.sir);
            lblNoiOutput1.Content = createListsForPrint(l.noi);
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
