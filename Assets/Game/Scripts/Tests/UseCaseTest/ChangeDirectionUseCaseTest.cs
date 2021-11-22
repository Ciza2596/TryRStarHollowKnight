using System;
using Entity.Model;
using Main.UseCase.Actors.Edit;
using Main.UseCase.Respository;
using MainTests.ExtenjectTestFramwork;
using NUnit.Framework;

public class ChangeDirectionUseCaseTest : DDDUnitTestFixture
{
    [Test]
    public void Should_Succeed_When_ChangeDirection() {
        var actorRepository        = new ActorRepository();
        var changeDirectionUseCase = new ChangeDirectionUseCase(domainEventBus, actorRepository);
        var input                  = new ChangeDirectionInput();

        string actorId = Guid.NewGuid().ToString();


        var actor = ActorBuilder.NewInstance()
                                .SetActorId(actorId)
                                .Build();

        actorRepository.Save(actor);

        var direction = 123;
        input.ActorId   = actorId;
        input.Direction = direction;
        changeDirectionUseCase.Execute(input);

        var actorById = actorRepository.FindById(actorId);
        Assert.NotNull(actorById);
        Assert.AreEqual(direction, actor.Direction);
    }
}
