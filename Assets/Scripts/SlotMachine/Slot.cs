using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlotValue
{
    Banana,
    Cherry,
    Clover,
    Strawbery,
    Grape
}
public class Slot : MonoBehaviour
{

    int randomValue;
    float timeInterval;
    float speed;
    public SlotValue stoppedSlot;
    SlotMachine _slotMachine;

    void Start()
    {
        _slotMachine = gameObject.GetComponentInParent<SlotMachine>();
    }

    public IEnumerator Spin()
    {
        timeInterval = _slotMachine._timeInterval;
        randomValue = Random.Range(0, 90);
        speed = 30f + randomValue;
        while(speed >= 10f)
        {
            speed = speed / 1.01f;
            transform.Translate(Vector2.up * Time.deltaTime * -speed);
            if (transform.localPosition.y <= -3f)
            {
                transform.localPosition = new Vector2(transform.localPosition.x, 2f);
            }

            yield return new WaitForSeconds(timeInterval);
        }
        StartCoroutine(IEndSpin());
        yield return null;
    }
    
    IEnumerator IEndSpin()
    {
        Vector2 newPos = Vector2.zero;
        float x = -4f;
        float y = 0;
        while (speed >= 2f)
        {
            newPos = new Vector2(transform.localPosition.x, 0f);

            for (int i = 0; i < 5; i++)
            {
                if (transform.localPosition.y < x)
                {
                    y = x - 1f;
                    newPos.y = y;
                    break;
                }
                else
                {
                    x += 1f;
                }
            }

            transform.localPosition = Vector2.MoveTowards(transform.localPosition, newPos, speed * Time.deltaTime);

            if ((Vector2)transform.localPosition == newPos)
            {
                speed = 0;
            }

            speed = speed / 1.01f;
            yield return new WaitForSeconds(timeInterval);
        }
        speed = 0;
        CheckResults();
        yield return null;
    }

    void CheckResults()
    {
        switch (transform.localPosition.y)
        {
            case -2f:
                stoppedSlot = SlotValue.Banana;
                break;
            case -1f:
                stoppedSlot = SlotValue.Cherry;
                break;
            case 0f:
                stoppedSlot = SlotValue.Clover;
                break;
            case 1f:
                stoppedSlot = SlotValue.Strawbery;
                break;
            default:
                stoppedSlot = SlotValue.Grape;
                break;
        }

        _slotMachine.WaitResults();
    }
}
