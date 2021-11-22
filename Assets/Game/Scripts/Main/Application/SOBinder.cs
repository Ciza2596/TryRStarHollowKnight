using Main.ScriptableObject;
using UnityEngine;
using Zenject;

namespace Main.Application
{
    [CreateAssetMenu(fileName =  "SOBinder", menuName = "HK/SoBinder")]
    public class SOBinder : ScriptableObjectInstaller
    {
        public ActorDataOverView ActorDataOverView;         

        public override void InstallBindings() {
            Container.BindInstance (ActorDataOverView.ActorDatas);
            Container.BindInstance (ActorDataOverView);
        }
    }
}