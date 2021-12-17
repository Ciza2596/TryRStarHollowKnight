using DDDCore.Model;

namespace Main.Entity.State.Events
{
    public class AmountModified :DomainEvent
    {
        public string ActorId;
        public string StateName;
        public int    Amount;
        public AmountModified(string actorID, string stateName, int amount) {
            ActorId     = actorID;
            StateName   = stateName;
            Amount = amount;
        }
    }
}
