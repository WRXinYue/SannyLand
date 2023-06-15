using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform palyer;

    void Update()
    {
        transform.position = new Vector3(palyer.position.x, 0,-10f);
    }
}
