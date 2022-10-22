using System.Collections;
using System.Collections.Generic;
using Flow;
using Grid;
using Models;
using UnityEngine;
using Views;

public class Game : MonoBehaviour
{

    private DayCycleView _dayCycleView;
    private DayCycle _dayCycle;

    private void Awake()
    {
        _dayCycleView = FindObjectOfType<DayCycleView>();
        _dayCycle = FindObjectOfType<DayCycle>();

        _dayCycleView.Bind(_dayCycle);
    }

    private void Start()
    {
        _dayCycle.NextDay();
    }
}
