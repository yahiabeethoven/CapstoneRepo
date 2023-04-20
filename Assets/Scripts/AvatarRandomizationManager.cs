using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
        int avatarCount = avatars.Length;
        subjectAvatarIndex = Random.Range(0, avatarCount);
        subject = subjectAvatarIndex;
        opponentAvatarIndex = Random.Range(0, avatarCount);
        opponent = opponentAvatarIndex;
    }

    public GameObject[] avatars;
    public Sprite[] avatarSprites;

    public int subjectAvatarIndex { get; private set; }
    public int opponentAvatarIndex { get; private set; }
    public int currentScene;
    public int subject;
    public int opponent;

    private TMPro.TMP_Text avatarRace;
    public Material handColor;

    Color whiteTone = new Color(255f / 255f, 226f / 255f, 191f / 255f);
    Color asianTone = new Color(255f / 255f, 214f / 255f, 180f / 255f);
    Color arabTone = new Color(179f / 255f, 97f / 255f, 35f / 255f);
    Color blackTone = new Color(102f / 255f, 71f / 255f, 46f / 255f);

    //private void Awake()
    //{
    //    if (Instance == null)
    //    {
    //        Instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    public void Start()
    {
        // Randomly select two avatars from the list
        
        ChangeHandColor();
        if (GameObject.Find("Transporter").GetComponent<TransporterController>().destination == "Area 1")
        {
            foreach (GameObject Char in avatars)
            {
                if (Char.activeInHierarchy)
                {
                    Char.SetActive(false);
                }
            }
            GetOpponentAvatar().SetActive(true);
            ChangeCanvasScene2();
        }
        else
        {
            foreach (GameObject Char in avatars)
            {
                if (Char.activeInHierarchy)
                {
                    Char.SetActive(false);
                }
            }
            ChangeCanvasScene1();
        }
    }

    public void Update()
    {
        if (GameObject.Find("Transporter").GetComponent<TransporterController>().destination == "Area 1")
        {
            currentScene = 2;
            foreach (GameObject Char in avatars)
            {
                if (Char != GetOpponentAvatar() && Char.activeInHierarchy)
                {
                    Char.SetActive(false);
                }
            }
            if (!GetOpponentAvatar().activeInHierarchy)
            {
                GetOpponentAvatar().SetActive(true);
            }
            ChangeCanvasScene2();

        }
        else if (GameObject.Find("Transporter").GetComponent<TransporterController>().destination == "Area 2")
        {
            foreach (GameObject Char in avatars)
            {
                if (Char.activeInHierarchy)
                {
                    Char.SetActive(false);
                }
            }
            currentScene = 1;
            ChangeCanvasScene1();
        }
    }

    public GameObject GetSubjectAvatar()
    {
        return avatars[subjectAvatarIndex];
    }

    public GameObject GetOpponentAvatar()
    {
        return avatars[opponentAvatarIndex];
    }

    public void ChangeCanvasScene1()
    {
        GameObject.Find("AvatarSprite").GetComponentInChildren<Image>().sprite = avatarSprites[subjectAvatarIndex];
        avatarRace = GameObject.Find("AvatarDescription").GetComponentInChildren<TMP_Text>();

        if (subjectAvatarIndex == 0)
        {
            avatarRace.text = "YOU:\nRacial Background: White\nGender: Female";
        }
        else if (subjectAvatarIndex == 1)
        {
            avatarRace.text = "YOU:\nRacial Background: Arab\nGender: Female";
        }
        else if (subjectAvatarIndex == 2)
        {
            avatarRace.text = "YOU:\nRacial Background: Asian\nGender: Female";
        }
        else if (subjectAvatarIndex == 3)
        {
            avatarRace.text = "YOU:\nRacial Background: Black\nGender: Male";
        }
        else if (subjectAvatarIndex == 4)
        {
            avatarRace.text = "YOU:\nRacial Background: Arab\nGender: Male";
        }
        else if (subjectAvatarIndex == 5)
        {
            avatarRace.text = "YOU:\nRacial Background: White\nGender: Male";
        }
    }

    public void ChangeCanvasScene2()
    {
        avatarRace = GameObject.Find("OpponentDescription").GetComponentInChildren<TMP_Text>();

        GameObject.Find("OpponentSprite").GetComponentInChildren<Image>().sprite = avatarSprites[opponentAvatarIndex];

        if (opponentAvatarIndex == 0)
        {
            avatarRace.text = "OPPONENT:\nRacial Background: White\nGender: Female";
        }
        else if (opponentAvatarIndex == 1)
        {
            avatarRace.text = "OPPONENT:\nRacial Background: Arab\nGender: Female";
        }
        else if (opponentAvatarIndex == 2)
        {
            avatarRace.text = "OPPONENT:\nRacial Background: Asian\nGender: Female";
        }
        else if (opponentAvatarIndex == 3)
        {
            avatarRace.text = "OPPONENT:\nRacial Background: Black\nGender: Male";
        }
        else if (opponentAvatarIndex == 4)
        {
            avatarRace.text = "OPPONENT:\nRacial Background: Arab\nGender: Male";
        }
        else if (opponentAvatarIndex == 5)
        {
            avatarRace.text = "OPPONENT:\nRacial Background: White\nGender: Male";
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
}
