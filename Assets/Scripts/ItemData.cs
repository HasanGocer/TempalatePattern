using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoSingleton<ItemData>
{
    [System.Serializable]
    public class Field
    {
        public int objectTypeCount, objectCount;
    }

    public Field field;
    public Field standart;
    public Field factor;
    public Field constant;
    public Field maxFactor;
    public Field max;
    public Field fieldPrice;

    public void AwakeID()
    {
        field.objectTypeCount = standart.objectTypeCount + (factor.objectTypeCount * constant.objectTypeCount);
        fieldPrice.objectTypeCount = fieldPrice.objectTypeCount * factor.objectTypeCount;
        field.objectCount = standart.objectCount + (factor.objectCount * constant.objectCount);
        fieldPrice.objectCount = fieldPrice.objectCount * factor.objectCount;

        if (factor.objectTypeCount > maxFactor.objectTypeCount)
        {
            factor.objectTypeCount = maxFactor.objectTypeCount;
            field.objectTypeCount = standart.objectTypeCount + (factor.objectTypeCount * constant.objectTypeCount);
            fieldPrice.objectTypeCount = fieldPrice.objectTypeCount / (factor.objectTypeCount - 1);
            fieldPrice.objectTypeCount = fieldPrice.objectTypeCount * factor.objectTypeCount;
        }
        if (factor.objectCount > maxFactor.objectCount)
        {
            factor.objectCount = maxFactor.objectCount;
            field.objectCount = standart.objectCount + (factor.objectCount * constant.objectCount);
            fieldPrice.objectCount = fieldPrice.objectCount / (factor.objectCount - 1);
            fieldPrice.objectCount = fieldPrice.objectCount * factor.objectCount;
        }

    }

    public void SetObjectTypeCount()
    {
        factor.objectTypeCount++;

        field.objectTypeCount = standart.objectTypeCount + (factor.objectTypeCount * constant.objectTypeCount);
        fieldPrice.objectTypeCount = fieldPrice.objectTypeCount / (factor.objectTypeCount - 1);
        fieldPrice.objectTypeCount = fieldPrice.objectTypeCount * factor.objectTypeCount;

        if (factor.objectTypeCount > maxFactor.objectTypeCount)
        {
            factor.objectTypeCount = maxFactor.objectTypeCount;
            field.objectTypeCount = standart.objectTypeCount + (factor.objectTypeCount * constant.objectTypeCount);
            fieldPrice.objectTypeCount = fieldPrice.objectTypeCount / (factor.objectTypeCount - 1);
            fieldPrice.objectTypeCount = fieldPrice.objectTypeCount * factor.objectTypeCount;
        }

        GameManager.Instance.FactorPlacementWrite(factor);
    }

    public void SetObjectCount()
    {
        factor.objectCount++;

        field.objectCount = standart.objectCount + (factor.objectCount * constant.objectCount);
        fieldPrice.objectCount = fieldPrice.objectCount / (factor.objectCount - 1);
        fieldPrice.objectCount = fieldPrice.objectCount * factor.objectCount;

        if (factor.objectCount > maxFactor.objectCount)
        {
            factor.objectCount = maxFactor.objectCount;
            field.objectCount = standart.objectCount + (factor.objectCount * constant.objectCount);
            fieldPrice.objectCount = fieldPrice.objectCount / (factor.objectCount - 1);
            fieldPrice.objectCount = fieldPrice.objectCount * factor.objectCount;
        }

        GameManager.Instance.FactorPlacementWrite(factor);
    }
}
