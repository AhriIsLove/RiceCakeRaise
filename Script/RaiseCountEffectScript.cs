using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaiseCountEffectScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //1���� �ı� ����
        Destroy(gameObject, 1);
    }

    // Update is called once per frame
    void Update()
    {
        //UI ���
        transform.position = transform.position + new Vector3(0, 0.05f, 0);

        ///UI ���� �����ϰ�
        //�ؽ�Ʈ
        Color textColor = gameObject.GetComponent<Text>().color;//���� ����
        textColor.a = textColor.a - 0.005f;//����ȭ
        gameObject.GetComponent<Text>().color = textColor;//����
        //�̹���
        Color imageColor = gameObject.transform.GetChild(0).GetComponent<Image>().color;//���� ����
        imageColor.a = imageColor.a - 0.005f;//����ȭ
        gameObject.transform.GetChild(0).GetComponent<Image>().color = imageColor;//����
    }
}
