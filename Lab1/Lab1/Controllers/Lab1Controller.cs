using System;
using System.Collections.Generic;
using Lab1.Models;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;

namespace Lab1.Controllers
{
    public class Lab1Controller : ApiController
    {
        static List<User> users = new List<User>()
        {
           new User(){Login="user1",Password="1",Flag=true },
           new User(){Login="user2",Password="2",Flag=true },
           new User(){Login="user3",Password="3",Flag=true }
        };

        [HttpGet]
        public bool IsUpdate(string login)
        {
            return users.First(p => p.Login == login).Flag;
        }

        [HttpGet]
        public async Task<List<Message>> GetWall(string login)
        {
            List<Message> result = JsonConvert.DeserializeObject<List<Message>>(await FileTools.ReadFile());
            for (int i = 0; i < result.Count; i++)
                if (result[i].Login == login)
                    result[i].Login = "Вы: ";
            users.First(p => p.Login == login).Flag = false;
            return result;
        }

        [HttpPost]
        public bool Login(string login,string password)
        {
            if (users.FirstOrDefault(p => p.Login == login && p.Password == password) != null)
                return true;
            else
                return false;
        }

        [HttpPost]
        public async void SendMessage(string message, string login)
        {
            Message mes = new Message() { Login = login, Text = message };
            await FileTools.WriteToFile(mes);
            for (int i = 0; i < users.Count; i++)
                users[i].Flag = true;
        }
    }
}
