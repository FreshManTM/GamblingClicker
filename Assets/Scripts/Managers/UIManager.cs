using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _moneyText;
    [SerializeField] TextMeshProUGUI _perClickMoneyText;
    [SerializeField] TextMeshProUGUI _passiveMoneyText;
    [SerializeField] Image[] _screenButtons;

    MoneyManager _moneyManager;

    private void Start()
    {
        _moneyManager = MoneyManager.Instance;
    }
    private void Update()
    {
        _moneyText.text = _moneyManager.GetMoneyInfo(0).ToString() + " $";
        _perClickMoneyText.text = _moneyManager.GetMoneyInfo(1).ToString() + " $";
        _passiveMoneyText.text = _moneyManager.GetMoneyInfo(2).ToString() + " $";
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

        SoundManager.Instance.PlaySound(SoundManager.Sound.MenuButton);
    }

    public void ButtonClickAnim(GameObject button)
    {
        var newScale = new Vector2 (.8f, .8f);
        button.transform.DOScale(newScale, .1f).OnComplete(() => button.transform.DOScale(Vector2.one, .1f));
    }

    public void ComabinationButton(GameObject panel)
    {
        if ((Vector2)panel.transform.localScale == Vector2.one)
        {
            panel.transform.DOScale(Vector2.zero, .3f);
        }
        else
        {
            panel.transform.DOScale(Vector2.one, .3f);
        }

        SoundManager.Instance.PlaySound(SoundManager.Sound.MenuButton);
    }

    public void TestWIN()
    {
        EffectsManager.Instance.WinParticle();
        SoundManager.Instance.PlaySound(SoundManager.Sound.Win);

        MoneyManager.Instance.ChangeMoney(10000);
    }

}
