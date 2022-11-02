using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid.Managers
{
    public class WorldManager : MonoBehaviour
    {
        [SerializeField, Tooltip("Список разрушаемых блоков на поле (Work in progress)")]
        private List<GameObject> _blocks;
        public int BlockCount { get; private set; } = 1;
        private static Vector3 s_rotator = new Vector3(1, 1, 1);
        [SerializeField, Tooltip("Переменная определяющая скорость вращения разрушаемых блоков.")]
        private float _rotationSpeed = 10f;

        [SerializeField, Tooltip("Ссылка на игровой мячик. (Work in progress)")]
        private GameObject _ball;
        [SerializeField, Tooltip("Скорость игрового мячика.")]
        private float _ballSpeed = 2;
        [Range(1, 1.5f)]
        private float _addSpeed = 1;
        public int PauseSpeed = 1;
        public bool IsShot = false;
        public Vector3 Direction;
        private Transform _startParent;



        // Start is called before the first frame update
        void Start()
        {

            Direction = _ball.transform.forward;
            _startParent = _ball.transform.parent;
            StartCoroutine(RotateBlocks());
        }

        // Update is called once per frame
        void Update()
        {
            BlockCount = _blocks.Count;
        }

        #region World Management

        public void DestroyBlock(GameObject block)
        {
            _blocks.Remove(block);
            Destroy(block);
        }

        public void ResetBall()
        {
            //to do
            if (PauseSpeed == (float)PauseStatus.Paused) return;
            IsShot = false;
            Direction = _ball.transform.forward;
            _ball.transform.SetParent(_startParent);
            _ball.transform.position = _ball.transform.parent.transform.position + new Vector3(0,0,4);
        }

        public IEnumerator MoveBall()
        {
            if (PauseSpeed == (float)PauseStatus.Paused) yield return null;
            IsShot = true;
            _ball.transform.SetParent(null);
            while (IsShot)
            {
                _ball.transform.Translate(Direction * _addSpeed * _ballSpeed * PauseSpeed * Time.deltaTime);
                yield return null;
            }
            //Мячик должен двигаться вперед и изменять траекторию исходя из данных о столкновении
        }

        private IEnumerator RotateBlocks()
        {
            while (true)
            {

                foreach (var block in _blocks)
                {
                    block.transform.rotation *= Quaternion.Euler(s_rotator * _rotationSpeed * PauseSpeed * Time.deltaTime);
                }
                yield return null;
            }
        }

        public void PauseGame()
        {
            PauseSpeed = (int)PauseStatus.Paused;
            Time.timeScale = 0f;
        }

        public void UnpauseGame()
        {
            Time.timeScale = 1f;
            PauseSpeed = (int)PauseStatus.Playing;
        }

        public enum PauseStatus
        {
            Paused,
            Playing
        }
        #endregion
    }
}
