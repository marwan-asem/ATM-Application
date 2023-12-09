
using My_ATM_Application.CoreProject.Entities;
using My_ATM_Application.UI;

namespace My_ATM_Application.Application
{
    internal class MainPage
    {
    
        static void Main(string[] args)
        {
           
           // this is a packaup project

           
            ATM_App atmApp = new ATM_App();
            atmApp.InitializeData();
            atmApp.Run();   


            Console.ReadKey();  
        }
    }
}
