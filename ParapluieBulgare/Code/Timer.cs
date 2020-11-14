using System;
using System.Collections.Generic;
using System.Text;

namespace ParapluieBulgare.Code
{
    class Timer
    {
        //Real-time seconds
        private static double secondsUntilGameOver;
        private double secondsSinceBeginning;
        private double secondsSinceIncrement;

        //In-game time displayed in hours and minutes, does not correspond to real-life time
        private int currentHour;
        private int currentMinutes;

        private static int initialHour = 9;
        private static int finalHour = 18;

        private static int minuteIncrement = 5;
        private static double secondsUntilIncrement;
        
        public Timer(double length)
        {
            secondsSinceBeginning = 0;
            secondsUntilGameOver = length;
            
            currentHour = initialHour;
            currentMinutes = 0;

            secondsUntilIncrement = length / ( (finalHour - initialHour) * (60 / minuteIncrement) );
        }

        public string getTime()
        {
            string time = currentHour.ToString() + ":";
            if (currentMinutes < 10) time += "0";
            time += currentMinutes.ToString();

            return time;
        }

        public void incrementTime()
        {
            currentMinutes += minuteIncrement;

            if (currentMinutes >= 60)
            {
                currentMinutes = 0;
                currentHour++;
            }
        }

        public void update(double deltaTime) //make sure deltaTime is in seconds not milliseconds
        {
            secondsSinceBeginning += deltaTime;
            secondsSinceIncrement += deltaTime;

            if (secondsSinceIncrement >= secondsUntilIncrement)
            {
                incrementTime();
                secondsSinceIncrement -= secondsUntilIncrement;
            }

        }

        public bool isOver()
        {
            return secondsSinceBeginning >= secondsUntilGameOver;
        }


    }
}
