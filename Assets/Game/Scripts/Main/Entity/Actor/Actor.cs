using DDDCore.Model;
using Entity.Events;

namespace Entity
{
    public class Actor: AggregateRoot　
    {

    #region Public Variables
        public string ActorDataId { get; }                  
        public int    Direction   { get; private set; }                                             

    #endregion


    #region Constructor

        public Actor(string actorId, string actorDataId)
            : base(actorId) {
            
            ActorDataId = actorDataId;
            Direction   = 1;
            AddDomainEvent (new ActorCreated(GetId(), ActorDataId, Direction));
        }

    #endregion

        public void ChangeDirection(int direction) {
            Direction = direction;
            AddDomainEvent (new DirectionChanged(GetId(), Direction));
        }
    }
}