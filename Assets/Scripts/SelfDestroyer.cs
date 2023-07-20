using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroyer : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("ActiveFalse", 2.2f);
    }

    private void ActiveFalse()
    {
        gameObject.SetActive(false);
    }
}
