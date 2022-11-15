using Bouvet.CashRegister.Core;
using Tommy;

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

        var config = ReadConfig();
        var register = MakeRegister(config);

        register.Execute("menu");

        while (true)
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

    private static ICommandExecutor MakeRegister(Config config)
    {
        var builder = new CommandExecutorBuilder()
            .WithCommands(config.CommandsToLoad);
        
        if (config.IsLoggingEnabled)
        {
            Console.WriteLine("Input logging enabled");
            builder.WithLogging(config.LoggingFilepath);
        }
        
        if (config.IsAuthorizationEnabled)
        {
            Console.WriteLine("Authorization enabled");
            builder.WithAuthorization(config.AuthorizationPassword);
        }
        
        if (config.IsSupervisorNotificationEnabled)
        {
            Console.WriteLine("Supervisor notification enabled");
            builder.WithSupervisorNotification(config.Supervisor);
        }

        return builder.Build();
    }

    private class Config
    {
        public List<string> CommandsToLoad { get; } = new();
        public bool IsLoggingEnabled { get; set; }
        public string LoggingFilepath { get; set; } = "out.log";
        public bool IsAuthorizationEnabled { get; set; }
        public string AuthorizationPassword { get; set; } = Guid.NewGuid().ToString();
        public bool IsSupervisorNotificationEnabled { get; set; }
        public string Supervisor { get; set; } = "UNKNOWN";
    }

    private static Config ReadConfig()
    {
        Console.WriteLine("Reading config.toml...");
        using (var reader = File.OpenText("config.toml"))
        {
            TomlTable table = TOML.Parse(reader);
            var config = new Config();
            
            config.IsLoggingEnabled = table["logging"]["enabled"].AsBoolean;
            config.LoggingFilepath = table["logging"]["filepath"];

            config.IsAuthorizationEnabled = table["authorization"]["enabled"].AsBoolean;
            config.AuthorizationPassword = table["authorization"]["password"];

            config.IsSupervisorNotificationEnabled = table["error_notification"]["enabled"].AsBoolean;
            config.Supervisor = table["error_notification"]["supervisor"];

            config.CommandsToLoad.AddRange(table["commands_to_load"].AsArray.Children.Select(x => (string)x));
            return config;
        }
    }
}