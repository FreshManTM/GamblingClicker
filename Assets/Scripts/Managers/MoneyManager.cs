using System.Collections;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;
    [Header("Debug only")]
    [SerializeField] int _money;
    [SerializeField] int _moneyPerClick = 1;
    [SerializeField] int _passiveMoney;
    public int _progressMoney { get; private set; }

    EffectsManager _effectsManager;
    SoundManager _soundManager;

    private void Awake()
    {
        Instance = this;
        LoadPrefs();
    }
    private void Start()
    {
        _effectsManager = EffectsManager.Instance;
        _soundManager = SoundManager.Instance;
        StartCoroutine(PassiveEarning());
    }

    void LoadPrefs()
    {
        if (PlayerPrefs.HasKey("_money"))
        {
            _money = PlayerPrefs.GetInt("_money");
        }

        if (PlayerPrefs.HasKey("_moneyPerClick"))
        {
            _moneyPerClick = PlayerPrefs.GetInt("_moneyPerClick");
        }

        if (PlayerPrefs.HasKey("_passiveMoney"))
        {
            _passiveMoney = PlayerPrefs.GetInt("_passiveMoney");
        }

        if (PlayerPrefs.HasKey("_progressMoney"))
        {
            _progressMoney = PlayerPrefs.GetInt("_progressMoney");
        }
    }

    public void OnClick()
    {
        ChangeMoney(_moneyPerClick);
        _effectsManager.SpawnDollarParticle();
        _soundManager.PlaySound(SoundManager.Sound.Tap);
    }

    IEnumerator PassiveEarning()
    {
        if(_passiveMoney > 0)
        {
            ChangeMoney(_passiveMoney);
            _effectsManager.SpawnChipParticle();
        }

        yield return new WaitForSeconds(2f);
        StartCoroutine(PassiveEarning());
    }

    public int GetMoneyInfo(int n)
    {
        switch (n)
        {
            case 1:
                return (_moneyPerClick + 1) * 1000;
            case 2:
                return (_passiveMoney + 5) *1000;
            default: 
                return _money;
        }
    }

    public void ChangeMoney(int change)
    {
        _money += change;
        if(change > 0)
        {
            _progressMoney += change;
        }
    }

    public void IncreaseMoneyPerClick()
    {
        int price = (_moneyPerClick + 1) * 1000;
        if (_money - price >= 0)
        {
            _moneyPerClick += 1;
            ChangeMoney(-price);

            _soundManager.PlaySound(SoundManager.Sound.Upgrade);
        }
    }
    public void IncreasePassiveMoney()
    {
        int price = (_passiveMoney + 5) * 1000;
        if (_money - price >= 0)
        {
            _passiveMoney += 5;
            ChangeMoney(-price);

            _soundManager.PlaySound(SoundManager.Sound.Upgrade);
        }
    }

    void SaveMoneyPrefs()
    {
        PlayerPrefs.SetInt("_money", _money);
        PlayerPrefs.SetInt("_moneyPerClick", _moneyPerClick);
        PlayerPrefs.SetInt("_passiveMoney", _passiveMoney);
        //Debug only, remove before build
        //if(_progressMoney < _money)
        //    _progressMoney = _money;
        PlayerPrefs.SetInt("_progressMoney", _progressMoney);
    }

    #if UNITY_IOS && !UNITY_EDITOR
        private void OnApplicationPause(bool pause)
        {
            if(pause)
            {
                SaveMoneyPrefs();
            }
        }
    #else
        private void OnApplicationQuit()
        {
            SaveMoneyPrefs();
        }
    #endif
}
