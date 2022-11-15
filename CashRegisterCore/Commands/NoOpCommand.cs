namespace Bouvet.CashRegister.Core.Commands;

/*
    In computer science, a NOP, no-op, or NOOP (pronounced "no op"; 
    short for no operation) is a machine language instruction and 
    its assembly language mnemonic, programming language statement, 
    or computer protocol command that does nothing.
    - wikipedia
*/
internal class NoOpCommand : ICommand
{
    public string DescriptiveName => throw new NotImplementedException();

    public string CommandName => throw new NotImplementedException();

    public string UsageExample => throw new NotImplementedException();

    public CommandResult Execute(List<string> arguments, Register register)
    {
        return new CommandResult();
    }
}
