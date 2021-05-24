using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parser : MonoBehaviour
{
    public book[] Parse(string csvfile)
    {
        List<book> list = new List<book>(); //책목록받을 리스트
        TextAsset csvData = Resources.Load<TextAsset>(csvfile); //csv데이터 읽어올 클래스
        string[] data = csvData.text.Split(new char[] { '\n' }); //읽어올 문자열 배열 = 읽어온 csv데이터를 띄워쓰기 기준으로 저장

        for (int i = 0; i < data.Length;) //띄워쓰기기준으로 저장한 data의 길이 = 총 행수
        {
            string[] row = data[i].Split(new char[] { ',' }); // 컴마기준으로 나뉘는 csv파일 다시 언패킹

            book BOOK = new book(); //책 객체 생성

            BOOK.index = row[0]; //책 번호
            BOOK.title = row[1]; //책 제목
            BOOK.writer = row[2]; //책 저자
            BOOK.publisher = row[3]; //출판사
            BOOK.year = row[4]; //출판연도 저장


            list.Add(BOOK); //책정보 저장한 객체를 book 리스트에 추가함

            if (++i < data.Length)
            {
                ;
            }
        }
        return list.ToArray(); //모든 책 저장한 book 리스트를 배열로 캐스트해 반환
    }
}
