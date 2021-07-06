using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using VkNet;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace botseminar
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public string s;
        private void button1_Click(object sender, EventArgs e)
        {
            var api_user = new VkApi();
            // обработать исключения!
            api_user.Authorize(new ApiAuthParams
            {
                AccessToken = getAuthForUser()
            });
            var getFriends = api_user.Friends.Get(new VkNet.Model.RequestParams.FriendsGetParams
            {
                Fields = VkNet.Enums.Filters.ProfileFields.All
            });
            foreach (User user in getFriends)
            {
                s += user.FirstName + user.LastName + "\n";
                s += user.Sex + "\n";
                s += user.Relation + "\n";
                s += user.HasMobile;
                s += user.BirthDate;
                listBox1.Items.Add(s);
                s = "";
            }

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public static string getAuthForUser()
        {
            string fileName = "auth_vk.txt";
            string token = "2ac7969805d15eca96887b8f2d939c358635bc6ee29e1acded3f640be46e4e7ecf2bd541c6874f1ada820";
            try
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    token = sr.ReadLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return token;
        }
    }
}
