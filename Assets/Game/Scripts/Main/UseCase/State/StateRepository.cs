using DDDCore.Usecase;
using Main.Entity.State;

namespace Main.UseCase.Repository
{
    public class StateRepository : AbstractRepository<IState> , IStateRepository
    {
        public IState FindState(string actorId, string stateName) {
            return FindAll().Find(_ => _.ActorId == actorId && _.Name == stateName);
        }
    }
}
