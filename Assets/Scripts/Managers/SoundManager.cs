using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] AudioSource _winSound;
    [SerializeField] AudioSource _menuButtonSound;
    [SerializeField] AudioSource _tapSound;
    [SerializeField] AudioSource _upgradeSound;
    [SerializeField] AudioSource _slotMachineStartSound;

    private void Awake()
    {
        Instance = this;
    }

    public void PlaySound(Sound sound)
    {
        AudioSource audio;
        switch (sound)
        {
            case Sound.Win:
                audio = _winSound;
                break;
            case Sound.MenuButton:
                audio = _menuButtonSound;
                break;
            case Sound.Tap:
                audio = _tapSound;
                break;
            case Sound.Upgrade:
                audio = _upgradeSound;
                break;
            case Sound.SlotMachineStart:
                audio = _slotMachineStartSound;
                break;
            default:
                audio = _tapSound;
                break;
        }
        audio.Play();
    }
    public enum Sound
    {
        MenuButton,
        Tap,
        Upgrade,
        SlotMachineStart,
        Win,
        Bancnote
    }
}

