using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSystem : MonoSingleton<LevelSystem>
{
    [SerializeField] private float _levelConstant, _levelConstantPlus;
    [SerializeField] private int _levelStandart, _maxXP;
    [SerializeField] private Image _barImage;
    public int levelMod;

    public int generalMoneyFactor, generalXPFactor;
    public int XP;



    public void LevelStart()
    {
        _maxXP = _levelStandart;
        while (true)
        {
            if (XP >= _maxXP)
            {
                _levelConstant += _levelConstantPlus;
                _maxXP = (int)(_maxXP * _levelConstant);
            }
            else
            {
                _barImage.fillAmount = (float)XP / (float)_maxXP;
                break;
            }
        }
    }

    public void BarLerp(int XPPlus)
    {
        for (int i = 0; i < XPPlus; i++)
        {
            XP++;
            _barImage.fillAmount = (float)XP / (float)_maxXP;
            LevelUpgradeCheck();
            GameManager.Instance.SetXP();
        }
    }

    public void LevelUpgradeCheck()
    {
        if (XP >= _maxXP)
        {
            _levelConstant += _levelConstantPlus;
            _maxXP = (int)(_maxXP * _levelConstant);
            XP = 0;
            GameManager.Instance.level++;
            GameManager.Instance.SetLevel();

        }
    }
}
