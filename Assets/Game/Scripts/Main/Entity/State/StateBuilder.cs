using System;
using DDDCore.Model;

namespace Main.Entity.State
{
    public class StateBuilder : AbstractBuilder<StateBuilder, State>
    {
        private string _stateId;
        private string _stateName;
        private int    _amount;
        private string _actorId;


        public override State Build() {
            _stateId = _stateId == null ? Guid.NewGuid().ToString() : _stateId;
            var state = new State(_stateId, _actorId, _stateName, _amount);

            return state;
        }

        public StateBuilder SetStateId(string stateId) {
            _stateId = stateId;
            return this;
        }

        public StateBuilder SetStateName(string stateName) {
            _stateName = stateName;
            return this;
        }

        public StateBuilder SetAmount(int amount) {
            _amount = amount;
            return this;
        }

        public StateBuilder SetActorId(string actorId) {
            _actorId = actorId;
            return this;
        }
    }
}
