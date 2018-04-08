using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spektr;
using System;
using System.Linq;

public class HookController : MonoBehaviour {

    private SliderJoint2D distanceJoint;
    private int hookableLayer = -1;
    private int hookableLayerMask = -1;
    private TargetJoint2D[] hooks;
    private LineRenderer[] lines;
    private int currentHookIndex = -1;
    private bool allHooksEnabled = false;

    public float maxHookLength = 3f;

    public LineRenderer lineRenderer;

    public float hookSpeed = 10f;
    public int hooksCount = 1;

    private LightningRenderer[] lineElectricity;


    // Use this for initialization
    void Start() {
        //distanceJoint = GetComponent<SliderJoint2D>();
        hookableLayer = LayerMask.NameToLayer("HookableTerrain");
        hookableLayerMask = LayerMask.GetMask("HookableTerrain");

        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.widthMultiplier = 0.05f;
        lineRenderer.positionCount = 0;

        lineElectricity = transform.GetChild(0).GetComponents<Spektr.LightningRenderer>();

        lineElectricity[0].enabled = false;

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
            if (!hooks[0].enabled)
            {
                Vector3 pointerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pointerPosition.z = 0;

                var hits = Physics2D.RaycastAll(transform.position, pointerPosition - transform.position, maxHookLength, hookableLayerMask);

                var lenToPointer = (pointerPosition - transform.position).magnitude;

                var listHits = new List<RaycastHit2D>(hits).OrderBy(hit => Math.Abs((transform.position - (Vector3)hit.point).magnitude - lenToPointer)).ToList();

                

                if (listHits.Count > 0)
                {
                    var firstHit = listHits[0];

                    var point = new Vector2(firstHit.point.x, firstHit.point.y);
                    hooks[0].enabled = true;
                    hooks[0].target = point;

                    lineElectricity[0].enabled = true;

                    lineRenderer.positionCount += 2;
                }



                //foreach (var hit in hits)
                //{
                //    var lenToCollider = (transform.position - (Vector3)hit.point).magnitude;

                //    Debug.Log("LEN TO COLLIDER:" + lenToCollider);
                //    Debug.Log("LEN TO POINTER:" + lenToPointer);
                //    Debug.Log("BOUND X:" + hit.collider.GetComponent<SpriteRenderer>().bounds.size.x);

                //    if (lenToCollider > lenToPointer && lenToCollider < hit.collider.GetComponent<SpriteRenderer>().bounds.size.x)
                //    {
                //        continue;
                //    }

                //    Debug.Log("1337");

                //    var point = new Vector2(hit.point.x, hit.point.y);
                //    hooks[1].enabled = true;
                //    hooks[1].target = point;

                //    lineRenderer.positionCount += 2;

                //    break;
                //}
            } else
            {
                hooks[0].enabled = false;
                lineElectricity[0].enabled = false;
                lineRenderer.positionCount -= 2;
            }
        }

        //if (Input.GetMouseButtonDown(1))
        //{

        //    if (!hooks[1].enabled)
        //    {
        //        Vector3 pointerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //        pointerPosition.z = 0;

        //        var hits = Physics2D.RaycastAll(transform.position, pointerPosition - transform.position, 100f, hookableLayerMask);

        //        var lenToPointer = (pointerPosition - transform.position).magnitude;

        //        foreach (var hit in hits)
        //        {
        //            var lenToCollider = (transform.position - (Vector3)hit.point).magnitude;

        //            Debug.Log("LEN TO COLLIDER:" + lenToCollider);
        //            Debug.Log("LEN TO POINTER:" + lenToPointer);

        //            if (lenToCollider < lenToPointer && lenToCollider >= hit.collider.GetComponent<SpriteRenderer>().bounds.size.x)
        //            {
        //                continue;
        //            }

        //            var point = new Vector2(hit.point.x, hit.point.y);
        //            hooks[1].enabled = true;
        //            hooks[1].target = point;

        //            lineRenderer.positionCount += 2;

        //            break;
        //        }

        //        //RaycastHit2D hit = Physics2D.Raycast(transform.position, pointerPosition - transform.position, 100f, hookableLayerMask);

        //        //var lenToPointer = (pointerPosition - transform.position).magnitude;

        //        //if (hit.collider != null)
        //        //{
        //        //    var lenToCollider = (transform.position - (Vector3)hit.point).magnitude;

        //        //    if (lenToCollider >= lenToPointer)
        //        //    {
        //        //        hit.point = new Vector2(hit.point.x, hit.point.y);
        //        //        hooks[1].enabled = true;
        //        //        hooks[1].target = hit.point;

        //        //        lineRenderer.positionCount += 2;
        //        //    }
        //        //}
        //    }
        //    else
        //    {
        //        hooks[1].enabled = false;
        //        lineRenderer.positionCount -= 2;
        //    }

            //var currentHook = hooks[currentHookIndex];
            //if (currentHook.enabled)
            //{
            //    currentHook.enabled = false;
            //    lineRenderer.positionCount -= 2;
            //    currentHookIndex = currentHookIndex == 0 ? hooks.Length - 1 : currentHookIndex - 1;
            //}
        //}

        // -------------------------

        List<Vector3> points = new List<Vector3>();
        List<Vector3> firstHook = new List<Vector3>();

        if (hooks[0].enabled)
        {
            var firstPoint = new Vector3(hooks[0].target.x, hooks[0].target.y);
            var secondPoint = new Vector3(transform.position.x, transform.position.y);
            var pointsCoordinates = new List<Vector3> { firstPoint, secondPoint };
            points.AddRange(pointsCoordinates);

            firstHook.AddRange(pointsCoordinates);
        }

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPositions(points.ToArray());

        var lrFirst = lineElectricity[0];
        //var lrSecond = lineElectricity[1];

        if (firstHook.Count > 0)
        {
            lrFirst.receiverPosition = firstHook[0];
            lrFirst.emitterPosition = firstHook[1];
        }

        //if (secondHook.Count > 0)
        //{
        //    lrSecond.receiverPosition = secondHook[0];
        //    lrSecond.emitterPosition = secondHook[1];
        //}
    }
}
