using DddInPractice.Logic;
using FluentAssertions;

namespace DddInPractice.Tests;

public class MoneySpecs
{
    [Fact]
    public void Sum_Of_Two_Moneys_Produces_Correct_Result()
    {
        var money1 = new Money(1,2,3,4,5,6);
        var money2 = new Money(1,2,3,4,5,6);

        var sum = money1 + money2;

        sum.OneCentCount.Should().Be(2);
        sum.TenCentCount.Should().Be(4);
        sum.QuarterCentCount.Should().Be(6);
        sum.OneDollarCount.Should().Be(8);
        sum.FiveDollarCount.Should().Be(10);
        sum.TwentyDollarCount.Should().Be(12);
    }

    [Fact]
    public void Two_Money_Instances_Equal_If_Contain_The_Same_Money_Amount()
    {
        var money1 = new Money(1,2,3,4,5,6);
        var money2 = new Money(1,2,3,4,5,6);

        money1.Should().Be(money2);
        money1.GetHashCode().Should().Be(money2.GetHashCode());
    }
    [Fact]
    public void Two_Money_Instances_Do_Not_Equal_If_Contain_Different_Money_Amount()
    {
        var money1 = new Money(1,0,0,0,0,0);
        var money2 = new Money(0,0,0,0,0,1);

        money1.Should().NotBe(money2);
        money1.GetHashCode().Should().NotBe(money2.GetHashCode());
    }

    [Theory]
    [InlineData(-1,0,0,0,0,0)]
    [InlineData(0,-1,0,0,0,0)]
    [InlineData(0,0,-1,0,0,0)]
    [InlineData(0,0,0,-1,0,0)]
    [InlineData(0,0,0,0,-1,0)]
    [InlineData(0,0,0,0,0,-1)]
    public void Cannot_Create_Money_With_Negative_Value(int oneCent, int tenCent, int quarter, int oneDollar,
        int fiveDollar, int twentyDollar)
    {
        Action action = () => new Money(oneCent, tenCent, quarter, oneDollar, fiveDollar, twentyDollar);

        action.Should().Throw<InvalidOperationException>();
    }
    
    [Theory]
    [InlineData(0,0,0,0,0,0,0)]
    [InlineData(1,0,0,0,0,0,0.01)]
    [InlineData(1,2,0,0,0,0,0.21)]
    [InlineData(1,2,3,0,0,0, 0.96)]
    [InlineData(1,2,3,4,0,0,4.96)]
    [InlineData(1,2,3,4,5,0,29.96)]
    [InlineData(1,2,3,4,5,6,149.96)]
    [InlineData(11,0,0,0,0,0,0.11)]
    [InlineData(110,0,0,0,100,0,501.1)]
    public void Amount_Is_Calculated_Correctly(int oneCent, int tenCent, int quarter, int oneDollar,
        int fiveDollar, int twentyDollar, decimal expectedAmount)
    {
        var money = new Money(oneCent, tenCent, quarter, oneDollar, fiveDollar, twentyDollar);
        money.Amount.Should().Be(expectedAmount);
    }

    [Fact]
    public void Subtraction_Of_Two_Moneys_Produces_Correct_Result()
    {
        var money1 = new Money(10,10,10,10,10,10);
        var money2 = new Money(1,2,3,4,5,6);

        var result = money1 - money2;
        
        result.OneCentCount.Should().Be(9);
        result.TenCentCount.Should().Be(8);
        result.QuarterCentCount.Should().Be(7);
        result.OneDollarCount.Should().Be(6);
        result.FiveDollarCount.Should().Be(5);
        result.TwentyDollarCount.Should().Be(4);
    }

    [Fact]
    public void Cannot_Subtract_More_Than_Exists()
    {
        var money1 = new Money(0,1,0,0,0,0);
        var money2 = new Money(1,0,0,0,0,0);

        Action action = () =>
        {
            Money money = money1 - money2;
        };

        action.Should().Throw<InvalidOperationException>();
    }
}