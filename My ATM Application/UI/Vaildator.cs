using My_ATM_Application.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_ATM_Application.UI
{
    public static class Vaildator
    {
        public static T Convert<T>(string prompt)
        {
            bool vaild=false;
            string userInput;
            while (!vaild)
            {
                userInput = Utility.GetUserInput(prompt);
                try
                {
                    var converter = TypeDescriptor.GetConverter(typeof(T));
                    if(converter != null )
                    {
                        return (T) converter.ConvertFromString(userInput);
                    }
                    else
                    {
                        return default;
                    }
                }
                catch
                {
                    Utility.PrintMassage("Invaild Input.Try again.", false);

                }
            }
            return default;
        }
    }
}
