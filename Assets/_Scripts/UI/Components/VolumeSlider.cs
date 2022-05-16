using UnityEngine;
using UnityEngine.UI;

namespace Hackathon
{
    public class VolumeSlider : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        private static readonly float DefaultVolume = 0.1f;

        // Start is called before the first frame update
        void Start()
        {
            AudioManager.Instance.ChangeMasterVolume(DefaultVolume);
            _slider.value = AudioListener.volume;
            _slider.onValueChanged.AddListener(val => AudioManager.Instance.ChangeMasterVolume(val));
        }
    }
}
