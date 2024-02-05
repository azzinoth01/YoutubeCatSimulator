using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour {
    [HideInInspector]
    public bool isOnFloor = false;

    public float despawnTimer;
    public float respawnTimer;

    public Action<float> action;
    public Vector3 startPosition;

    [SerializeField] private AudioClip _audio;
    [SerializeField] private float _rageValue;
    [SerializeField] private Quaternion _baseRotation;
    private void Start() {
        startPosition = gameObject.transform.position;
        _baseRotation = transform.rotation;
    }
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.layer == 6) //layer Floor
        {
            Debug.Log("colliding");
            isOnFloor = true;
            AudioSource.PlayClipAtPoint(_audio, transform.position);

            GameManager.Instance.IncreaseAnnoyance(_rageValue);

            //  action?.Invoke(1);
            //   Invoke("DespawnObject", despawnTimer);
            Invoke("RespawnObject", respawnTimer);
        }
    }

    //public void DespawnObject() {
    //    gameObject.SetActive(false);
    //}

    public void RespawnObject() {
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.transform.rotation = _baseRotation;
        // gameObject.SetActive(true);
        gameObject.transform.position = startPosition;


    }
}
