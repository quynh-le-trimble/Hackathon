using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hackathon
{
    public class AudioManager : PersistentSingleton<AudioManager>
    {
        [SerializeField] private AudioSource _musicSource, _effectsSource;

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
            _musicSource.mute = !_musicSource.mute;
        }
    }
}
