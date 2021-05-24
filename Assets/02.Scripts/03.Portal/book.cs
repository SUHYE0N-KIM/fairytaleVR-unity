using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//dialogue = book  dialogueevent=booklist interactionevent = viewing
[System.Serializable]
public class book : MonoBehaviour
{
    public Image cover; //표지
    public string index; //인덱스
    public string title; //제목
    public string writer; //저자
    public string publisher; //출판사
    public string year; //출판년도
}

[System.Serializable]
public class booklist
{
    public string name;
    public int no;
    public book books;
}


