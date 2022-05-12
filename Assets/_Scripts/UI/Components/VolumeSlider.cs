using UnityEngine;
using UnityEngine.UI;


namespace Hackathon
{
    public class VolumeSlider : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        // Start is called before the first frame update
        void Start()
        {
            _slider.value = AudioListener.volume;
            _slider.onValueChanged.AddListener(val => AudioManager.Instance.ChangeMasterVolume(val));    
        } 
    }
}
