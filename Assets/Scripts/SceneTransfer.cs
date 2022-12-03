using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransfer : MonoBehaviour
{
    public string toScene = "XR";
    public bool changeLayer = true;
    public int toLayer = 0;

    private string previousScene;
    int previousLayer = 0;

    public void Transfer()
    {
        if (gameObject.scene.name == toScene)
        {
            return;
        }
        if (transform.parent != null)
        {
            transform.SetParent(null);
        }
        Scene newScene = SceneManager.GetSceneByName(toScene);
        if (newScene.IsValid())
        {
            previousScene = gameObject.scene.name;
            previousLayer = gameObject.layer;
            SceneManager.MoveGameObjectToScene(gameObject, newScene);
            if (changeLayer)
            {
                gameObject.layer = toLayer;
            }
        }
    }
    public void Return()
    {
        if (previousScene == string.Empty)
        {
            return;
        }
        Scene prevScene = SceneManager.GetSceneByName(previousScene);
        if (!prevScene.IsValid())
        {
            prevScene = SceneManager.GetActiveScene();
        }
        if(changeLayer)
        {
            gameObject.layer = previousLayer;
        }
        if (gameObject.scene.name == prevScene.name)
        {
            return;
        }
        SceneManager.MoveGameObjectToScene(gameObject, prevScene);

    }
}
