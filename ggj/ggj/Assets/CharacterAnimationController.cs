using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    public Transform rootHead;
    public Transform rootBody;
    public Transform rootHand;

    public Transform transformHead;
    public Transform transformBody;
    public Transform transformHand;

    public Transform[] hatOptions;

    public void Setup(int hatIndex)
    {
        Transform t = hatOptions[hatIndex];
        foreach (Transform tt in hatOptions)
        {
            tt.gameObject.SetActive(false);
        }
        t.gameObject.SetActive(true);

        SetTransformToRoot(t, rootHead);
        SetTransformToRoot(transformBody, rootBody);
        SetTransformToRoot(transformHand, rootHand);
    }


    void SetTransformToRoot(Transform t, Transform root)
    {
        t.SetParent(root, false);
        t.localPosition = Vector3.zero;
        t.localRotation = Quaternion.identity;
        t.localScale = Vector3.one;
    }


    public void ShowHand(bool isShown)
    {
        transformHand.gameObject.SetActive(isShown);
    }
}
