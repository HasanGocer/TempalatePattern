using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttons : MonoSingleton<Buttons>
{
    //managerde bulunacak

    [SerializeField] private GameObject _money;

    public GameObject _startPanel;
    [SerializeField] private Button _startButton;

    [SerializeField] private Button _settingButton;
    [SerializeField] private GameObject _settingGame;

    [SerializeField] private Sprite _red, _green;
    [SerializeField] private Button _settingBackButton;
    [SerializeField] private Button _soundButton, _vibrationButton;

    public GameObject winPanel, failPanel;
    [SerializeField] private Button _winPrizeButton, _failButton;
    public Button winButton;

    public Text finishGameMoneyText;
    [SerializeField] private GameObject _startObject1;

    public Text moneyText, levelText;

    private void Start()
    {
        GameObject.FindObjectOfType<AdManager>().InitializeAds();
        ButtonPlacement();
        SettingPlacement();
        levelText.text = GameManager.Instance.level.ToString();
    }
    public IEnumerator NoThanxOnActive()
    {
        yield return new WaitForSeconds(3);
        winButton.gameObject.SetActive(true);
    }

    private void SettingPlacement()
    {
        if (GameManager.Instance.sound == 1)
        {
            _soundButton.gameObject.GetComponent<Image>().sprite = _green;
            SoundSystem.Instance.MainMusicPlay();
        }
        else
        {
            _soundButton.gameObject.GetComponent<Image>().sprite = _red;
            SoundSystem.Instance.MainMusicStop();
        }

        if (GameManager.Instance.vibration == 1)
        {
            _vibrationButton.gameObject.GetComponent<Image>().sprite = _green;
        }
        else
        {
            _vibrationButton.gameObject.GetComponent<Image>().sprite = _red;
        }
    }
    private void ButtonPlacement()
    {
        _startButton.onClick.AddListener(StartButton);
        _settingButton.onClick.AddListener(SettingButton);
        _settingBackButton.onClick.AddListener(SettingBackButton);
        _soundButton.onClick.AddListener(SoundButton);
        _vibrationButton.onClick.AddListener(VibrationButton);
        _winPrizeButton.onClick.AddListener(() => StartCoroutine(WinPrizeButton()));
        winButton.onClick.AddListener(() => StartCoroutine(WinButton()));
        _failButton.onClick.AddListener(FailButton);
        ObjectOpenSystem.Instance.newImageButton.onClick.AddListener(() => StartCoroutine(ObjectOpenSystem.Instance.NewImageButton()));
    }


    private void StartButton()
    {
        SoundSystem.Instance.CallEffectStart();
        GridReset.Instance.startButton();
        MarketSystem.Instance.GameStart();
        _startObject1.SetActive(true);
        _startPanel.SetActive(false);
        MaterialSystem.Instance.rivalPlayer.material = MaterialSystem.Instance.RivalMaterials[GameManager.Instance.level % MaterialSystem.Instance.RivalMaterials.Count];
        GameManager.Instance.isStart = true;
        RandomSystem.Instance.StartRandomSystem();
        FightBarSystem.Instance.StartFightBar();
        AnimControl.Instance.StartAnimencer();
        FightBarSystem.Instance.StartBarPanel();
        AdManager.Instance.bannerView.Hide();
    }
    private IEnumerator WinPrizeButton()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable && AdManager.Instance.IsReadyInterstitialAd())
        {
            SoundSystem.Instance.CallEffectFinish();
            AdManager.Instance.interstitial.Show();
            BarSystem.Instance.BarStopButton();
            winButton.enabled = false;
            _winPrizeButton.enabled = false;
            MarketSystem.Instance.FinishGameBackToTheMaterial();
            Vibration.Vibrate(30);
            yield return new WaitForSeconds(3);
            if (!LevelSystem.Instance.newObjectTime)
                SceneManager.LoadScene(0);
            else
                ObjectOpenSystem.Instance.NewObjectOpenPanel();
        }
    }
    private IEnumerator WinButton()
    {
        SoundSystem.Instance.CallEffectFinish();
        MoneySystem.Instance.MoneyTextRevork(GameManager.Instance.addedMoney);
        LevelSystem.Instance.NewLevelCheckField();
        winButton.enabled = false;
        _winPrizeButton.enabled = false;
        MarketSystem.Instance.FinishGameBackToTheMaterial();
        yield return new WaitForSeconds(1.3f);
        if (!LevelSystem.Instance.newObjectTime)
            SceneManager.LoadScene(0);
        else
            ObjectOpenSystem.Instance.NewObjectOpenPanel();
    }
    private void FailButton()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable && AdManager.Instance.IsReadyInterstitialAd())
            AdManager.Instance.interstitial.Show();
        SoundSystem.Instance.CallEffectFail();
        MoneySystem.Instance.MoneyTextRevork(GameManager.Instance.addedMoney);
        SceneManager.LoadScene(0);
    }
    private void SettingButton()
    {
        _startPanel.SetActive(false);
        _settingGame.SetActive(true);
        _settingButton.gameObject.SetActive(false);
        _money.SetActive(false);
    }
    private void SettingBackButton()
    {
        _startPanel.SetActive(true);
        _settingGame.SetActive(false);
        _settingButton.gameObject.SetActive(true);
        _money.SetActive(true);
    }
    private void SoundButton()
    {
        if (GameManager.Instance.sound == 1)
        {
            GameManager.Instance.sound = 0;
            _soundButton.gameObject.GetComponent<Image>().sprite = _red;
            SoundSystem.Instance.MainMusicStop();
            GameManager.Instance.sound = 0;
            GameManager.Instance.SetSound();
        }
        else
        {
            GameManager.Instance.sound = 1;
            _soundButton.gameObject.GetComponent<Image>().sprite = _green;
            SoundSystem.Instance.MainMusicPlay();
            GameManager.Instance.sound = 1;
            GameManager.Instance.SetSound();
        }
    }
    private void VibrationButton()
    {
        if (GameManager.Instance.vibration == 1)
        {
            GameManager.Instance.vibration = 0;
            _vibrationButton.gameObject.GetComponent<Image>().sprite = _red;
            GameManager.Instance.vibration = 0;
            GameManager.Instance.SetVibration();
        }
        else
        {
            GameManager.Instance.vibration = 1;
            _vibrationButton.gameObject.GetComponent<Image>().sprite = _green;
            GameManager.Instance.vibration = 1;
            GameManager.Instance.SetVibration();
        }
    }

}
