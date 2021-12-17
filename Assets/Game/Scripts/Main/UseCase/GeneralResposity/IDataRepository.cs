
using Main.DomainData;

namespace Main.UseCase.Repository
{
    public interface IDataRepository
    {
        IActorData  GetActorDomainData(string actorDataId);
    }
}
