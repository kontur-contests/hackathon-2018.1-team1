using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spektr;

public class HookController : MonoBehaviour {

    private SliderJoint2D distanceJoint;
    private int hookableLayer = -1;
    private int hookableLayerMask = -1;
    private TargetJoint2D[] hooks;
    private LineRenderer[] lines;
    private int lengthOfLineRenderer = 3;
    private int currentHookIndex = -1;
    private bool allHooksEnabled = false;

    public float hookSpeed = 10f;
    public int hooksCount = 2;


    // Use this for initialization
    void Start() {
        //distanceJoint = GetComponent<SliderJoint2D>();
        hookableLayer = LayerMask.NameToLayer("HookableTerrain");
        hookableLayerMask = LayerMask.GetMask("HookableTerrain");

        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.widthMultiplier = 0.05f;
        lineRenderer.positionCount = lengthOfLineRenderer;

        //Spektr.LightningRenderer lineElectricityRenderer = gameObject.AddComponent<Spektr.LightningRenderer>();
        //lineElectricityRenderer.shader = Shader.Find("Hidden/Lightning");
        //lineElectricityRenderer.randomSeed = 0;
        //lineElectricityRenderer.mes

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
	void FixedUpdate () {
        if (Input.GetMouseButtonDown(0))
        {
            var nextHook = GetNextHook();

            Debug.Log("Next hook" + nextHook);

            Vector3 pointerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pointerPosition.z = 0;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, pointerPosition - transform.position, 100f, hookableLayerMask);

            if (hit.collider != null)
            {
                Debug.Log("Collider:" + hit.collider);

                hit.point = new Vector2(hit.point.x, hit.point.y);
                if (!nextHook.enabled)
                {
                    nextHook.enabled = true;
                }

                nextHook.target = hit.point;
            }
        }

        List<Vector3> points = new List<Vector3>();
        List<Vector3> firstHook = new List<Vector3>();
        List<Vector3> secondHook = new List<Vector3>();

        for (var i = 0; i < hooks.Length; i++)
        {
            if (hooks[i].enabled) {
                var firstPoint = new Vector3(hooks[i].target.x, hooks[i].target.y);
                var secondPoint = new Vector3(transform.position.x, transform.position.y);
                var pointsCoordinates = new List<Vector3> { firstPoint, secondPoint };
                points.AddRange(pointsCoordinates);

                if (i == 0) {
                    firstHook.AddRange(pointsCoordinates);
                } else if (i == 1)
                {
                    secondHook.AddRange(pointsCoordinates);
                }
            }
        }

        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPositions(points.ToArray());

        LightningRenderer[] lineElectricity = transform.GetChild(0).GetComponents<Spektr.LightningRenderer>();

        var lrFirst = lineElectricity[0];
        var lrSecond = lineElectricity[1];

        if (firstHook.Count > 0)
        {
            lrFirst.receiverPosition = firstHook[0];
            lrFirst.emitterPosition = firstHook[1];
        }

        if (secondHook.Count > 0)
        {
            lrSecond.receiverPosition = secondHook[0];
            lrSecond.emitterPosition = secondHook[1];
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
