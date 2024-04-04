using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachine : MonoBehaviour
{
    public int money;
    public int price;
    public Slot[] slots;
    public Combinations[] combinations;
    public float timeInterval = 0.025f;
    private int stoppedSlots = 3;
    private bool isSpin = false;
    public bool isAuto;

    public void Spin()
    {
        if (!isSpin && money - price >= 0)
        {
            ChangeMoney(-price);
            isSpin = true;
            foreach (Slot i in slots)
            {
                i.StartCoroutine("Spin");
            }
        }
    }

    public void WaitResults()
    {
        stoppedSlots -= 1;
        if(stoppedSlots <= 0)
        {
            stoppedSlots = 3;
            CheckResults();
        }
    }

    public void CheckResults()
    {
        isSpin = false;
        foreach (Combinations i in combinations)
        {
            Debug.Log(slots[0].gameObject.GetComponent<Slot>().stoppedSlot.ToString());
            Debug.Log(slots[1].gameObject.GetComponent<Slot>().stoppedSlot.ToString());
            Debug.Log(slots[2].gameObject.GetComponent<Slot>().stoppedSlot.ToString());
            if (slots[0].gameObject.GetComponent<Slot>().stoppedSlot.ToString() == i.FirstValue.ToString()
                && slots[1].gameObject.GetComponent<Slot>().stoppedSlot.ToString() == i.SecondValue.ToString()
                && slots[2].gameObject.GetComponent<Slot>().stoppedSlot.ToString() == i.ThirdValue.ToString())
            {
                ChangeMoney(i.prize);
            }
        }
        if (isAuto)
        {
            Invoke("Spin", 0.4f);
        }
    }
    private void ChangeMoney(int count)
    {
        money += count;
    }
    public void SetAuto()
    {
        if (!isAuto)
        {
            timeInterval /= 10;
            isAuto = true;
            Spin();
        }
        else
        {
            timeInterval *= 10;
            isAuto = false;
        }
    }
}

[System.Serializable]
public class Combinations
{
    public enum SlotValue
    {
        Banana,
        Cherry,
        Clover,
        Strawbery,
        Grape
    }

    public SlotValue FirstValue;
    public SlotValue SecondValue;
    public SlotValue ThirdValue;
    public int prize;
}
