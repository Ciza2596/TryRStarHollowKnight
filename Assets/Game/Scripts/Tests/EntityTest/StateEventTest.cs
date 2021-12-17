using Main.Entity.State;
using Main.Entity.State.Events;
using NUnit.Framework;

public class StateEventTest
{
    [Test]
    public void Publish_StateCreated() {
        var actorId      = "1230";
        var stateName    = "123";
        var amount       = 99;
        var state        = new State(null, actorId, stateName, amount);
        var stateCreated = state.FindDomainEvent<StateCreated>();
        Assert.NotNull(stateCreated);
        Assert.AreEqual(actorId,   stateCreated.ActorId);
        Assert.AreEqual(stateName, stateCreated.StateName);
        Assert.AreEqual(amount ,   stateCreated.Amount);
    }


    [Test]
    public void Publish_ModifiedAmount() {
        var actorId   = "1230";
        var stateName = "123";
        var amount    = 99;
        var state = StateBuilder.NewInstance()
                                .SetActorId(actorId)
                                .SetStateName(stateName)
                                .SetAmount(amount)
                                .Build();

        int newAmount = 1003;
        state.SetAmount(newAmount);
        
        var amountModified = state.FindDomainEvent<AmountModified>();
        Assert.NotNull(amountModified);
        Assert.AreEqual(actorId,       amountModified.ActorId);
        Assert.AreEqual(stateName,     amountModified.StateName);
        Assert.AreEqual(newAmount, amountModified.Amount);
    }
}
