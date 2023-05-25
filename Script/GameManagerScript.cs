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
    //떡 갯수
    string riceCakeCount;
    //떡 갯수 UI
    public GameObject g_RiceCakeCount;

    //떡 오브젝트
    public GameObject g_RiceCake;

    //가져온 데이터 저장소
    string[] readDatas;

    ///떡 업그레이드 현황 딕셔너리
    Dictionary<int, int> dic_UpgradeLevel = new Dictionary<int, int>();
    ///떡 업그레이드 비용 딕셔너리
    Dictionary<int, string> dic_UpgradeCost = new Dictionary<int, string>();
    ///떡 업그레이드 비용 증가 비율 딕셔너리
    Dictionary<float, string> dic_UpgradeCostRate = new Dictionary<float, string>();
    ///떡 업그레이드 생산량 딕셔너리
    Dictionary<int, string> dic_UpgradeRaise = new Dictionary<int, string>();
    ///떡 업그레이드 생산량 증가 딕셔너리
    Dictionary<int, string> dic_UpgradeRaiseRate = new Dictionary<int, string>();
    //떡 업그레이드 Max단계
    int MaxUpgrade = 3;

    //떡 업그레이드 UI
    public GameObject g_UpgradeUI;

    //파일 경로
    public string m_filePath;

    // Start is called before the first frame update
    void Start()
    {
        //설정 초기화
        SettingInit();

        //떡 초기화
        RiceCakeInit();

        //떡 업그레이드 초기화
        UpgradeInit();

        //떡 생산 시작
        CreateRiceCake();

        //데이터 불러오기
        LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        //떡 갱신
        UpdateRiceCakeCount();
    }

    /// <summary>
    /// 설정 초기화
    /// </summary>
    void SettingInit()
    {
        //경로 초기화
        m_filePath = "Build\\Datas";

        //버튼 이벤트에 매개변수 입력을 위한 이벤트 연결(for문 실행 안됨)
        g_UpgradeUI.transform.GetChild(0).GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate { UpgradeCreateRiceCake(1); });
        g_UpgradeUI.transform.GetChild(1).GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate { UpgradeCreateRiceCake(2); });
        g_UpgradeUI.transform.GetChild(2).GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate { UpgradeCreateRiceCake(3); });
    }

    void RiceCakeInit()
    {
        //떡 초기화
        riceCakeCount = 0.ToString();
    }

    //떡 업그레이드 초기화
    void UpgradeInit()
    {
        //떡 업그레이드 초기화
        dic_UpgradeLevel.Clear();
        dic_UpgradeCost.Clear();
        dic_UpgradeRaise.Clear();
        dic_UpgradeCostRate.Clear();
        dic_UpgradeRaiseRate.Clear();

        //떡 업그레이드 데이터 가져오기
        string filePath = m_filePath + "\\UpgradeDatas.txt";
        FileInfo file = new FileInfo(filePath);
        //파일 존재 확인
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
            Debug.Log(filePath + "파일을 찾을 수 없음");
        }

        ///가져온 데이터로 초기화
        //최대 업그레이드
        MaxUpgrade = readDatas.Length - 1;
        //업그레이드 정보 적용
        for (int i=1; i<= MaxUpgrade; i++)
        {
            string[] upgradeDatas = readDatas[i].Split(',');

            //떡 업그레이드 레벨 초기화
            dic_UpgradeLevel.Add(i, 0);
            //떡 업그레이드 비용 초기화
            dic_UpgradeCost.Add(i, upgradeDatas[DATAID.COST]);
            //떡 업그레이드 증가량 초기화
            dic_UpgradeRaise.Add(i, upgradeDatas[DATAID.RAISE]);
            //떡 업그레이드 비용 증가 비율 초기화
            dic_UpgradeCostRate.Add(i, upgradeDatas[DATAID.COST_RATE]);
            //떡 업그레이드 증가량 비율 초기화
            dic_UpgradeRaiseRate.Add(i, upgradeDatas[DATAID.RAISE_RATE]);
        }

        //업그레이드UI 갱신
        UpdateUpgradeUI();
    }

    //떡 터치
    public void TouchRiceCake()
    {
        //떡 증가(탭:level1)
        riceCakeCount = InfiniteNumberScript.PlusNumbers(riceCakeCount, dic_UpgradeRaise[1]);
        //i_RiceCakeCount += dic_UpgradeRaise[1];

        //떡 증가 이펙트
        g_RiceCake.GetComponent<RiceCakeScript>().RaiseCountEffect(dic_UpgradeRaise[1]);
    }

    //떡 갱신
    public void UpdateRiceCakeCount()
    {
        //떡 갯수 UI 갱신
        g_RiceCakeCount.GetComponent<Text>().text = "떡 : " + InfiniteNumberScript.ShowInfiniteNumber(riceCakeCount);
    }

    //떡 생산 업그레이드
    public void UpgradeCreateRiceCake(int level/*단계*/, bool pay = true)
    {
        //지불 비용 체크
        if (pay)
        {
            //지불 가능 체크
            if (!InfiniteNumberScript.CompareNumbers(riceCakeCount, dic_UpgradeCost[level]))
            {
                return;
            }

            //비용지불
            riceCakeCount = InfiniteNumberScript.MinusNumbers(riceCakeCount, dic_UpgradeCost[level]);
        }

        //업그레이드 레벨 변경
        dic_UpgradeLevel[level] = dic_UpgradeLevel[level] + 1;
        //업그레이드 비용 증가
        dic_UpgradeCost[level] = InfiniteNumberScript.MultipleNumbers(dic_UpgradeCost[level], float.Parse(dic_UpgradeCostRate[level]));
        //생산량 증가
        dic_UpgradeRaise[level] = InfiniteNumberScript.PlusNumbers(dic_UpgradeRaise[level], dic_UpgradeRaiseRate[level]);

        //떡 갱신
        UpdateUpgradeUI();
    }

    //업그레이드UI 갱신
    void UpdateUpgradeUI()
    {
        for (int i = 1; i <= MaxUpgrade; i++)
        {
            //레벨
            g_UpgradeUI.transform.GetChild(i - 1).GetChild(2).GetComponent<Text>().text = dic_UpgradeLevel[i].ToString();
            //비용
            g_UpgradeUI.transform.GetChild(i - 1).GetChild(3).GetComponent<Text>().text = dic_UpgradeCost[i].ToString();
        }
    }

    //떡 생산 함수
    void CreateRiceCake()
    {
        //초당 생산량
        for(int i=2; i<=MaxUpgrade; i++)
        {
            //2:토끼
            //3:장인
            riceCakeCount = InfiniteNumberScript.PlusNumbers(riceCakeCount, dic_UpgradeRaise[i]);
        }

        //재귀함수(1초 사이클)
        Invoke("CreateRiceCake", 1.0f);
    }

    public void testFunction()
    {
        //돈복사 버그
        //riceCakeCount = InfiniteNumberScript.PlusNumbers(riceCakeCount, 500.ToString());

        //현재 게임 데이터 삭제
        //떡 초기화
        RiceCakeInit();
        //떡 업그레이드 초기화
        UpgradeInit();

        //세이브 테스트
        string filePath = m_filePath + "\\SaveFile.txt";
        FileInfo file = new FileInfo(filePath);
        //파일 존재 확인
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
            Debug.Log(filePath + "파일을 찾을 수 없음");

            return;
        }

        string[] upgradeDatas = readDatas[0].Split(',');

        //현재 떡 수 불러오기
        riceCakeCount = upgradeDatas[DATAID.MONEY];

        for (int i = 1; i < upgradeDatas.Length; i++)
        {
            //현재 레벨의 업그레이드 개수 불러오기
            int.TryParse(upgradeDatas[i], out int saveCount);

            //저장된 개수까지 업그레이드하기
            for(int j=0; j< saveCount; j++)
            {
                UpgradeCreateRiceCake(i, false);//불러오기때문에 비용을 지불하지 않는다.
            }
        }

        //떡 갱신
        UpdateUpgradeUI();
    }

    void LoadData()
    {

    }

    /// <summary>
    /// 데이터 식별ID
    /// </summary>
    static public class DATAID
    {
        //떡 업그레이드 정보
        public const int COST = 0;
        public const int RAISE = 1;
        public const int COST_RATE = 2;
        public const int RAISE_RATE = 3;

        //사용자 정보
        public const int MONEY = 0;
        public const int UPGRADE1 = 1;
        public const int UPGRADE2 = 2;

    }

}
