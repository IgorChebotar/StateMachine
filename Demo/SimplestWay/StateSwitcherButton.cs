using UnityEngine;
using UnityEngine.UI;

namespace SimpleMan.StateMachineSimpleDemo
{
    public class StateSwitcherButton : MonoBehaviour
    {
        public EStates state;
        private Button _button;

        private void OnEnable()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Clicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Clicked);
        }

        private void Clicked()
        {
            ExampleStateMachine stateMachine = SceneManager.instance.stateMachine;
            stateMachine.SwitchState(state);
        }
    }
}

