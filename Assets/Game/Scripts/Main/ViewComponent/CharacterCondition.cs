using System;
using Main.ViewComponent;
using Sirenix.OdinInspector;

namespace Main.ViewComponent
{
    public interface ICharacterCondition
    {
        public bool IsMoving    { get; set; }
        public bool IsAttacking {get;  set; }
        public bool IsOnGround  { get; set; }

        public bool CanMove();
    }

    [Serializable]
    public class CharacterCondition : ICharacterCondition
    {
    #region Public Variables
        [ShowInInspector]
        public bool IsMoving    { get; set; }
        [ShowInInspector]
        public bool IsAttacking { get; set; }
        [ShowInInspector]
        public bool IsOnGround  { get; set; }

    #endregion

    #region public Methods

        public bool CanMove() {
            //在地面且攻擊，不可移動，空中可以左右移動
            if (IsMoving) {
                if (IsAttacking) {
                    if (IsOnGround)
                        //Ground
                        return false;
                    //Air
                    return true;
                }

                return true;
            }

            return false;
        }

    #endregion
    }
}