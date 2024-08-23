using System.Collections;
using UnityEngine;

public enum SlotValue
{
    J,
    A,
    X,
    Flower,
    Heart
}
public class Slots : MonoBehaviour
{

    int randomValue;
    float timeInterval;
    float speed;
    public SlotValue stoppedSlot;
    SMachine _slotMachine;

    void Start()
    {
        _slotMachine = gameObject.GetComponentInParent<SMachine>();
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
                stoppedSlot = SlotValue.J;
                break;
            case -1f:
                stoppedSlot = SlotValue.A;
                break;
            case 0f:
                stoppedSlot = SlotValue.X;
                break;
            case 1f:
                stoppedSlot = SlotValue.Flower;
                break;
            default:
                stoppedSlot = SlotValue.Heart;
                break;
        }

        _slotMachine.WaitResults();
    }
}
