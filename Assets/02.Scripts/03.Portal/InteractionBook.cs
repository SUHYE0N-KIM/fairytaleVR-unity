using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
namespace Valve.VR.InteractionSystem
{
    public class InteractionBook : MonoBehaviour
    {
        [SerializeField] Text title, writer, publisher, year, index; // 책제목, 저자, 출판사, 출판연도, 책번호
        [SerializeField] Image image; //책표지
        [SerializeField] Sprite jack;
        [SerializeField] string csvFileName; // 읽어올 .csv파일이름
        [SerializeField] int bookEA; //책갯수
        int current = 0; //현재 책 index

        public static string imgURL;
        public static Sprite sprites; // 책 표지 스프라이트
        book selected; //선택된 책
        book[] BOOKS; //전체 책목록

        void Awake()
        {
            parser theParser = GetComponent<parser>(); //parser.cs 스크립트에서 파씽클래스가져옴
            BOOKS = theParser.Parse(csvFileName); //csv파일 이름 매개변수로 받아서 BOOKS[]읽어옴
        }
        void Update()
        {
            if ((current-1) != 0)
            image.sprite = sprites; // 1004 오류수정
        }
        void choiceBook(int booknumber) //현재책의 정보뽑아오는 메쏘드
        {                               //selected = book구조체
            selected = BOOKS[booknumber]; // book[] BOOKS = book[책번호]
            title.text = selected.title; //책제목 = 선택된 book의 title
            interAPI();
            Debug.Log(imgURL);
            NAVER.fx(imgURL);
            image.sprite = sprites;
            writer.text = selected.writer; //이하동문
            publisher.text = selected.publisher;
            year.text = selected.year;
            index.text = selected.index; //여기까지
            if (booknumber == 0) 
                image.sprite = jack;
        }
        void interAPI()
        {
            pushTitle(title.text);
            NAVER.getBookInform();
        }
        public void pushTitle(string bookNAME)
        {
            NAVER.query = bookNAME;
        }

        public void EarBook()
        {
            if (current + 1 > bookEA) // 만약(현재책번호+1 > 마지막책번호)이면
            {
                current = 1; //현재책번호 = 첫번째 책번호
            }
            else
            {
                current += 1; //아닐경우엔 다음책번호
            }
            choiceBook(current - 1); //현재책번호의 책 뽑아옴
            Debug.Log(imgURL);
        }

        public void PreBook()
        {
            if (current - 1 == 0) // 만약(현재책번호-1<첫번째책번호)이면
                current = bookEA; //현재책번호 = 마지막 책번호
            else current -= 1; //아닐경우엔 이전책번호

            choiceBook(current - 1); //현재책번호의 책 뽑아옴
        }
    }
}