using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    //[SerializeField]
    //private List<Object> objects;
    [SerializeField]
    private AnnoyanceMeter annoyanceMeter;

    private static GameManager _instance;

    public static GameManager Instance {
        get {
            return _instance;
        }

    }

    private void Awake() {
        _instance = this;
    }

    private void Start() {

        //foreach (Object obj in objects) {

        //    obj.action += IncreaseAnnoyance;
        //}
    }

    public void IncreaseAnnoyance(float value = 1) {
        annoyanceMeter.annoyanceLevel = annoyanceMeter.annoyanceLevel + value;
    }
}
