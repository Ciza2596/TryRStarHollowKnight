using System.Collections.Generic;

namespace Main.DomainData
{
    public interface IActorData
    {
        string ActorDataId { get; }

        List<IStateData> StateDatas { get; }
    }
}
