using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SerachRoadButton : MonoBehaviour
{
    [SerializeField] GameObject SerachBOX;
    Animator animator;
    bool isSeraching=false;


    private void Awake()
    {
        animator = SerachBOX.GetComponent<Animator>();
    }

    public void OnclickButton()
    {
        if (isSeraching)
        {

            animator.Play("UnSearchBOX");
            isSeraching = false;
        }

        else
        {
            animator.Play("SerachBOX");
            isSeraching = true;

        }

    }

}
