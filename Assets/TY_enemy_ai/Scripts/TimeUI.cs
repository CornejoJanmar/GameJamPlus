using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private WaveSpawner waveSpawner;
    [SerializeField] private Image timeImg;

    private int divideFillAmount = 10;

    private void Update()
    {
        timeImg.fillAmount = waveSpawner.cooldownTimer / waveSpawner.currentWave.TimeBeforeThisWave; ;
    }
}
