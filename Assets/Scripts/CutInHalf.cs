using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutInHalf : MonoBehaviour
{
    private Material capMaterial;
    public Transform[] blades;
    private List<GameObject> pieces = new List<GameObject>();
    RaycastHit[] hits;
    int MaxDistance = 500;

    void Start()
    {
        blades = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 1; i < blades.Length; i++)
            {
                //make an array of every hit registered by one blade
                //hits = Physics.SphereCastAll(new Ray(blades[i].position, blades[i].forward), 0.5f,MaxDistance);
                hits = Physics.BoxCastAll(blades[i].position,new Vector3(0.2f,0.2f,0.2f),blades[i].forward,blades[i].rotation,MaxDistance);
                Debug.Log(hits.Length);
                foreach (RaycastHit hit in hits)
                {
                    //For every hit check that it collided with a gameobject
                    if (hit.collider.gameObject && hit.collider.gameObject.tag == "Destructable")
                    {
                        //Assign the cap material to be the same as the objects material
                        //Then add the pieces to a list
                        capMaterial = hit.collider.gameObject.GetComponent<MeshRenderer>().material;
                        pieces.AddRange(MeshCut.Cut(hit.collider.gameObject, blades[i].position, blades[i].right, capMaterial));

                        for (int j = 1; j < 3; j++)
                        {
                            GameObject piece = pieces[pieces.Count - j];

                            if (!piece.GetComponent<MeshCollider>())
                            {
                                piece.AddComponent<MeshCollider>();
                            }

                            piece.GetComponent<MeshCollider>().convex = true;

                            piece.GetComponent<MeshCollider>().sharedMesh = null;
                            piece.GetComponent<MeshCollider>().sharedMesh = piece.GetComponent<MeshFilter>().mesh;

                            piece.tag = "Destructable";

                            if (!piece.GetComponent<Rigidbody>())
                            {
                                //apply drag to pieces to halt the explosion
                                piece.AddComponent<Rigidbody>();
                                //piece.GetComponent<Rigidbody>().velocity = Vector3.up * 25.0f;
                            }
                        }
                    }
                }
            }
        }
    }
}
