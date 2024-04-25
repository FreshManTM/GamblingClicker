using UnityEngine;

public class SlotMachine : MonoBehaviour
{
    [SerializeField] Slot[] _slots;
    [SerializeField] Combinations[] _combinations;

    public float _timeInterval = 0.025f;

    [SerializeField] int _price;
    [SerializeField] bool _isAuto;

    int _stoppedSlots = 3;
    bool _isSpin = false;

    MoneyManager _moneyManager;
    Animator _animator;
    private void Start()
    {
        _moneyManager = MoneyManager.Instance;
        _animator = GetComponent<Animator>();
    }
    public void Spin()
    {
        if (!_isSpin)
        {
            if (_moneyManager.GetMoneyInfo(0) - _price >= 0)
            {
                _animator.SetTrigger("Spin");
                _moneyManager.ChangeMoney(-_price);
                _isSpin = true;
                foreach (Slot i in _slots)
                {
                    i.StartCoroutine("Spin");
                }

                SoundManager.Instance.PlaySound(SoundManager.Sound.SlotMachineStart);
            }
        }
    }

    public void WaitResults()
    {
        _stoppedSlots -= 1;
        if(_stoppedSlots <= 0)
        {
            _stoppedSlots = 3;
            CheckResults();
        }
    }

    public void CheckResults()
    {
        Debug.Log(_slots[0].stoppedSlot.ToString());
        Debug.Log(_slots[1].stoppedSlot.ToString());
        Debug.Log(_slots[2].stoppedSlot.ToString());

        _isSpin = false;

        foreach (Combinations combination in _combinations)
        {
            if (_slots[0].stoppedSlot.ToString() == combination.FirstValue.ToString()
                && _slots[1].stoppedSlot.ToString() == combination.SecondValue.ToString()
                && _slots[2].stoppedSlot.ToString() == combination.ThirdValue.ToString())
            {
                _moneyManager.ChangeMoney(combination.prize);
                EffectsManager.Instance.WinParticle();
                SoundManager.Instance.PlaySound(SoundManager.Sound.Win);
                break;
            }
        }
        if (_isAuto)
        {
            Invoke("Spin", 0.4f);
        }
    }

    public void SetAuto()
    {
        if (!_isAuto)
        {
            _timeInterval /= 10;
            _isAuto = true;
            Spin();
        }
        else
        {
            _timeInterval *= 10;
            _isAuto = false;
        }
    }
}

[System.Serializable]
public class Combinations
{
    public enum SlotValue
    {
        Banana,
        Cherry,
        Clover,
        Strawbery,
        Grape
    }

    public SlotValue FirstValue;
    public SlotValue SecondValue;
    public SlotValue ThirdValue;
    public int prize;
}
