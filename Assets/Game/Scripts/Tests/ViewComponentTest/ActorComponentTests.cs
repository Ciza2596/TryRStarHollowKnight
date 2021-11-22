using Main.ViewComponent;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;


public class ActorComponentTests
{
    private GameObject          _gameObject;
    private ActorComponent      _actorComponent;
    private Text                _textComponent;
    private Transform           _rendererTransform;
    private IUnityComponent     _unityComponent;
    private ICharacterCondition _characterCondition;


    [SetUp]
    public void Setup() {
        _gameObject     = new GameObject();
        _actorComponent = _gameObject.AddComponent<ActorComponent>();

    #region Text

        _textComponent                   = _gameObject.AddComponent<Text>();
        _actorComponent.Text_IdAndDataId = _textComponent;

    #endregion

    #region Renderer

        _rendererTransform             = new GameObject("Renderer").transform;
        _actorComponent.ActorTransform = _rendererTransform;

    #endregion

    #region Mock IUnityComponent

        _unityComponent                = Substitute.For<IUnityComponent>();
        _actorComponent.UnityComponent = _unityComponent;
        Assert.NotNull(_actorComponent.UnityComponent);

    #endregion

    #region Character Condition

        _characterCondition                = Substitute.For<ICharacterCondition>();
        _actorComponent.CharacterCondition = _characterCondition;
        Assert.NotNull(_actorComponent.CharacterCondition);

    #endregion
    }


    [Test]
    public void Should_Succeed_When_Call_SetTest() {
        //arrange
        var displayText = "dhipskl";

        //act
        _actorComponent.SetText(displayText);
        //assert
        Assert.NotNull(_actorComponent.Text_IdAndDataId);
        Assert.AreEqual(displayText, _actorComponent.Text_IdAndDataId.text);
    }

    [Test]
    [TestCase(1, -1)]
    [TestCase(0, 1)]
    public void Should_Succeed_When_Call_ChangeDirection(int directionValue, int expectedScaleValue) {
        //act
        _actorComponent.SetDirection(directionValue);
        //assert
        Assert.AreEqual(expectedScaleValue, _rendererTransform.localScale.x);
    }

#region MoveCharactor

    [Test]
    public void Should_Call_MoveCharacter_When_Call_MoveCharacter() {
        //act
        _actorComponent.MoveCharacter();
        //assert
        ShouldCallMoveCharacter();
    }

    [Test]
    [TestCase(true)]
    [TestCase(false)]
    public void Should_Call_MoveCharacter_When_CanMove_Set_Value(bool canMoving) {
        _characterCondition.CanMove().Returns(canMoving);
        //act
        _actorComponent.Update();
        //assert
        if(canMoving)
            ShouldCallMoveCharacter();
        else
            ShouldNotCallMoveCharacter();
    }

    private void ShouldCallMoveCharacter() {
        var movement = _actorComponent.GetMovement();
        _unityComponent.Received(1).MoveCharacter(movement);
    }

    private void ShouldNotCallMoveCharacter() {
        var movement = _actorComponent.GetMovement();
        _unityComponent.DidNotReceive().MoveCharacter(movement);
    }

    [Test]
    [TestCase(true)]
    [TestCase(false)]
    public void Should_Set_Condition_IsOnGround_When_Call_UnityComponent_IsGrounding(bool isGrounding) {
        _characterCondition.IsOnGround = !isGrounding;
        _unityComponent.IsGrounding().Returns(isGrounding);
        //Act
        _actorComponent.Update();
        //Assert
        Assert.AreEqual(isGrounding, _characterCondition.IsOnGround);
    }

#endregion

#region Jump

    [Test]
    public void Should_Is_Jumping_true_When_Call_Jump() {
        //arrange
        _actorComponent.CharacterCondition.IsOnGround = true;
        Assert.AreEqual(true, _actorComponent.CharacterCondition.IsOnGround);
        //act
        _actorComponent.Jump();
        //assert
        Assert.AreEqual(false, _actorComponent.CharacterCondition.IsOnGround);
    }

    [Test]
    public void Should_Call_PlayAnimation_Jump_When_Call_Jump() {
        //act
        _actorComponent.Jump();
        //assert
        _unityComponent.Received(1).PlayAnimation("Jump");
    }

    [Test]
    public void Should_Call_AddForce_When_Call_Jump() {
        var JumpForce = 10;
        _actorComponent.JumpForce = JumpForce;
        //act
        _actorComponent.Jump();
        //assert
        _unityComponent.Received(1).AddForce(Vector2.up * JumpForce);
    }

#endregion

#region Attack

    [Test]
    public void Should_Is_Attacking_true_When_Call_Attack() {
        //arrange
        _actorComponent.CharacterCondition.IsAttacking = false;
        Assert.AreEqual(false, _actorComponent.CharacterCondition.IsAttacking);
        //act
        _actorComponent.Attack();
        //assert
        Assert.AreEqual(true, _actorComponent.CharacterCondition.IsAttacking);
    }

    [Test]
    public void Should_Call_PlayAnimation_Attack_When_Call_Attack() {
        //act
        _actorComponent.Attack();
        //assert
        _unityComponent.Received(1).PlayAnimation("Attack", _actorComponent.OnAttackEnd);
    }

    [Test]
    public void Should_Is_Attacking_False_When_Call_OnAttackEnd() {
        _characterCondition.IsAttacking = true;
        Assert.AreEqual(true, _characterCondition.IsAttacking);
        //act
        _actorComponent.OnAttackEnd();
        //assert
        Assert.AreEqual(false, _characterCondition.IsAttacking);
    }


#endregion
}
