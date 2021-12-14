using System;
using System.Collections.Generic;
using Main.GameDataStructure;
using Main.UseCase.Repository;
using NUnit.Framework;
using Zenject;


public class DataRepositoryTest : ZenjectUnitTestFixture
{
    [Test]
    public void Should_Success_Data_When_GetActorDomainData() {
       //Arrange
        var actorDataOverView = new ActorDataOverView();
        var actorData         = new ActorData(){ActorDomainData =  new ActorDomainData()};
        
        var actorDataId = Guid.NewGuid().ToString();
        actorData.ActorDataId = actorDataId;
        
        var actorDatas        = new List<ActorData>(){actorData};
        actorDataOverView.ActorDatas = actorDatas;
        
        Container.BindInstance(actorDataOverView).AsSingle();
        Container.Bind<DataRepository>().AsSingle();

        var dataRepository = Container.Resolve<DataRepository>();
        
        //Ac
        var actorDomainData = dataRepository.GetActorDomainData(actorDataId);

        //Assert
        Assert.NotNull(actorDomainData);
    }

}
