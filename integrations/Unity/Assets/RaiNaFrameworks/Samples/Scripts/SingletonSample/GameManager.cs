using TMPro;
using UnityEngine;

namespace RaiNa.Unity.Samples.Singleton
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton Setup

        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            // Very Simple Singleton Pattern
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            //-----------------------------//

            _scoreText.text = $"Normal Singleton Score : {_score}";
        }

        #endregion

        #region Main Works

        [SerializeField] private TMP_Text _scoreText;

        private int _score = 0;

        public void AddScore()
        {
            _scoreText.text = $"Normal Singleton Score : {_score++}";
        }

        #endregion
    }
}
