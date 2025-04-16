using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    public static LevelManager Instance;

    [SerializeField]
    private GameObject _loaderCanvas;

    [SerializeField]
    private Image _progressBar;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public async void LoadScene(string sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        _loaderCanvas.SetActive(true);

        do
        {
            await Task.Delay(100);
            _progressBar.fillAmount = scene.progress;
        } while (scene.progress < 0.9f);

        scene.allowSceneActivation = true;

        while (!scene.isDone)
        {
            await Task.Delay(100); 
        }
    }

    public async void LoadSceneIndex(int sceneIndex)
    {
        var scene = SceneManager.LoadSceneAsync(sceneIndex);
        scene.allowSceneActivation = false;

        _loaderCanvas.SetActive(true);

        do
        {
            await Task.Delay(100);
            _progressBar.fillAmount = scene.progress;
        } while (scene.progress < 0.9f);

        scene.allowSceneActivation = true;

        while (!scene.isDone)
        {
            await Task.Delay(100);
        }
    }

}
