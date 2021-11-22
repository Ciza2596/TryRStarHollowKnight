using Entity.Events;
using Entity.Model;
using NUnit.Framework;


public class ActorEventTests
{
    [Test]
    public void Should_Publish_Actor_When_Create_Actor() {
        var actorId     = "1234";
        var actorDataId = "Pokemon";
        var actor = ActorBuilder.NewInstance()
                                .SetActorId(actorId)
                                .SetActorDataId(actorDataId)
                                .Build();
        var domainEvents = actor.GetDomainEvents();
        Assert.AreEqual(1, domainEvents.Count);
        var actorCreated = domainEvents[0] as ActorCreated;
        Assert.AreEqual(actorId,     actorCreated.ActorId);
        Assert.AreEqual(actorDataId, actorCreated.ActorDataId);
        Assert.AreEqual(1,           actorCreated.Direction);
    }

    [Test]
    public void Should_Publish_Change_Direction_When_Change_Direction() {
        var actorId   = "1234";
        var direction = 1234;
        var actor = ActorBuilder.NewInstance()
                                .SetActorId(actorId)
                                .Build();
        actor.ChangeDirection(direction);

        var domainEvents = actor.GetDomainEvents();
        Assert.AreEqual(2, domainEvents.Count);
        var directionChanged = domainEvents[1] as DirectionChanged;
        Assert.AreEqual(actorId,   directionChanged.ActorId);
        Assert.AreEqual(direction, directionChanged.Direction);
    }
}
