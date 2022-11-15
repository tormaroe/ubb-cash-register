using Bouvet.CashRegister.Core.Commands;

namespace Bouvet.CashRegister.Core;

internal static class CommandDiscovery
{
    /*
        Magical method to automatically find and instantiate
        all classes that implements ICommand.
    */
    internal static List<ICommand> FindAllCommands()
    {
        var commandType = typeof(ICommand);
        
        var concreteCommandTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => t.IsClass && !t.IsAbstract && commandType.IsAssignableFrom(t))
            .Where(t => t != typeof(NoOpCommand)); // Remove the special NoOpCommand
        
        return concreteCommandTypes
            .Select(t => (ICommand)Activator.CreateInstance(t)!)
            .ToList();
    }
}