using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class LoadScene : MonoBehaviour
{
    static LoadScene instance;

    [SerializeField] string firstSceneToLoad;
    [SerializeField] float duration = 2f;
    CanvasGroup CanvasGroup;
    Scene lastLoadScene;


    private void Awake()
    {
        instance = this;
        CanvasGroup = GetComponentInChildren<CanvasGroup>();

    }

    void Start()
    {
        lastLoadScene = SceneManager.GetSceneByName(firstSceneToLoad);
        LoadingScene(firstSceneToLoad, duration);
    }

    public static void LoadingScene(string sceneName, float duration) { instance.LoadingSceneInternal(sceneName,duration); }

    public void LoadingSceneInternal(string sceneName, float duration) { StartCoroutine(LoadSceneCoroutine(sceneName, duration)); }


    IEnumerator LoadSceneCoroutine(string sceneName, float duration)
    {
        yield return new WaitForSeconds(duration);
        if (lastLoadScene.isLoaded)
        {
            Tween fadeOut = CanvasGroup.DOFade(1f, 0.3f);
            while (fadeOut.playedOnce) { yield return null; }

            AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(lastLoadScene);
            while (!unloadOperation.isDone) { yield return null; }
        }

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (!loadOperation.isDone) { yield return null; }
        lastLoadScene = SceneManager.GetSceneByName(sceneName);

        Tween fadeIn = CanvasGroup.DOFade(0f, 0.3f);
        while (fadeIn.playedOnce) { yield return null; }
    }
}


