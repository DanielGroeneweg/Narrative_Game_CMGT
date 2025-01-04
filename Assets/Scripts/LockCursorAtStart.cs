using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockCursorAtStart : MonoBehaviour
{
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
