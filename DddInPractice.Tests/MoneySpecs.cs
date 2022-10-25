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
}