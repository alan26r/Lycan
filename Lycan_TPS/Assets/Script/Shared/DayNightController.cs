using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightController : MonoBehaviour
{
    public Light m_sun;
    public float m_secondsInFullDay = 120f; // 120 secondes pour tester mais en version finale c'est 10 800
    [Range(0, 1)]
    public float m_currentTimeOfDelay = 0;
    [HideInInspector]
    public float m_timeMultiplier = 1f;
    private float m_sunInitialIntensity;

    private void Start()
    {
        m_sunInitialIntensity = m_sun.intensity;
    }

    private void Update()
    {
        UpdateSun();
        m_currentTimeOfDelay += (Time.deltaTime / m_secondsInFullDay) * m_timeMultiplier;

        if(m_currentTimeOfDelay >= 1)
        {
            m_currentTimeOfDelay = 0;
        }
    }

    private void UpdateSun()
    {
        m_sun.transform.localRotation = Quaternion.Euler((m_currentTimeOfDelay * 360f) - 90, 170, 0);
        float intensityMultiplier = 1;

        if(m_currentTimeOfDelay <= 0.23f || m_currentTimeOfDelay >= 0.75f)
        {
            intensityMultiplier = 0;
        }
        else if(m_currentTimeOfDelay <= 0.25f)
        {
            intensityMultiplier = Mathf.Clamp01((m_currentTimeOfDelay - 0.23f) * (1 / 0.02f));
        }
        else if(m_currentTimeOfDelay >= 0.73f)
        {
            intensityMultiplier = Mathf.Clamp01(1 - ((m_currentTimeOfDelay - 0.73f) * (1 / 0.02f)));
        }

        m_sun.intensity = m_sunInitialIntensity * intensityMultiplier;
    }
}
