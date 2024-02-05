using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {
    [SerializeField] private CatMovement _cat;


    private void OnTriggerEnter(Collider other) {
        Debug.Log("ground hit");
        _cat.IsGround = true;
    }
}
