using Xunit;
using FluentAssertions;
using Bouvet.CashRegister.Core;
using System.Collections.Generic;

namespace Bouvet.CashRegister.Tests;

public class RegisterTests
{
    private Register register;
    private List<string> outputBuffer = new();

    public RegisterTests()
    {
        register = new Register()
        {
            Output = outputBuffer.Add
        };

    }

    [Fact]
    public void Must_add_products_to_catalogue()
    {
        register.Execute("cat pizza 56 500");
        register.Execute("cat pepsimax 20.99 150");
        register.Execute("cat poopbags 14.50 200");

        outputBuffer.Clear();
        register.Execute("list");

        outputBuffer.Should().Contain("  - pizza                         56.00        500");
        outputBuffer.Should().Contain("  - pepsimax                      20.99        150");
        outputBuffer.Should().Contain("  - pizza                         56.00        500");
    }

    [Fact]
    public void Must_add_item_to_shopping_cart()
    {
        register.Execute("cat pizza 56 500");
        
        register.Execute("buy pizza");

        register.ShoppingCart.TotalAmount.Should().Be(1);
        register.ShoppingCart.TotalPrice.Should().Be(56);
    }

    [Fact]
    public void Must_add_multiple_items_to_shopping_cart()
    {
        register.Execute("cat pepsimax 20.99 150");
        register.Execute("cat poopbags 14.50 200");
        
        register.Execute("buy pepsimax");
        register.Execute("buy poopbags");

        register.ShoppingCart.TotalAmount.Should().Be(2);
        register.ShoppingCart.TotalPrice.Should().Be(20.99M + 14.5M);
    }

    [Fact]
    public void Must_add_item_to_shopping_cart_with_amount()
    {
        register.Execute("cat pepsimax 20.99 150");
        
        register.Execute("buy pepsimax 4");

        register.ShoppingCart.TotalAmount.Should().Be(4);
        register.ShoppingCart.TotalPrice.Should().Be(20.99M * 4);
    }

    [Fact]
    public void Must_accept_correct_pay()
    {
        register.Execute("cat pepsimax 20.99 150");
        register.Execute("buy pepsimax");

        outputBuffer.Clear();
        register.Execute("pay 30");

        register.ShoppingCart.TotalPrice.Should().Be(0);
        outputBuffer.Should().Contain("  Amount paid: 30.00");
        outputBuffer.Should().Contain("  Cash back: 9.01");
    }
}