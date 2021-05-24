using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class diaNext : MonoBehaviour
{

    [SerializeField] public Text Speaker; //화자
    [SerializeField] public Text Content; //내용
    [SerializeField] string csvFileName; // 읽어올 .csv파일이름
    [SerializeField] public GameObject showCanvas;
    [SerializeField] public GameObject momCanvas;
    [SerializeField] public GameObject nagneCanvas;
    [SerializeField] public GameObject bean;
    [SerializeField] public static GameObject cow;
    [SerializeField] public GameObject bow;
    public static int index = 0;
    public static dialist[] dList;
    private dialist nowDial;
    private void Awake() //실행하자마자 책목록 읽어와야함
    {
        diaParser theParser = GetComponent<diaParser>(); //parser.cs 스크립트에서 파씽클래스가져옴
        dList = theParser.Parse(csvFileName); //csv파일 이름 매개변수로 받아서 BOOKS[]읽어옴
    }
    void Start()
    {
        Speaker.text = dList[index].talker;
        Content.text = dList[index].dial;
    }
    void Update()
    {
        nextDial();
    }
    public void nextDial()
    {
        if (dList[index].index == "s1"|| dList[index].index == "s3")
        {
            showCanvas.SetActive(false);
        }
        if(dList[index].index == "s2")
        {
            showCanvas.SetActive(false);
            momCanvas.SetActive(true);
            bean.GetComponent<MeshRenderer>().enabled = true;
        }
        if (dList[index].index == "s4")
        {
            showCanvas.SetActive(false);
            bow.SetActive(true);
        }
        else
        {
            Speaker.text = dList[index].talker;
            Content.text = dList[index].dial;
        }
    }
}