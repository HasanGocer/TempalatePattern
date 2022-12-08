using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContractUISystem : MonoSingleton<ContractUISystem>
{
    [SerializeField] private List<Sprite> _TemplateImage = new List<Sprite>();

    [SerializeField] private List<Image> _PosContractImage = new List<Image>();
    [SerializeField] private List<Text> _PosContractText = new List<Text>();
    public Button backContractButton, acceptedContractButton;

    public List<Button> PosNewContractButton = new List<Button>();

    [SerializeField] private List<Image> _PosSelectImage = new List<Image>();
    [SerializeField] private List<Text> _PosSelectText = new List<Text>();


    public int contractCount;
    public int selectContractCount;
    public int contractLimitCount;

    public ContractSystem.Contract[] contract;




    //Contract penceresini açtýr
    public void ContractUIPlacement(ContractSystem.Contract contract)
    {
        Buttons.Instance.contractGame.SetActive(true);

        for (int i = 0; i < _PosContractImage.Count; i++)
        {
            _PosContractImage[i].gameObject.SetActive(false);
            _PosContractText[i].gameObject.SetActive(false);
        }

        for (int i2 = 0; i2 < contract.objectTypeCount.Count; i2++)
        {
            _PosContractImage[i2].sprite = _TemplateImage[contract.objectTypeCount[i2]];
            _PosContractImage[i2].gameObject.SetActive(true);
            _PosContractText[i2].text = contract.objectCount[i2].ToString();
            _PosContractText[i2].gameObject.SetActive(true);
        }
    }

    //seçilen contract açýlýr(UI)
    public void TouchContractButton(ContractSystem.Contract contract, int sContractCount)
    {
        Buttons.Instance.contractSelectGame.SetActive(true);
        Buttons.Instance.newContractSelectGame.SetActive(false);
        selectContractCount = sContractCount;

        for (int i = 0; i < _PosSelectImage.Count; i++)
        {
            _PosSelectImage[i].gameObject.SetActive(false);
            _PosSelectText[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < contract.objectTypeCount.Count; i++)
        {
            _PosSelectImage[i].sprite = _TemplateImage[contract.objectTypeCount[i]];
            _PosSelectImage[i].gameObject.SetActive(true);
            _PosSelectText[i].text = contract.objectCount[i].ToString();
            _PosSelectText[i].gameObject.SetActive(true);
        }

    }

    //Seçilen contract kabul edilir
    public void SelectTheContract(int contractCount, ContractSystem.Contract contract)
    {
        Buttons.Instance.contractSelectGame.SetActive(false);
        ContractSystem.Instance.FocusContract.Contracts[contractCount] = contract;
        ContractSystem.Instance.FocusContract.Contracts[contractCount].contractBool = true;
        ContractSystem.Instance.FocusContract.Contracts[contractCount].contractBuy = true;
        ContractSystem.Instance.ObjectCountUpdate();
        ContractSystem.Instance.WaitSystemCountPlacement(GarbageSystem.Instance.waitBarUSCount, contractCount);
        GameManager.Instance.ContractPlacementWrite(ContractSystem.Instance.FocusContract);
    }

    //Contracts Penceresi geri açýlýr
    public void BackToTheContracts()
    {
        Buttons.Instance.contractSelectGame.SetActive(false);
        Buttons.Instance.newContractSelectGame.SetActive(true);
        for (int i = 0; i < contractLimitCount; i++)
        {
            PosNewContractButton[i].gameObject.SetActive(true);
        }
    }

    //Yeni Contract lar atanýr
    public void NewContractPlacement()
    {
        Buttons.Instance.newContractSelectGame.SetActive(true);
        for (int i = 0; i < contractLimitCount; i++)
        {
            contract[i] = ContractSystem.Instance.NewContractForUI(LevelSystem.Instance.levelMod, GameManager.Instance.level * LevelSystem.Instance.generalXPFactor, ItemData.Instance.field.dirtyGarbage);
            PosNewContractButton[i].gameObject.SetActive(true);
        }
    }
}
