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
            String[] timeParts = time.Split(':');
            int hour, minutes;

            try
            {
                hour = Convert.ToInt32(timeParts[0]);
                minutes = Convert.ToInt32(timeParts[1]);
            }
            catch(Exception e)
            {
                return false;
            }

            if (hour < 0 || hour > 23 || minutes < 0 || minutes > 59) return false;
            else return true;
        }
    }
}
