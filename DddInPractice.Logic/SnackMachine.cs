using static DddInPractice.Logic.Money;

namespace DddInPractice.Logic;

public class SnackMachine : AggregateRoot
{
    public virtual Money MoneyInside { get; protected set; }
    public virtual decimal MoneyInTransaction { get; protected set; }
    protected virtual List<Slot> Slots { get; set; }

    public SnackMachine()
    {
        MoneyInside = None;
        MoneyInTransaction = 0;
        Slots = new List<Slot>
        {
            new Slot(this, 1),
            new Slot(this, 2),
            new Slot(this, 3),
        };
    }

    public virtual SnackPile GetSnackPile(int position) => GetSlot(position).SnackPile;


    public virtual void InsertMoney(Money money)
    {
        Money[] coinsAndNotes = { Cent, TenCent, Quarter, Dollar, FiveDollar, TwentyDollar };
        if (!coinsAndNotes.Contains(money))
        {
            throw new InvalidOperationException();
        }

        MoneyInTransaction += money.Amount;
        MoneyInside += money;
    }

    public virtual void ReturnMoney()
    {
        var moneyToReturn = MoneyInside.Allocate(MoneyInTransaction);
        MoneyInside -= moneyToReturn;
        MoneyInTransaction = 0;
    }

    public virtual void BuySnack(int positions)
    {
        var slot = GetSlot(positions);
        if (slot.SnackPile.Price > MoneyInTransaction)
        {
            throw new InvalidOperationException();
        }
        slot.SnackPile = slot.SnackPile.SubstractOne();
        var change = MoneyInside.Allocate(MoneyInTransaction - slot.SnackPile.Price);
        if (change.Amount < MoneyInTransaction - slot.SnackPile.Price)
        {
            throw new InvalidOperationException();
        }
        
        MoneyInside -= change;
        MoneyInTransaction = 0;
    }

    public virtual void LoadSnacks(int position, SnackPile snackPile)
    {
        Slot slot = GetSlot(position);
        slot.SnackPile = snackPile;
    }

    private Slot GetSlot(int position) => Slots.Single(x => x.Position == position);

    public void LoadMoney(Money money)
    {
        MoneyInside += money;
    }
}