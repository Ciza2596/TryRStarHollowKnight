using System.Collections;
using AutoBot.Scripts.Utilities;
using Main.ViewComponent;
using NUnit.Framework;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.TestTools;


public class UnityComponentTest
{
    [Test]
    public void Should_Velocity_Is_Correct_When_Jump() {
        //arrange
        var gameObject     = new GameObject();
        var rigidbody2D    = gameObject.AddComponent<Rigidbody2D>();
        var unityComponent = new UnityComponent(rigidbody2D);
        //act
        var upForce = Vector2.up * 1234;
        unityComponent.AddForce(upForce);
        //Assert
        Assert.AreEqual(upForce, rigidbody2D.velocity);
    }

    [UnityTest]
    public IEnumerator Should_Play_Animation_Via_Animator_When_Call_PlayAnimation() {
        //arrange
        var objs               = CustomEditorUtility.GetAssets("sword_man");
        var animatorController = objs.Find(obj => obj is AnimatorController) as RuntimeAnimatorController;

        var gameObject = new GameObject();
        var animator   = gameObject.AddComponent<Animator>();
        animator.runtimeAnimatorController = animatorController;
        var unityComponent = new UnityComponent(animator);
        var isJump         = animator.GetCurrentAnimatorStateInfo(0).IsName("Jump");
        Assert.AreEqual(false, isJump);

        var isIdle = animator.GetCurrentAnimatorStateInfo(0).IsName("Idle");
        Assert.AreEqual(true, isIdle);
        //act
        unityComponent.PlayAnimation("Jump");
        yield return null;
        // assert
        isJump = animator.GetCurrentAnimatorStateInfo(0).IsName("Jump");
        Assert.AreEqual(true, isJump);
    }

    [Test]
    public void Should_Position_Is_Correct_When_Call_Move_Character() {
        //arrange
        var gameObject     = new GameObject();
        var transform      = gameObject.transform;
        var unityComponent = new UnityComponent(transform);

        transform.position = new Vector3(0, 0, 0);
        var move = new Vector2(1, 0);
        //act
        unityComponent.MoveCharacter(move);
        //Assert
        Assert.AreEqual(move, (Vector2) transform.position);
    }
}
