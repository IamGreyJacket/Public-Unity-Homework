using System.Collections;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace Arkanoid.Units.Player
{

    public class PlayerController : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private PlayerControls _controls;
        [SerializeField, Tooltip("Скорость перемещения платформы.")]
        private float _speed = 1.5f;
        [SerializeField, Tooltip("Переменная, указывающая на то, управляется ли платформа Игроком 1. (Work in progress)")]
        private bool _isPlayer1 = false;

        private bool IsShot = false;
        UnityEngine.InputSystem.InputAction player;

        private void Awake()
        {
            _controls = new PlayerControls();
        }
        private void OnEnable()
        {
            _controls.GameMap.Enable();

            if (_isPlayer1)
            {
                player = _controls.GameMap.MovementPlayer1;
                _controls.GameMap.Pause.performed += OnPause;
                _controls.GameMap.Shoot.started += OnShoot;
                _controls.GameMap.ResetBall.started += OnResetBall;
            }
            else
            {
                player = _controls.GameMap.MovementPlayer2;
            }
        }

        #region On Controls Input

        private IEnumerator OnMovement()
        {
            if (Managers.GameManager.Self.World.PauseSpeed == (int)Managers.WorldManager.PauseStatus.Paused) yield return null;
            else
            {
                var translate = player.ReadValue<Vector2>();
                if (_isPlayer1)
                {
                    _rigidbody.velocity += new Vector3(translate.x, translate.y, 0f) * _speed * Time.deltaTime;
                    //transform.position += new Vector3(translate.x, translate.y, 0f) * Speed * Time.deltaTime;
                    yield return null;
                }
                else
                {
                    _rigidbody.velocity += new Vector3(translate.x * -1, translate.y, 0f) * _speed * Time.deltaTime;
                    //transform.position += new Vector3(translate.x * -1, translate.y, 0f) * Speed * Time.deltaTime;
                    yield return null;
                }
            }
        }

        private void OnShoot(CallbackContext context)
        {
            if (Managers.GameManager.Self.World.PauseSpeed == (int)Managers.WorldManager.PauseStatus.Paused) return;
            IsShot = Managers.GameManager.Self.World.IsShot;
            if (!IsShot)
            {
                Debug.Log("The ball should be released");
                Managers.GameManager.Self.MoveBall();
            }
        }

        private void OnResetBall(CallbackContext context)
        {

            Managers.GameManager.Self.ResetBall();
        }

        private void OnPause(CallbackContext context) 
        {
            Managers.GameManager.Self.PauseGame();
        }

        #endregion


        private void OnDisable()
        {
            _controls.GameMap.ResetBall.started -= OnResetBall;
            _controls.GameMap.Shoot.started -= OnShoot;
            _controls.GameMap.Pause.performed -= OnPause;

            _controls.GameMap.Disable();
        }
        private void OnDestroy()
        {
            _controls.Dispose();
        }
        // Start is called before the first frame update
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            StartCoroutine(OnMovement());
        }
    }
}
