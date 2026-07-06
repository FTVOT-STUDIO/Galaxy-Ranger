using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    void LateUpdate()
    {
        if (transform.position.y > -18)
            return;
        
        transform.Translate(0, 28, 0, Space.Self);
    }
}
