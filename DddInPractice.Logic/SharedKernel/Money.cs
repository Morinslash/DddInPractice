namespace DddInPractice.Logic;

public class Money : ValueObject<Money>
{
    public static readonly Money None = new Money(0, 0, 0, 0, 0, 0);
    public static readonly Money Cent = new Money(1, 0, 0, 0, 0, 0);
    public static readonly Money TenCent = new Money(0, 1, 0, 0, 0, 0);
    public static readonly Money Quarter = new Money(0, 0, 1, 0, 0, 0);
    public static readonly Money Dollar = new Money(0, 0, 0, 1, 0, 0);
    public static readonly Money FiveDollar = new Money(0, 0, 0, 0, 1, 0);
    public static readonly Money TwentyDollar = new Money(0, 0, 0, 0, 0, 1);
    public int OneCentCount { get; }
    public int TenCentCount { get; }
    public int QuarterCentCount { get; }
    public int OneDollarCount { get; }
    public int FiveDollarCount { get; }
    public int TwentyDollarCount { get; }

    public decimal Amount =>
        OneCentCount * 0.01m +
        TenCentCount * 0.10m +
        QuarterCentCount * 0.25m +
        OneDollarCount +
        FiveDollarCount * 5 +
        TwentyDollarCount * 20;

    private Money()
    {
    }

    public Money(int oneCentCount, int tenCentCount, int quarterCentCount, int oneDollarCount, int fiveDollarCount,
        int twentyDollarCount) : this()
    {
        if (oneCentCount < 0)
        {
            throw new InvalidOperationException();
        }

        if (tenCentCount < 0)
        {
            throw new InvalidOperationException();
        }

        if (quarterCentCount < 0)
        {
            throw new InvalidOperationException();
        }

        if (oneDollarCount < 0)
        {
            throw new InvalidOperationException();
        }

        if (fiveDollarCount < 0)
        {
            throw new InvalidOperationException();
        }

        if (twentyDollarCount < 0)
        {
            throw new InvalidOperationException();
        }

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

    public static Money operator -(Money money1, Money money2)
    {
        return new Money(money1.OneCentCount - money2.OneCentCount,
            money1.TenCentCount - money2.TenCentCount,
            money1.QuarterCentCount - money2.QuarterCentCount,
            money1.OneDollarCount - money2.OneDollarCount,
            money1.FiveDollarCount - money2.FiveDollarCount,
            money1.TwentyDollarCount - money2.TwentyDollarCount);
    }

    public static Money operator *(Money money1, int multiplier)
    {
        Money sum = new Money(
            money1.OneCentCount * multiplier,
            money1.TenCentCount * multiplier,
            money1.QuarterCentCount * multiplier,
            money1.OneDollarCount * multiplier,
            money1.FiveDollarCount * multiplier,
            money1.TwentyDollarCount * multiplier);
        return sum;
    }

    protected override bool EqualsCore(Money other) =>
        OneCentCount == other.OneCentCount
        && TenCentCount == other.TenCentCount
        && QuarterCentCount == other.QuarterCentCount
        && OneDollarCount == other.OneDollarCount
        && FiveDollarCount == other.FiveDollarCount
        && TwentyDollarCount == other.TwentyDollarCount;

    protected override int GetHashCodeCore()
    {
        unchecked
        {
            int hashCode = OneCentCount;
            hashCode = (hashCode * 397) ^ TenCentCount;
            hashCode = (hashCode * 397) ^ QuarterCentCount;
            hashCode = (hashCode * 397) ^ OneDollarCount;
            hashCode = (hashCode * 397) ^ FiveDollarCount;
            hashCode = (hashCode * 397) ^ TwentyDollarCount;
            return hashCode;
        }
    }

    public override string ToString()
    {
        if (Amount < 1)
        {
            return "??" + (Amount * 100).ToString("0");
        }

        return "$" + Amount.ToString("0.00");
    }

    public bool CanAllocate(decimal amount)
    {
        var money = AllocateCore(amount);
        return money.Amount == amount;
    }

    public Money Allocate(decimal amount) => CanAllocate(amount) ?  AllocateCore(amount) : throw new
        InvalidOperationException();

    private Money AllocateCore(decimal amount)
    {
        int twentyDollarCount = Math.Min((int)(amount / 20), TwentyDollarCount);
        amount -= twentyDollarCount * 20;

        int fiveDollarCount = Math.Min((int)(amount / 5), FiveDollarCount);
        amount -= fiveDollarCount * 5;

        int oneDollarCount = Math.Min((int)amount, OneDollarCount);
        amount -= oneDollarCount;

        int quarterCount = Math.Min((int)(amount / 0.25m), QuarterCentCount);
        amount -= quarterCount * 0.25m;

        int tenCentCount = Math.Min((int)(amount / 0.1m), TenCentCount);
        amount -= tenCentCount * 0.1m;

        int oneCentCount = Math.Min((int)(amount / 0.01m), OneCentCount);

        return new Money(
            oneCentCount,
            tenCentCount,
            quarterCount,
            oneDollarCount,
            fiveDollarCount,
            twentyDollarCount);
    }
}