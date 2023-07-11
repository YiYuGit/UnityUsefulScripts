using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using TMPro;

/// <summary>
/// This is a simple on screen clock example
/// Use Text Mesh Pro 
/// The leading zero make the digits of all time the same
/// 
/// It can also be used on non UI object, for example, make a 3D object TMP text in world space
/// The clock can be used in world space
/// </summary>

public class ClockExample : MonoBehaviour
{
    //Drag and drop TMP text here for the clock
    public TMP_Text textClock;
 
    void Update()
    {
        DateTime time = DateTime.Now;
        string hour = LeadingZero(time.Hour);
        string minute = LeadingZero(time.Minute);
        string second = LeadingZero(time.Second);
        textClock.text = hour + ":" + minute + ":" + second;
    }
    string LeadingZero(int n)
    {
        return n.ToString().PadLeft(2, '0');
    }
}