using UnityEngine;

namespace RaiNa.Unity.Samples
{
    public class StateMachine : MonoBehaviour
    {
        private enum MachineState { Red, Yellow, Green }

        private MachineState _currentState = MachineState.Red;

        private void Start()
        {
            // Step 1: Enter the initial state when the scene starts.
            EnterState(_currentState);
        }

        private void Update()
        {
            // Step 2: Change state with keyboard input.

            if (Input.GetKeyDown(KeyCode.A))
                ChangeState(MachineState.Red);
            if (Input.GetKeyDown(KeyCode.S))
                ChangeState(MachineState.Yellow);
            if (Input.GetKeyDown(KeyCode.D))
                ChangeState(MachineState.Green);
        }

        private void ChangeState(MachineState nextState)
        {
            // Step 3: Ignore the request if the new state is the same as the current one.
            if (_currentState == nextState)
            {
                return;
            }

            // Step 4: Exit the current state before entering the next one.
            ExitState(_currentState);
            _currentState = nextState;

            // Step 5: Enter the new state and run its setup logic.
            EnterState(_currentState);
        }

        private void EnterState(MachineState state)
        {
            // Step 6: Run the logic for the selected state.
            switch (state)
            {
                case MachineState.Green:
                    Debug.Log("Enter State: Green");
                    break;
                case MachineState.Yellow:
                    Debug.Log("Enter State: Yellow");
                    break;
                case MachineState.Red:
                    Debug.Log("Enter State: Red");
                    break;
            }
        }

        private void ExitState(MachineState state)
        {
            // Step 7: Clean up when leaving a state.
            Debug.Log($"Leaving state: {state}");
        }
    }
}
