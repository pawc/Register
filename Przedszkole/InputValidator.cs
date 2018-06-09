using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przedszkole
{
    class InputValidator
    {
        public static Boolean validateTime(String time)
        {
            int[] timeConverted = convertTime(time);
            if (timeConverted == null) return false;
            int hour = timeConverted[0];
            int minutes = timeConverted[1];

            if (hour < 0 || hour > 23 || minutes < 0 || minutes > 59) return false;
            else return true;
        }

        private static int[] convertTime(String time)
        {
            String[] timeParts = time.Split(':');
            int hour, minutes;

            try
            {
                hour = Convert.ToInt32(timeParts[0]);
                minutes = Convert.ToInt32(timeParts[1]);
            }
            catch (Exception e)
            {
                return null;
            }
            int[] result = { hour, minutes };
            return result;
        }

        public static Boolean compareTimes(String timeIn, String timeOut)
        {
            int[] convertedTimeIn = convertTime(timeIn);
            int[] convertedTimeOut = convertTime(timeOut);
            if (convertedTimeIn[0] > convertedTimeOut[0]) return false;
            if (convertedTimeIn[0] == convertedTimeOut[0] && convertedTimeIn[1] < convertedTimeOut[1]) return false;
            else return true;
        }
    }
}
