using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShaker : MonoBehaviour
{
    CinemachineVirtualCamera virtualCamera;

    [Header("Shake")]
    [SerializeField, Tooltip("intensifies all shake movements by this amount")]
    float shakeIntensityMultiplier = 1f;

    [SerializeField, Tooltip("maximum time spent shaking after AddShake() is called")]
    float maxShakeTime = 1f;

    [SerializeField, Tooltip("decides the shake intensity based on time left in shake")]
    AnimationCurve shakeIntensityCurve;

    [SerializeField, Tooltip("how fast the shake movements are, NOTE: this overrides 'Frequency Gain' in the CinemachineVirtualCamera component")]
    float shakeFrequency = 1f;

    float shakeTime = 0f;
    float longShakeTime = 0f;
    float longShakeIntensity = 0f;

    private void Start()
    {
        virtualCamera = GameObject.FindWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>();
        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = shakeFrequency;
    }

    private void Update()
    {

        // logs an error if the scene does not contain a cinemachine virtual camera tagged 'VirtualCamera'
        if (virtualCamera == null)
        {
            Debug.LogError(this.name + " requires a cinemachine virtual camera tagged 'VirtualCamera' in the scene.");
            return;
        }

        shakeTime = Mathf.Clamp(shakeTime, 0, maxShakeTime);

        // shake for time overrides all other shaking
        if (longShakeTime > 0)
        {
            longShakeTime -= Time.deltaTime;
            virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = longShakeIntensity * shakeIntensityMultiplier;
        }
        // 
        else if (shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;
            virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = shakeIntensityCurve.Evaluate(shakeTime) * shakeIntensityMultiplier;
        }
        else
        {
            virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        }
    }

    /// <summary>
    /// Increments the amount of shake time by the specified intensity, this is capped at the maxShakeTime and decreased over time
    /// </summary>
    public void AddShake(float intensity)
    {
        shakeTime += intensity;
    }

    /// <summary>
    /// Shakes the screen for a set amount of time with the specified intensity
    /// </summary>
    public void ShakeForTime(float time, float intensity)
    {
        longShakeIntensity = intensity;
        longShakeTime = time;
    }

    /// <summary>
    /// Cancels all shaking done by this script
    /// </summary>
    public void StopShake()
    {
        shakeTime = 0;
        longShakeTime = 0;
    }
}
