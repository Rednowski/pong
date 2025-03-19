using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Wynik : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Animator animator;

    public void Highlight()
    {
        animator.SetTrigger("highlight");
    }
    public void setWynik(int value)
    {
        if(value < -1)
        {
            text.text = "";
        } else
        {
            text.text = value.ToString();
        }
            
    }
}
