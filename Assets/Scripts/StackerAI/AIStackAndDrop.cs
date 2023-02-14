using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AIStackAndDrop : MonoBehaviour
{
    [SerializeField] private float _stackMoveTime;
    [SerializeField] private int _AIStackerContractCount;
    [SerializeField] private GameObject _stackParent;
    [SerializeField] private List<GameObject> _stackersStack = new List<GameObject>();
    [SerializeField] private List<int> _stackerStackCount = new List<int>();
    [SerializeField] private int _stackMaxStackCount;
    [SerializeField] private bool backpackÝsFull;
    [SerializeField] private bool backToTheHome;

    public IEnumerator WalkAI(GameObject WaitPlane)
    {
        while (true)
        {
            yield return null;
            //AI kullanýma açýlmýþsa ve contracttaki obje istemeye devam ediyorsa  ve backpack dolmamýþsa
            if (ContractSystem.Instance.FocusContract.Contracts[_AIStackerContractCount].contractBool && !(ContractControl()) && !backpackÝsFull)
            {
                //Contracttaki obje tipler
                for (int i1 = 0; i1 < ContractSystem.Instance.FocusContract.Contracts[_AIStackerContractCount].objectTypeCount.Count; i1++)
                {
                    //Dýþardaki objeler
                    for (int i2 = 0; i2 < ObjectManager.Instance.objectÝnGame.Count; i2++)
                    {
                        //dýþardaki objenin tipi ve contract ta aranan tip ayný ise
                        if (ContractSystem.Instance.FocusContract.Contracts[_AIStackerContractCount].objectTypeCount[i1] == i2 && ObjectManager.Instance.objectÝnGame[i2].gameObjectÝnGame.Count > 0)
                        {
                            //Contractta aranan obje tipinin aranma sayýsý
                            for (int i3 = 0; i3 < ContractSystem.Instance.FocusContract.Contracts[_AIStackerContractCount].objectCount[i1]; i3++)
                            {
                                //backpack dolmamýþsa
                                if (!backpackÝsFull)
                                {
                                    List<GameObject> gameObjectÝnGame = ObjectManager.Instance.objectÝnGame[i2].gameObjectÝnGame;
                                    int lastStackCount = gameObjectÝnGame.Count - 1;
                                    lastStackCount = OpenObjectCall(lastStackCount, gameObjectÝnGame);
                                    //obje yerde mi kontrol ediyor
                                    if (lastStackCount != -1)
                                    {
                                        GameObject lastObjectGO = gameObjectÝnGame[lastStackCount];
                                        Vector3 lastObjectV3 = lastObjectGO.transform.position;

                                        transform.DOMove(lastObjectV3, Vector3.Distance(transform.position, lastObjectV3) * AIManager.Instance.AIDistanceConstant);
                                        yield return new WaitForSeconds(Vector3.Distance(transform.position, lastObjectV3) * AIManager.Instance.AIDistanceConstant);
                                        //objenin yanýna gittik ama obje hala orda mý ve o obje bizim objemiz mi kontrol ediyoruz
                                        if (lastObjectGO == gameObjectÝnGame[lastStackCount] && transform.position == gameObjectÝnGame[lastStackCount].transform.position)
                                        {
                                            _stackersStack.Add(lastObjectGO);
                                            _stackerStackCount.Add(i2);
                                            if (_stackerStackCount.Count == ItemData.Instance.field.AIStackCount[_AIStackerContractCount])
                                            {
                                                backpackÝsFull = true;
                                            }
                                            _stackersStack[_stackersStack.Count - 1].GetComponent<ObjectTouchPlane>().inWaitPlace = true;
                                            _stackersStack[_stackersStack.Count - 1].GetComponent<ObjectTouchPlane>().StackÝnPlayer(true);
                                            _stackersStack[_stackersStack.Count - 1].transform.SetParent(_stackParent.transform);
                                            _stackersStack[_stackersStack.Count - 1].transform.DOMove(new Vector3(_stackParent.transform.position.x, _stackParent.transform.position.y + AIManager.Instance.stackDistance * _stackersStack.Count, _stackParent.transform.position.z), _stackMoveTime);
                                            yield return new WaitForSeconds(_stackMoveTime);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if (backpackÝsFull && !backToTheHome)
                StartCoroutine(GoToTheStackOut(WaitPlane));
        }
    }

    private IEnumerator GoToTheStackOut(GameObject stackOutPlace)
    {
        //mother base dönüþ
        backToTheHome = true;
        transform.DOMove(stackOutPlace.GetComponent<WaitSystem>().AIWaitPlace.transform.position, AIManager.Instance.AIDistanceConstant * Vector3.Distance(stackOutPlace.GetComponent<WaitSystem>().objectPos.transform.position, transform.position));
        yield return new WaitForSeconds(AIManager.Instance.AIDistanceConstant * Vector3.Distance(stackOutPlace.transform.position, transform.position));
        StartCoroutine(StackOut(stackOutPlace));
    }

    private IEnumerator StackOut(GameObject lastPos)
    {
        //mother base eþya býrakma
        for (int i = 0; i < _stackersStack.Count; i++)
        {
            _stackersStack[i].transform.DOMove(lastPos.GetComponent<WaitSystem>().objectPos.transform.position, _stackMoveTime);
            ContractSystem.Instance.ContractDownÝtem(_AIStackerContractCount, _stackerStackCount[i], 0, false);
            _stackersStack[i].transform.SetParent(lastPos.GetComponent<WaitSystem>().objectPos.transform);
            _stackersStack.RemoveAt(i);
            _stackerStackCount.RemoveAt(i);
        }
        yield return new WaitForSeconds(_stackMoveTime);

        //contract tamamlanýnca kendi yapar
        /*_stackersStack[i].GetComponent<ObjectTouchPlane>().AddedObjectPool(_stackerStackCount[i]);
        RocketManager.Instance.AddedObjectPool(_stackersStack[i]);*/

        backpackÝsFull = false;
        backToTheHome = false;
    }

    public bool ObjectControl(int objectCount, List<int> objectTypeCount)
    {
        bool inThere = true;
        for (int i = 0; i < objectTypeCount.Count; i++)
        {
            if (objectTypeCount[i] == objectCount)
                inThere = false;
        }
        return inThere;
    }

    public bool ContractControl()
    {
        bool game = true;
        List<bool> globalBool = new List<bool>();
        for (int i1 = 0; i1 < ContractSystem.Instance.FocusContract.Contracts[_AIStackerContractCount].objectTypeCount.Count; i1++)
        {
            globalBool.Add(false);
            for (int i2 = 0; i2 < ContractSystem.Instance.FocusContract.Contracts[_AIStackerContractCount].objectCount[i1]; i2++)
            {
                int contractCount = ContractSystem.Instance.FocusContract.Contracts[_AIStackerContractCount].objectCount[i1];
                for (int i3 = 0; i3 < _stackersStack.Count; i3++)
                {
                    if (_stackerStackCount[i3] == ContractSystem.Instance.FocusContract.Contracts[_AIStackerContractCount].objectTypeCount[i1])
                    {
                        contractCount--;
                    }
                }
                if (contractCount == 0)
                {
                    globalBool[i1] = true;
                }
            }
        }

        for (int i = 0; i < globalBool.Count; i++)
        {
            if (!globalBool[i])
                game = false;
        }
        if (game)
        {
            backpackÝsFull = true;
        }
        return game;
    }

    public int OpenObjectCall(int maxCount, List<GameObject> objects)
    {
        for (int i = maxCount; i >= 0; i--)
        {
            if (!objects[i].GetComponent<ObjectTouchPlane>().inWaitPlace)
            {
                return i;
            }
        }
        return -1;
    }
}
