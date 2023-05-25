using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaiseCountEffectScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //1초후 파괴 예약
        Destroy(gameObject, 1);
    }

    // Update is called once per frame
    void Update()
    {
        //UI 상승
        transform.position = transform.position + new Vector3(0, 0.05f, 0);

        ///UI 점차 투명하게
        //텍스트
        Color textColor = gameObject.GetComponent<Text>().color;//색상 추출
        textColor.a = textColor.a - 0.005f;//투명화
        gameObject.GetComponent<Text>().color = textColor;//적용
        //이미지
        Color imageColor = gameObject.transform.GetChild(0).GetComponent<Image>().color;//색상 추출
        imageColor.a = imageColor.a - 0.005f;//투명화
        gameObject.transform.GetChild(0).GetComponent<Image>().color = imageColor;//적용
    }
}
