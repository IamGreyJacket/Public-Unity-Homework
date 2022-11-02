
using UnityEngine.SceneManagement;
using UnityEngine;

using UnityEditor;

public class MenuController : MonoBehaviour
{

    public void OnNewGame_Editor()
    {
        SceneManager.LoadScene("GamePlayScene");
        SceneManager.UnloadSceneAsync("MainMenuScene");
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
