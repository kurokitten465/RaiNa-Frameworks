using UnityEngine;
using Kuro.UniKit.DesignPatterns;

namespace Kuro.UniKit.Samples.Singleton
{
    public class SingletonA : MonoSingleton<SingletonA>
    {
        public string Text = "Hello World";

        public void Print()
        {
            Debug.Log($"{this} working!");
            Debug.Log($"Initialize: {IsInitialize}");
            Debug.Log($"Print: {Text}");
        }
    }
}
