using DDDCore.Model;
using Main.Entity.State.Events;

namespace Main.Entity.State
{
    public interface IState : IAggregateRoot
    {
        
        public string ActorId { get; }
        public string Name    { get;  }
        public int    Amount  { get;  }

        public IState SetAmount(int amount);

    }


    public class State : AggregateRoot, IState
    {
        public State(string id, string actorId, string stateName, int amount)
            : base(id) {
            ActorId = actorId;
            Name    = stateName;
            Amount  = amount;
            AddDomainEvent(new StateCreated(ActorId, Name, Amount ));
        }
        
        public string ActorId { get; private set; }
        public string Name    { get; private set; }
        public int    Amount  { get; private set; }


        public IState SetAmount(int amount) {
            Amount = amount;
            
            AddDomainEvent(new AmountModified(ActorId, Name, Amount));
            return this;
        }
        
    }
}
