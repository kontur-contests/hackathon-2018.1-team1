using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookController : MonoBehaviour {

    private SliderJoint2D distanceJoint;
    private int terrainLayer = -1;

	// Use this for initialization
	void Start () {
        distanceJoint = GetComponent<SliderJoint2D>();
        terrainLayer = LayerMask.GetMask("Terrain");
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            if (!distanceJoint.enabled)
            {
                Vector3 pointerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pointerPosition.z = 0;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, pointerPosition - transform.position, 100f, terrainLayer);

                Debug.DrawRay(transform.position, pointerPosition - transform.position, Color.green);

                if (hit.collider != null)
                {
                    hit.point = new Vector2(hit.point.x, hit.point.y);

                    distanceJoint.connectedAnchor = hit.point;
                    distanceJoint.enabled = true;
                }
            }
        } else if (Input.GetMouseButtonUp(0))
        {
            distanceJoint.enabled = false;
        }
    }
}
