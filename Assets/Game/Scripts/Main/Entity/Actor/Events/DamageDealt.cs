using DDDCore.Model;

namespace Entity.Events
{
    public class DamageDealt : DomainEvent
    {
        public DamageDealt(string actorId, int currentHealth) {
            ActorId       = actorId;
            CurrentHealth = currentHealth;

        }
        public string ActorId       { get; private set; }
        public int    CurrentHealth { get; private set; }
    }
}
