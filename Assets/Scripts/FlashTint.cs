using UnityEngine;
using System.Collections;

public class FlashTint : MonoBehaviour
{
    SpriteRenderer sr;

    public Color blinkColor = new Color(2, 0, 0);

    private bool isBlinking = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Blink ()
    {
        sr.color = blinkColor;
    }

    void Update()
    {
         sr.color = Color.Lerp(sr.color, Color.white, Time.deltaTime / 0.5f);
    }
}
