using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutInHalf : MonoBehaviour
{
    public Material capMaterial;
    //void OnMouseDown()
    //{
        
    //    GameObject[] pieces = MeshCut.Cut(gameObject, transform.position, transform.right, GetComponent<Renderer>().material);

    //    for (int i = 0; i < pieces.Length; i++)
    //    {
    //        if (!pieces[i].GetComponent<BoxCollider>())
    //            pieces[i].AddComponent<BoxCollider>();

    //        if (!pieces[i].GetComponent<Rigidbody>())
    //            pieces[i].AddComponent<Rigidbody>();
    //    }
    //}

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if(Physics.Raycast(transform.position,transform.forward, out hit))
            {
                GameObject[] pieces = MeshCut.Cut(hit.collider.gameObject, transform.position, transform.right, capMaterial);

                for (int i = 0; i < pieces.Length; i++)
                {
                    if (!pieces[i].GetComponent<MeshCollider>())
                    {
                        pieces[i].AddComponent<MeshCollider>();
                        pieces[i].GetComponent<MeshCollider>().convex = true;    
                    }

                    if (!pieces[i].GetComponent<Rigidbody>())
                        pieces[i].AddComponent<Rigidbody>();
                }
            }
        }
    }



    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 5.0f);
        Gizmos.DrawLine(transform.position + transform.up * 0.5f, transform.position + transform.up * 0.5f + transform.forward * 5.0f);
        Gizmos.DrawLine(transform.position + -transform.up * 0.5f, transform.position + -transform.up*0.5f + transform.forward * 5.0f);

        Gizmos.DrawLine(transform.position, transform.position + transform.up * 0.5f);
        Gizmos.DrawLine(transform.position, transform.position + -transform.up * 0.5f);
    }
}
