using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookController : MonoBehaviour {

    private SliderJoint2D distanceJoint;
    private int hookableLayer = -1;
    private int hookableLayerMask = -1;

    public float hookSpeed = 10f;

    public int hooksCount = 2;
    private TargetJoint2D[] hooks;

    private int currentHookIndex = -1;

    private bool allHooksEnabled = false;

    // Use this for initialization
    void Start() {
        //distanceJoint = GetComponent<SliderJoint2D>();
        hookableLayer = LayerMask.NameToLayer("HookableTerrain");
        hookableLayerMask = LayerMask.GetMask("HookableTerrain");

        hooks = new TargetJoint2D[hooksCount];

        for (var i = 0; i < hooks.Length; i++)
        {
            TargetJoint2D tj = gameObject.AddComponent(typeof(TargetJoint2D)) as TargetJoint2D;
            tj.enabled = false;

            hooks[i] = tj;
        }
    }

    TargetJoint2D GetNextHook()
    {
        currentHookIndex = (currentHookIndex + 1) % hooksCount;
        return hooks[currentHookIndex];
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            var nextHook = GetNextHook();

            Debug.Log("Next hook" + nextHook);

            Vector3 pointerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pointerPosition.z = 0;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, pointerPosition - transform.position, 100f, hookableLayerMask);

            if (hit.collider != null)
            {
                Debug.Log("Collder:" + hit.collider);

                hit.point = new Vector2(hit.point.x, hit.point.y);
                if (!nextHook.enabled)
                {
                    nextHook.enabled = true;
                }

                nextHook.target = hit.point;
            }
        }

        //    var allHooksEnabled = false;

        //    for (var i = 0; i < hooks.Length; i++)
        //    {
        //        var hook = hooks[i];

        //        if (!hook.enabled)
        //        {
        //            Vector3 pointerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //            pointerPosition.z = 0;
        //            RaycastHit2D hit = Physics2D.Raycast(transform.position, pointerPosition - transform.position, 100f, hookableLayerMask);

        //            if (hit.collider != null)
        //            {
        //                hit.point = new Vector2(hit.point.x, hit.point.y);

        //                hook.connectedAnchor = hit.point;
        //                hook.enabled = true;
        //                hook.useMotor = true;
        //            }

        //            return;
        //        } else if (i == hooks.Length - 1)
        //        {
        //            allHooksEnabled = true;
        //            break;
        //        }

        //    }

        //    if (allHooksEnabled)
        //    {
        //        for (var i = 0; i < hooks.Length; i++)
        //        {
        //            var hook = hooks[i];

        //            if (hook.enabled)
        //            {
        //                hook.enabled = false;
        //                return;
        //            }
        //        }
        //    }
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == hookableLayer)
        {

        }
    }
}
