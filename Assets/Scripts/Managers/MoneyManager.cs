using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;
    [Header("Debug only")]
    [SerializeField] int money;
    [SerializeField] int moneyPerClick = 1;
    [Space]
    [SerializeField] GameObject _particle;
    private void Awake()
    {
        Instance = this;
    }

    public void OnClick()
    {
        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        AddMoney();
        Instantiate(_particle, touchPos, _particle.transform.rotation);
    }

    private void AddMoney()
    {
        money += moneyPerClick;
    }

    public int GetMoney()
    {
        return money;
    }

    public void IncreaseMoneyPerClick(int increaseNumber)
    {
        moneyPerClick += increaseNumber;
    }
}
