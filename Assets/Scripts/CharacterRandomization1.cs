//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using TMPro;
//using UnityEngine.UI;


//public class CharacterRandomization : MonoBehaviour
//{
//    public List<GameObject> characters;
//    public List<Sprite> avatarSprites;
//    public GameObject subjectAvatar;
//    public GameObject opponentAvatar;

//    public Sprite subjectSprite;
//    public Sprite opponentSprite;
//    public Material handColor;

//    public int subjectIndex;
//    public int opponentIndex;
//    private TMPro.TMP_Text avatarRace;

//    Color whiteTone = new Color(255f / 255f, 226f / 255f, 191f / 255f);
//    Color asianTone = new Color(255f / 255f, 214f / 255f, 180f / 255f);
//    Color arabTone = new Color(179f / 255f, 97f / 255f, 35f / 255f);
//    Color blackTone = new Color(102f / 255f, 71f / 255f, 46f / 255f);

//    public static CharacterRandomization Instance { get; private set; }
//    private void Awake()
//    {
//        if (Instance != this && Instance != null)
//        {
//            Destroy(this);
//            Debug.Log("Destroyed Other Instance");
//        }
//        else
//        {
//            Instance = this;
//        }
//    }

//    public void Start()
//    {
//        Debug.Log("Randomization script started");
//        int x;
//        int y;

//        if (PlayerPrefs.HasKey("subjectIndex") && PlayerPrefs.HasKey("opponentIndex"))
//        {
//            x = PlayerPrefs.GetInt("subjectIndex");
//            y = PlayerPrefs.GetInt("opponentIndex");
//        }
//        else
//        {
//            x = Random.Range(0, characters.Count);
//            y = Random.Range(0, characters.Count);
//        }

//        foreach (GameObject Char in characters)
//        {
//            if (Char.activeInHierarchy)
//            {
//                Char.SetActive(false);
//            }
//        }
//        subjectSprite = avatarSprites[x];
//        subjectAvatar = characters[x];

//        opponentSprite = avatarSprites[y];
//        opponentAvatar = characters[y];

//        subjectIndex = x;
//        opponentIndex = y;


//        if (GameObject.FindGameObjectWithTag("Transporter").GetComponent<TransporterController>().destination == "Area 2")
//        {
//            ChangeProjectedCanvas1(subjectSprite, subjectIndex);
//            ChangeHandColor(subjectIndex);
//        }
//        else
//        {

//            opponentAvatar.SetActive(true);
//            ChangeProjectedCanvas2(opponentSprite, opponentIndex);
//        }
//    }

//    public void Update()
//    {
//        if (GameObject.FindGameObjectWithTag("Transporter").GetComponent<TransporterController>().destination == "Area 2" && PlayerManager.Instance.hasVisitedArea2 == true)
//        {
//            opponentAvatar.SetActive(false);
//            ChangeProjectedCanvas1(subjectSprite, subjectIndex);
//            ChangeHandColor(subjectIndex);
//        }
//        //else
//        //{
//        //    foreach (GameObject Char in characters)
//        //    {
//        //        if (Char.activeInHierarchy)
//        //        {
//        //            Char.SetActive(false);
//        //        }
//        //    }
//        //    opponentAvatar.SetActive(true);
//        //    ChangeProjectedCanvas2(opponentSprite, opponentIndex);
//        //}
//    }
//    public void ChangeProjectedCanvas1(Sprite spr, int index)
//    {

//        avatarRace = GameObject.Find("AvatarDescription").GetComponentInChildren<TMP_Text>();

//        GameObject.Find("AvatarSprite").GetComponentInChildren<Image>().sprite = spr;

//        if (index == 0)
//        {
//            avatarRace.text = "YOU:\nRacial Background: White\nGender: Female";
//        }
//        else if (index == 1)
//        {
//            avatarRace.text = "YOU:\nRacial Background: Arab\nGender: Female";
//        }
//        else if (index == 2)
//        {
//            avatarRace.text = "YOU:\nRacial Background: Asian\nGender: Female";
//        }
//        else if (index == 3)
//        {
//            avatarRace.text = "YOU:\nRacial Background: Black\nGender: Male";
//        }
//        else if (index == 4)
//        {
//            avatarRace.text = "YOU:\nRacial Background: Arab\nGender: Male";
//        }
//        else if (index == 5)
//        {
//            avatarRace.text = "YOU:\nRacial Background: White\nGender: Male";
//        }
//    }

//    public void ChangeProjectedCanvas2(Sprite spr, int index)
//    {
//        if (!opponentAvatar.activeSelf)
//        {
//            opponentAvatar.SetActive(true);
//        }
//        avatarRace = GameObject.Find("OpponentDescription").GetComponentInChildren<TMP_Text>();

//        GameObject.Find("OpponentSprite").GetComponentInChildren<Image>().sprite = spr;

//        if (index == 0)
//        {
//            avatarRace.text = "OPPONENT:\nRacial Background: White\nGender: Female";
//        }
//        else if (index == 1)
//        {
//            avatarRace.text = "OPPONENT:\nRacial Background: Arab\nGender: Female";
//        }
//        else if (index == 2)
//        {
//            avatarRace.text = "OPPONENT:\nRacial Background: Asian\nGender: Female";
//        }
//        else if (index == 3)
//        {
//            avatarRace.text = "OPPONENT:\nRacial Background: Black\nGender: Male";
//        }
//        else if (index == 4)
//        {
//            avatarRace.text = "OPPONENT:\nRacial Background: Arab\nGender: Male";
//        }
//        else if (index == 5)
//        {
//            avatarRace.text = "OPPONENT:\nRacial Background: White\nGender: Male";
//        }
//    }

//    public void ChangeHandColor(int index)
//    {
//        if (index == 0)
//        {
//            handColor.color = whiteTone;
//        }
//        else if (index == 1)
//        {
//            handColor.color = arabTone;
//        }
//        else if (index == 2)
//        {
//            handColor.color = asianTone;
//        }
//        else if (index == 3)
//        {
//            handColor.color = blackTone;
//        }
//        else if (index == 4)
//        {
//            handColor.color = arabTone;
//        }
//        else if (index == 5)
//        {
//            handColor.color = whiteTone;
//        }
//    }
//}
