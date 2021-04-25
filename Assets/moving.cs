using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving : MonoBehaviour
{
    public Vector3 Velovity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.position += Velovity * Time.fixedDeltaTime;
    }
}
