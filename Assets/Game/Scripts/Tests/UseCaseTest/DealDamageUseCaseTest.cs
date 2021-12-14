using System;
using Entity.Model;
using Main.UseCase.Actor.Edit;
using Main.UseCase.Repository;
using MainTests.ExtenjectTestFramwork;
using NUnit.Framework;


public class DealDamageUseCaseTest : DDDUnitTestFixture
{
    [Test]
    public void Should_Succeed_When_DealDamage() {
        var actorRepository        = new ActorRepository();
        var dealDamageUseCase = new DealDamageUseCase(domainEventBus, actorRepository);
        var input                  = new DealDamageInput();

        var    health  = 99;
        var    damage  = 89;
        string actorId = Guid.NewGuid().ToString();
        var actor = ActorBuilder.NewInstance()
                                .SetActorId(actorId)
                                .SetHealth(health)
                                .Build();
             
        actorRepository.Save(actor);
        
        input.ActorId = actorId;
        input.Damage  = damage;
        dealDamageUseCase.Execute(input);

        var actorById = actorRepository.FindById(actorId);
        Assert.NotNull(actorById);
        Assert.AreEqual(health - damage, actor.Health);
    }
}
