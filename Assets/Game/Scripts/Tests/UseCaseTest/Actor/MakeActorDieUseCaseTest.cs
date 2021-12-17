using Main.Entity.Actor;
using Main.UseCase.Repository;
using MainTests.ExtenjectTestFramwork;
using NUnit.Framework;
using Main.UseCase.Actor.Edit;

public class MakeActorDieUseCaseTest : DDDUnitTestFixture
{
    [Test]
    public void Should_Succeed_When_MakeActorDie() {
        var actorRepository     = new ActorRepository();
        var makeActorDieUseCase = new MakeActorDieUseCase(domainEventBus, actorRepository);
        var input               = new MakeActorDieInput();

        var actorId = "1234";
        var newActor = ActorBuilder.NewInstance()
                                   .SetActorId(actorId)
                                   .Build();

        actorRepository.Save(newActor);
        var actor = actorRepository.FindById(actorId);

        input.ActorId = actorId;
        Assert.NotNull(actor);
        Assert.AreEqual(false, actor.IsDead);
        makeActorDieUseCase.Execute(input);
        
        Assert.AreEqual(true, actor.IsDead, "actor IsDead false.");
    }
}