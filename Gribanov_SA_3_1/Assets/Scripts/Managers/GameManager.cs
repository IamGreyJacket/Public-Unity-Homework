
using UnityEngine;

namespace Arkanoid.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField, Tooltip("Количество жизней на обоих игроков.")]
        private int _health = 3;

        public static GameManager Self;
        [SerializeField, Tooltip("Ссылка на WorldManager (Work in Progress)")]
        public WorldManager World;
        [SerializeField, Tooltip("Ссылка на контроллер паузы")]
        private Assistants.PauseController _pause;

        private void Awake()
        {
            Self = this;
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (_health <= 0 || World.BlockCount <= 0) GameOver();
        }

        #region Game Management

        public void ReduceHealth()
        {
            _health -= 1;
        }

        public void GameOver()
        {
            Debug.Log($"The game is over!");
            World.ResetBall(); //это я плохо сделал что сюда Ресет запихнул. Постоянно вызывается.
        }

        public void MoveBall()
        {
            StartCoroutine(World.MoveBall());
        }

        public void ResetBall()
        {
            World.ResetBall();
        }

        public void PauseGame()
        {
            World.PauseGame();
            _pause.gameObject.SetActive(true);
        }
        public void UnpauseGame()
        {
            World.UnpauseGame();
        }

        #endregion

        #region Info Getter
        public int GetHealth()
        {
            return _health;
        }
        #endregion
    }
}
