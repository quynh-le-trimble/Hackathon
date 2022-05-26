
using System;
using TMPro;
using UnityEngine;

namespace Hackathon
{
    public class TimerSystem : MonoBehaviour
    {        
        public const float DefaultTimeRemaining = 10;
        public float m_TimeRemaining = 0;
        public Action OnTimerEnd;

        [SerializeField] private TextMeshProUGUI m_DisplayText;
        public bool timerIsRunning = false;

        private void Update()
        {
            if (!timerIsRunning) return;

            if (m_TimeRemaining > 0)
            {
                m_TimeRemaining -= Time.deltaTime;
            }
            else
            {
                OnTimerEnd?.Invoke();
                timerIsRunning = false;
            }

            DisplayTime(m_TimeRemaining);
        }

        public void StartTimer()
        {
            SetTime();
            timerIsRunning = true;
        }

        public void SetTime()
        {
            if (m_TimeRemaining <= 0)
            {
                m_TimeRemaining = DefaultTimeRemaining;
            }

            DisplayTime(m_TimeRemaining);
        }

        private void DisplayTime(float timeToDisplay)
        {
            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);
            float milliSeconds = (timeToDisplay % 1) * 1000;
            m_DisplayText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}