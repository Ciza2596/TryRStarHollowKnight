using System.Collections.Generic;
using Main.GameDataStructure;
using Main.ViewComponent;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Main.presenter
{
    public class ActorMapper
    {
        private List<ActorViewData> _actorViewDatas = new List<ActorViewData>();

        [Inject] private DiContainer       _container;
        [Inject] private IActorDataOverview _actorDataOverview;

        public void CreateActorViewData(string actorId, string actorDataId, int direction) {
            var actorData   = _actorDataOverview.FindActorData(actorDataId) as ActorData;
            var actorPrefab = actorData.ActorPrefab;
            var actorInstance =
                _container.InstantiatePrefab(actorPrefab, Random.insideUnitCircle * 5, Quaternion.identity, null);

            var actorComponent   = actorInstance.GetComponent<ActorComponent>();
            var text_IdAndDataId = $"{actorDataId} - {actorId.Substring(actorId.Length - 2, 1)}";
            //var health        = actorData.Health;
            actorComponent.SetIdAndDataId(text_IdAndDataId);
            actorComponent.SetDirection(direction);
            //actorComponent.SetHealthText(health);
            var actorViewData = new ActorViewData(actorId, actorDataId, actorComponent);
            _actorViewDatas.Add(actorViewData);
        }

        public ActorComponent GetActorComponent(string actorId) {
            var actorViewData = _actorViewDatas.Find(data => data.ActorId == actorId);
            return actorViewData.ActorComponent;
        }
    }

    public class ActorViewData
    {
        public string         ActorId        { get; }
        public string         ActorDataId    { get; }
        public ActorComponent ActorComponent { get; }

        public ActorViewData(string actorId, string actorDataId, ActorComponent actorComponent) {
            ActorId        = actorId;
            ActorDataId    = actorDataId;
            ActorComponent = actorComponent;
        }
    }
}
