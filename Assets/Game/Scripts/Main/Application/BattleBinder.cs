using DDDCore;
using DDDCore.Model;
using Main.Controller;
using Main.DomainEventHandler;
using Main.EventHandler.View;
using Main.Input;
using Main.Input.Event;
using Main.Input.Events;
using Main.presenter;
using Main.GameDataStructure;
using Main.UseCase.Actor.Create;
using Main.UseCase.Actor.Edit;
using Main.UseCase.Repository;
using Main.UseCase.State;
using Main.UseCase.State.Edit;
using Main.ViewComponent.Events;
using Zenject;

namespace Main.Application
{   
    public class BattleBinder: MonoInstaller
    {
        public override void InstallBindings() {

            SignalBusInstaller.Install (Container);
            
            BindEvent();
            BindEventHandler();
            BindController();
            BindRepository();
            BindUseCase();
            BindMapper();

            Container.BindInterfacesAndSelfTo<InputManager>().AsSingle();
            
        }


        private void BindEvent() {
            Container.DeclareSignal<DomainEvent>();
            Container.DeclareSignal<InputHorizontal>();
            Container.DeclareSignal<ButtonDownJump>();
            Container.DeclareSignal<ButtonDownAttack>();
            Container.DeclareSignal<AnimEvent>();
            Container.DeclareSignal<HitBoxTriggered>();

            Container.Bind<EventStore>().AsSingle().NonLazy();
            Container.Bind<IDomainEventBus>().To<DomainEventBus>().AsSingle();
        }

        private void BindEventHandler() {
            Container.Bind<ActorViewEventHandler>().AsSingle().NonLazy();
            Container.Bind<StateViewEventHandler>().AsSingle().NonLazy();
            Container.Bind<NotifyState>().AsSingle().NonLazy();
        }

        private void BindController() {
            Container.Bind<ActorController>().AsSingle();
            Container.Bind<StateController>().AsSingle();
        }

        private void BindRepository() {
            Container.Bind<ActorRepository>().AsSingle();
            Container.Bind<IDataRepository>().To<DataRepository>().AsSingle();
            Container.Bind<IStateRepository>().To<StateRepository>().AsSingle();
        }

        private void BindUseCase() {
            //actor
            Container.Bind<CreateActorUseCase>().AsSingle();
            Container.Bind<ChangeDirectionUseCase>().AsSingle();
            Container.Bind<MakeActorDieUseCase>().AsSingle();

            //State
            Container.Bind<CreateStateUseCase>().AsSingle();
            Container.Bind<ModifyAmountUseCase>().AsSingle();
        }

        private void BindMapper() {
            Container.Bind<ActorMapper>().AsSingle();
        }
    }       
}            