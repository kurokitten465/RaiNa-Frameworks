using UnityEngine;
using TMPro;
using RaiNa.Unity.Services;

namespace RaiNa.Unity.Samples.Singleton
{
    public class RaiNaGameManager : MonoSingleton<RaiNaGameManager>
    {
        #region Singleton Setup

        // To makes the code works on Awake() we must override from OnInitialized()
        // To preventing singleton initialize failed
        public override void OnInitialized()
        {
            _scoreText.text = $"RaiNa Singleton Score : {_score}";
        }

        #endregion

        #region Main Works

        [SerializeField] private TMP_Text _scoreText;

        private int _score = 0;

        public void AddScore()
        {
            _scoreText.text = $"RaiNa Singleton Score : {_score++}";
        }

        #endregion
    }
}
