using Main.UseCase.Repository;
using Sirenix.OdinInspector;
using  UnityEngine;


namespace Main.GameDataStructure
{
    [CreateAssetMenu(fileName =  "ActorData", menuName = "HK/CreateActorData")]
    public class ActorData: UnityEngine.ScriptableObject
    {
       //不屬於view 跟 Domaon 純粹是這筆資料的Id
        public string ActorDataId;
        
        //View Data
        public GameObject ActorPrefab;
        
        //Domain Data
        public ActorDomainData ActorDomainData;

        [SerializeField] private float _healthMultiply;
        [SerializeField] private float _baseHealth;
        
        
        [Button]
        private void SetHealth() {
            int health = Mathf.RoundToInt(_baseHealth * _healthMultiply);
            ActorDomainData.Health = health;
        }
    }
}