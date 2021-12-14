using System;
using DDDCore.Model;

namespace Entity.Model
{
    public class ActorBuilder : AbstractBuilder<ActorBuilder, Actor>
    {
        private string _actorId;
        private string _actorDataID;
        private int    _health;

        public override Actor Build() {
            _actorId = (_actorId == null )? Guid.NewGuid().ToString() : _actorId;
            var actor = new Actor (_actorId, 
                                   _actorDataID,
                                   _health);
            return actor;
        }

        public ActorBuilder SetActorId(string actorId) {
            _actorId = actorId;
            return this;
        }

        public ActorBuilder SetActorDataId(string actorDataId) {
            _actorDataID = actorDataId;
            return this;
        }

        public ActorBuilder SetHealth(int health) {
            _health = health;
            return this;
        }
    }
}