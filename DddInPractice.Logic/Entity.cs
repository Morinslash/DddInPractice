using NHibernate.Proxy;

namespace DddInPractice.Logic;

public abstract class Entity
{
    public virtual long Id { get; protected set; }

    public override bool Equals(object obj)
    {
        var other = obj as Entity;
        if (ReferenceEquals(other, null))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        if (GetRealType() != other.GetRealType())
        {
            return false;
        }

        if (Id == 0 || other.Id == 0)
        {
            return false;
        }

        return Id == other.Id;
    }

    public static bool operator ==(Entity a, Entity b)
    {
        if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
        {
            return true;
        }

        if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
        {
            return false;
        }
        return a.Equals(b);
    }

    public static bool operator !=(Entity a, Entity b) => !(a == b);

    public override int GetHashCode() => (GetType().ToString() + Id).GetHashCode();

    // here we are leaking the persistence specific code to the Domain section, be careful about it!
    private Type GetRealType() => NHibernateProxyHelper.GetClassWithoutInitializingProxy(this);
}