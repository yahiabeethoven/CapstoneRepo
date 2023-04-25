using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
using UnityEditor;
using System.Linq;

public class AvatarRandomizationManager : MonoBehaviour
{
    private static AvatarRandomizationManager instance;

    public static AvatarRandomizationManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        if (avatars.Count == 0)
        {
            LoadPrefabsFromFolder();
        }
    }

    //public GameObject[] avatars;
    public List<GameObject> avatars = new List<GameObject>();
    public GameObject avatarInstance;
    public Sprite[] avatarSprites;

    public int subjectAvatarIndex { get; private set; }
    public int opponentAvatarIndex { get; private set; }
    public string subjectAvatarRace { get; private set; }
    public string opponentAvatarRace { get; private set; }
    public int subject;
    public int opponent;
    public Vector3 position;
    public Quaternion rotation;

    private TMPro.TMP_Text avatarRace;
    public Material handColor;

    Color whiteTone = new Color(255f / 255f, 226f / 255f, 191f / 255f);
    Color asianTone = new Color(255f / 255f, 214f / 255f, 180f / 255f);
    Color arabTone = new Color(179f / 255f, 97f / 255f, 35f / 255f);
    Color blackTone = new Color(102f / 255f, 71f / 255f, 46f / 255f);

    public string folderPath;

    public void Start()
    {
        int avatarCount = avatars.Count;
        if (avatarCount > 0)
        {
            subjectAvatarIndex = Random.Range(0, avatarCount);
            subject = subjectAvatarIndex;
            opponentAvatarIndex = Random.Range(0, avatarCount);
            opponent = opponentAvatarIndex;
        }

        if (avatars.Count > 0)
        {
            avatarInstance = Instantiate(avatars[opponentAvatarIndex], position, rotation);
            DontDestroyOnLoad(avatarInstance);
        }
        else
        {
            Debug.LogWarning("No prefabs found in folder: " + folderPath);
        }


        ChangeHandColor();
    }


    void LoadPrefabsFromFolder()
    {
        avatars.AddRange(Resources.LoadAll<GameObject>(folderPath));
    }

    public void Update()
    {
        if (GameObject.Find("Transporter").GetComponent<TransporterController>().destination == "Area 1")
        {
            ChangeCanvasScene2();
            ChangeCanvasScene1();

        }
        else if (GameObject.Find("Transporter").GetComponent<TransporterController>().destination == "Area 2")
        {
            ChangeCanvasScene1();
        }
    }

    public GameObject GetSubjectAvatar()
    {
        return avatars[subjectAvatarIndex];
    }

    public GameObject GetOpponentAvatar()
    {
        return avatarInstance;
    }

    public void ChangeCanvasScene1()
    {
        GameObject.Find("AvatarSprite").GetComponentInChildren<Image>().sprite = avatarSprites[subjectAvatarIndex];
        avatarRace = GameObject.Find("AvatarDescription").GetComponentInChildren<TMP_Text>();

        if (subjectAvatarIndex == 0)
        {
            avatarRace.text = "You:\nMary Olson";
            subjectAvatarRace = "White_Female";
        }
        else if (subjectAvatarIndex == 1)
        {
            avatarRace.text = "You:\nAisha Khalil";
            subjectAvatarRace = "Arab_Female";
        }
        else if (subjectAvatarIndex == 2)
        {
            avatarRace.text = "You:\nWei Li";
            subjectAvatarRace = "Asian_Female";
        }
        else if (subjectAvatarIndex == 3)
        {
            avatarRace.text = "You:\nDarnell Jackson";
            subjectAvatarRace = "Black_Male";
        }
        else if (subjectAvatarIndex == 4)
        {
            avatarRace.text = "You:\nAbd al-Hakiim Amar";
            subjectAvatarRace = "Arab_Male";
        }
        else if (subjectAvatarIndex == 5)
        {
            avatarRace.text = "You:\nThomas Wagner";
            subjectAvatarRace = "White_Male";
        }
    }

    public void ChangeCanvasScene2()
    {
        avatarRace = GameObject.Find("OpponentDescription").GetComponentInChildren<TMP_Text>();

        GameObject.Find("OpponentSprite").GetComponentInChildren<Image>().sprite = avatarSprites[opponentAvatarIndex];

        if (opponentAvatarIndex == 0)
        {
            avatarRace.text = "Opponent:\nMary Olson";
            opponentAvatarRace = "White_Female";
        }
        else if (opponentAvatarIndex == 1)
        {
            avatarRace.text = "Opponent:\nAisha Khalil";
            opponentAvatarRace = "Arab_Female";
        }
        else if (opponentAvatarIndex == 2)
        {
            avatarRace.text = "Opponent:\nWei Li";
            opponentAvatarRace = "Asian_Female";
        }
        else if (opponentAvatarIndex == 3)
        {
            avatarRace.text = "Opponent:\nDarnell Jackson";
            opponentAvatarRace = "Black_Male";
        }
        else if (opponentAvatarIndex == 4)
        {
            avatarRace.text = "Opponent:\nAbd al-Hakiim Amar";
            opponentAvatarRace = "Arab_Male";
        }
        else if (opponentAvatarIndex == 5)
        {
            avatarRace.text = "Opponent:\nThomas Wagner";
            opponentAvatarRace = "White_Male";
        }
    }

    public void ChangeHandColor()
    {
        if (subjectAvatarIndex == 0)
        {
            handColor.color = whiteTone;
        }
        else if (subjectAvatarIndex == 1)
        {
            handColor.color = arabTone;
        }
        else if (subjectAvatarIndex == 2)
        {
            handColor.color = asianTone;
        }
        else if (subjectAvatarIndex == 3)
        {
            handColor.color = blackTone;
        }
        else if (subjectAvatarIndex == 4)
        {
            handColor.color = arabTone;
        }
        else if (subjectAvatarIndex == 5)
        {
            handColor.color = whiteTone;
        }
    }

    private void OnApplicationQuit()
    {
        avatars.Clear();
    }
}


