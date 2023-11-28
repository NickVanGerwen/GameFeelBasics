using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPitcher : MonoBehaviour
{
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] float pitchIncrementAmount = 0.1f;
    [SerializeField] float startPitch = 1f;
    [SerializeField] float maxPitch = 1.5f;
    [SerializeField] float maxIntervalInSeconds = 0.5f;

    float timer = 0;

    void Update()
    {
        if (m_AudioSource.pitch > startPitch)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                m_AudioSource.pitch -= startPitch;
            }
        }
    }

    public void PlayPitchedSound()
    {
        m_AudioSource.Play();
        m_AudioSource.pitch += pitchIncrementAmount;
        m_AudioSource.pitch = Mathf.Clamp(m_AudioSource.pitch, startPitch, maxPitch);
        timer = maxIntervalInSeconds;
    }
}
