using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script will read the dialogue content, find the case or default case then print the content of the case.
/// if case not exist, print default.
/// </summary>

public class TestSwitchCase : MonoBehaviour
{
    int dialogue = 3;

    void Start()
    {
        switch(dialogue)
       
        {
      case 2:
         print("Goodbye, old friend");
            break;
      case 1:
         print("Hello there");
            break;
            default:
         print("Incorrect dialogue value");
            break;
        }
    }
    
}
