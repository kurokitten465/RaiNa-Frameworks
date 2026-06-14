using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;

namespace KuroKitten.UniKuroKit.StateMachine
{
    public static class StateMachinePlayerLoop
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Install()
        {
            var playerLoop = PlayerLoop.GetCurrentPlayerLoop();

            Inject(ref playerLoop, typeof(Update), StateMachineScheduler.Update);
            Inject(ref playerLoop, typeof(FixedUpdate), StateMachineScheduler.FixedUpdate);

            #if UNITY_EDITOR
            UnityEditor.EditorApplication.playModeStateChanged += OnPlayerModeStateChanged;
            #endif

            PlayerLoop.SetPlayerLoop(playerLoop);
        }

        private static void Inject(ref PlayerLoopSystem root, Type target, PlayerLoopSystem.UpdateFunction fn)
        {
            for (int i = 0; i < root.subSystemList.Length; i++)
            {
                ref var sys = ref root.subSystemList[i];

                if (sys.type == target)
                {
                    var list = new List<PlayerLoopSystem>(sys.subSystemList)
                    {
                        new() {
                            type = typeof(StateMachineScheduler),
                            updateDelegate = fn
                        }
                    };

                    sys.subSystemList = list.ToArray();
                    return;
                }
            }
        }

        #if UNITY_EDITOR
        private static void OnPlayerModeStateChanged(UnityEditor.PlayModeStateChange change)
        {
            if (change == UnityEditor.PlayModeStateChange.ExitingEditMode ||
                change == UnityEditor.PlayModeStateChange.ExitingPlayMode)
                PlayerLoop.SetPlayerLoop(PlayerLoop.GetDefaultPlayerLoop());
        }
        #endif
    }
}
