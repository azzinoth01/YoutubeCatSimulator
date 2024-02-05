using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnnoyanceMeter : MonoBehaviour
{
    [SerializeField]
    private Image metterImage;
    public float annoyanceLevel;
    void Update()
    {
        metterImage.fillAmount = annoyanceLevel /100;   
    }
}
