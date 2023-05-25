using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteNumber
{
    public class InfiniteNumberScript : MonoBehaviour
    {
        //인스턴스
        public static InfiniteNumberScript instance;

        private void Awake()
        {
            //instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// 문자열 -> 숫자
        /// </summary>
        /// <param name="sNumber"></param>
        /// <returns></returns>
        public static List<int> StringToInfiniteNumber(string sNumber)
        {
            //리턴용 숫자 리스트 선언
            List<int> iInfiniteNumber = new List<int>();

            //문자열 길이
            int length = sNumber.Length;

            //단위 수
            int unitCount = (length - 1) / 3;

            //단위 수 만큼 문자열 자르기
            for (int i = 1; i <= unitCount + 1; i++)
            {
                string getStringNumber = "0";
                int getIntNumeber = 0;
                //남겨진 문자열 길이가 3이상인지 확인
                if (length >= 3)
                {
                    //오른쪽부터 3개씩 자르기
                    getStringNumber = sNumber.Substring(sNumber.Length - (i * 3), 3);

                    //남겨진 문자열 길이 3 감소
                    length -= 3;
                }
                else
                {
                    //왼쪽부터 남겨진 문자열 개수만큼 자르기
                    getStringNumber = sNumber.Substring(0, length);
                }

                //String -> Int 변환
                getIntNumeber = int.Parse(getStringNumber);

                //추출된 숫자를 담기
                iInfiniteNumber.Add(getIntNumeber);
            }

            //반환
            return iInfiniteNumber;
        }

        /// <summary>
        /// 숫자 -> 문자열
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string InfiniteNumberToString(List<int> number)
        {
            //반환 문자열 초기화
            string strNumber = "";

            //문자열로 변환
            for (int i = number.Count - 1; i >= 0; i--)
            {
                //맨 앞 단위일 경우
                if (i == number.Count - 1)
                {
                    strNumber += number[i].ToString();
                }
                else
                {
                    //자리수가 안맞을 경우 0으로 자리수 채우기
                    if (number[i] < 100)
                    {
                        strNumber += "0";
                        if (number[i] < 10)
                        {
                            strNumber += "0";
                        }
                    }
                    strNumber += number[i].ToString();
                }
            }

            //Debug.Log(strNumber);

            //반환
            return strNumber;
        }

        /// <summary>
        /// 숫자 전시용으로 변환
        /// </summary>
        /// <param name="sNumber"></param>
        /// <returns></returns>
        public static string ShowInfiniteNumber(string sNumber)
        {
            //리턴용 숫자 리스트 선언
            List<int> iInfiniteNumber = new List<int>();

            //숫자형으로 변환
            iInfiniteNumber = StringToInfiniteNumber(sNumber);

            //변환된 숫자형의 길이 
            int unitCount = iInfiniteNumber.Count;

            //전시용 숫자
            string ShowNumber = "";
            if (unitCount == 1)//단위가 없는 경우
            {
                ShowNumber = iInfiniteNumber[unitCount - 1].ToString();
            }
            else//단위가 있는경우(소수점 추가 전시)
            {
                ShowNumber = iInfiniteNumber[unitCount - 1].ToString() + "." + String.Format("{0:000}", iInfiniteNumber[unitCount - 2]) + GetUnit(unitCount);
            }

            //반환
            return ShowNumber;
        }

        /// <summary>
        /// 단위 얻기
        /// </summary>
        /// <param name="unitCount"></param>
        /// <returns></returns>
        public static string GetUnit(int unitCount)
        {
            //단위 초기화
            string unit = "";

            //단위 수 -> 단위 알파벳
            switch (unitCount)
            {
                case 2: unit = "A"; break;//1,000
                case 3: unit = "B"; break;//1,000,000
                case 4: unit = "C"; break;//1,000,000,000
                case 5: unit = "D"; break;
                default: unit = ""; break;//0~999
            }

            //반환
            return unit;
        }

        /// <summary>
        /// 계산+
        /// </summary>
        /// <param name="numberA"></param>
        /// <param name="numberB"></param>
        /// <returns></returns>
        public static string PlusNumbers(string numberA, string numberB)
        {
            //리턴용 숫자 리스트 선언
            List<int> iInfiniteNumber = new List<int>();
            //계산용 숫자 리스트 선언
            List<int> iInfiniteNumberA = new List<int>();
            List<int> iInfiniteNumberB = new List<int>();

            //A가 더 큰 경우
            if (CompareNumbers(numberA, numberB))
            {
                //숫자A 변환
                iInfiniteNumberA = StringToInfiniteNumber(numberA);

                //숫자B 변환
                iInfiniteNumberB = StringToInfiniteNumber(numberB);
            }
            else//B가 더 큰 경우
            {
                //A와 B를 교체하여 계산

                //숫자A 변환
                iInfiniteNumberA = StringToInfiniteNumber(numberB);

                //숫자B 변환
                iInfiniteNumberB = StringToInfiniteNumber(numberA);
            }

            //숫자A의 사이즈
            int sizeA = iInfiniteNumberA.Count;
            //숫자B의 사이즈
            int sizeB = iInfiniteNumberB.Count;

            //작은수(B)의 size만큼 반복하여 더하기
            for (int i = 0; i < sizeB; i++)
            {
                //리턴 대상에게 더한값을 삽입
                int number = iInfiniteNumberA[i] + iInfiniteNumberB[i];

                iInfiniteNumber.Add(number);

                //작은수(B)의 마지막이였다면 A의 남은 숫자 그대로 부여
                if (i == sizeB - 1)
                {
                    for (int j = i + 1; j < sizeA; j++)
                    {
                        iInfiniteNumber.Add(iInfiniteNumberA[j]);
                    }
                }
            }

            //반환
            return InfiniteNumberToString(SettingUnitNumber(iInfiniteNumber));
        }

        /// <summary>
        /// 계산-
        /// </summary>
        /// <param name="numberA"></param>
        /// <param name="numberB"></param>
        /// <returns></returns>
        public static string MinusNumbers(string numberA, string numberB)
        {
            //리턴용 숫자 리스트 선언
            List<int> iInfiniteNumber = new List<int>();
            //계산용 숫자 리스트 선언
            List<int> iInfiniteNumberA = new List<int>();
            List<int> iInfiniteNumberB = new List<int>();

            //A가 더 큰 경우
            if (CompareNumbers(numberA, numberB))
            {
                //숫자A 변환
                iInfiniteNumberA = StringToInfiniteNumber(numberA);

                //숫자B 변환
                iInfiniteNumberB = StringToInfiniteNumber(numberB);
            }
            else//B가 더 큰 경우
            {
                //A와 B를 교체하여 계산

                //숫자A 변환
                iInfiniteNumberA = StringToInfiniteNumber(numberB);

                //숫자B 변환
                iInfiniteNumberB = StringToInfiniteNumber(numberA);
            }

            //숫자A의 사이즈
            int sizeA = iInfiniteNumberA.Count;
            //숫자B의 사이즈
            int sizeB = iInfiniteNumberB.Count;

            //작은수(B)의 size만큼 반복하여 빼기
            for (int i = 0; i < sizeB; i++)
            {
                //리턴 대상에게 더한값을 삽입
                int number = iInfiniteNumberA[i] - iInfiniteNumberB[i];

                iInfiniteNumber.Add(number);

                //작은수(B)의 마지막이였다면 A의 남은 숫자 그대로 부여
                if (i == sizeB - 1)
                {
                    for (int j = i + 1; j < sizeA; j++)
                    {
                        iInfiniteNumber.Add(iInfiniteNumberA[j]);
                    }
                }
            }

            //반환
            return InfiniteNumberToString(SettingUnitNumber(iInfiniteNumber));
        }

        /// <summary>
        /// 계산*
        /// </summary>
        /// <param name="numberA"></param>
        /// <param name="multiValue"></param>
        /// <returns></returns>
        public static string MultipleNumbers(string numberA, float multiValue)
        {
            if (multiValue <= 1)//나누기 불가능
            {
                return numberA;
            }

            //리턴용 숫자 리스트 선언
            List<int> iInfiniteNumber = new List<int>();
            //계산용 숫자 리스트 선언
            List<int> iInfiniteNumberA = new List<int>();

            //숫자A 변환
            iInfiniteNumberA = StringToInfiniteNumber(numberA);

            //숫자A의 사이즈
            int sizeA = iInfiniteNumberA.Count;

            //숫자의 size만큼 반복하여 더하기
            for (int i = 0; i < sizeA; i++)
            {
                //리턴 대상에게 더한값을 삽입
                int number = (int)(iInfiniteNumberA[i] * multiValue);

                iInfiniteNumber.Add(number);
            }

            //반환
            return InfiniteNumberToString(SettingUnitNumber(iInfiniteNumber));
        }


        /// <summary>
        /// 단위 조절(3자리를 넘어가는 단위 조절)
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static List<int> SettingUnitNumber(List<int> number)
        {
            //낮은 단위 부터 에러 찾기
            for (int i = 0; i < number.Count; i++)
            {
                //1000을 넘어가는 숫자를 찾은경우
                if (number[i] >= 1000)
                {
                    int upperNumber = number[i] / 1000;
                    number[i] = number[i] % 1000;

                    //단위를 새로 만들어야 하는 경우
                    if (number.Count == i + 1)
                    {
                        number.Add(upperNumber);
                    }
                    else
                    {
                        number[i + 1] += upperNumber;
                    }
                }

                //-가 된 숫자를 찾은경우
                if (number[i] < 0)
                {
                    //앞단위의 1을 가져오기
                    number[i] += 1000;
                    number[i + 1]--;
                }
            }

            //0인 앞단위 처리
            for (int i = number.Count - 1; i >= 0; i--)
            {
                //맨 앞자리가 0인 경우
                if (number[i] == 0)
                {
                    number.RemoveAt(i);
                }
                else
                {
                    break;
                }
            }

            //남는게 없다면
            if (number.Count == 0)
            {
                //0으로 초기화
                number.Add(0);
            }

            //반환
            return number;
        }

        /// <summary>
        /// numberA와 numberB를 비교하여 
        /// A가 더 크(거나같다)면 true 
        /// B가 더 크면 false
        /// </summary>
        /// <param name="numberA"></param>
        /// <param name="numberB"></param>
        /// <returns></returns>
        public static bool CompareNumbers(string numberA, string numberB)
        {
            //계산용 숫자 리스트 선언
            List<int> iInfiniteNumberA = new List<int>();
            List<int> iInfiniteNumberB = new List<int>();

            //숫자A 변환
            iInfiniteNumberA = StringToInfiniteNumber(numberA);

            //숫자B 변환
            iInfiniteNumberB = StringToInfiniteNumber(numberB);//리턴 대상

            //숫자1의 사이즈
            int sizeA = iInfiniteNumberA.Count;
            //숫자2의 사이즈
            int sizeB = iInfiniteNumberB.Count;

            if (sizeA > sizeB)
            {
                return true;
            }
            else if (sizeA < sizeB)
            {
                return false;
            }
            else//if (sizeA == sizeB)
            {
                //앞자리 수부터 비교
                for (int i = sizeA - 1; i >= 0; i--)
                {
                    //A가 더 큰경우
                    if (iInfiniteNumberA[i] - iInfiniteNumberB[i] > 0)
                    {
                        return true;
                    }
                    else if (iInfiniteNumberA[i] - iInfiniteNumberB[i] < 0)
                    {
                        return false;
                    }
                }

                //여기까지 왔으면 두 수는 동일하다
                //true든 false든 상관 없다
                //true로 해주자
                return true;
            }
        }
    }
}
