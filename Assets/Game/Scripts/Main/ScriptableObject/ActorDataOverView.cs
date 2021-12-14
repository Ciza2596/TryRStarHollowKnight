using System.Collections.Generic;
using UnityEngine;

namespace Main.GameDataStructure
{
    [CreateAssetMenu (fileName = "ActorDataOverView", menuName = "HK/CreateActorDataOverView")]
    public class ActorDataOverView : UnityEngine.ScriptableObject
    {
        public List<ActorData> ActorDatas = new List<ActorData>();

        public ActorData FindActorData(string actorDataId) {
            return ActorDatas.Find ( data => data.ActorDataId == actorDataId);
        }
    }
}