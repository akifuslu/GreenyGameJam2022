using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MessageBus.ClearSubs();
    }
}
