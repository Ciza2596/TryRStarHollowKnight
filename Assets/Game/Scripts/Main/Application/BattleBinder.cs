using DDDCore;
using DDDCore.Model;
using Main.Controller;
using Main.EventHandler.View;
using Main.Input;
using Main.presenter;
using Main.UseCase.Actor.Create;
using Main.UseCase.Actors.Edit;
using Main.UseCase.Respository;
using Main.ViewComponent;
using Zenject;

namespace Main.Application
{   
    public class BattleBinder: MonoInstaller
    {
        public override void InstallBindings() {

        #region Event

            SignalBusInstaller.Install (Container);
            Container.DeclareSignal<DomainEvent>();
            Container.DeclareSignal<Input_Horizontal>();
            Container.DeclareSignal<ButtonDownJump>();
            Container.DeclareSignal<ButtonDownAttack>();
            Container.DeclareSignal<AnimEvent>();
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

        #endregion


        #region UseCase

            Container.Bind<CreateActorUseCase>().AsSingle();
            Container.Bind<ChangeDirectionUseCase>().AsSingle();

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