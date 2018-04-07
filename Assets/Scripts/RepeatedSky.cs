using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatedSky : MonoBehaviour {
    public SpriteRenderer firstBackground;
    public SpriteRenderer secondBackground;
    public float scrollSpeed;
    private float verticalHeight;

    public static void Swap<T>(ref T lhs, ref T rhs)
    {
        T temp = lhs;
        lhs = rhs;
        rhs = temp;
    }

    private void Awake()
    {
        verticalHeight = secondBackground.bounds.size.y;
    }
	
	void Update () {
		if (transform.position.y >= secondBackground.transform.position.y)
        {
            SwapBackgrounds(ref firstBackground, ref secondBackground);
        }
        if (transform.position.y <= firstBackground.transform.position.y)
        {
            SwapBackgrounds(ref secondBackground, ref firstBackground, -1);
        }
	}

    private void SwapBackgrounds(ref SpriteRenderer firstBackground, ref SpriteRenderer secondBackground, int positive=1)
    {
        firstBackground.transform.position = new Vector3(0, secondBackground.transform.position.y + positive * verticalHeight, firstBackground.transform.position.z);
        Swap(ref firstBackground, ref secondBackground);
    }
}
