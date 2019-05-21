using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


public class DestructTool : EditorWindow
{

    public enum DestructionStates { SHATTER, SPLINTER, CRUMBLE };
    public DestructionStates d_state;

    private Material capMaterial;
    public Transform[] blades;
    private List<GameObject> pieces = new List<GameObject>();
    private string objectName = "";
    RaycastHit[] hits;
    int MaxDistance = 500;

    int selected = 0;

    [MenuItem("Window/DestructTool")]
    public static void ShowWindow()
    {
        GetWindow<DestructTool>("Destruct");
    }

    void OnGUI()
    {
        d_state = (DestructionStates)EditorGUILayout.EnumPopup("Destruction Type:", d_state);

        if (GUILayout.Button("Destroy"))
        {
            pieces.Clear();
            blades = GameObject.Find("Blades").GetComponentsInChildren<Transform>();

            for (int i = 1; i < blades.Length; i++)
            {
                //make an array of every hit registered by one blade
                hits = Physics.BoxCastAll(blades[i].position, new Vector3(0.2f, 0.2f, 0.2f), blades[i].forward, blades[i].rotation, MaxDistance);
                foreach (RaycastHit hit in hits)
                {
                    //For every hit check that it collided with a gameobject
                    if (hit.collider.gameObject && hit.collider.gameObject.tag == "Destructable")
                    {
                        if(objectName == "")
                        {
                            objectName = hit.collider.gameObject.name;
                        }                       
                        //Assign the cap material to be the same as the objects material
                        //Then add the pieces to a list
                        capMaterial = hit.collider.gameObject.GetComponent<MeshRenderer>().sharedMaterial;
                        pieces.AddRange(MeshCut.Cut(hit.collider.gameObject, blades[i].position, blades[i].right, capMaterial));


                        for (int j = 1; j < 3; j++)
                        {
                            GameObject piece = pieces[pieces.Count - j];

                            if (!piece.GetComponent<MeshCollider>())
                            {
                                if (piece.GetComponent<Collider>())
                                {
                                    DestroyImmediate(piece.GetComponent<Collider>());
                                }
                                piece.AddComponent<MeshCollider>();     
                            }

                            piece.GetComponent<MeshCollider>().convex = true;

                            piece.GetComponent<MeshCollider>().sharedMesh = null;
                            piece.GetComponent<MeshCollider>().sharedMesh = piece.GetComponent<MeshFilter>().sharedMesh;

                            AssetDatabase.CreateAsset(piece.GetComponent<MeshFilter>().sharedMesh, "Assets/Meshes/" + objectName + (pieces.Count - j) + ".asset");
                            AssetDatabase.SaveAssets();

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
            objectName = "";

        }
        if (GUILayout.Button("Save Prefab"))
        {
            GameObject[] objects = Selection.gameObjects;
            foreach (GameObject obj in objects)
            {
                 PrefabUtility.CreatePrefab("Assets/Prefabs/" + obj.name + ".prefab", obj);
            }
        }
    }
}

