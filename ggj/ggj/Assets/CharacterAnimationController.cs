using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    public Transform rootHead;
    public Transform rootBody;
    public Transform rootHandL;
    public Transform rootHandR;

    public Transform transformHead;
    public Transform transformBody;
    public Transform transformHandL;
    public Transform transformHandR;

    void Start ()
    {
        SetTransformToRoot(transformHead, rootHead);
        SetTransformToRoot(transformBody, rootBody);
    }


    void SetTransformToRoot(Transform t, Transform root)
    {
        t.SetParent(root, false);
        t.localPosition = Vector3.zero;
        t.localRotation = Quaternion.identity;
        t.localScale = Vector3.one;
    }
}
