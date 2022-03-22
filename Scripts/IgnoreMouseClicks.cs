using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreMouseClicks : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
