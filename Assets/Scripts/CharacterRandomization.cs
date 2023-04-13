using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterRandomization : MonoBehaviour
{
    public List<GameObject> characters;
    public List<Texture2D> avatarImages;
    public GameObject currentAvatar;
    public Texture2D currentImage;
    public Material projectedCanvas;
    public int currentIndex;
    private TMPro.TMP_Text avatarRace;

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
        currentImage = avatarImages[x];
        currentAvatar = characters[x];
        characters[x].SetActive(true);
        currentIndex = x;
        Debug.Log("the current character is number: " + x);
        if (GameObject.FindGameObjectWithTag("Transporter").GetComponent<TransporterController>().destination == "Area 2")
        {
            ChangeProjectedCanvas(currentImage, currentIndex);
        }
        
    }
    public void ChangeProjectedCanvas(Texture2D img, int index)
    {
        Material newMaterial = new Material(projectedCanvas);

        // Assign the texture to the material's main texture property
        newMaterial.mainTexture = img;

        // Set the new material on the object's renderer
        GameObject.Find("Quad").GetComponent<Renderer>().material = newMaterial;
        avatarRace = GameObject.Find("UpdateAvatar").GetComponentInChildren<TMP_Text>();

        if (index == 0)
        {
            avatarRace.text = "Ethnic Background:\nWhite Female";
        }
        else if (index == 1)
        {
            avatarRace.text = "Ethnic Background:\nArab Female";
        }
        else if (index == 3)
        {
            avatarRace.text = "Ethnic Background:\nAsian Female";
        }
        else if (index == 4)
        {
            avatarRace.text = "Ethnic Background:\nBlack Male";
        }
        else if (index == 5)
        {
            avatarRace.text = "Ethnic Background:\nArab Male";
        }
        else
        {
            avatarRace.text = "Ethnic Background:\nWhite Male";
        }
    }
    
}
