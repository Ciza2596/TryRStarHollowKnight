using DDDCore;
using Main.Entity.State;
using Main.Entity.State.Events;
using Main.UseCase.Repository;
using Main.UseCase.State.Edit;
using NSubstitute;
using NUnit.Framework;


public class ModifyAmountUseCaseTest
{
    private string              _stateName;
    private string              _actorId;
    private IStateRepository    _repository;
    private IDomainEventBus     _domainEventBus;
    private ModifyAmountUseCase _modifyAmountUseCase;
    private ModifyAmountInput   _input;
    private IState              _state;

    [SetUp]
    public void SetUp() {
        _stateName = "123";
        _actorId   = "123";

        _repository          = Substitute.For<IStateRepository>();
        _domainEventBus      = Substitute.For<IDomainEventBus>();
        _modifyAmountUseCase = new ModifyAmountUseCase(_domainEventBus, _repository);
        _input               = new ModifyAmountInput();
    }


    [Test]
    public void ModifyAmount() {
        var stateAmount    = 100;
        var amount         = -11;
        var exceptedAmount = stateAmount + amount;

        CreateState(stateAmount);
        SetInput(amount);
        _modifyAmountUseCase.Execute(_input);

        //assert
        //100 + -11 = 89
        //Assert.AreEqual(exceptedAmount, _state.Amount);
        _state.Received(1).SetAmount(exceptedAmount);
        //AssetEventArg(exceptedAmount);
    }

    [Test]
    public void ModifyAmount_When_Amount_Is_Greater_Than_State_Amount() {
        var stateAmount    = 100;
        var amount         = -150;
        var exceptedAmount = 0;

        CreateState(stateAmount);
        SetInput(amount);
        _modifyAmountUseCase.Execute(_input);

        //assert
        //100 + -150 = 0
        //Assert.AreEqual(exceptedAmount, _state.Amount);
        _state.Received(1).SetAmount(exceptedAmount);

        //AssetEventArg(exceptedAmount);
    }


    private void AssetEventArg(int exceptedAmount) {
        _domainEventBus.Received(1).PostAll(_state);

        _state.Received(1).AddDomainEvent(Arg.Is<AmountModified>(d =>
                                                                     d.ActorId      == _actorId
                                                                     && d.StateName == _stateName
                                                                     && d.Amount    == exceptedAmount
                                                                ));
        // var amountModified = _state.FindDomainEvent<AmountModified>();
        // Assert.NotNull(amountModified);
        // Assert.AreEqual(_actorId,       amountModified.ActorID);
        // Assert.AreEqual(_stateName,     amountModified.StateName);
        // Assert.AreEqual(exceptedAmount, amountModified.Amount);
    }

    private void SetInput(int amount) {
        _input.ActorId   = _actorId;
        _input.StateName = _stateName;
        _input.Amount    = amount;
    }

    private void CreateState(int stateAmount) {
        // _state = StateBuilder.NewInstance()
        //                      .SetActorId(_actorId)
        //                      .SetStateName(_stateName)
        //                      .SetAmount(stateAmount)
        //                      .Build();
        _state = Substitute.For<IState>();
        _state.Amount.Returns(stateAmount);
        _repository.FindState(_actorId, _stateName).Returns(_state);
    }
}
