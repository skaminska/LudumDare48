using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerPosition;
    Vector3 offset;

    void Start()
    {
        offset = new Vector3(0, 0, -15);
    }

    void LateUpdate()
    {
        transform.position = playerPosition.position + offset;
    }
}
