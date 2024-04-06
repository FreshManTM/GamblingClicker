using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _moneyText;
    [SerializeField] Image[] _screenButtons;
    [SerializeField] GameObject _upgradeButton;
    private void Update()
    {
        _moneyText.text = MoneyManager.Instance.GetMoney().ToString();
    }

    public void SetButtonColor(bool isGambling)
    {
        Color tempColor;
        if (isGambling)
        {
            tempColor = _screenButtons[1].color;
            tempColor.a = 1f;
            _screenButtons[1].color = tempColor;

            tempColor.a = .5f;
            _screenButtons[0].color = tempColor;
        }
        else
        {
            tempColor = _screenButtons[0].color;
            tempColor.a = 1f;
            _screenButtons[0].color = tempColor;

            tempColor.a = .5f;
            _screenButtons[1].color = tempColor;
        }
    }

    public void UpgradeButtonAnim()
    {
        var newScale = new Vector2 (.8f, .8f);
        _upgradeButton.transform.DOScale(newScale, .1f).OnComplete(() => _upgradeButton.transform.DOScale(Vector2.one, .1f));
    }
}
