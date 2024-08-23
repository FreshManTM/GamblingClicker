using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] AudioSource _bgMusic;
    [SerializeField] AudioSource _winSound;
    [SerializeField] AudioSource _menuButtonSound;
    [SerializeField] AudioSource _tapSound;
    [SerializeField] AudioSource _upgradeSound;
    [SerializeField] AudioSource _slotMachineStartSound;
    [SerializeField] AudioSource _failSound;

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
            case Sound.Fail:
                audio = _failSound;
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

    public void MuteBackgroundMusic(CanvasGroup canvasGroup)
    {
        if(!_bgMusic.isPlaying)
            _bgMusic.Play();
        if (_bgMusic.mute)
        {
            _bgMusic.mute = false;
            canvasGroup.alpha = 1f;
        }
        else
        {
            _bgMusic.mute = true;
            canvasGroup.alpha = 0.5f;
        }
    }
    public enum Sound
    {
        MenuButton,
        Tap,
        Upgrade,
        SlotMachineStart,
        Win,
        Fail,
        Bancnote
    }
}

