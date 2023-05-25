using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InfiniteNumber;
using System.IO;
using System.Data;
using System.Threading;
using UnityEngine.UIElements;

public class GameManagerScript : MonoBehaviour
{
    //�� ����
    string riceCakeCount;
    //�� ���� UI
    public GameObject g_RiceCakeCount;

    //�� ������Ʈ
    public GameObject g_RiceCake;

    //������ ������ �����
    string[] readDatas;

    ///�� ���׷��̵� ��Ȳ ��ųʸ�
    Dictionary<int, int> dic_UpgradeLevel = new Dictionary<int, int>();
    ///�� ���׷��̵� ��� ��ųʸ�
    Dictionary<int, string> dic_UpgradeCost = new Dictionary<int, string>();
    ///�� ���׷��̵� ��� ���� ���� ��ųʸ�
    Dictionary<float, string> dic_UpgradeCostRate = new Dictionary<float, string>();
    ///�� ���׷��̵� ���귮 ��ųʸ�
    Dictionary<int, string> dic_UpgradeRaise = new Dictionary<int, string>();
    ///�� ���׷��̵� ���귮 ���� ��ųʸ�
    Dictionary<int, string> dic_UpgradeRaiseRate = new Dictionary<int, string>();
    //�� ���׷��̵� Max�ܰ�
    int MaxUpgrade = 3;

    //�� ���׷��̵� UI
    public GameObject g_UpgradeUI;

    //���� ���
    public string m_filePath;

    // Start is called before the first frame update
    void Start()
    {
        //���� �ʱ�ȭ
        SettingInit();

        //�� �ʱ�ȭ
        RiceCakeInit();

        //�� ���׷��̵� �ʱ�ȭ
        UpgradeInit();

        //�� ���� ����
        CreateRiceCake();

        //������ �ҷ�����
        LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        //�� ����
        UpdateRiceCakeCount();
    }

    /// <summary>
    /// ���� �ʱ�ȭ
    /// </summary>
    void SettingInit()
    {
        //��� �ʱ�ȭ
        m_filePath = "Build\\Datas";

        //��ư �̺�Ʈ�� �Ű����� �Է��� ���� �̺�Ʈ ����(for�� ���� �ȵ�)
        g_UpgradeUI.transform.GetChild(0).GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate { UpgradeCreateRiceCake(1); });
        g_UpgradeUI.transform.GetChild(1).GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate { UpgradeCreateRiceCake(2); });
        g_UpgradeUI.transform.GetChild(2).GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate { UpgradeCreateRiceCake(3); });
    }

    void RiceCakeInit()
    {
        //�� �ʱ�ȭ
        riceCakeCount = 0.ToString();
    }

    //�� ���׷��̵� �ʱ�ȭ
    void UpgradeInit()
    {
        //�� ���׷��̵� �ʱ�ȭ
        dic_UpgradeLevel.Clear();
        dic_UpgradeCost.Clear();
        dic_UpgradeRaise.Clear();
        dic_UpgradeCostRate.Clear();
        dic_UpgradeRaiseRate.Clear();

        //�� ���׷��̵� ������ ��������
        string filePath = m_filePath + "\\UpgradeDatas.txt";
        FileInfo file = new FileInfo(filePath);
        //���� ���� Ȯ��
        if(file.Exists)
        {
            StreamReader reader = new StreamReader(filePath);
            string read = reader.ReadToEnd();
            reader.Close();
            readDatas = read.Split("\r\n");
            Debug.Log(readDatas);
        }
        else
        {
            Debug.Log(filePath + "������ ã�� �� ����");
        }

        ///������ �����ͷ� �ʱ�ȭ
        //�ִ� ���׷��̵�
        MaxUpgrade = readDatas.Length - 1;
        //���׷��̵� ���� ����
        for (int i=1; i<= MaxUpgrade; i++)
        {
            string[] upgradeDatas = readDatas[i].Split(',');

            //�� ���׷��̵� ���� �ʱ�ȭ
            dic_UpgradeLevel.Add(i, 0);
            //�� ���׷��̵� ��� �ʱ�ȭ
            dic_UpgradeCost.Add(i, upgradeDatas[DATAID.COST]);
            //�� ���׷��̵� ������ �ʱ�ȭ
            dic_UpgradeRaise.Add(i, upgradeDatas[DATAID.RAISE]);
            //�� ���׷��̵� ��� ���� ���� �ʱ�ȭ
            dic_UpgradeCostRate.Add(i, upgradeDatas[DATAID.COST_RATE]);
            //�� ���׷��̵� ������ ���� �ʱ�ȭ
            dic_UpgradeRaiseRate.Add(i, upgradeDatas[DATAID.RAISE_RATE]);
        }

        //���׷��̵�UI ����
        UpdateUpgradeUI();
    }

    //�� ��ġ
    public void TouchRiceCake()
    {
        //�� ����(��:level1)
        riceCakeCount = InfiniteNumberScript.PlusNumbers(riceCakeCount, dic_UpgradeRaise[1]);
        //i_RiceCakeCount += dic_UpgradeRaise[1];

        //�� ���� ����Ʈ
        g_RiceCake.GetComponent<RiceCakeScript>().RaiseCountEffect(dic_UpgradeRaise[1]);
    }

    //�� ����
    public void UpdateRiceCakeCount()
    {
        //�� ���� UI ����
        g_RiceCakeCount.GetComponent<Text>().text = "�� : " + InfiniteNumberScript.ShowInfiniteNumber(riceCakeCount);
    }

    //�� ���� ���׷��̵�
    public void UpgradeCreateRiceCake(int level/*�ܰ�*/, bool pay = true)
    {
        //���� ��� üũ
        if (pay)
        {
            //���� ���� üũ
            if (!InfiniteNumberScript.CompareNumbers(riceCakeCount, dic_UpgradeCost[level]))
            {
                return;
            }

            //�������
            riceCakeCount = InfiniteNumberScript.MinusNumbers(riceCakeCount, dic_UpgradeCost[level]);
        }

        //���׷��̵� ���� ����
        dic_UpgradeLevel[level] = dic_UpgradeLevel[level] + 1;
        //���׷��̵� ��� ����
        dic_UpgradeCost[level] = InfiniteNumberScript.MultipleNumbers(dic_UpgradeCost[level], float.Parse(dic_UpgradeCostRate[level]));
        //���귮 ����
        dic_UpgradeRaise[level] = InfiniteNumberScript.PlusNumbers(dic_UpgradeRaise[level], dic_UpgradeRaiseRate[level]);

        //�� ����
        UpdateUpgradeUI();
    }

    //���׷��̵�UI ����
    void UpdateUpgradeUI()
    {
        for (int i = 1; i <= MaxUpgrade; i++)
        {
            //����
            g_UpgradeUI.transform.GetChild(i - 1).GetChild(2).GetComponent<Text>().text = dic_UpgradeLevel[i].ToString();
            //���
            g_UpgradeUI.transform.GetChild(i - 1).GetChild(3).GetComponent<Text>().text = dic_UpgradeCost[i].ToString();
        }
    }

    //�� ���� �Լ�
    void CreateRiceCake()
    {
        //�ʴ� ���귮
        for(int i=2; i<=MaxUpgrade; i++)
        {
            //2:�䳢
            //3:����
            riceCakeCount = InfiniteNumberScript.PlusNumbers(riceCakeCount, dic_UpgradeRaise[i]);
        }

        //����Լ�(1�� ����Ŭ)
        Invoke("CreateRiceCake", 1.0f);
    }

    public void testFunction()
    {
        //������ ����
        //riceCakeCount = InfiniteNumberScript.PlusNumbers(riceCakeCount, 500.ToString());

        //���� ���� ������ ����
        //�� �ʱ�ȭ
        RiceCakeInit();
        //�� ���׷��̵� �ʱ�ȭ
        UpgradeInit();

        //���̺� �׽�Ʈ
        string filePath = m_filePath + "\\SaveFile.txt";
        FileInfo file = new FileInfo(filePath);
        //���� ���� Ȯ��
        if (file.Exists)
        {
            StreamReader reader = new StreamReader(filePath);
            string read = reader.ReadToEnd();
            reader.Close();
            readDatas = read.Split("\r\n");
            Debug.Log(readDatas);
        }
        else
        {
            Debug.Log(filePath + "������ ã�� �� ����");

            return;
        }

        string[] upgradeDatas = readDatas[0].Split(',');

        //���� �� �� �ҷ�����
        riceCakeCount = upgradeDatas[DATAID.MONEY];

        for (int i = 1; i < upgradeDatas.Length; i++)
        {
            //���� ������ ���׷��̵� ���� �ҷ�����
            int.TryParse(upgradeDatas[i], out int saveCount);

            //����� �������� ���׷��̵��ϱ�
            for(int j=0; j< saveCount; j++)
            {
                UpgradeCreateRiceCake(i, false);//�ҷ����⶧���� ����� �������� �ʴ´�.
            }
        }

        //�� ����
        UpdateUpgradeUI();
    }

    void LoadData()
    {

    }

    /// <summary>
    /// ������ �ĺ�ID
    /// </summary>
    static public class DATAID
    {
        //�� ���׷��̵� ����
        public const int COST = 0;
        public const int RAISE = 1;
        public const int COST_RATE = 2;
        public const int RAISE_RATE = 3;

        //����� ����
        public const int MONEY = 0;
        public const int UPGRADE1 = 1;
        public const int UPGRADE2 = 2;

    }

}
