using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshAnimator : MonoBehaviour
{
    [SerializeField] private Mesh _mesh;

    [SerializeField] private MeshFilter _meshFilter;
    

    // Update is called once per frame
    void Update()
    {
        _meshFilter.mesh = _mesh;
    }
}