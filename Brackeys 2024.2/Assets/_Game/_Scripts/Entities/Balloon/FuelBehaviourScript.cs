using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FuelBehaviourScript : MonoBehaviour
{
    #region Vari�veis
    [SerializeField] private Slider chargeBar;
    [SerializeField] private float fillRate = 0.5f;
    [SerializeField] private float drainRate = 0.05f;
    [SerializeField] private float delayBeforeDraining = 1f;
    private bool isFilling = false;
    private float fillDelayTimer = 0f;
    private TimerManager _timerManager;
    private BalloonCollision _balloonCollision;
    private BackgroundScroller _backgroundScroller;

    private bool _canFill = true;
    #endregion

    #region Fun��es Unity
    void Awake()
    {
        _timerManager = FindObjectOfType<TimerManager>();
        _balloonCollision = FindObjectOfType<BalloonCollision>();
        _backgroundScroller = FindObjectOfType<BackgroundScroller>();
    }

    void Update()
    {
        if (!_canFill) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isFilling = true;
            fillDelayTimer = 0f;
            _backgroundScroller.ySpeed *= 2f;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            _timerManager.timeCounter += _timerManager.time * BalloonStats.Speed;
        }

        if (isFilling)
        {
            chargeBar.value += fillRate * Time.deltaTime;

            if (chargeBar.value >= chargeBar.maxValue)
            {
                _balloonCollision.ReduceDurability(BalloonStats.Durability);
                Debug.Log("MORREU");
                chargeBar.value = chargeBar.maxValue;
                isFilling = false;
                _canFill = false;
            }
        }
        else
        {
            fillDelayTimer += Time.deltaTime;
            if (fillDelayTimer >= delayBeforeDraining)
            {
                chargeBar.value -= drainRate * Time.deltaTime;

                if (chargeBar.value <= chargeBar.minValue)
                {
                    chargeBar.value = chargeBar.minValue;
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isFilling = false;
            _backgroundScroller.ySpeed *= 0.5f;
        }
    }
    #endregion

    #region Fun��es Pr�prias
    #endregion
}
