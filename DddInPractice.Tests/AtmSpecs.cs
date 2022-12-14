using DddInPractice.Logic;
using DddInPractice.Logic.Atms;
using DddInPractice.Logic.Common;
using FluentAssertions;
using static DddInPractice.Logic.Money;

namespace DddInPractice.Tests;

public class AtmSpecs
{
    [Fact]
    public void Take_Money_Exchanges_Money_With_Commission()
    {
        var atm = new Atm();
        atm.LoadMoney(Dollar);
        
        atm.TakeMoney(1m);

        atm.MoneyInside.Amount.Should().Be(0m);
        atm.MoneyCharged.Should().Be(1.01m);
    }

    [Fact]
    public void Commission_Is_At_Lease_One_Cent()
    {
        var atm = new Atm();
        atm.LoadMoney(Cent);
        
        atm.TakeMoney(0.01m);
        atm.MoneyCharged.Should().Be(0.02m);
    }
    
    [Fact]
    public void Commission_Is_Rounded_Up_To_The_Next_Cent()
    {
        var atm = new Atm();
        atm.LoadMoney(Dollar + TenCent);
        
        atm.TakeMoney(1.1m);
        atm.MoneyCharged.Should().Be(1.12m);
    }
    
    [Fact]
    public void Take_Money_Raises_An_Event()
    {
        var atm = new Atm();
        atm.LoadMoney(Dollar);
        
        atm.TakeMoney(1m);

        atm.ShouldContainBalanceChangedEvent(1.01m);
    }
}

internal static class AtmExtensions
{
    public static void ShouldContainBalanceChangedEvent(this Atm atm, decimal delta)
    {
        BalanceChangedEvent domainEvent = (BalanceChangedEvent)atm.DomainEvents
            .SingleOrDefault(x => x.GetType() == typeof(BalanceChangedEvent));
        domainEvent.Should().NotBeNull();
        domainEvent.Delta.Should().Be(delta);
    }
}