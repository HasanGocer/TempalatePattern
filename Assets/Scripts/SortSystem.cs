using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortSystem : MonoBehaviour
{
    void merge(int[] array, int beggin, int middle, int end)
    {
        int Left = middle - beggin + 1;
        int right = end - middle;

        int[] leftArray = new int[Left];
        int[] rightArray = new int[right];
        int i, j;

        for (i = 0; i < Left; ++i)
            leftArray[i] = array[beggin + i];
        for (j = 0; j < right; ++j)
            rightArray[j] = array[middle + 1 + j];

        i = 0;
        j = 0;

        int k = beggin;
        while (i < Left && j < right)
        {
            if (leftArray[i] <= rightArray[j])
            {
                array[k] = leftArray[i];
                i++;
            }
            else
            {
                array[k] = rightArray[j];
                j++;
            }
            k++;
        }

        while (i < Left)
        {
            array[k] = leftArray[i];
            i++;
            k++;
        }

        while (j < right)
        {
            array[k] = rightArray[j];
            j++;
            k++;
        }
    }

    public void sort(int[] array, int beggin, int end)
    {
        if (beggin < end)
        {
            int middle = beggin + (end - beggin) / 2;

            sort(array, beggin, middle);
            sort(array, middle + 1, end);

            merge(array, beggin, middle, end);
        }
    }
}
