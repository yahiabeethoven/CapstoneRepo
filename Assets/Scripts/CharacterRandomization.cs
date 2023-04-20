using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterRandomization : MonoBehaviour
{
    private GameObject opponentAvatar;
    public string scene1Name;
    public string scene2Name;

    //private void Start()
    //{
    //    AvatarRandomizationManager avatarManager = AvatarRandomizationManager.Instance;
    //    if (avatarManager != null)
    //    {
    //        opponentAvatar = avatarManager.GetOpponentAvatar();
    //        opponentAvatar.SetActive(false);
    //    }
    //}

    //private void OnEnable()
    //{
    //    SceneManager.sceneLoaded += OnSceneLoaded;
    //}

    //private void OnDisable()
    //{
    //    SceneManager.sceneLoaded -= OnSceneLoaded;
    //}

    //private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    AvatarRandomizationManager avatarManager = AvatarRandomizationManager.Instance;
    //    if (avatarManager != null)
    //    {
    //        foreach (GameObject Char in avatarManager.avatars)
    //        {
    //            if (Char.activeInHierarchy)
    //            {
    //                Char.SetActive(false);
    //                Debug.Log("Deleted avatarsss");
    //            }
    //        }

    //        if (scene.name == scene2Name)
    //        {
    //            Debug.Log("We are in scene 2-------");
    //            // activate opponent avatar if in scene 2
    //            avatarManager.GetOpponentAvatar().SetActive(true);
    //            Debug.Log("set char to active");

    //            // update avatar descriptions
    //            avatarManager.ChangeCanvasScene2();
    //            Debug.Log("Updated canvas in scene 2");
    //        }
    //        else if (scene.name == scene1Name)
    //        {
    //            Debug.Log("We are in scene 1!!!!!!");
    //            if (avatarManager.GetOpponentAvatar().activeInHierarchy)
    //            {
    //                // deactivate opponent avatar if in scene 1
    //                avatarManager.GetOpponentAvatar().SetActive(false);
    //            }

    //            // update avatar descriptions
    //            avatarManager.ChangeCanvasScene1();
    //            Debug.Log("Updated canvas in scene 1");
    //        }
    //    }
    //}
}
