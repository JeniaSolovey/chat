using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using lernWcf;

namespace lernWcf
{
    [ServiceContract]
    public interface IMyObject
    {
        [OperationContract]
        void send(string nick, string str);
        [OperationContract]
        string checkMes(string nick);
    }


    public class Mes
    {
        public string nick;
        public string mes;
    }
    public class MyObject : IMyObject
    {
        public static List<Mes> mes;
        public string checkMes(string nick)
        {
            var res = mes.Where(s => s.nick == nick).Select(s => s.mes).ToArray();

            string result = "";

            foreach (var item in res)
            {
                result += item + "\n";
            }

            mes.RemoveAll(s => s.nick == nick);
            return result;
        }

        public void send(string nick, string str)
        {
            mes.Add(new Mes() { nick = nick, mes = str });
        }
    }
}

namespace server
{
    
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(MyObject), new Uri("http://localhost:8080/signals"));
            host.AddServiceEndpoint(typeof(IMyObject), new BasicHttpBinding(), "");
            host.Open();
            Console.WriteLine("Server start");
            MyObject.mes = new List<Mes>();
            Console.ReadLine();

            host.Close();
        }
    }
}