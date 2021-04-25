using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyColor : MonoBehaviour
{
    public Color C = Color.white;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().material.color = C;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
