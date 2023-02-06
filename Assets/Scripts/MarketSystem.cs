using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketSystem : MonoSingleton<MarketSystem>
{
    [System.Serializable]
    public class FieldBool
    {
        public List<bool> PlayerColor = new List<bool>();
        public int equipedCount;
    }

    [System.Serializable]
    public class FieldRivalColor
    {
        public List<int> PlayerColorPrice = new List<int>();
        public List<Material> PlayerColorMaterial = new List<Material>();
        public List<Button> PlayerImageButton = new List<Button>();
        public List<Image> PlayerImageLock = new List<Image>();
    }

    public FieldBool fieldBool;
    public FieldRivalColor fieldPlayerColor;

    [SerializeField] private Button _useButton, _equipedButton, _moneyBuyButton, _addBuyButton;
    [SerializeField] private Text _moneyBuyPriceText;
    [SerializeField] private Button _marketOpenButton, _marketCloseButton;
    public GameObject marketPanel;
    [SerializeField] private Material Mat2D;
    public int fieldCount;

    public void MarketStart()
    {
        MarketOnOffPlacement();
        DownButtonPlacement();
        MarketStartPlacement();
    }

    public void GameStart()
    {
        _marketOpenButton.gameObject.SetActive(false);
    }

    public void MarketOpenButton()
    {
        Buttons.Instance._startPanel.SetActive(false);
        _marketOpenButton.gameObject.SetActive(false);
        marketPanel.SetActive(true);
    }

    public void MarketCloseButton()
    {
        Buttons.Instance._startPanel.SetActive(true);
        _marketOpenButton.gameObject.SetActive(true);
        marketPanel.SetActive(false);
    }

    private void SelectMarketPlayerColorFieldButton(int count)
    {
        fieldCount = count;
        if (fieldBool.PlayerColor[count] && fieldBool.equipedCount == count)
        {
            _equipedButton.gameObject.SetActive(true);
            _useButton.gameObject.SetActive(false);
            _moneyBuyButton.gameObject.SetActive(false);
            _addBuyButton.gameObject.SetActive(false);
        }
        else if (fieldBool.PlayerColor[count])
        {
            _equipedButton.gameObject.SetActive(false);
            _useButton.gameObject.SetActive(true);
            _moneyBuyButton.gameObject.SetActive(false);
            _addBuyButton.gameObject.SetActive(false);
        }
        else
        {
            _equipedButton.gameObject.SetActive(false);
            _useButton.gameObject.SetActive(false);
            _moneyBuyButton.gameObject.SetActive(true);
            _addBuyButton.gameObject.SetActive(true);
            _moneyBuyPriceText.text = fieldPlayerColor.PlayerColorPrice[count].ToString();
            //money para atamasý yap
        }
    }
    private void SelectMarketPlayerColorUseButton()
    {
        _equipedButton.gameObject.SetActive(true);
        _useButton.gameObject.SetActive(false);
        fieldBool.equipedCount = fieldCount;
    }
    private void SelectMarketPlayerColorMoneyBuyButton()
    {
        GameManager gameManager = GameManager.Instance;
        if (gameManager.money >= fieldPlayerColor.PlayerColorPrice[fieldCount])
        {
            MoneySystem.Instance.MoneyTextRevork(fieldPlayerColor.PlayerColorPrice[fieldCount] * -1);
            _equipedButton.gameObject.SetActive(false);
            _useButton.gameObject.SetActive(true);
            _moneyBuyButton.gameObject.SetActive(false);
            _addBuyButton.gameObject.SetActive(false);
            fieldPlayerColor.PlayerImageLock[fieldCount].gameObject.SetActive(false);
            fieldBool.PlayerColor[fieldCount] = true;
        }
    }
    private void SelectMarketPlayerColorAddBuyButton()
    {
        //add
        /*
                     _equipedButton.gameObject.SetActive(false);
            _useButton.gameObject.SetActive(true);
                    _moneyBuyButton.gameObject.SetActive(false);
            _addBuyButton.gameObject.SetActive(false);
            fieldBool.PlayerColor[fieldCount] = true;
            fieldPlayerColor.PlayerImageLock[fieldCount].gameObject.SetActive(false);
            GameManager.Instance.MarketPlacementWrite(fieldBool);
         
         */
    }
    private void DownButtonPlacement()
    {
        _useButton.onClick.AddListener(SelectMarketPlayerColorUseButton);
        _moneyBuyButton.onClick.AddListener(SelectMarketPlayerColorMoneyBuyButton);
        _addBuyButton.onClick.AddListener(SelectMarketPlayerColorAddBuyButton);
    }
    private void MarketStartPlacement()
    {
        for (int i = 0; i < fieldPlayerColor.PlayerImageButton.Count; i++)
        {
            Material mat = new Material(Mat2D.shader);
            mat.color = fieldPlayerColor.PlayerColorMaterial[i].color;
            fieldPlayerColor.PlayerImageButton[i].transform.GetChild(0).GetComponent<Image>().material = mat;
            fieldPlayerColor.PlayerImageButton[i].transform.GetChild(0).GetComponent<Image>().material.color = fieldPlayerColor.PlayerColorMaterial[i].color;
            if (fieldBool.PlayerColor[i])
                fieldPlayerColor.PlayerImageLock[i].gameObject.SetActive(false);
        }
        fieldPlayerColor.PlayerImageButton[0].onClick.AddListener(() => SelectMarketPlayerColorFieldButton(0));
        fieldPlayerColor.PlayerImageButton[1].onClick.AddListener(() => SelectMarketPlayerColorFieldButton(1));
        fieldPlayerColor.PlayerImageButton[2].onClick.AddListener(() => SelectMarketPlayerColorFieldButton(2));
        fieldPlayerColor.PlayerImageButton[3].onClick.AddListener(() => SelectMarketPlayerColorFieldButton(3));
        fieldPlayerColor.PlayerImageButton[4].onClick.AddListener(() => SelectMarketPlayerColorFieldButton(4));
        fieldPlayerColor.PlayerImageButton[5].onClick.AddListener(() => SelectMarketPlayerColorFieldButton(5));
        fieldPlayerColor.PlayerImageButton[6].onClick.AddListener(() => SelectMarketPlayerColorFieldButton(6));
        fieldPlayerColor.PlayerImageButton[7].onClick.AddListener(() => SelectMarketPlayerColorFieldButton(7));
        fieldPlayerColor.PlayerImageButton[8].onClick.AddListener(() => SelectMarketPlayerColorFieldButton(8));
    }
    private void MarketOnOffPlacement()
    {
        _marketOpenButton.onClick.AddListener(MarketOpenButton);
        _marketCloseButton.onClick.AddListener(MarketCloseButton);
    }
}
