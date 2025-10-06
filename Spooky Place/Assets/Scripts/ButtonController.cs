using UnityEngine;
using System.Collections;

public class ButtonController : MonoBehaviour
{
    public Animator doorAnim = null;

    public bool doorOpen = false;

    private string openAnimationName = "DoorOpens";
    private string openAnimationName1 = "Door1Opens";


    public void PlayAnimation()
    {
        
        if (!doorOpen)
        {
            doorAnim.Play(openAnimationName, 0, 0.0f);
            doorOpen = true;
        }
        else
        {
            if (!doorOpen)
            {
                doorOpen = false;
            }
        }

        if (!doorOpen)
        {
            doorAnim.Play(openAnimationName1, 0, 0.0f);
            doorOpen = true;
        }
        else
        {
            if (!doorOpen)
            {
                doorOpen = false;
            }
        }
    }

}



