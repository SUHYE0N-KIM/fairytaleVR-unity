using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NAVER : MonoBehaviour
{
    private static NAVER naver;

    private void Awake() => naver = this;


    public static string query = ""; // 검색할 문자열
    public static string imglink = "";
    public static Sprite sprite;
    public static Texture2D texture;


    public static void getBookInform()
    {
        Main();
    }
    public static void fx(string MediaUrl)
    {
        naver.StartCoroutine(DownloadImage(MediaUrl));
    }
    static IEnumerator DownloadImage(string MediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
        {
            texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            Rect rect = new Rect(0, 0, texture.width, texture.height);

            sprite = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));
            Valve.VR.InteractionSystem.InteractionBook.sprites = sprite;
        }
    }

    static void Main()
    {
        string url = "https://openapi.naver.com/v1/search/image?query=" + query + "&sort=sim"; // 결과가 JSON 포맷
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Headers.Add("X-Naver-Client-Id", "BKRlpTSxMkXXRpZASm0c"); // 클라이언트 아이디
        request.Headers.Add("X-Naver-Client-Secret", "zMMm19Soqq");       // 클라이언트 시크릿
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        string status = response.StatusCode.ToString();
        if (status == "OK")
        {
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            string text = reader.ReadToEnd();
            //Debug.Log(url);
            string[] data = text.Split(new char[] { '\n' });
            string[] row = data[9].Split(new char[] { '"' });
            imglink = row[3];
            Valve.VR.InteractionSystem.InteractionBook.imgURL = imglink;
            //Debug.Log(row[3]);
            //Debug.Log(text);
            //Console.WriteLine(text);
        }
        else
        {
            Console.WriteLine("Error 발생=" + status);
        }
    }
}