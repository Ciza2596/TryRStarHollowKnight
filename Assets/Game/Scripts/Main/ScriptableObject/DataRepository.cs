using Main.DomainData;
using Main.UseCase.Repository;
using Zenject;

namespace Main.GameDataStructure
{
    public class DataRepository : IDataRepository
    {
        [Inject] private IActorDataOverview _actorDataOverView;

        public IActorData GetActorDomainData(string actorDataId) {
            return _actorDataOverView.FindActorData(actorDataId);
        }
    }
}
