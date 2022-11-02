using UnityEditor;

using UnityEngine.SceneManagement;

using UnityEngine;
using UnityEngine.UI;

namespace Arkanoid.Managers.Assistants
{
    public class PauseController : MonoBehaviour
    {
        //[SerializeField]
        //private bool _isPlayer1;

        public void OnResume_Editor()
        {
            gameObject.SetActive(false);
            Managers.GameManager.Self.UnpauseGame();
        }

        public void OnRestart_Editor()
        {
            SceneManager.LoadScene("GamePlayScene");
            Time.timeScale = 1f;
        }

        public void OnExit_Editor()
        {
            EditorApplication.isPlaying = false;
        }


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}