using Main.GameDataStructure;
using UnityEngine;
using Zenject;

namespace Main.Application
{
    [CreateAssetMenu(fileName =  "SOBinder", menuName = "HK/SoBinder")]
    public class SOBinder : ScriptableObjectInstaller
    {
        public ActorDataOverview actorDataOverview;         

        public override void InstallBindings() {
            Container.BindInstance (actorDataOverview as IActorDataOverview);
        }
    }
}