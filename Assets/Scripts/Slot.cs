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

    private void Start()
    {
        _slotMachine = gameObject.GetComponentInParent<SlotMachine>();
    }
    public IEnumerator Spin()
    {
        timeInterval = _slotMachine.timeInterval;
        randomValue = Random.Range(0, 90);
        speed = 30f + randomValue;
        while(speed >= 10f)
        {
            speed = speed / 1.01f;
            transform.Translate(Vector2.up * Time.deltaTime * -speed);
            if (transform.localPosition.y <= -1.5f)
            {
                transform.localPosition = new Vector2(transform.localPosition.x, 1.5f);
            }

            yield return new WaitForSeconds(timeInterval);
        }
        StartCoroutine("EndSpin");
        yield return null;
    }
    private IEnumerator EndSpin()
    {
        Vector2 newPos = Vector2.zero;
        print("endSpin " + gameObject.name);
        while (speed >= 2f)
        {
            newPos = new Vector2(transform.localPosition.x, 0f);

            if (transform.localPosition.y < -0.75f)
            {
                newPos.y = -1.5f;
            }
            else if (transform.localPosition.y < 0f)
            {
                newPos.y = -0.75f;
            }
            else if (transform.localPosition.y < 0.75f)
            {
                newPos.y = 0f;
            }
            else if (transform.localPosition.y < 1.5f)
            {
                newPos.y = 0.75f;
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
    private void CheckResults()
    {
        if (transform.localPosition.y == -1.5f)
        {
            stoppedSlot = SlotValue.Banana;
        }
        else if (transform.localPosition.y == -0.75f)
        {
            stoppedSlot = SlotValue.Cherry;
        }
        else if (transform.localPosition.y == 0f)
        {
            stoppedSlot = SlotValue.Clover;
        }
        else if (transform.localPosition.y == 0.75f)
        {
            stoppedSlot = SlotValue.Strawbery;
        }
        else if (transform.localPosition.y == 1.5f)
        {
            stoppedSlot = SlotValue.Grape;
        }

        _slotMachine.WaitResults();
    }
}
