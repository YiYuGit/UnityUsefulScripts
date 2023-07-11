using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// This script can make a UI object turn on hen mouseis on top of it, 
/// and turn off when mouse moved away.
/// Don’t use OnMouseEnter for UI, instead, use OnPointerEnter.
/// 
/// This script should be attached to the UI object that controls the "popupWindowObject"
/// ,not the "popupWindowObject" itself
/// Because once the popupWindowObject itself is setActvie false, it can not detect pointer enter
/// 
/// Example: this script can be used with a clock or a menu object, when pointer hover above the control UI object, the 
/// "popupWindowObject" will appear. when mouse is away, the object disappear.
/// 
/// Note, for objects in the scene, if just need to let it disappear, try to use  rend.enabled = true/ or false; 
/// so the object itself is still there.
/// 
/// When control mutliple obejct, use Gameobject[] list.
/// </summary>


public class MouseEnablePopupWindow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Drop the Pop Up Objects here")]
    //Drop the object that you want to control with UI element here
    //public GameObject popupWindowObject;
    public GameObject[] popupWindowObject;

    private void Start()
    {
        //popupWindowObject.SetActive(false);
        
        for (int i = 0; i < popupWindowObject.Length; i++)
        {
            popupWindowObject[i].SetActive(false);
        }
        
    }

    private void Update()
    {
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
       // print("OnMouseEnter");
       //popupWindowObject.SetActive(true);

        
        for (int i = 0; i < popupWindowObject.Length; i++)
        {
            popupWindowObject[i].SetActive(true);
        }
        
    }
    public void OnPointerExit(PointerEventData eventData)
    {
       // print("OnMouseExit");
       //popupWindowObject.SetActive(false);

        
        for (int i = 0; i < popupWindowObject.Length; i++)
        {
            popupWindowObject[i].SetActive(false);
        }
        
    }


}