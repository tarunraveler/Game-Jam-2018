using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bande : MonoBehaviour
{

    public Sprite[] sprites;
    public float waitTime;
    private int current = 0;
    private Image _image;
    private void Start()
    {
        StartCoroutine(ScorriBande());
        _image = GetComponent<Image>();
        
    }

    IEnumerator ScorriBande()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            current++;
            if (current >= sprites.Length)
                current = 0;
            _image.sprite = sprites[current];
        }
    }
}
