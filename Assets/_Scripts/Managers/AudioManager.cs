using UnityEngine;

namespace Hackathon
{
    public class AudioManager : Singleton<AudioManager>
    {
        [SerializeField]
        private AudioSource m_musicSource, _effectsSource;

        public void PlaySound(AudioClip clip)
        {
            _effectsSource.PlayOneShot(clip);
        }

        public void ChangeMasterVolume(float value)
        {
            AudioListener.volume = value;
        }

        public void ToggleEffects()
        {
            _effectsSource.mute = !_effectsSource.mute;
        }

        public void ToggleMusic()
        {
            m_musicSource.mute = !m_musicSource.mute;
        }
    }
}
