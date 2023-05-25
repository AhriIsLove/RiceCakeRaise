using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteNumber
{
    public class InfiniteNumberScript : MonoBehaviour
    {
        //�ν��Ͻ�
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
        /// ���ڿ� -> ����
        /// </summary>
        /// <param name="sNumber"></param>
        /// <returns></returns>
        public static List<int> StringToInfiniteNumber(string sNumber)
        {
            //���Ͽ� ���� ����Ʈ ����
            List<int> iInfiniteNumber = new List<int>();

            //���ڿ� ����
            int length = sNumber.Length;

            //���� ��
            int unitCount = (length - 1) / 3;

            //���� �� ��ŭ ���ڿ� �ڸ���
            for (int i = 1; i <= unitCount + 1; i++)
            {
                string getStringNumber = "0";
                int getIntNumeber = 0;
                //������ ���ڿ� ���̰� 3�̻����� Ȯ��
                if (length >= 3)
                {
                    //�����ʺ��� 3���� �ڸ���
                    getStringNumber = sNumber.Substring(sNumber.Length - (i * 3), 3);

                    //������ ���ڿ� ���� 3 ����
                    length -= 3;
                }
                else
                {
                    //���ʺ��� ������ ���ڿ� ������ŭ �ڸ���
                    getStringNumber = sNumber.Substring(0, length);
                }

                //String -> Int ��ȯ
                getIntNumeber = int.Parse(getStringNumber);

                //����� ���ڸ� ���
                iInfiniteNumber.Add(getIntNumeber);
            }

            //��ȯ
            return iInfiniteNumber;
        }

        /// <summary>
        /// ���� -> ���ڿ�
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string InfiniteNumberToString(List<int> number)
        {
            //��ȯ ���ڿ� �ʱ�ȭ
            string strNumber = "";

            //���ڿ��� ��ȯ
            for (int i = number.Count - 1; i >= 0; i--)
            {
                //�� �� ������ ���
                if (i == number.Count - 1)
                {
                    strNumber += number[i].ToString();
                }
                else
                {
                    //�ڸ����� �ȸ��� ��� 0���� �ڸ��� ä���
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

            //��ȯ
            return strNumber;
        }

        /// <summary>
        /// ���� ���ÿ����� ��ȯ
        /// </summary>
        /// <param name="sNumber"></param>
        /// <returns></returns>
        public static string ShowInfiniteNumber(string sNumber)
        {
            //���Ͽ� ���� ����Ʈ ����
            List<int> iInfiniteNumber = new List<int>();

            //���������� ��ȯ
            iInfiniteNumber = StringToInfiniteNumber(sNumber);

            //��ȯ�� �������� ���� 
            int unitCount = iInfiniteNumber.Count;

            //���ÿ� ����
            string ShowNumber = "";
            if (unitCount == 1)//������ ���� ���
            {
                ShowNumber = iInfiniteNumber[unitCount - 1].ToString();
            }
            else//������ �ִ°��(�Ҽ��� �߰� ����)
            {
                ShowNumber = iInfiniteNumber[unitCount - 1].ToString() + "." + String.Format("{0:000}", iInfiniteNumber[unitCount - 2]) + GetUnit(unitCount);
            }

            //��ȯ
            return ShowNumber;
        }

        /// <summary>
        /// ���� ���
        /// </summary>
        /// <param name="unitCount"></param>
        /// <returns></returns>
        public static string GetUnit(int unitCount)
        {
            //���� �ʱ�ȭ
            string unit = "";

            //���� �� -> ���� ���ĺ�
            switch (unitCount)
            {
                case 2: unit = "A"; break;//1,000
                case 3: unit = "B"; break;//1,000,000
                case 4: unit = "C"; break;//1,000,000,000
                case 5: unit = "D"; break;
                default: unit = ""; break;//0~999
            }

            //��ȯ
            return unit;
        }

        /// <summary>
        /// ���+
        /// </summary>
        /// <param name="numberA"></param>
        /// <param name="numberB"></param>
        /// <returns></returns>
        public static string PlusNumbers(string numberA, string numberB)
        {
            //���Ͽ� ���� ����Ʈ ����
            List<int> iInfiniteNumber = new List<int>();
            //���� ���� ����Ʈ ����
            List<int> iInfiniteNumberA = new List<int>();
            List<int> iInfiniteNumberB = new List<int>();

            //A�� �� ū ���
            if (CompareNumbers(numberA, numberB))
            {
                //����A ��ȯ
                iInfiniteNumberA = StringToInfiniteNumber(numberA);

                //����B ��ȯ
                iInfiniteNumberB = StringToInfiniteNumber(numberB);
            }
            else//B�� �� ū ���
            {
                //A�� B�� ��ü�Ͽ� ���

                //����A ��ȯ
                iInfiniteNumberA = StringToInfiniteNumber(numberB);

                //����B ��ȯ
                iInfiniteNumberB = StringToInfiniteNumber(numberA);
            }

            //����A�� ������
            int sizeA = iInfiniteNumberA.Count;
            //����B�� ������
            int sizeB = iInfiniteNumberB.Count;

            //������(B)�� size��ŭ �ݺ��Ͽ� ���ϱ�
            for (int i = 0; i < sizeB; i++)
            {
                //���� ��󿡰� ���Ѱ��� ����
                int number = iInfiniteNumberA[i] + iInfiniteNumberB[i];

                iInfiniteNumber.Add(number);

                //������(B)�� �������̿��ٸ� A�� ���� ���� �״�� �ο�
                if (i == sizeB - 1)
                {
                    for (int j = i + 1; j < sizeA; j++)
                    {
                        iInfiniteNumber.Add(iInfiniteNumberA[j]);
                    }
                }
            }

            //��ȯ
            return InfiniteNumberToString(SettingUnitNumber(iInfiniteNumber));
        }

        /// <summary>
        /// ���-
        /// </summary>
        /// <param name="numberA"></param>
        /// <param name="numberB"></param>
        /// <returns></returns>
        public static string MinusNumbers(string numberA, string numberB)
        {
            //���Ͽ� ���� ����Ʈ ����
            List<int> iInfiniteNumber = new List<int>();
            //���� ���� ����Ʈ ����
            List<int> iInfiniteNumberA = new List<int>();
            List<int> iInfiniteNumberB = new List<int>();

            //A�� �� ū ���
            if (CompareNumbers(numberA, numberB))
            {
                //����A ��ȯ
                iInfiniteNumberA = StringToInfiniteNumber(numberA);

                //����B ��ȯ
                iInfiniteNumberB = StringToInfiniteNumber(numberB);
            }
            else//B�� �� ū ���
            {
                //A�� B�� ��ü�Ͽ� ���

                //����A ��ȯ
                iInfiniteNumberA = StringToInfiniteNumber(numberB);

                //����B ��ȯ
                iInfiniteNumberB = StringToInfiniteNumber(numberA);
            }

            //����A�� ������
            int sizeA = iInfiniteNumberA.Count;
            //����B�� ������
            int sizeB = iInfiniteNumberB.Count;

            //������(B)�� size��ŭ �ݺ��Ͽ� ����
            for (int i = 0; i < sizeB; i++)
            {
                //���� ��󿡰� ���Ѱ��� ����
                int number = iInfiniteNumberA[i] - iInfiniteNumberB[i];

                iInfiniteNumber.Add(number);

                //������(B)�� �������̿��ٸ� A�� ���� ���� �״�� �ο�
                if (i == sizeB - 1)
                {
                    for (int j = i + 1; j < sizeA; j++)
                    {
                        iInfiniteNumber.Add(iInfiniteNumberA[j]);
                    }
                }
            }

            //��ȯ
            return InfiniteNumberToString(SettingUnitNumber(iInfiniteNumber));
        }

        /// <summary>
        /// ���*
        /// </summary>
        /// <param name="numberA"></param>
        /// <param name="multiValue"></param>
        /// <returns></returns>
        public static string MultipleNumbers(string numberA, float multiValue)
        {
            if (multiValue <= 1)//������ �Ұ���
            {
                return numberA;
            }

            //���Ͽ� ���� ����Ʈ ����
            List<int> iInfiniteNumber = new List<int>();
            //���� ���� ����Ʈ ����
            List<int> iInfiniteNumberA = new List<int>();

            //����A ��ȯ
            iInfiniteNumberA = StringToInfiniteNumber(numberA);

            //����A�� ������
            int sizeA = iInfiniteNumberA.Count;

            //������ size��ŭ �ݺ��Ͽ� ���ϱ�
            for (int i = 0; i < sizeA; i++)
            {
                //���� ��󿡰� ���Ѱ��� ����
                int number = (int)(iInfiniteNumberA[i] * multiValue);

                iInfiniteNumber.Add(number);
            }

            //��ȯ
            return InfiniteNumberToString(SettingUnitNumber(iInfiniteNumber));
        }


        /// <summary>
        /// ���� ����(3�ڸ��� �Ѿ�� ���� ����)
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static List<int> SettingUnitNumber(List<int> number)
        {
            //���� ���� ���� ���� ã��
            for (int i = 0; i < number.Count; i++)
            {
                //1000�� �Ѿ�� ���ڸ� ã�����
                if (number[i] >= 1000)
                {
                    int upperNumber = number[i] / 1000;
                    number[i] = number[i] % 1000;

                    //������ ���� ������ �ϴ� ���
                    if (number.Count == i + 1)
                    {
                        number.Add(upperNumber);
                    }
                    else
                    {
                        number[i + 1] += upperNumber;
                    }
                }

                //-�� �� ���ڸ� ã�����
                if (number[i] < 0)
                {
                    //�մ����� 1�� ��������
                    number[i] += 1000;
                    number[i + 1]--;
                }
            }

            //0�� �մ��� ó��
            for (int i = number.Count - 1; i >= 0; i--)
            {
                //�� ���ڸ��� 0�� ���
                if (number[i] == 0)
                {
                    number.RemoveAt(i);
                }
                else
                {
                    break;
                }
            }

            //���°� ���ٸ�
            if (number.Count == 0)
            {
                //0���� �ʱ�ȭ
                number.Add(0);
            }

            //��ȯ
            return number;
        }

        /// <summary>
        /// numberA�� numberB�� ���Ͽ� 
        /// A�� �� ũ(�ų�����)�� true 
        /// B�� �� ũ�� false
        /// </summary>
        /// <param name="numberA"></param>
        /// <param name="numberB"></param>
        /// <returns></returns>
        public static bool CompareNumbers(string numberA, string numberB)
        {
            //���� ���� ����Ʈ ����
            List<int> iInfiniteNumberA = new List<int>();
            List<int> iInfiniteNumberB = new List<int>();

            //����A ��ȯ
            iInfiniteNumberA = StringToInfiniteNumber(numberA);

            //����B ��ȯ
            iInfiniteNumberB = StringToInfiniteNumber(numberB);//���� ���

            //����1�� ������
            int sizeA = iInfiniteNumberA.Count;
            //����2�� ������
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
                //���ڸ� ������ ��
                for (int i = sizeA - 1; i >= 0; i--)
                {
                    //A�� �� ū���
                    if (iInfiniteNumberA[i] - iInfiniteNumberB[i] > 0)
                    {
                        return true;
                    }
                    else if (iInfiniteNumberA[i] - iInfiniteNumberB[i] < 0)
                    {
                        return false;
                    }
                }

                //������� ������ �� ���� �����ϴ�
                //true�� false�� ��� ����
                //true�� ������
                return true;
            }
        }
    }
}
