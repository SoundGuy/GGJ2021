using System;
using UnityEngine;
using Mechanics;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TimeVisualizer : MonoBehaviour
{
    [SerializeField]
    private string m_startDate;

    [SerializeField]
    private string m_dateFormat = "hh/mm/ss";

    private TextMeshProUGUI m_timeText;

    private TimeManager m_timeManager;

    private DateTime m_initialDate;

    private void Awake()
    {
        m_initialDate = DateTime.Parse(m_startDate);

        m_timeText = GetComponent<TextMeshProUGUI>();

        m_timeManager = FindObjectOfType<TimeManager>();

        if (m_timeManager == null)
        {
            throw new Exception("Missing TimeManager in scene");
        }

        m_timeText.text = m_initialDate.Date.ToString();
    }

    private void Update()
    {
        DateTime currentDate = m_initialDate + TimeSpan.FromSeconds(m_timeManager.ElapsedTime);

        m_timeText.text = currentDate.ToString(m_dateFormat);   
    }
}
