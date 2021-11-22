using UnityEngine;
using Zenject;

namespace Main.ViewComponent
{
    public class AnimationCallBack : MonoBehaviour
    {
        [Inject] private SignalBus _signalBus;


        public void OnAnimationEvent(string eventId) {
            _signalBus.Fire (new AnimEvent (eventId));
        }
    }
}