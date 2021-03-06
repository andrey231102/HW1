using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] float _time;
    [SerializeField] float _angleOfRotation;

    private bool _isClosed = true;

    private void OnMouseDown()
    {
        if (_isClosed)
        {
            StartCoroutine(RotateDoor(Quaternion.AngleAxis(_angleOfRotation, Vector3.up), _time));
            _isClosed = false;
        }
        else
        {
            StartCoroutine(RotateDoor(Quaternion.AngleAxis(2 * _angleOfRotation, Vector3.up), _time));
            _isClosed = true;
        }
    }

    private IEnumerator RotateDoor(Quaternion endValue,float duration)
    {
        float time = 0;
        Quaternion startValue = transform.rotation;

        while (time < duration)
        {
            transform.rotation = Quaternion.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
    }
}
