using DDDCore;
using DDDCore.Model;
using Main.Controller;
using Main.EventHandler.View;
using Main.Input;
using Main.Input.Event;
using Main.Input.Events;
using Main.presenter;
using Main.GameDataStructure;
using Main.UseCase.Actor.Create;
using Main.UseCase.Actor.Edit;
using Main.UseCase.Repository;
using Main.ViewComponent.Events;
using Zenject;

namespace Main.Application
{   
    public class BattleBinder: MonoInstaller
    {
        public override void InstallBindings() {

        #region Event

            SignalBusInstaller.Install (Container);
            Container.DeclareSignal<DomainEvent>();
            Container.DeclareSignal<InputHorizontal>();
            Container.DeclareSignal<ButtonDownJump>();
            Container.DeclareSignal<ButtonDownAttack>();
            Container.DeclareSignal<AnimEvent>();
            Container.DeclareSignal<HitBoxTriggered>();
            
            
            Container.Bind<EventStore>().AsSingle().NonLazy();
            Container.Bind<IDomainEventBus>().To<DomainEventBus>().AsSingle();

        #endregion

        #region EventHandler
                                         
            Container.Bind<ViewEventHandler>().AsSingle().NonLazy();

        #endregion

        #region Controller

            Container.Bind<ActorController>().AsSingle();

        #endregion

        #region Repository

            Container.Bind<ActorRepository>().AsSingle();
            Container.Bind<IDataRepository>().To<DataRepository>().AsSingle();

        #endregion


        #region UseCase

            Container.Bind<CreateActorUseCase>().AsSingle();
            Container.Bind<ChangeDirectionUseCase>().AsSingle();
            Container.Bind<DealDamageUseCase>().AsSingle();
            Container.Bind<MakeActorDieUseCase>().AsSingle();

        #endregion
            
        #region Mapper

            Container.Bind<ActorMapper>().AsSingle();

        #endregion

        #region Input

            Container.BindInterfacesAndSelfTo<InputManager>().AsSingle();

        #endregion
        }
    }       
}            