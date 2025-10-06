using UnityEngine;
using System.Collections;

public class ButtonController : MonoBehaviour
{
    public Animator doorAnim = null;

    public bool doorOpen = false;

    public string openAnimationName = "DoorOpens";


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

    }

}



