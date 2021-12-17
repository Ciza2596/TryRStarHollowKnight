using System.Collections.Generic;
using Main.DomainData;
using UnityEngine;

namespace Main.GameDataStructure
{
    [CreateAssetMenu (fileName = "ActorDataOverView", menuName = "HK/CreateActorDataOverView")]
    public class ActorDataOverview : ScriptableObject ,IActorDataOverview
    {
        [SerializeField]
        private List<ActorData> _actorDatas = new List<ActorData>();

        public IActorData FindActorData(string actorDataId) {
            return _actorDatas.Find( data => data.ActorDataId == actorDataId);
        }

        public List<ActorData> FindAll() => _actorDatas;
    }

    public interface IActorDataOverview
    {
        IActorData FindActorData(string actorDataId);

        List<ActorData> FindAll();
    }
}