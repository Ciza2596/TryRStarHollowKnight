using Main.Input.Event;
using Main.Input.Events;
using Rewired;
using Zenject;

namespace Main.Input
{
    public class InputManager: IInitializable, ITickable
    {
        private Player _player;
        private int    _playerId = 0;
        private bool   _initialized;

        private float _lastHorizontalValue = -999;
        
        [Inject] private SignalBus _signalBus;
        
        
        public void Initialize() {
            // Get the Rewired Player object for this player.
            _player = ReInput.players.GetPlayer(_playerId);
            
           _initialized = true;
        }

        public void Tick() {
            if(!ReInput.isReady) return; // Exit if Rewired isn't ready. This would only happen during a script recompile in the editor.
            if(!_initialized) Initialize(); // Reinitialize after a recompile in the editor

            GetInput();
        }
        
        private void GetInput() {
            // Get the input from the Rewired Player. All controllers that the Player owns will contribute, so it doesn't matter
            // whether the input is coming from a joystick, the keyboard, mouse, or a custom controller.

        #region Movement

            // -1:left, 0:no press, 1:right
            var horizontalValue = _player.GetAxisRaw ("Move Horizontal");
            if(_lastHorizontalValue != horizontalValue)
                _signalBus.Fire(new InputHorizontal(horizontalValue));
            _lastHorizontalValue = horizontalValue;    

        #endregion

        #region Jump

            var buttonDown_Jump = _player.GetButtonDown ("Jump");
            if(buttonDown_Jump)
                _signalBus.Fire (new ButtonDownJump());     

        #endregion

        #region Attack

            var buttonDown_Attack = _player.GetButtonDown("Attack");
            if (buttonDown_Attack) {
                _signalBus.Fire (new ButtonDownAttack()); 
            }


        #endregion

        }
    }
}
