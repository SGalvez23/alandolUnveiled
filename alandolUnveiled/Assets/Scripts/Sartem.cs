using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Sartem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init();
    }

    // Update is called once per frame
    void Update()
    {
        transform.DORotate(new Vector3(360, 0, 0), 10, RotateMode.FastBeyond360);
    }
}
