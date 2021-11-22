using NUnit.Framework;
using Main.UseCase.Actor.Create;
using Main.UseCase.Respository;

using MainTests.ExtenjectTestFramwork;

namespace Tests.ActorTests 
{
    public class CreateActorUseCaseTests : DDDUnitTestFixture
    {
        [Test]
        public void Should_Succeed_When_Create_Actor() {

            var actorId     = "1234";
            var actorDataId = "Pokemon";
            
            var actorRepository    = new ActorRepository();
            var createActorUseCase = new CreateActorUseCase (domainEventBus, actorRepository);
            var input              = new CreateActorInput();
            
            input.ActorId     = actorId;
            input.ActorDataId = actorDataId;
            createActorUseCase .Execute (input);

            var actor = actorRepository.FindById (actorId);
            Assert.NotNull (actor);
            Assert.NotNull (actor.GetId());
            Assert.AreEqual (actorId, actor.GetId());
            Assert.NotNull (actor.ActorDataId);
            Assert.AreEqual (actorDataId , actor.ActorDataId);
            //腳色預設面右
            Assert.AreEqual (1, actor.Direction);
        }
    }
}

