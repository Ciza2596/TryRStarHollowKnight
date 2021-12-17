using System;
using Main.DomainData;
using Main.GameDataStructure;
using NSubstitute;
using NUnit.Framework;
using Zenject;


public class DataRepositoryTest : ZenjectUnitTestFixture
{
    [Test]
    public void Should_Success_Data_When_GetActorDomainData() {
        //Arrange
        var actorDataId       = Guid.NewGuid().ToString();
        var actorDataOverView = Substitute.For<IActorDataOverview>();
        var actorData         = Substitute.For<IActorData>();
        actorDataOverView.FindActorData(actorDataId).Returns(actorData);

        Container.BindInstance(actorDataOverView).AsSingle();
        Container.Bind<DataRepository>().AsSingle();

        var dataRepository = Container.Resolve<DataRepository>();


        //Ac
        var actorDomainData = dataRepository.GetActorDomainData(actorDataId);

        //Assert
        Assert.NotNull(actorDomainData);
    }
}
