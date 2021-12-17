using DDDCore.Model;

namespace Main.Entity.State.Events
{
    public class StateCreated: DomainEvent
    {
    #region Public Variables
        
        public string ActorId   { get; }
        public string StateName { get; }
        public int    Amount    { get; }

    #endregion

    #region Constructor

        public StateCreated( string actorId,
                             string stateName,
                             int    amount) {
            ActorId   = actorId;
            StateName = stateName;
            Amount    = amount;
        }

    #endregion
    }
}
