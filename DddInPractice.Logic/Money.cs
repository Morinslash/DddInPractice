namespace DddInPractice.Logic;

public class Money
{
    public int OneCentCount { get; private set; }
    public int TenCentCount { get; private set; }
    public int QuarterCentCount { get; private set; }
    public int OneDollarCount { get; private set; }
    public int FiveDollarCount { get; private set; }
    public int TwentyDollarCount { get; private set; }

    public Money(int oneCentCount, int tenCentCount, int quarterCentCount, int oneDollarCount, int fiveDollarCount,
        int twentyDollarCount)
    {
        OneCentCount = oneCentCount;
        TenCentCount = tenCentCount;
        QuarterCentCount = quarterCentCount;
        OneDollarCount = oneDollarCount;
        FiveDollarCount = fiveDollarCount;
        TwentyDollarCount = twentyDollarCount;
    }

    public static Money operator +(Money money1, Money money2)
    {
        Money sum = new Money(
            money1.OneCentCount + money2.OneCentCount,
            money1.TenCentCount + money2.TenCentCount,
            money1.QuarterCentCount + money2.QuarterCentCount,
            money1.OneDollarCount + money2.OneDollarCount,
            money1.FiveDollarCount + money2.FiveDollarCount,
            money1.TwentyDollarCount + money2.TwentyDollarCount
        );
        return sum;
    }
}