using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadLevel : MonoBehaviour {
    public string level;
    public void LoadScene()
    {
        SceneManager.LoadScene(level);
    }
}
