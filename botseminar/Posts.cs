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
    public partial class Posts : Form
    {
        public Posts()
        {
            InitializeComponent();
        }
        public static string getAuthForUser()
        {
            string fileName = @"auth_vk.txt";
            string token = "716fe24a5db66f3eae3e39fd6f00a5871858a69ca947ca3b39ef15f5128eed66b5d15feeec157d66dfc8a";
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

        private void button1_Click(object sender, EventArgs e)
        {
            var api_user = new VkApi();
            // обработать исключения!
            api_user.Authorize(new ApiAuthParams
            {
                AccessToken = getAuthForUser()
            });
            var get = api_user.Wall.Get(new WallGetParams());
            foreach (var wallPosts in get.WallPosts)
            {
                listBox1.Items.Add(Encoding.UTF8.GetString(Encoding.Default.GetBytes(wallPosts.Text)));
            }
        }
    }
}
