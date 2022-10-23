using System;
using UnityEngine;
using Utility;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private SerializedDictionary<SfxClips,AudioClip> sfxClipDict;
        

        private void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        public AudioClip GetClip(SfxClips targetClip)
        {
            if (sfxClipDict.ContainsKey(targetClip))
            {
                return sfxClipDict[targetClip];
            }

            return null;
        }


        public void PlaySfx(AudioClip clip)
        {
            if (!clip) return;
            sfxSource.PlayOneShot(clip);
        }
        
        public void PlayMusic(AudioClip clip)
        {
            if (!clip) return;
            musicSource.clip = clip;
            musicSource.Play();
        }
        
    }
}
