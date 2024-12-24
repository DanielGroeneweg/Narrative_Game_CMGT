using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlatePuzzle : MonoBehaviour
{
    public bool first;
    public bool second;
    public bool third;
    public bool fourth;

    public UnityEvent correct;
    public void First(bool value) { first = value; }
    public void Second(bool value) { second = value; }
    public void Third(bool value) { third = value; }
    public void Fourth(bool value) { fourth = value; }

    public void CheckPuzzle()
    {
        if (first && second && third && fourth)
        {
            correct?.Invoke();
        }
    }
}