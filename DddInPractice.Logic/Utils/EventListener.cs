using DddInPractice.Logic.Common;
using NHibernate.Event;

namespace DddInPractice.Logic.Utils;

public class EventListener : IPostInsertEventListener, IPostDeleteEventListener, IPostUpdateEventListener,
    IPostCollectionUpdateEventListener
{
    public Task OnPostInsertAsync(PostInsertEvent @event, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void OnPostInsert(PostInsertEvent ev)
    {
        DispatchEvent(ev.Entity as AggregateRoot);
    }

    public Task OnPostDeleteAsync(PostDeleteEvent @event, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void OnPostDelete(PostDeleteEvent ev)
    {
        DispatchEvent(ev.Entity as AggregateRoot);
    }

    public Task OnPostUpdateAsync(PostUpdateEvent @event, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void OnPostUpdate(PostUpdateEvent ev)
    {
        DispatchEvent(ev.Entity as AggregateRoot);
    }

    public Task OnPostUpdateCollectionAsync(PostCollectionUpdateEvent @event, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void OnPostUpdateCollection(PostCollectionUpdateEvent ev)
    {
        DispatchEvent(ev.AffectedOwnerOrNull as AggregateRoot);
    }

    private void DispatchEvent(AggregateRoot aggregateRoot)
    {
        foreach (var domainEvent in aggregateRoot.DomainEvents)
        {
            DomainEvents.Dispatch(domainEvent);
        }
        aggregateRoot.ClearEvents();
    }
}