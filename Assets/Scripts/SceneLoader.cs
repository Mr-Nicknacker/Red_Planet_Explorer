using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public enum SceneName
    {
        MainMenuScene,
        GameScene,
        GameOverScene
    }
    public static void LoadScene(SceneName sceneName)
    {
        SceneManager.LoadScene(sceneName.ToString());
    }
}
