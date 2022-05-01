using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hackathon
{
    public class PlaySoundOnStart : MonoBehaviour
    {
        [SerializeField] private AudioClip _clip;
        
        void Start()
        {
            AudioManager.Instance.PlaySound(_clip);
        }
    }
}
