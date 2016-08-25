using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace lernWcf
{
    public class Program
    {
        [ServiceContract] // Говорим WCF что это интерфейс для запросов сервису
        public interface IMyObject
        {
            [OperationContract] // Делегируемый метод.
            string GetCommandString(int i);
        }
        static void Main(string[] args)
        {

        }
    }
}
