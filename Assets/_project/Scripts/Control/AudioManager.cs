using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Nara.MFGJS2020.Control
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource backgroundAudioSource;
        [SerializeField] private AudioSource effectAudioSource;
        [SerializeField] private AudioSource messageAudioSource;

        [Range(0, 1)] [SerializeField] private float randomRange = .2f;

        [SerializeField] private AudioClip background;
        [Range(0, 1)] [SerializeField] private float winVolume = 1;
        [SerializeField] private AudioClip win;
        [Range(0, 1)] [SerializeField] private float loseVolume = 1;
        [SerializeField] private AudioClip lose;
        [Range(0, 1)] [SerializeField] private float moneyVolume = 1;
        [SerializeField] private AudioClip[] money;
        [Range(0, 1)] [SerializeField] private float notEnoughMoneyVolume = 1;

        [FormerlySerializedAs("notEnoughMoney")] [SerializeField]
        private AudioClip[] fail;

        [Range(0, 1)] [SerializeField] private float enemyAttackVolume = 1;
        [SerializeField] private AudioClip[] enemyAttack;
        [Range(0, 1)] [SerializeField] private float beginTurnVolume = 1;
        [SerializeField] private AudioClip[] beginTurn;

        public void StartBackground()
        {
            backgroundAudioSource.clip = background;
            backgroundAudioSource.loop = true;
            backgroundAudioSource.Play();
        }

        public void PlayMoneyChange()
        {
            PlayRandom(effectAudioSource, money, moneyVolume);
        }

        public void PlayFail()
        {
            PlayRandom(effectAudioSource, fail, notEnoughMoneyVolume);
        }

        public void PlayEnemyAttack()
        {
            PlayRandom(effectAudioSource, enemyAttack, enemyAttackVolume);
        }

        public void PlayBeginTurn()
        {
            PlayRandom(messageAudioSource, beginTurn, beginTurnVolume);
        }

        public IEnumerator PlayWin()
        {
            yield return PlayMessage(win, winVolume);
        }

        public IEnumerator PlayLose()
        {
            yield return PlayMessage(lose, loseVolume);
        }

        private IEnumerator PlayMessage(AudioClip clip, float volume)
        {
            backgroundAudioSource.Pause();
            messageAudioSource.PlayOneShot(clip, volume);
            yield return new WaitWhile(() => messageAudioSource.isPlaying);
            backgroundAudioSource.UnPause();
        }

        private void PlayRandom(AudioSource source, AudioClip[] clips, float volume)
        {
            var clip = clips[Random.Range(0, clips.Length)];
            source.Stop();
            source.pitch = Random.Range(1 - randomRange, 1 + randomRange);
            source.PlayOneShot(clip, volume);
        }
    }
}