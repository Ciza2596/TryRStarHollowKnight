namespace Main.ViewComponent.Events
{
    public class HitBoxTriggered
    {

        public ActorComponent TriggerActorComponent { get; }

        public HitBoxTriggered(ActorComponent triggerActorComponent) {
            TriggerActorComponent = triggerActorComponent;
        }
    }
}
