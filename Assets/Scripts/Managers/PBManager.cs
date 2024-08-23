using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PBManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _progressText;
    [SerializeField] TextMeshProUGUI _levelText;
    [SerializeField] Slider _progressSlider;
    [SerializeField] Button[] _buttons;
    
    MoneyManager _moneyManager;
    
    string[] _levels = new string[10]{
    "Novice",
    "Apprentice",
    "Beginner",
    "Intermediate",
    "Skilled",
    "Advanced",
    "Expert",
    "Master",
    "Veteran",
    "Elite"}; 
    public int _currentLevel = 1;
    int _nextlevelValue;
    int progressMoney;

    void Start()
    {
        _moneyManager = MoneyManager.Instance;
        SetLevel(_moneyManager._progressMoney);    
    }

    void Update()
    {
        progressMoney = _moneyManager._progressMoney;
        _nextlevelValue = _currentLevel * 10000;
        if (progressMoney < _nextlevelValue)
        {
            _progressText.text = $"{progressMoney}/{_nextlevelValue}";
            _progressSlider.value = (progressMoney - (_currentLevel - 1) * 10000);
        }
        else
        {
            SetLevel(progressMoney);
        }
    }

    void SetLevel(int money)
    {
        for (int i = 1; i <= _levels.Length; i++)
        {
            if(money >= i * 10000)
            {
                _currentLevel = i + 1;
            }
        }

        _levelText.text = $"Level {_currentLevel}: {_levels[_currentLevel - 1]}";

        CheckUnblockButton();
    }

    void CheckUnblockButton()
    {

        if (_currentLevel >= 4 && !_buttons[0].interactable)
        {
            _buttons[0].interactable = true;
            _buttons[0].GetComponentInChildren<TextMeshProUGUI>().gameObject.SetActive(false);
        }

        if (_currentLevel >= 8 && !_buttons[0].interactable)
        {
            _buttons[1].interactable = true;
            _buttons[1].GetComponentInChildren<TextMeshProUGUI>().gameObject.SetActive(false);
        }
    }
}
