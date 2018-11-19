using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutInHalf : MonoBehaviour
{
    void OnMouseDown()
    {
        
        GameObject[] pieces = MeshCut.Cut(gameObject, transform.position, transform.right, GetComponent<Renderer>().material);

        for (int i = 0; i < pieces.Length; i++)
        {
            if (!pieces[i].GetComponent<BoxCollider>())
                pieces[i].AddComponent<BoxCollider>();

            if (!pieces[i].GetComponent<Rigidbody>())
                pieces[i].AddComponent<Rigidbody>();
        }
    }
}
