using DDDCore.Model;

namespace Main.Entity.Actor.Events
{
    public class DirectionChanged: DomainEvent
    {
        public string ActorId;
        public int    Direction;

        public DirectionChanged(string actorId, int direction) {
            ActorId   = actorId;
            Direction = direction;
        }
    }
}