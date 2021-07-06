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
                listBox1.Items.Add(Encoding.UTF8.GetString(Encoding.Default.GetBytes(user.FirstName)) + " " + Encoding.UTF8.GetString(Encoding.Default.GetBytes(user.LastName)) + " " + user.Sex + " " + user.BirthDate);
            }
            var api_group = new VkApi();
            // обработать исключения!
            api_group.Authorize(new ApiAuthParams
            {
                AccessToken = getAuthForGroup()
            });

            // получить список подписчиков сообщества (для сообщества)
            var getFollowers = api_group.Groups.GetMembers(new GroupsGetMembersParams()
            {
                GroupId = "205575031",
                Fields = VkNet.Enums.Filters.UsersFields.FirstNameAbl
            });
            foreach (User user in getFollowers)
                listBox2.Items.Add(Encoding.UTF8.GetString(Encoding.Default.GetBytes(user.FirstName)) + " " + Encoding.UTF8.GetString(Encoding.Default.GetBytes(user.LastName)) + " " + user.Sex + " " + user.BirthDate);
            

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public static string getAuthForUser()
        {
            string fileName = "auth_vk.txt";
            string token = "716fe24a5db66f3eae3e39fd6f00a5871858a69ca947ca3b39ef15f5128eed66b5d15feeec157d66dfc8a";
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
        public static string getAuthForGroup()
        {
            string fileName = @"auth_vk.txt";
            string token = "097e0d1651e1e326e546b0a6f3996370e5ed4a1ba5bc7126fcc3653bd1d4b1df299d839e8ab77bb17faae";
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
