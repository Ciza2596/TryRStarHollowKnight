using DDDCore.Model;

namespace Entity.Events
{
    public class ActorCreated : DomainEvent
    {
    #region Public Variables

        public string ActorId     { get; }
        public string ActorDataId { get; }
        public int    Direction   { get; }

    #endregion

    #region Constructor

        public ActorCreated(string  actorId
                            ,string actorDataId
                            ,int direction) {
            ActorId     = actorId;
            ActorDataId = actorDataId;
            Direction   = direction;
        }

    #endregion
    }
}