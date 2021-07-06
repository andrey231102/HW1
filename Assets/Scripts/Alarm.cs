using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private float _duration;

    private AudioSource _alarm;
    private Coroutine _alarmSwitch;

    private void Start()
    {
        _alarm = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _alarm.Play();
            _alarmSwitch = StartCoroutine(ChangeVolume(_alarm, _duration));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _alarm.Stop();
            StopCoroutine(_alarmSwitch);
        }
    }

    private IEnumerator ChangeVolume(AudioSource alarm, float duration)
    {
        float time = 0;
        while (true)
        {
            alarm.volume = Mathf.PingPong(time / duration,1f);
            time += Time.deltaTime;
            yield return null;
        }
    }
}
