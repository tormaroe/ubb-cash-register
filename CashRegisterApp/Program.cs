using Bouvet.CashRegister.Core;

internal class Program
{
    private static void Main(string[] args)
    {

        Console.WriteLine(@"
        
   .d8888b.                    888      888b     d888                   888                      
  d88P  Y88b                   888      8888b   d8888                   888                      
  888    888                   888      88888b.d88888                   888                      
  888         8888b.  .d8888b  88888b.  888Y88888P888  8888b.  .d8888b  888888 .d88b.  888d888   
  888            ""88b 88K      888 ""88b 888 Y888P 888     ""88b 88K      888   d8P  Y8b 888P""     
  888    888 .d888888 ""Y8888b. 888  888 888  Y8P  888 .d888888 ""Y8888b. 888   88888888 888       
  Y88b  d88P 888  888      X88 888  888 888   ""   888 888  888      X88 Y88b. Y8b.     888       
   ""Y8888P""  ""Y888888  88888P' 888  888 888       888 ""Y888888  88888P'  ""Y888 ""Y8888  888       
                                                                                               
                                                                                               
                                                    .d8888b.   .d8888b.   .d8888b.   .d8888b.  
                                                   d88P  Y88b d88P  Y88b d88P  Y88b d88P  Y88b 
                                                        .d88P 888    888 888    888 888    888 
                                                       8888""  888    888 888    888 888    888 
                                                        ""Y8b. 888    888 888    888 888    888 
                                                   888    888 888    888 888    888 888    888 
                                                   Y88b  d88P Y88b  d88P Y88b  d88P Y88b  d88P 
                                                    ""Y8888P""   ""Y8888P""   ""Y8888P""   ""Y8888P""  
                                                                                               
        ");

        Register register = new();
        register.PrintMenu();

        while(true) 
        {
            Console.Write(">> ");
            var userInput = Console.ReadLine();

            var result = register.Execute(userInput);

            if (result.IsQuit)
            {
                break;
            }
        }
    }
}