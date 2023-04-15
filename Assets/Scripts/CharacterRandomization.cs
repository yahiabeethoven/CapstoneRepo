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
    public Material projectedCanvas;
    public int currentIndex;
    private TMPro.TMP_Text avatarRace;

    private Texture2D canvasBg;

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
            //GameObject.Find("AvatarSprite").GetComponentInChildren<Image>().sprite = currentSprite;
        }
        
    }
    public void ChangeProjectedCanvas(Sprite spr, int index)
    {
        //Material newMaterial = new Material(projectedCanvas);

        //// Assign the texture to the material's main texture property
        //newMaterial.mainTexture = img;

        //// Set the new material on the object's renderer
        //GameObject.Find("Quad").GetComponent<Renderer>().material = newMaterial;
        avatarRace = GameObject.Find("AvatarDescription").GetComponentInChildren<TMP_Text>();

        //TextureToSprite textureToSprite = GameObject.Find("AvatarDescriptionBG").GetComponentInChildren<TextureToSprite>();
        //textureToSprite.sourceImage = img;

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
    
}
