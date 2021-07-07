using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VkNet;
using VkNet.Model;
using VkNet.Model.RequestParams;
using System.IO;
using System.Globalization;

namespace botseminar
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        public static string getAuthForGroup()
        {
            string fileName = @"auth_vk.txt";
            string token = "";
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
        public static string getAuthForUser()
        {
            string fileName = @"auth_vk.txt";
            string token = "";
            try
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    sr.ReadLine();
                    token = sr.ReadLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return token;
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int count = 0;
            int sum = 0;
            int year = 0;
            var api_group = new VkApi();
            api_group.Authorize(new ApiAuthParams
            {
                AccessToken = getAuthForGroup()
            });
            var users = api_group.Friends.Get(new VkNet.Model.RequestParams.FriendsGetParams
            {
                UserId = Convert.ToInt32(textBox1.Text),
                Fields = VkNet.Enums.Filters.ProfileFields.All,
            });
            DateTime today = DateTime.Now;
            foreach (var item in users)
            {
                if (item.BirthDate != null)
                {
                    string[] tokens = item.BirthDate.Split('.');
                    if (tokens.Length == 3)
                    {
                        year = 2021 - Convert.ToInt32(tokens[2]);
                        count++;
                        sum += year;
                    }
                }
            }
            MessageBox.Show(Convert.ToString(sum / count));
        }
    }
}
