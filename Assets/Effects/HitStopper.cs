using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStopper : MonoBehaviour
{
    [SerializeField] float hitStopTime = 0.1f;

    public void HitStop()
    {
        Time.timeScale = 0f;
        Time.fixedDeltaTime = Time.timeScale * .02f;
        StartCoroutine(EndHitStop());
    }
    private IEnumerator EndHitStop()
    {
        yield return new WaitForSecondsRealtime(hitStopTime);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }
}
