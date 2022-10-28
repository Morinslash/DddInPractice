using FluentNHibernate.Mapping;
using NHibernate.Event;

namespace DddInPractice.Logic;

public class SnackMap : ClassMap<Snack>
{
    public SnackMap()
    {
        Id(x => x.Id);
        Map(x => x.Name);
    }
}