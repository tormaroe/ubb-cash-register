# ubb-kata-cachregister

First, review the solution, inclduing the tests. Run the program and try to use the different commands.


## User story 1A: Clean up Execute

As we have added more and more features to CashRegister 3000 the current design has become difficult to work with and extend. Apply the Open/Closed principle, making it possible to extend the register with commands without having to modify `Register.cs`.

Hint: Command pattern


## User story 1B: Configurable command list

As an IT manager in our store I want to be able to configure which commands are available for our cash registers.


## User story 2: Optional features

As a store manager I want to be able to start the cash register with some optional features. The optional features are:

1. For security reasons, log all input to a transaction log.
2. Some commands require authorization (user must enter a password if he/she hasn't done so during the last two minutes). The commands are: `cat`, `load`.
3. If the register operator triggers three or more errors within 10 minutes, notify a supervisor (somehow).

As the functionality in `Register.cs` is critical for our business, adding these extra features should be done in the safest way possible.

Hint: Decorator pattern, possibly also Builder pattern.


## User story 3: Optional checkout calculations

Sometimes CashRegister 3000 is used in environments where checkout calculations needs to be done with "less exactness". Make an option to run with checkout rules that only use whole monetary units. If the total shopping cart prize is X.49 or lower, round down to X. If the total is higher, round up.

Implement unit tests.

Hint: Strategy pattern.


## User story 4: Bonus rules

Make it possible to configure rules like:

- For each 3 pepsimax bought, refund the price of one
- For each pizza bought, the prize of one pepsimax is reduced by 50%

The rules can be hardcoded in C# to save development time, but a mechanism to plug rules in or out should be made.

Implement unit tests.

What patterns are helpfull here?