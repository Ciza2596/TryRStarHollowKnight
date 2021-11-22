using Main.ViewComponent;
using NUnit.Framework;


public class CharacterConditionTests
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
        Should_Response(true);
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
        Should_Response(false);
    }

    [Test]
    public void Should_Return_False_When_IsMoving_False() {
        //Assert
        Should_Response(false);
    }

    private void Should_Response(bool expectedCanMove) {
        var canMove = _characterCondition.CanMove();
        Assert.AreEqual(expectedCanMove, canMove);
    }
}
