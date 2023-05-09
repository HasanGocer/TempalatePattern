using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskUISystem : MonoSingleton<TaskUISystem>
{
    public GameObject TaskPanel;
    [SerializeField] List<Image> _TaskImage = new List<Image>();
    public List<GameObject> taskObject = new List<GameObject>();
    [SerializeField] List<TMP_Text> _TaskTextPos = new List<TMP_Text>();

    public void UIPlacement()
    {
        TaskSystem.Contract contract = TaskSystem.Instance.focusContract;

        TaskPanel.SetActive(true);

        for (int i = 0; i < contract.objectTypeCount.Count; i++)
        {
            taskObject[i].gameObject.transform.GetChild(contract.objectTypeCount[i]).gameObject.SetActive(true);
            _TaskTextPos[i].gameObject.transform.parent.gameObject.SetActive(true);
            _TaskTextPos[i].text = contract.objectCount[i].ToString();
        }
    }

    public void TaskDown(int typeCount)
    {
        TaskSystem.Contract contract = TaskSystem.Instance.focusContract;
        if (contract.objectTypeCount.Contains(typeCount))
        {
            int tempTypeCount = contract.objectTypeCount.IndexOf(typeCount);
            TaskSystem.Instance.ObjectCountUpdate(tempTypeCount);

            _TaskTextPos[tempTypeCount].text = contract.objectCount[tempTypeCount].ToString();

            if (contract.objectCount[tempTypeCount] <= 0)
            {
                _TaskTextPos[tempTypeCount].gameObject.transform.parent.gameObject.SetActive(false);
                _TaskImage[tempTypeCount].gameObject.SetActive(true);
            }
        }
    }
}
