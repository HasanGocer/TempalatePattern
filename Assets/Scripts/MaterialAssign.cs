using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialAssign : MonoBehaviour
{
    [SerializeField] private Material objectMaterial;
    Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = objectMaterial;
    }

}
