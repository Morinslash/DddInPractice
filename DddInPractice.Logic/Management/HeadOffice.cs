using static DddInPractice.Logic.Money;

namespace DddInPractice.Logic.Management;

public class HeadOffice : AggregateRoot
{
    public virtual decimal Balance { get; protected set; }
    public virtual Money Cash { get; protected set; } = None;

    public virtual void ChangeBalance(decimal delta)
    {
        Balance += delta;
    }
}