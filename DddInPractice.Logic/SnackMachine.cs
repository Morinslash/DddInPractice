using static DddInPractice.Logic.Money;

namespace DddInPractice.Logic;

public class SnackMachine : AggregateRoot
{
    public virtual Money MoneyInside { get; protected set; } = None;
    public virtual Money MoneyInTransaction { get; protected set; } = None;
    protected virtual List<Slot> Slots { get; set; }

    public SnackMachine()
    {
        MoneyInside = None;
        MoneyInTransaction = None;
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

        MoneyInTransaction += money;
    }

    public virtual void ReturnMoney()
    {
        MoneyInTransaction = None;
    }

    public virtual void BuySnack(int positions)
    {
        var slot = GetSlot(positions);
        slot.SnackPile = slot.SnackPile.SubstractOne();
        MoneyInside += MoneyInTransaction;
        MoneyInTransaction = None;
    }

    public virtual void LoadSnacks(int position, SnackPile snackPile)
    {
        Slot slot = GetSlot(position);
        slot.SnackPile = snackPile;
    }

    private Slot GetSlot(int position) => Slots.Single(x => x.Position == position);
}