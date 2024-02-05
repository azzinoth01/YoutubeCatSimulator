using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardHit : MonoBehaviour {

    [SerializeField] private float _baseDelay;
    [SerializeField] private float _rageValue;
    private float _delay;
    private bool _canRage;
    [SerializeField] private AudioClip _audio;

    private void OnTriggerEnter(Collider other) {

        if (_canRage) {
            _canRage = false;
            GameManager.Instance.IncreaseAnnoyance(_rageValue);
            _delay = _baseDelay;
        }

        AudioSource.PlayClipAtPoint(_audio, transform.position);

    }

    // Start is called before the first frame update
    void Start() {
        _canRage = true;
    }

    // Update is called once per frame
    void Update() {

        _delay = _delay - Time.deltaTime;

        if (_canRage == false && _delay <= 0) {
            _canRage = true;
        }


    }
}
