using System.Collections.Generic;
using System.Linq;
using Main.DomainData;
using Sirenix.OdinInspector;
using UnityEngine;


namespace Main.GameDataStructure
{
    [CreateAssetMenu(fileName = "ActorData", menuName = "HK/CreateActorData")]
    public class ActorData : ScriptableObject, IActorData
    {
        //不屬於view 跟 Domaon 純粹是這筆資料的Id
        [SerializeField] private string _actorDataId;

        //View Data
        public GameObject ActorPrefab;


        [SerializeField] [ValidateInput("@_health>0")]
        private int _health;

        [SerializeField] [ValidateInput("@_atk>0")]
        private int _atk;
        
        public string           ActorDataId => _actorDataId;
        public List<IStateData> StateDatas  => _stateDatas.Cast<IStateData>().ToList();

        [SerializeField]
        private List<StateData> _stateDatas;
    }
}
