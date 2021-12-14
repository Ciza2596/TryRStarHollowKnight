using Main.UseCase.Repository;
using Zenject;

namespace Main.GameDataStructure
{
    public class DataRepository : IDataRepository
    {
        [Inject] private ActorDataOverView _actorDataOverView;

        public ActorDomainData GetActorDomainData(string actorDataId) {
            var actorData       =_actorDataOverView.FindActorData(actorDataId);
            var actorDomainData = actorData.ActorDomainData;
            return actorDomainData;
        }
    }
}
