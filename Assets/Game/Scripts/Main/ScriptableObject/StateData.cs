using System;
using Main.DomainData;
using UnityEngine;

namespace Main.GameDataStructure
{
    [Serializable]
    public class StateData: IStateData
    {
        [SerializeField]
        private string _stateName;
        [SerializeField]
        private int _amount;

        public string StateName => _stateName;
        public int    Amount    => _amount;
    }
}
