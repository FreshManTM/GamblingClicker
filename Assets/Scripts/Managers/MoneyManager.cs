using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;
    [Header("Debug only")]
    [SerializeField] int _money;
    [SerializeField] int _moneyPerClick = 1;
    [Space]
    [SerializeField] GameObject _particle;

    private void Awake()
    {
        Instance = this;
        LoadPrefs();
    }

    void LoadPrefs()
    {
        if (PlayerPrefs.HasKey("_moneyPerClick"))
        {
            _moneyPerClick = PlayerPrefs.GetInt("_moneyPerClick");
        }
        if (PlayerPrefs.HasKey("_money"))
        {
            _money = PlayerPrefs.GetInt("_money");
        }
    }

    void AddMoney()
    {
        _money += _moneyPerClick;
    }

    public void OnClick()
    {
        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

        AddMoney();
        Instantiate(_particle, touchPos, _particle.transform.rotation);
    }

    public int GetMoney()
    {
        return _money;
    }

    public void ChangeMoney(int change)
    {
        _money += change;
    }

    public void IncreaseMoneyPerClick(int increaseNumber)
    {
        _moneyPerClick += increaseNumber;
    }

#if UNITY_IOS && !UNITY_EDITOR
    private void OnApplicationPause(bool pause)
    {
        if(pause)
        {
            SaveMoneyPrefs()
        }
    }
#else
    private void OnApplicationQuit()
    {
        SaveMoneyPrefs();
    }
#endif

    void SaveMoneyPrefs()
    {
        PlayerPrefs.SetInt("_money", _money);
        PlayerPrefs.SetInt("_moneyPerClick", _moneyPerClick);
    }
}
