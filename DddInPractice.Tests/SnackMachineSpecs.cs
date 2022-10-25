using DddInPractice.Logic;
using FluentAssertions;
using static DddInPractice.Logic.Money;

namespace DddInPractice.Tests;

public class SnackMachineSpecs
{
    [Fact]
    public void Return_Money_Empties_Money_In_Transaction()
    {
        var snackMachine = new SnackMachine();
        snackMachine.InsertMoney(Dollar);

        snackMachine.ReturnMoney();

        snackMachine.MoneyInTransaction.Amount.Should().Be(0m);
    }

    [Fact]
    public void Inserted_Money_Goes_To_Money_In_Transaction()
    {
        var snackMachine = new SnackMachine();
        snackMachine.InsertMoney(Cent);
        snackMachine.InsertMoney(Dollar);

        snackMachine.MoneyInTransaction.Amount.Should().Be(1.01m);
    }

    [Fact]
    public void Cannot_Insert_More_Than_One_Coin_Or_Note_At_A_Time()
    {
        var snackMachine = new SnackMachine();
        var twoCent = Cent + Cent;

        Action action = () => snackMachine.InsertMoney(twoCent);

        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Money_In_Transaction_Goes_To_Money_Inside_After_Purchase()
    {
        var snackMachine = new SnackMachine();
        snackMachine.InsertMoney(Dollar);
        snackMachine.InsertMoney(Dollar);

        snackMachine.BuySnack();

        snackMachine.MoneyInTransaction.Should().Be(None);
        snackMachine.MoneyInside.Amount.Should().Be(2m);
    }
}