using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOfStartInfo : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.SetActive(false);
        }
    }
}
