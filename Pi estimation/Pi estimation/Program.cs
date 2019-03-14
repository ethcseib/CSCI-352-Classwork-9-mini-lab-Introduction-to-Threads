using System;
using System.Diagnostics;
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
            /*<Summary> A List of type Thread and a List of type FindPiThread are instantiated and a stopwatch object is started i.e. time starts recording.
             In the first loop the lists are filled and the threads are started. Second loop the threads join back up when their calculations are finished.
             The third loop will sum the dart throws that land inside the circle. Then pi is calculated and printed alongside the time it took to do the thread calculations.</Summary>*/

            Stopwatch watch = new Stopwatch();//will record time
            int throws = 0;//holds the total darts to be thrown
            double sum = 0;//sums the darts that land inside the circle
            double pi = 0;//holds the pi value
            
            Console.WriteLine("How many darts should be thrown?");
            string x = Console.ReadLine();
            throws = Convert.ToInt32(x);

            Console.WriteLine("How many threads should be used?");
            string ThreadCount = Console.ReadLine();


            List<Thread> ThreadList = new List<Thread>(Convert.ToInt32(ThreadCount));//threading list created
            List<FindPiThread> FindPiList = new List<FindPiThread>(Convert.ToInt32(ThreadCount));//FindPiThread list created

            watch.Start();//the timer starts

            for(int i = 0; i < Convert.ToInt32(ThreadCount); i++)
            {
                
                FindPiList.Add(new FindPiThread(throws));//creates an instance of FindPiThread and adds it to a list with the given throws
                ThreadList.Add(new Thread(FindPiList[i].ThrowDarts));//Instantiates the threads requested by the user and passes them the function for calculations and adds them to the list

                ThreadList[i].Start();//starts threading
                Thread.Sleep(16);//stops computing for 16 milliseconds
            }

            for(int i = 0; i < Convert.ToInt32(ThreadCount); i++)//makes the threads join back up
            {
                ThreadList[i].Join();
            }
            
            for(int i = 0; i < Convert.ToInt32(ThreadCount); i++)//sums the darts that landed inside the quarter of the circle
            {
                sum += FindPiList[i].DartsThrown;
            }

            throws *= Convert.ToInt32(ThreadCount);//accounts for the number of threads that threw darts
            pi = 4 * (sum / throws);//pi is calculated

            watch.Stop();//timer stops

            TimeSpan ts = watch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            Console.WriteLine("Pi is approximately " + Convert.ToString(pi) + ". This took " + elapsedTime);
            Console.ReadKey();
        }
    }

    class FindPiThread
    {
        int DartThrows;//# of darts to throw
        int CriticalDarts;//the darts that land in the circle
        Random rand;

        public int DartsThrown//accessor for the darts that land inside the circle
        {
            get { return CriticalDarts; }
        }

        public FindPiThread(int throws)
        {
            DartThrows = throws;
            CriticalDarts = 0;
            rand = new Random();
        }

        public void ThrowDarts()//throws the darts at the board and determines if they hit inside our circle
        {
            int i = 0;
            rand = new Random();
            double x = 0;
            double y = 0;

            while(i < DartThrows)
            {
                x = rand.NextDouble();
                y = rand.NextDouble();

                if (Math.Sqrt(x * x + y * y) <= 1)//calculates the hypotenuse 
                    CriticalDarts++;

                i++;
            }
        }
    }
}
