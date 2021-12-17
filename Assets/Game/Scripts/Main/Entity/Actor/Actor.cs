using DDDCore.Model;
using Main.Entity.Actor.Events;

namespace Main.Entity.Actor
{
    public class Actor: AggregateRoot　
    {

    #region Public Variables
        public string ActorDataId { get; }                  
        public int    Direction   { get; private set; }
        public bool   IsDead      { get; private set;}

    #endregion


    #region Constructor

        public Actor(string actorId, 
                     string actorDataId)
            : base(actorId) {
            
            ActorDataId = actorDataId;
            Direction   = 1;
            AddDomainEvent (new ActorCreated(GetId(), ActorDataId, Direction));
        }

    #endregion

        public void ChangeDirection(int direction) {
            Direction = direction;
            if(!IsDead) 
                AddDomainEvent (new DirectionChanged(GetId(), Direction));
        }

        public void DealDamage(int damage) {
            //Health -= damage;
            
            //AddDomainEvent(new DamageDealt(GetId(),Health));
        }

        public void MakeDie() {
            IsDead = true;
            AddDomainEvent(new ActorDead(GetId()));
        }
    }
}