using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RiceCakeScript : MonoBehaviour
{
    //증가 효과 프리팹
    public GameObject g_RaiseCountEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //증가 효과 발생
    public void RaiseCountEffect(string value)
    {
        //증가 효과 생성
        GameObject raiseCountEffect = Instantiate(g_RaiseCountEffect, gameObject.transform);

        //증가값 표시
        raiseCountEffect.GetComponent<Text>().text = "+" + value;

        ///증가 효과 위치 랜덤 변경
        raiseCountEffect.transform.position = raiseCountEffect.transform.position + new Vector3(Random.Range(-50.0f, 50.0f), Random.Range(-50.0f, 50.0f), 0);
    }

    public void RaiseCountChange(int value)
    {

    }
}
