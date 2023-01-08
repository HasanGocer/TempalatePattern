using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridReset : MonoSingleton<GridReset>
{
    [SerializeField] private Button ResetButton;

    public void startButton()
    {
        ResetButton.gameObject.SetActive(true);
        ResetButton.onClick.AddListener(GridSystemReset);
    }

    public void finishGame()
    {
        ResetButton.gameObject.SetActive(false);
    }

    public void GridSystemReset()
    {
        // reklam yaz
        if (Application.internetReachability != NetworkReachability.NotReachable && AdManager.Instance.IsReadyInterstitialAd())
        {
            AdManager.Instance.interstitial.Show();
            List<int> ObjectsCountInt = RandomSystem.Instance.ObjectCountInt;
            int limit = RandomSystem.Instance.ObjectList.Count - 1;
            for (int i = limit; i >= 0; i--)
            {
                GameObject obj = RandomSystem.Instance.ObjectList[i];
                obj.GetComponent<Draw>().LineRendererCanceled();
                ObjectID objectID = obj.GetComponent<ObjectID>();
                RandomSystem.Instance.ObjectGrid[objectID.lineCount, objectID.ColumnCount] = false;
                RandomSystem.Instance.ObjectPoolAdd(obj, RandomSystem.Instance.ObjectList, objectID.objectID);
            }
            RandomSystem.Instance.ObjectList.Clear();
            RandomSystem.Instance.ObjectCountInt.Clear();
            RandomSystem.Instance.ObjectTypeInt.Clear();
        }
    }
}
