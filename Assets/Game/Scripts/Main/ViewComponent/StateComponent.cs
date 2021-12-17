using UnityEngine;
using UnityEngine.UI;

namespace Main.ViewComponent
{
    public class StateComponent: MonoBehaviour
    {
        [SerializeField] private Text _text;

        public void SetText(string stateName, int amount) {
            _text.text = stateName+": " + amount;
        }
    }
}
