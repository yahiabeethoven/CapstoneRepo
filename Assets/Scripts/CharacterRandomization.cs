using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class CharacterRandomization : MonoBehaviour
{
    public List<GameObject> characters;
    public List<Sprite> avatarSprites;
    public GameObject currentAvatar;
    public Sprite currentSprite;
    public Material handColor;
    public int currentIndex;
    private TMPro.TMP_Text avatarRace;

    Color whiteTone = new Color(255f / 255f, 226f / 255f, 191f / 255f);
    Color asianTone = new Color(255f / 255f, 214f / 255f, 180f / 255f);
    Color arabTone = new Color(179f / 255f, 97f / 255f, 35f / 255f);
    Color blackTone = new Color(102f / 255f, 71f / 255f, 46f / 255f);

    public static CharacterRandomization Instance { get; private set; }
    private void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    public void Start()
    {

        Debug.Log("Randomization script started");
        int x = Random.Range(0, characters.Count);//make dynamic by doing characters size instead of a constant number 5
        foreach (GameObject Char in characters)
        {
            if (Char.activeInHierarchy)
            {
                Char.SetActive(false);
            }
        }
        currentSprite = avatarSprites[x];
        currentAvatar = characters[x];
        characters[x].SetActive(true);
        currentIndex = x;
        Debug.Log("the current character is number: " + x);
        if (GameObject.FindGameObjectWithTag("Transporter").GetComponent<TransporterController>().destination == "Area 2")
        {
            ChangeProjectedCanvas(currentSprite, currentIndex);
            ChangeHandColor(currentIndex);
        }
        
    }
    public void ChangeProjectedCanvas(Sprite spr, int index)
    {
        avatarRace = GameObject.Find("AvatarDescription").GetComponentInChildren<TMP_Text>();

        GameObject.Find("AvatarSprite").GetComponentInChildren<Image>().sprite = spr;

        if (index == 0)
        {
            avatarRace.text = "Ethnic Background:\nWhite Female";
        }
        else if (index == 1)
        {
            avatarRace.text = "Ethnic Background:\nArab Female";
        }
        else if (index == 2)
        {
            avatarRace.text = "Ethnic Background:\nAsian Female";
        }
        else if (index == 3)
        {
            avatarRace.text = "Ethnic Background:\nBlack Male";
        }
        else if (index == 4)
        {
            avatarRace.text = "Ethnic Background:\nArab Male";
        }
        else if (index == 5)
        {
            avatarRace.text = "Ethnic Background:\nWhite Male";
        }
    }

    public void ChangeHandColor(int index)
    {
        if (index == 0)
        {
            handColor.color = whiteTone;
        }
        else if (index == 1)
        {
            handColor.color = arabTone;
        }
        else if (index == 2)
        {
            handColor.color = asianTone;
        }
        else if (index == 3)
        {
            handColor.color = blackTone;
        }
        else if (index == 4)
        {
            handColor.color = arabTone;
        }
        else if (index == 5)
        {
            handColor.color = whiteTone;
        }
    }
}
