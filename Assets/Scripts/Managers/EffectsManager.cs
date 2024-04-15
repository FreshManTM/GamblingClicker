using System.Collections;
using System.Collections.Generic;
using UnityEditor.VisionOS;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    public static EffectsManager Instance;

    [SerializeField] GameObject _dolallarParticle;
    [SerializeField] GameObject _chipParticle;
    [SerializeField] Transform[] _chipSpawnPoints;
    [SerializeField] ParticleSystem _winParticle;


    private void Awake()
    {
        Instance = this;
    }

    public void SpawnChipParticle()
    {
        Vector2 spawnPos = _chipSpawnPoints[Random.Range(0, _chipSpawnPoints.Length)].position;
        Instantiate(_chipParticle, spawnPos, _chipParticle.transform.rotation);
    }

    public void SpawnDollarParticle()
    {
        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        Instantiate(_dolallarParticle, touchPos, _dolallarParticle.transform.rotation);
    }

    public void WinParticle()
    {
        _winParticle.Play();
    }
}
