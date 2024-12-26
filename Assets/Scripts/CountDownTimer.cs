using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CountDownTimer : MonoBehaviour
{
    public UnityEvent TimerRanOut;
    public TMP_Text text;

    public float minutesLeft = 5;
    public float secondsLeft = 0;
    private bool timeLeft = true;

    private void Start()
    {
        if (secondsLeft >= 10) text.text = minutesLeft + ":" + secondsLeft;
        else text.text = minutesLeft + ":0" + secondsLeft;
        InvokeRepeating(nameof(DoCountDown), 1, 1);
    }
    private void DoCountDown()
    {
        if (timeLeft)
        {
            if (minutesLeft <= 0 && secondsLeft <= 0)
            {
                timeLeft = false;
            }
            
            else if (secondsLeft <= 0)
            {
                minutesLeft -= 1;
                secondsLeft += 59;
            }

            else secondsLeft -= 1;

            if (secondsLeft >= 10) text.text = minutesLeft + ":" + secondsLeft;
            else text.text = minutesLeft + ":0" + secondsLeft;

        }

        else TimerRanOut?.Invoke();
    }
}
