using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public GameObject objectforFollow;

    private void FixedUpdate()
    {
        transform.position = new Vector3
            (objectforFollow.transform.position.x,
            objectforFollow.transform.position.y,
            transform.position.z);
    }
}
