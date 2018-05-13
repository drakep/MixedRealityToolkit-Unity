using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneSwitcher : MonoBehaviour
{

    public void SwitchScene()
    {
		//int nextLevel = (Application.loadedLevel + 1) % Application.levelCount;
		//Application.LoadLevel(nextLevel);
		Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
	}
}