using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigateTo : MonoBehaviour
{
    public void NavigateToScene(string loadScene)
    {
        LoadScene.LoadingScene(loadScene, 0.5f);
    }
    public void OnUnload(string sceneUnLoad)
    {
        SceneManager.UnloadSceneAsync(sceneUnLoad);
    }
}
