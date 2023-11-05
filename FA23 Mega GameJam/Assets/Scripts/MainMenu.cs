using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] AudioClip MainMenuTheme;

    void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManage.Quit();
        }
    }
    public void PlayGame()
    {
        SceneManage.LoadNextScene();
    }

    public void CreditsScene()
    {
        SceneManage.LoadCredits();
    }

    public void QuitGame()
    {
        SceneManage.Quit(); 
    }
}
