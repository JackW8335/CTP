using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCreator : MonoBehaviour {

    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

	// Use this for initialization
	void Start () {
        mesh = GetComponent<MeshFilter>().mesh;

        MakeNewData();
        CreateMesh();
	}

    private void MakeNewData()
    {
        vertices = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 0, 1), new Vector3(1, 0, 0), new Vector3(1, 0, 1) };
        triangles = new int[] { 0, 1, 2, 2, 1, 3 };
    }

    private void CreateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
