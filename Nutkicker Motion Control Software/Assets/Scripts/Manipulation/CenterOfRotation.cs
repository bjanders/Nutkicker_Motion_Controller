using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script makes sure that the center of rotation is offset from the motion platform by exactly the offset used to create that plaform
//in the very first place.

    [ExecuteInEditMode]
public class CenterOfRotation : MonoBehaviour
{
    Transformer ParentTransformer;
    void Update()
    {
        ParentTransformer = transform.parent.GetComponent<Transformer>();

        Vector3 ParentOffset = new Vector3( ParentTransformer.Offset_Sway,
                                            ParentTransformer.Offset_Heave,
                                            ParentTransformer.Offset_Surge);

        transform.localPosition = -ParentOffset;
    }
}
