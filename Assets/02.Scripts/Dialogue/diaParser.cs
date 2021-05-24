using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diaParser : MonoBehaviour
{
    public dialist[] Parse(string csvfile)
    {
        List<dialist> list = new List<dialist>(); //책목록받을 리스트
        TextAsset csvData = Resources.Load<TextAsset>(csvfile); //csv데이터 읽어올 클래스

        string[] data = csvData.text.Split(new char[] { '\n' }); //읽어올 문자열 배열 = 읽어온 csv데이터를 띄워쓰기 기준으로 저장

        for (int i = 0; i < data.Length;) //띄워쓰기기준으로 저장한 data의 길이 = 총 행수
        {
            string[] row = data[i].Split(new char[] { ',' }); // 컴마기준으로 나뉘는 csv파일 다시 언패킹

            dialist dia = new dialist(); //대화리스트 객체 생성

            dia.index = row[0]; //대화인덱스
            dia.talker = row[1]; //화자
            dia.dial = row[2]; //대화내용


            list.Add(dia); //대화 리스트에 추가함

            if (++i < data.Length)
            {
                ;
            }
        }
        return list.ToArray(); //모든 대화리스트를 배열로 캐스트해 반환
    }
}