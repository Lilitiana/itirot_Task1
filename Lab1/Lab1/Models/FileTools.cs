using System;
using System.Collections.Generic;
using Lab1.Models;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace Lab1.Models
{
    public class FileTools
    {
        public static async Task<string> ReadFile()
        {
            using (StreamReader sr = new StreamReader("lab1.json"))
            {
                return await sr.ReadToEndAsync();
            }
        }

        public static async Task WriteToFile(Message message)
        {
            List<Message> list = new List<Message>();
            using (StreamReader sr = new StreamReader("lab1.json"))
            {
                string res = await sr.ReadToEndAsync();
                list = JsonConvert.DeserializeObject<List<Message>>(res);
            }
            list.Add(message);
            using (StreamWriter sw = new StreamWriter("lab1.json", false, System.Text.Encoding.UTF8))
            {
                string text = JsonConvert.SerializeObject(list);
                await sw.WriteLineAsync(text);
            }
        }      
    }
}