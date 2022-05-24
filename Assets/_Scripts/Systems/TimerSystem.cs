
using System;
using TMPro;
using UnityEngine;

namespace Hackathon
{
    public class TimerSystem : MonoBehaviour
    {
        public float m_TimeRemaining = 10;
        public Action OnTimerEnd;

        [SerializeField] private TextMeshProUGUI m_DisplayText;
        bool timerIsRunning = false;

        public void SetTime(float time)
        {
            m_TimeRemaining = time;
            DisplayTime(m_TimeRemaining);
        }

        public void StartTimer()
        {
            timerIsRunning = true;
        }

        public void StartTimer(float time)
        {
            m_TimeRemaining = time;
            timerIsRunning = true;
        }

        private void Update()
        {
            if (!timerIsRunning) return;

            if (m_TimeRemaining > 0)
            {
                m_TimeRemaining -= Time.deltaTime;
            }
            else
            {
                m_TimeRemaining = 0;
                OnTimerEnd?.Invoke();
                timerIsRunning = false;
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