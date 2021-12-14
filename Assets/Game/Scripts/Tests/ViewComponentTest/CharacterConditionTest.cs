using Main.ViewComponent;
using NUnit.Framework;


public class CharacterConditionTest
{
    private CharacterCondition _characterCondition;

    [SetUp]
    public void SetUp() {
        _characterCondition = new CharacterCondition();
    }

    [Test]
    [TestCase(true, false, false)]
    [TestCase(true, true,  false)]
    public void Should_Return_True_When_Get_CanMove_With_Arguments(bool isMoving, bool isAttacking, bool isOnGround) {
        //Act
        _characterCondition.IsMoving    = isMoving;
        _characterCondition.IsAttacking = isAttacking;
        _characterCondition.IsOnGround  = isOnGround;

        //Assert
        Should_CanMove_Response(true);
    }

    [Test]
    [TestCase(true, false)]
    [TestCase(false, true)]
    public void Should_Return_False_With_IsDead_True(bool isDead, bool exceptedCanMoving) {
        //Act
        _characterCondition.IsDead   = isDead;
        _characterCondition.IsMoving = true;
        
        //Assert
        Should_CanMove_Response(exceptedCanMoving);
    }

    [Test]
    [TestCase(false, false, false)]
    [TestCase(true,  true,  true)]
    public void Should_Return_False_When_Get_CanMove_With_Arguments(bool isMoving, bool isAttacking, bool isOnGround) {
        //Act
        _characterCondition.IsMoving    = isMoving;
        _characterCondition.IsAttacking = isAttacking;
        _characterCondition.IsOnGround  = isOnGround;

        //Assert
        Should_CanMove_Response(false);
    }

    [Test]
    public void Should_Return_False_When_IsMoving_False() {
        //Assert
        Should_CanMove_Response(false);
    }

    private void Should_CanMove_Response(bool expectedCanMove) {
        var canMove = _characterCondition.CanMove();
        Assert.AreEqual(expectedCanMove, canMove);
    }
}
