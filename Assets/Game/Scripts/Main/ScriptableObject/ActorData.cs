using System.Collections.Generic;
using  UnityEngine;


namespace Main.ScriptableObject
{
    [CreateAssetMenu(fileName =  "ActorData", menuName = "HK/CreateActorData")]
    public class ActorData: UnityEngine.ScriptableObject
    {
        public string     ActorDataId;
        public GameObject ActorPrefab;
    }
}