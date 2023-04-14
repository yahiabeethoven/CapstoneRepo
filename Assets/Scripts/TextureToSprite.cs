using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextureToSprite : MonoBehaviour
{
    public Texture2D sourceImage;
    private Sprite bgSprite;
    private Image myImage;

    public void Update()
    {
        myImage = GetComponentInChildren<Image>();
        if (sourceImage != null)
        {
            bgSprite = ConvertTextureToSprite();
            myImage.sprite = bgSprite;
        }

    }

    public Sprite ConvertTextureToSprite()
    {
        // Create a new Sprite from the Texture2D
        //Sprite newSprite = Sprite.Create(img, new Rect(0, 0, img.width, img.height), new Vector2(0.5f, 0.5f));

        Sprite newSprite = Sprite.Create(sourceImage, new Rect(0, 0, sourceImage.width, sourceImage.height), new Vector2(0.5f, 0.5f));

        myImage.material = GameObject.Find("Characters").GetComponent<CharacterRandomization>().projectedCanvas;

        return newSprite;
    }
}
