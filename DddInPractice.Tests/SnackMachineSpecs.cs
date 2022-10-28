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

        snackMachine.MoneyInTransaction.Should().Be(0m);
    }

    [Fact]
    public void Inserted_Money_Goes_To_Money_In_Transaction()
    {
        var snackMachine = new SnackMachine();
        snackMachine.InsertMoney(Cent);
        snackMachine.InsertMoney(Dollar);

        snackMachine.MoneyInTransaction.Should().Be(1.01m);
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
    public void BuySnack_Trades_Inserted_Money_For_A_Snack()
    {
        var snackMachine = new SnackMachine();
        snackMachine.LoadSnacks(1, new SnackPile(new Snack("Some snack"), 10, 1m));
        snackMachine.InsertMoney(Dollar);
        snackMachine.InsertMoney(Dollar);

        snackMachine.BuySnack(1);

        snackMachine.MoneyInTransaction.Should().Be(0);
        snackMachine.GetSnackPile(1).Quantity.Should().Be(9);
    }

    [Fact]
    public void Cannot_Make_Purchase_When_There_Is_Not_Snack()
    {
        var snackMachine = new SnackMachine();
        Action action = () => snackMachine.BuySnack(1);
        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Cannot_Make_Purchase_If_Not_Enough_Money_Inserted()
    {
        var snackMachine = new SnackMachine();
        snackMachine.LoadSnacks(1, new SnackPile(new Snack("Some snack"), 1, 2m));
        snackMachine.InsertMoney(Dollar);

        Action action = () => snackMachine.BuySnack(1);

        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Snack_Machine_Returns_Money_With_Highest_Denomination_First()
    {
        var snackMachine = new SnackMachine();
        snackMachine.LoadMoney(Dollar);
        
        snackMachine.InsertMoney(Quarter);
        snackMachine.InsertMoney(Quarter);
        snackMachine.InsertMoney(Quarter);
        snackMachine.InsertMoney(Quarter);
        snackMachine.ReturnMoney();

        snackMachine.MoneyInside.QuarterCentCount.Should().Be(4);
        snackMachine.MoneyInside.OneDollarCount.Should().Be(0);
    }

    [Fact]
    public void After_Purchase_Change_Is_Returned()
    {
        var snackMachine = new SnackMachine();
        snackMachine.LoadSnacks(1, new SnackPile(new Snack("Some snack"), 1, 0.5m));
        snackMachine.LoadMoney(TenCent * 10);
        
        snackMachine.InsertMoney(Dollar);
        snackMachine.BuySnack(1);

        snackMachine.MoneyInside.Amount.Should().Be(1.5m);
        snackMachine.MoneyInTransaction.Should().Be(0m);
        
    }

    [Fact]
    public void Cannot_Buy_Snack_If_Not_Enough_Change()
    {
        var snackMachine = new SnackMachine();
        snackMachine.LoadSnacks(1, new SnackPile(new Snack("Some snack"), 1, 0.5m));
        snackMachine.InsertMoney(Dollar);
        Action action = () => snackMachine.BuySnack(1);
        action.Should().Throw<InvalidOperationException>();
    }
}