using UnityEngine;
using System.Collections;

/// <summary>
/// This script is used for auto hide/unhide the UI icons on the side of the screen.
/// Attach this to a UI object(like the iconsPanel itself).
/// Put the icons under iconsPanel, then assign it to the script in the Inspector
/// Set the hideOffset and animationSpeed.
/// Set another trigger zone (using a button or canvas image), adjust its size and location, make it transparent(set image color RGBA to 0,0,0,0).
/// Add Event trigger: On Pointer Enter, call the OnPointerEnter(), and On Pointer Exit, call the OnPointerExit()
/// </summary>

public class AutoHideUI : MonoBehaviour
{
    // In this example, the icon object are < 100 pixel size, and they are vertically placed on the right side of the screen. 

    public RectTransform iconsPanel; // Assign the RectTransform of the UI icons container in the Inspector
    public float hideOffset = 100f; // The offset to move icons out of screen
    public float animationSpeed = 500f; // Speed of the slide animation

    private Vector3 hiddenPosition;
    private Vector3 visiblePosition;

    void Start()
    {
        // The hidden position in this case is on the right, so add the hideOffset to the x.
        // Change it to -hideOffset on x if it is on left, or try y or -y for vertical hide
        hiddenPosition = iconsPanel.localPosition + new Vector3(hideOffset, 0, 0);
        // the visiblePosition is the intial position, so put the icons where they should be on the canvas
        visiblePosition = iconsPanel.localPosition;
        // After getting the intial and hidden position, put the icons to the hidden location
        iconsPanel.localPosition = hiddenPosition;
    }

    // The next two methods should be called by the trigger zone
    public void OnPointerEnter()
    {
        //Debug.Log("Enter");
        if (this.isActiveAndEnabled)
        {
            StopAllCoroutines();
            StartCoroutine(SlideUI(visiblePosition));
        }
        
    }

    public void OnPointerExit()
    {
        //Debug.Log("Exit");
        if (this.isActiveAndEnabled)
        {
            StopAllCoroutines();
            StartCoroutine(SlideUI(hiddenPosition));
        }
        
    }

    private System.Collections.IEnumerator SlideUI(Vector3 targetPosition)
    {
        while (Vector3.Distance(iconsPanel.localPosition, targetPosition) > 0.01f)
        {
            iconsPanel.localPosition = Vector3.MoveTowards(iconsPanel.localPosition, targetPosition, animationSpeed * Time.deltaTime);
            yield return null;
        }
        iconsPanel.localPosition = targetPosition;
    }
}
