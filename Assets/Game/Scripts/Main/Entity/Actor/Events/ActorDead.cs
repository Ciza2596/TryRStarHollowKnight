using DDDCore.Model;

namespace Enitity.Events
{
    public class ActorDead : DomainEvent
    {
        public string ActorId;

        public ActorDead(string actorId) {
            ActorId = actorId;
        }
    }
}