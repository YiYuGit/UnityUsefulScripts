using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveUserInfo : MonoBehaviour
{
    /// <summary>
    /// This script get user input from canvas InputField, save them in PlayerPrefs and display then on the canvas Text
    /// First type in the infomation, then click save button to save, the save button need to be tied to the script
    /// The information in PlayerPrefs can be accessed across the scenes
    /// To reset the input, use the reset button
    /// For example, this script get user ID and speedlimit for the game in inputfield, save to PlayerPrefs and display the info one UI text
    ///
    /// </summary>
    
    public InputField textBoxID;
    public InputField textBoxSpeedLimit;

    public Text DisplayText;

    public void clickSaveButton()
    {
        PlayerPrefs.SetString("ID", textBoxID.text);
        int x = int.Parse(textBoxSpeedLimit.text);
        PlayerPrefs.SetInt("SpeedLimit", x);
        Debug.Log("Your ID and speed limit: " + PlayerPrefs.GetString("ID") + " , " + PlayerPrefs.GetInt("SpeedLimit") + " MPH");
        DisplayText.text = "Your ID and speed limit: " + PlayerPrefs.GetString("ID") + " , " + PlayerPrefs.GetInt("SpeedLimit") + " MPH";

        /*
         PlayerPrefs can SetString, SetFloat, SetInt
        If needed,  SetInt can also save boolean.

        To save:
        
        PlayerPrefs.SetInt("something", yes ? 1 : 0);

        To load:

        PlayerPrefs.GetInt("something") == 1 ? true : false;

         */

    }

    public void clickResetButton()
    {
        PlayerPrefs.DeleteKey("ID");
        PlayerPrefs.DeleteKey("SpeedLimit");
        DisplayText.text = "";
        textBoxID.text = "ID";
        textBoxSpeedLimit.text = "Speed Limit";

    }

}
