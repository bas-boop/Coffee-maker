using System.Collections;
using UnityEngine;

namespace Baz_geluk9.CoffeeMaker
{
    public class MusicManager : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField, Range(0, 10)] private float waitForMusicTime;

        private void Start() => StartCoroutine(WaitingTime(waitForMusicTime));

        private IEnumerator WaitingTime(float waitTime)
        {
            waitForMusicTime = waitTime;
            yield return new WaitForSeconds(waitTime);
            audioSource.Play();
        }
    }
}
