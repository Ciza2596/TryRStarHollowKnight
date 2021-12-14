
namespace Main.UseCase.Repository
{
    public interface IDataRepository
    {
        ActorDomainData  GetActorDomainData(string actorDataId);
    }
}
