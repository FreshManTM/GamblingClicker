using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] Transform[] _gameCanvases;
    [SerializeField] Transform _switchTarget;

    [SerializeField] TextMeshProUGUI _moneyText;
    [SerializeField] TextMeshProUGUI _perClickMoneyText;
    [SerializeField] TextMeshProUGUI _passiveMoneyText;
    [SerializeField] Image[] _screenButtons;

    MoneyManager _moneyManager;
    Vector3[] _startCanvasPositions = new Vector3[2];
    private void Start()
    {
        _moneyManager = MoneyManager.Instance;
        _startCanvasPositions[0] = _gameCanvases[0].position;
        _startCanvasPositions[1] = _gameCanvases[1].position;
    }
    private void Update()
    {
        _moneyText.text = _moneyManager.GetMoneyInfo(0).ToString();
        _perClickMoneyText.text = _moneyManager.GetMoneyInfo(1).ToString();
        _passiveMoneyText.text = _moneyManager.GetMoneyInfo(2).ToString();
    }

    public void ChangeCanvas(bool isSlotCanvas)
    {
        if(isSlotCanvas)
        {
            for (int i = 0; i < _gameCanvases.Length; i++)
            {
                _gameCanvases[i].DOMoveX(-_startCanvasPositions[i].x, 1f);
            }
        }
        else
        {
            for (int i = 0; i < _gameCanvases.Length; i++)
            {
                _gameCanvases[i].DOMoveX(_startCanvasPositions[i].x, 1f);
            }
        }
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

    public void OpenPanelButton(GameObject panel)
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
