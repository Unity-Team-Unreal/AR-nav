using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AniController : MonoBehaviour
{
    [SerializeField] Animator arDocent;
    [SerializeField] Slider arDocentTimeLine;

    void Start()
    {
        
    }

    void Update()
    {
        arDocent.Play("", -1, arDocentTimeLine.normalizedValue); // ""사이에 AR 도슨트의 애니메이션을 넣는다.
    }
}
