using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag.Equals("Cylinder"))
            PushModeSet();
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("Cylinder"))
            PushModeSet();
    }
    
    private void OnCollisionExit(Collision other)
    {
        if(other.gameObject.tag.Equals("Cylinder"))
            PlayerMovement.Instance.pushState = false;
    }
    
    private void PushModeSet()
    {
        PlayerMovement.Instance.pushState = true;
        PlayerMovement.Instance.runState = false;
    }
}
