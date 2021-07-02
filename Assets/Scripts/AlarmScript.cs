using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmScript : MonoBehaviour
{
    [SerializeField] private float _duration;

    private AudioSource _alarm;

    private void Start()
    {
        _alarm = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _alarm.Play();
            StartCoroutine(IncreaseVolume(_alarm, _duration));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _alarm.Stop();
        }
    }

    IEnumerator IncreaseVolume(AudioSource alarm, float duration)
    {
        float time = 0;
        while (alarm.volume<1)
        {
            alarm.volume = Mathf.Lerp(0f, 1f, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(DecreaseVolume(_alarm, duration));
    }

    IEnumerator DecreaseVolume(AudioSource alarm, float duration)
    {
        float time = 0;
        while (alarm.volume > 0)
        {
            time += Time.deltaTime;
            alarm.volume = Mathf.Lerp(1f, 0f, time / duration);
            yield return null;
        }
        StartCoroutine(IncreaseVolume(_alarm, duration));
    }
}
