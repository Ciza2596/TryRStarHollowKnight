using DDDCore.Usecase;
using Main.Entity.State;

namespace Main.UseCase.Repository
{
    public interface IStateRepository : IRepository<IState>
    {
        public IState FindState(string actorId, string stateName);
    }
}
