using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelBarSystem : MonoSingleton<LevelBarSystem>
{
    [SerializeField] private float _levelConstant, _levelConstantPlus;
    [SerializeField] private int _maxXP;

    [SerializeField] private Image _barImage;
    [SerializeField] private Text _levelText, _nextLevelText;


    public void LevelStart()
    {
        GameManager gameManager = GameManager.Instance;
        for (int i = 1; i < gameManager.level; i++)
        {
            _levelConstant += _levelConstantPlus;
            _maxXP = (int)(_maxXP * _levelConstant);
        }
        _barImage.fillAmount = (float)gameManager.XP / (float)_maxXP;
        _levelText.text = gameManager.level.ToString();
        _nextLevelText.text = (gameManager.level + 1).ToString();
    }

    public IEnumerator BarLerp(int XPPlus)
    {
        GameManager gameManager = GameManager.Instance;
        float count = 0;
        gameManager.XP += XPPlus;
        float limit = (float)gameManager.XP / (float)_maxXP;
        yield return null;
        while (true)
        {
            count += Time.deltaTime;
            _barImage.fillAmount = Mathf.Lerp(_barImage.fillAmount, limit, count);
            yield return new WaitForSeconds(Time.deltaTime);
            if (limit >= _barImage.fillAmount)
            {
                LevelUpgradeCheck();
                gameManager.SetXP();
                break;
            }
        }
    }

    public void LevelUpgradeCheck()
    {
        GameManager gameManager = GameManager.Instance;
        if (gameManager.XP >= _maxXP)
        {
            _levelConstant += _levelConstantPlus;
            _maxXP = (int)(_maxXP * _levelConstant);
            gameManager.XP = 0;
            gameManager.level++;
            gameManager.SetLevel();
            _levelText.text = gameManager.level.ToString();
            _nextLevelText.text = (gameManager.level + 1).ToString();

        }
    }
}
