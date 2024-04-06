using UnityEngine;
using System.Collections.Generic;

namespace SuperVeigar
{
    public enum SoundReferenceType
    {
        BOUND,
        BRICK_BREAK,
        BRICK_BOUND,
        BALL_START,
        BGM_TITLE,
        BGM_GAME,
    }

    public class SoundService : SingletonBehaviour<SoundService>
    {
        public bool isMute;
        [SerializeField] private List<AudioSource> soundAudioSources;
        [SerializeField] private AudioSource bgmAudioSource;
        [SerializeField] private SoundReference soundReference;

        private void Start()
        {
            isMute = false;
            DontDestroyOnLoad(gameObject);
        }

        public void PlaySound(AudioClip clip, bool isLoop = false)
        {
            if (isMute == false)
            {
                AudioSource audioSource = GetAudioSource();
                if (audioSource != null)
                {
                    audioSource.clip = clip;
                    audioSource.loop = isLoop;
                    audioSource.Play();
                }
            }

        }

        public void PlaySound(SoundReferenceType type, bool isLoop = false)
        {
            if (isMute == false)
            {
                AudioSource audioSource = GetAudioSource();
                if (audioSource != null)
                {
                    audioSource.clip = soundReference.audioClips[(int)type];
                    audioSource.loop = isLoop;
                    audioSource.Play();
                }
            }
        }

        public void PlayBGM(SoundReferenceType type)
        {
            if (isMute == false)
            {
                bgmAudioSource.Stop();
                bgmAudioSource.clip = soundReference.audioClips[(int)type];
                bgmAudioSource.loop = true;
                bgmAudioSource.Play();
            }
        }

        private AudioSource GetAudioSource()
        {
            for (int i = 0; i < soundAudioSources.Count; i++)
            {
                if (soundAudioSources[i].isPlaying == false)
                {
                    return soundAudioSources[i];
                }
            }
            return null;
        }
    }
}

