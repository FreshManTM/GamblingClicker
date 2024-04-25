using DG.Tweening;
using TMPro;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    public static EffectsManager Instance;

    [SerializeField] GameObject _dolallarParticle;
    [SerializeField] GameObject _chipParticle;
    [SerializeField] Transform[] _chipSpawnPoints;
    [SerializeField] ParticleSystem _winParticle;
    [SerializeField] GameObject _winText;
    [SerializeField] TextMeshProUGUI _moneyText;


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
        _winText.SetActive(true);
        _winText.transform.DOShakeScale(3f, .4f, 5).OnComplete(() => _winText.SetActive(false));
    }

    public void NotEnoutghMoneyAnim()
    {
        _moneyText.DOColor(Color.red, .2f).OnComplete(() => _moneyText.DOColor(Color.white, .2f));
        Vector3 punch = new Vector3(4, 0, 0);
        print(_moneyText.transform.localPosition);
        _moneyText.transform.DOPunchPosition(punch, .3f);
    }
}
