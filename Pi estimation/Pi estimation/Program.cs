using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace Pi_estimation
{
    class Program
    {
        static void Main(string[] args)
        {
            FindPiThread obj;
            Thread thread;
            int throws = 3;//Needs to change later

            Console.WriteLine("How many darts should be thrown?");
            //string throws = Console.ReadLine();

            obj = new FindPiThread(Convert.ToInt32(throws));

            thread = new Thread(obj.ThrowDarts);
            thread.Start();
            Console.ReadKey();
        }
    }

    class FindPiThread
    {
        int DartThrows;
        int CriticalDarts;//the darts that land in the one quarter of the board
        Random rand;

        int DartsThrown
        {
            get { return CriticalDarts; }
        }

        public FindPiThread(int throws)
        {
            DartThrows = throws;
            CriticalDarts = 0;
            rand = new Random();
        }

        public void ThrowDarts()
        { int i = 0;
            while(i < 3)
            {
                Console.WriteLine("hi");
                i++;
            }
        }
    }
}
