using DDDCore;
using Main.Entity.State.Events;
using Main.UseCase.Repository;
using Main.UseCase.State;
using NSubstitute;
using NUnit.Framework;

public class CreateStateUseCaseTest
{
    [Test]
    public void Should_Succeed_When_CreateState() {
        var stateName = "123";
        var amount    = 123;
        var actorId   = "123";

        var repository         = new StateRepository();
        var domainEventBus     = Substitute.For<IDomainEventBus>();
        var createStateUseCase = new CreateStateUseCase(domainEventBus, repository);
        var input              = new CreateStateInput();
        
        input.StateName = stateName;
        input.Amount    = amount;
        input.ActorId   = actorId;
        
        createStateUseCase.Execute(input);

        var states = repository.FindAll();
        Assert.AreEqual(1, states.Count);
        var state = states[0];
        
        //Assert properties
        Assert.NotNull(state.GetId());
        Assert.AreEqual(stateName, state.Name);
        Assert.AreEqual(amount,    state.Amount);
        Assert.AreEqual(actorId,    state.ActorId);

        //Assert event
        var stateCreated = state.FindDomainEvent<StateCreated>();
        Assert.NotNull(stateCreated);
        Assert.AreEqual(actorId, stateCreated.ActorId);
        Assert.AreEqual(stateName, stateCreated.StateName);
        Assert.AreEqual(amount, stateCreated.Amount);
        
        domainEventBus.Received(1).PostAll(state);
    }
}

