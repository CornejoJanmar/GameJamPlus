using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName ="SO/Waves", order = 1)]
public class WaveSo : ScriptableObject
{
    public string waveName;
    [field: SerializeField] public GameObject[] EnemiesInWave {get; private set;}
    [field: SerializeField] public float TimeBeforeThisWave { get; private set; }
    [field: SerializeField] public float NumberToSpawn { get; private set; }
}
