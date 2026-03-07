using UnityEngine;
using System.Collections;

public class StepSound : MonoBehaviour
{
    private float timerSpeed = 2f;

    private void Awake()
    {
        StartCoroutine(stepSound());
    }

    private IEnumerator stepSound()
    {
        while (true)
        {
            yield return new WaitForSeconds(timerSpeed);
            Debug.Log(" bruit 2 pas");
            AudioManager.Instance.PlaySound(AudioType.step, AudioSourceType.stepSource);
        } 
    }
}