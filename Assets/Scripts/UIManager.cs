using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _moneyText;

    private void Update()
    {
        _moneyText.text = MoneyManager.Instance.GetMoney().ToString();
    }
}
