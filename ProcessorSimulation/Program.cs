using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;


namespace ProcessorSimulation
{
    class Program
    {


        const int PROCESSORCOUNT = 32;
        const bool USING_GUASSIAN = false;

        static float totalWaitTime;      // add up all totalWaitCount(s)
        static float totalStillWaiting;  // add up all processors that are still waiting when you end program.
        static float totalConnections;   //total of all rejected and made requests
        static float average;          //(totalWaitTime + totalStillWaiting)/totalConnections

        public class Processor
        {
            //int processorNum;
            public int memoryValue;
            public float waitCount;      // wait count till connected,zero out when gets connected
            public float totalWaitCount; // total wait time of all  waitCount(s)
            public int priority;

        };






        static void Main(string[] args)
        {

            //used to test 1-2018 memory modules
            for (int i = 0; i < 2048; i++)
            {
                //zero out counts for when memory array increases
                average = 0;
                totalConnections = 0;
                totalWaitTime = 0;
                totalStillWaiting = 0;

                computeAverage(i);

                Console.WriteLine("Memory Modules: " + i + " average wait time: " + average + "\n");


            }


            Console.ReadLine();



        }


        //where all the work is
        static void computeAverage(int numMems)
        {
            List<Processor> processors = new List<Processor>();
            List<bool> memoryMods = new List<bool>();



            float currentAverage = 0;  //current differs from average by .02%
            float difference = float.MaxValue;

            //set up processors and memory modules
            initProcessors(processors);
            initmemMods(memoryMods, numMems + 1);



            //create a random number
            Random rnd = new Random();



            // outter loop to do connections, calcluate into current average and compare
            while (difference > .02)
            {
                //need to clear back to false each cycle
                clearMemModules(memoryMods);




                //connect to memory modules
                for (int i = 0; i < processors.Count; i++)
                {
                    //increment total connections failed or not
                    totalConnections++;



                    //set a value of 0 which means not waiting on a certain memory module
                    if (processors[i].waitCount == 0)
                    {
                        int num = (int)generateRandom(numMems, rnd);
                        processors[i].memoryValue = (int)generateRandom(numMems, rnd);
                    }

                    //if memory spot is not taken
                    if (!memoryMods[processors[i].memoryValue])
                    {
                        memoryMods[processors[i].memoryValue] = true; //connect to mem mod
                        processors[i].waitCount = 0;
                        processors[i].totalWaitCount += processors[i].waitCount;


                    }
                    else
                    {
                        //set up wait counts and total wait counts
                        processors[i].waitCount++;
                        //mem value is still set
                    }

                    //set priority
                    if (processors[i].waitCount > 5)
                    {
                        processors[i].priority++;
                    }


                }



                //calculate final values of all processors
                for (int i = 0; i < processors.Count; i++)
                {
                    totalWaitTime += processors[i].totalWaitCount;
                    totalStillWaiting += processors[i].waitCount;
                }




                //sort the list for priority
                processors.Sort((x, y) => x.priority.CompareTo(y.priority));

                //calculate average
                currentAverage = ((totalWaitTime + totalStillWaiting) / totalConnections);


                //math: compare with last runs average with this runs average, if less that .02% terminate
                difference = ((Math.Abs(currentAverage - average)) / ((currentAverage + average) / 2)) * 100;
                average = currentAverage;
                currentAverage = 0;
                totalStillWaiting = 0;

            }


        }


        //initialize to empty and set attributes to zero
        static void initProcessors(List<Processor> processors)
        {
            for (int i = 0; i < PROCESSORCOUNT; i++)
            {
                processors.Add(new Processor());
                processors[i].waitCount = 0;
                processors[i].totalWaitCount = 0;
                processors[i].priority = 0;



            }


        }

        //create memory mods and set to false
        static void initmemMods(List<bool> memoryMods, int count)
        {
            for (int i = 0; i < count; i++)
            {
                memoryMods.Add(false);
            }


        }


        //clear memory mods after initialized
        static void clearMemModules(List<bool> memoryMods)
        {

            for (int i = 0; i < memoryMods.Count; i++)
            {
                memoryMods[i] = false;
            }

        }

        //generate ransdom number based off if gaussian or not
        static double generateRandom(int numMems, Random rnd)
        {


            double random = rnd.Next(0, numMems);

            if (USING_GUASSIAN)
            {

                double stdDev;

                if (numMems > 2)
                {
                    stdDev = numMems / (numMems * 3);
                }
                else
                {
                    stdDev = 0;
                }

                var normalDist = new MathNet.Numerics.Distributions.Normal(random, stdDev);
                random = Math.Abs(normalDist.Sample());

            }
            return random;

        }


    }
}

