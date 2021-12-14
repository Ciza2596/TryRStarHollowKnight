using NUnit.Framework;
using Main.UseCase.Actor.Create;
using Main.UseCase.Repository;
using MainTests.ExtenjectTestFramwork;
using NSubstitute;


public class CreateActorUseCaseTest : DDDUnitTestFixture
{
    [Test]
    public void Should_Succeed_When_Create_Actor() {

        var actorId     = "1234";
        var actorDataId = "Pokemon";
        var health      = 123;
        
        var dataRepository  = NSubstitute.Substitute.For<IDataRepository>();
        var actorDomainData = new ActorDomainData(){ Health = health };
        dataRepository.GetActorDomainData(actorDataId).Returns(actorDomainData);

        var actorRepository    = new ActorRepository();
        var createActorUseCase = new CreateActorUseCase(domainEventBus, actorRepository, dataRepository);
        var input              = new CreateActorInput();

        input.ActorId     = actorId;
        input.ActorDataId = actorDataId;
        createActorUseCase.Execute(input);

        var actor = actorRepository.FindById(actorId);
        Assert.NotNull(actor);
        Assert.NotNull(actor.GetId());
        Assert.AreEqual(actorId, actor.GetId());
        Assert.NotNull(actor.ActorDataId);
        Assert.AreEqual(actorDataId, actor.ActorDataId);
        //腳色預設面右
        Assert.AreEqual(1, actor.Direction);
        Assert.AreEqual(health, actor.Health);  
        Assert.AreEqual(false, actor.IsDead);
    }
}