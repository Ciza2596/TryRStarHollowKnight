using DDDCore.Model;

namespace Main.Entity.Actor.Events
{
    public class ActorDead : DomainEvent
    {
        public string ActorId;

        public ActorDead(string actorId) {
            ActorId = actorId;
        }
    }
}