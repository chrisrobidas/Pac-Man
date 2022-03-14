using UnityEngine;

namespace Managers
{
    public class MusicManager : MonoBehaviour
    {
        [SerializeField] private AudioClip introMusic;
        [SerializeField] private AudioClip deathSound;
    
        [SerializeField] private AudioSource wakaWakaAudioSource;
        [SerializeField] private AudioSource ghostSirenAudioSource;

        private float elapsedTimeWithoutEating = 1.0f;
    
        private void Update()
        {
            elapsedTimeWithoutEating += Time.deltaTime;
            if (elapsedTimeWithoutEating > 0.25f)
            {
                wakaWakaAudioSource.Pause();
            }
        }

        public void PlayIntroductionMusic()
        {
            ghostSirenAudioSource.PlayOneShot(introMusic);
        }

        public void PlayGhostSirenSound()
        {
            ghostSirenAudioSource.Play();
        }

        public void PlayWakaWakaSound()
        {
            if (!wakaWakaAudioSource.isPlaying)
                wakaWakaAudioSource.Play();
        
            elapsedTimeWithoutEating = 0;
            wakaWakaAudioSource.UnPause();
        }

        public void StopGhostSirenSound()
        {
            ghostSirenAudioSource.Stop();
        }
    
        public void StopWakaWakaSound()
        {
            wakaWakaAudioSource.Stop();
        }

        public void PlayDeathSound()
        {
            ghostSirenAudioSource.PlayOneShot(deathSound);
        }
    }
}
