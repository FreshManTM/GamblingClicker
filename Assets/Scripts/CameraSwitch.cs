using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraSwitch : MonoBehaviour
{
    [SerializeField] Transform _targetCameraTransform;
    [SerializeField] float _transitionDuration = 1;

    float _defaultXPos;
    private void Start()
    {
        _defaultXPos = transform.position.x;
    }
    public void SwitchCameraView(bool moveRight)
    {
        if (moveRight)
            transform.DOMoveX(_targetCameraTransform.position.x, _transitionDuration);
        else
            transform.DOMoveX(_defaultXPos, _transitionDuration);
    }

}
