using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RiceCakeScript : MonoBehaviour
{
    //���� ȿ�� ������
    public GameObject g_RaiseCountEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //���� ȿ�� �߻�
    public void RaiseCountEffect(string value)
    {
        //���� ȿ�� ����
        GameObject raiseCountEffect = Instantiate(g_RaiseCountEffect, gameObject.transform);

        //������ ǥ��
        raiseCountEffect.GetComponent<Text>().text = "+" + value;

        ///���� ȿ�� ��ġ ���� ����
        raiseCountEffect.transform.position = raiseCountEffect.transform.position + new Vector3(Random.Range(-50.0f, 50.0f), Random.Range(-50.0f, 50.0f), 0);
    }

    public void RaiseCountChange(int value)
    {

    }
}
