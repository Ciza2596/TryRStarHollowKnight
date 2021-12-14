using DDDCore.Model;
using Enitity.Events;
using Entity.Events;

namespace Entity
{
    public class Actor: AggregateRoot　
    {

    #region Public Variables
        public string ActorDataId { get; }                  
        public int    Direction   { get; private set; }
        public int    Health      { get; private set; }
        public bool IsDead      { get; private set;}

    #endregion


    #region Constructor

        public Actor(string actorId, 
                     string actorDataId,
                     int health)
            : base(actorId) {
            
            ActorDataId = actorDataId;
            Health      = health;
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
            Health -= damage;
            
            AddDomainEvent(new DamageDealt(GetId(),Health));
        }

        public void MakeDie() {
            IsDead = true;
            AddDomainEvent(new ActorDead(GetId()));
        }
    }
}