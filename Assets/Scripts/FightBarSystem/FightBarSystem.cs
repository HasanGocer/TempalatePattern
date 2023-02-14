using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FightBarSystem : MonoSingleton<FightBarSystem>
{
    [SerializeField] private int playerScore, rivalScore;
    public int rivalMaxHit, rivalHitTime;
    [SerializeField] private Image bar;
    [SerializeField] private GameObject barPanel;

    public void StartFightBar()
    {
        BarUpdate(playerScore);
        StartCoroutine(RivalHitEnum(rivalHitTime, rivalMaxHit));
    }

    public void StartBarPanel()
    {
        barPanel.SetActive(true);
    }

    public void FinishBarPanel()
    {
        barPanel.SetActive(false);
    }

    public IEnumerator RivalHitEnum(int waitTime, int plusMaxRange)
    {
        while (GameManager.Instance.isStart)
        {
            yield return new WaitForSeconds(waitTime);
            int count = Random.Range(1, plusMaxRange);
            RivalScoreAdd(count);
            if (GameManager.Instance.isStart)
                StartCoroutine(AnimControl.Instance.CallHitRival(count));
        }
    }
    public void PlayerScoreAdd(int plus)
    {
        PlayerScoreAddBar(plus, ItemData.Instance.field.barHealth);
    }
    public void RivalScoreAdd(int plus)
    {
        RivalScoreAddBar(plus, ItemData.Instance.field.barHealth);
    }

    private void RivalScoreAddBar(int plus, int maxScore)
    {
        if (rivalScore > 0 || playerScore == 0)
        {
            rivalScore += plus;
        }
        else
        {
            playerScore -= plus;
            if (playerScore < 0)
            {
                rivalScore += playerScore - 1;
                playerScore = 0;
            }
        }
        BarUpdate(maxScore);
    }
    private void PlayerScoreAddBar(int plus, int maxScore)
    {
        if (playerScore > 0 || rivalScore == 0)
        {
            playerScore += plus;
        }
        else
        {
            rivalScore -= plus;
            if (rivalScore < 0)
            {
                playerScore += rivalScore * -1;
                rivalScore = 0;
            }
        }
        BarUpdate(maxScore);
    }
    private void BarUpdate(int maxScore)
    {
        if (playerScore > 0)
        {
            float count = 0.5f + (((float)playerScore / (float)maxScore) / 2);
            if (count >= 1)
            {
                ParticalSystem.Instance.ComboSetActiveFalse();
                FinishBarPanel();
                SoundSystem.Instance.CallEffectFinish();
                GridReset.Instance.finishGame();
                Buttons.Instance.winPanel.SetActive(true);
                StartCoroutine(Buttons.Instance.NoThanxOnActive());
                StartCoroutine(ParticalSystem.Instance.FinishTimePartical());
                StartCoroutine(BarSystem.Instance.BarImageFillAmountIenum());
                GameManager.Instance.isStart = false;
                RandomSystem.Instance.AllObjectCallBack();
                //StartCoroutine(AnimControl.Instance.FinishHim());
                AnimControl.Instance.CallPlayerWin();
                bar.fillAmount = 1;
                GameManager.Instance.level++;
                GameManager.Instance.SetLevel();
                LevelSystem.Instance.NewLevelCheckField();
                Buttons.Instance.finishGameMoneyText.text = MoneySystem.Instance.NumberTextRevork(GameManager.Instance.addedMoney);
            }
            else
                StartCoroutine(BarUpdateIenum(count - bar.fillAmount));
        }
        else if (rivalScore > 0)
        {
            float count = 0.5f - (((float)rivalScore / (float)maxScore) / 2);
            if (count <= 0)
            {
                FinishBarPanel();
                SoundSystem.Instance.CallEffectFail();
                GridReset.Instance.finishGame();
                Buttons.Instance.failPanel.SetActive(true);
                GameManager.Instance.isStart = false;
                RandomSystem.Instance.AllObjectCallBack();
                AnimControl.Instance.CallRivalWin();
                bar.fillAmount = 0;
            }
            else
                StartCoroutine(BarUpdateIenum(count - bar.fillAmount));
        }
        else
            bar.fillAmount = 0.5f;
    }

    public IEnumerator BarUpdateIenum(float count)
    {
        float timer = 0;
        float finish;
        float start = bar.fillAmount;

        finish = bar.fillAmount + count;

        while (true)
        {
            timer += Time.deltaTime;
            bar.fillAmount = Mathf.Lerp(start, finish, timer);
            yield return new WaitForEndOfFrame();
            if (bar.fillAmount == finish)
                break;
        }
    }
}
