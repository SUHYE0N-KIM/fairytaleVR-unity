using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class NetworkManager : MonoBehaviourPunCallbacks
{
    [Header("DisconnectPanel")] // 유니티 인스펙터 헤더 (서버 연결 전)
    public InputField NickNameInput; // 닉네임 입력창

    [Header("LobbyPanel")] // 유니티 인스펙터 헤더 (서버 연결 후 대기실)
    public GameObject LobbyPanel; // 방목록
    public InputField RoomInput; // 방만들때 방 이름
    public Text WelcomeText; // 연결확인 메시지 ("현재 닉네임"님 환영합니다)
    public Text LobbyInfoText; // 서버접속자 정보 (현재 닉네임, 서버)
    public Button[] CellBtn; // 방정보 (클릭시 룸패널로 이동)
    public Button PreviousBtn; // 방목록 이전페이지
    public Button NextBtn; // 방목록 다음페이지

    [Header("RoomPanel")] // 유니티 인스펙터 헤더 (방 입장 후)
    public GameObject RoomPanel; // 대화창
    public Text ListText; // 대화목록
    public Text RoomInfoText; // 방정보
    public Text[] ChatText; // 채팅메시지
    public InputField ChatInput;

    [Header("ETC")]
    public Text StatusText; // 현재 포톤서버 연결상태
    public PhotonView PV; // 동기화

    List<RoomInfo> myList = new List<RoomInfo>(); // 방정보 목록
    int currentPage = 1, maxPage, multiple;


    #region 방리스트 갱신
    // ◀버튼 -2 , ▶버튼 -1 , 셀 숫자
    public void MyListClick(int num)
    {
        if (num == -2) --currentPage;
        else if (num == -1) ++currentPage;
        else PhotonNetwork.JoinRoom(myList[multiple + num].Name);
        MyListRenewal();
    }

    void MyListRenewal()
    {
        // 최대페이지
        maxPage = (myList.Count % CellBtn.Length == 0) ? myList.Count / CellBtn.Length : myList.Count / CellBtn.Length + 1;

        // 이전, 다음버튼
        PreviousBtn.interactable = (currentPage <= 1) ? false : true;
        NextBtn.interactable = (currentPage >= maxPage) ? false : true;

        // 페이지에 맞는 리스트 대입
        multiple = (currentPage - 1) * CellBtn.Length;
        for (int i = 0; i < CellBtn.Length; i++)
        {
            CellBtn[i].interactable = (multiple + i < myList.Count) ? true : false;
            CellBtn[i].transform.GetChild(0).GetComponent<Text>().text = (multiple + i < myList.Count) ? myList[multiple + i].Name : "";
            CellBtn[i].transform.GetChild(1).GetComponent<Text>().text = (multiple + i < myList.Count) ? myList[multiple + i].PlayerCount + "/" + myList[multiple + i].MaxPlayers : "";
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        int roomCount = roomList.Count;
        for (int i = 0; i < roomCount; i++)
        {
            if (!roomList[i].RemovedFromList)
            {
                if (!myList.Contains(roomList[i])) myList.Add(roomList[i]);
                else myList[myList.IndexOf(roomList[i])] = roomList[i];
            }
            else if (myList.IndexOf(roomList[i]) != -1) myList.RemoveAt(myList.IndexOf(roomList[i]));
        }
        MyListRenewal();
    }
    #endregion


    #region 서버연결

    void Update()
    {
        StatusText.text = PhotonNetwork.NetworkClientState.ToString();
        // 현재 포톤서버 연결상태
        LobbyInfoText.text = (PhotonNetwork.CountOfPlayers - PhotonNetwork.CountOfPlayersInRooms) + "로비 / " + PhotonNetwork.CountOfPlayers + "접속";
        // 대기실 정보 (대기실에 있는 인원 / 대기실+룸에 있는 인원)
    }

    public void Connect() => PhotonNetwork.ConnectUsingSettings();
    // 포톤서버 접속

    public override void OnConnectedToMaster() => PhotonNetwork.JoinLobby();
    // 서버에 연결되면 대기실로 이동
    // Connect()에서 OnConnectedToMaster() 과정은 please wait... 이 과정이라고 생각

    public override void OnJoinedLobby()
    {
        LobbyPanel.SetActive(true);
        RoomPanel.SetActive(false);
        PhotonNetwork.LocalPlayer.NickName = NickNameInput.text;
        WelcomeText.text = PhotonNetwork.LocalPlayer.NickName + "님 환영합니다";
        // 서버 접속메시지
        myList.Clear();
        // 나의 정보 초기화
    }

    public void Disconnect() => PhotonNetwork.Disconnect();
    // 서버 접속해제

    public override void OnDisconnected(DisconnectCause cause)
    {
        LobbyPanel.SetActive(false);
        RoomPanel.SetActive(false);
    }
    #endregion


    #region 방
    public void CreateRoom() => PhotonNetwork.CreateRoom(RoomInput.text == "" ? "Room" + Random.Range(0, 100) : RoomInput.text, new RoomOptions { MaxPlayers = 4 });
    // 0부터 100까지 랜덤한 번호로 방을 생성
    public void JoinRandomRoom() => PhotonNetwork.JoinRandomRoom();
    // 방에 접속
    public void LeaveRoom() => PhotonNetwork.LeaveRoom();
    // 방 나가기
    public override void OnJoinedRoom()
    {
        RoomPanel.SetActive(true);
        RoomRenewal();
        ChatInput.text = "";
        for (int i = 0; i < ChatText.Length; i++) ChatText[i].text = "";
    }

    public override void OnCreateRoomFailed(short returnCode, string message) { RoomInput.text = ""; CreateRoom(); }

    public override void OnJoinRandomFailed(short returnCode, string message) { RoomInput.text = ""; CreateRoom(); }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        RoomRenewal();
        ChatRPC("<color=yellow>" + newPlayer.NickName + "님이 참가하셨습니다</color>");
        // 신규 플레이어 접속
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        RoomRenewal();
        ChatRPC("<color=yellow>" + otherPlayer.NickName + "님이 퇴장하셨습니다</color>");
        // 접속 플레이어 나감
    }

    void RoomRenewal()
    {
        ListText.text = "";
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            ListText.text += PhotonNetwork.PlayerList[i].NickName + ((i + 1 == PhotonNetwork.PlayerList.Length) ? "" : ", ");
        RoomInfoText.text = PhotonNetwork.CurrentRoom.Name + " / " + "현재 " + PhotonNetwork.CurrentRoom.PlayerCount + "명 / " + "최대 " + PhotonNetwork.CurrentRoom.MaxPlayers + "명";
        // 방정보 (방이름 / 현재접속인원 / 최대접속가능인원)
        // 네트워크 대역폭이 인수의 수와 크기를 고려해 4인으로 한정
    }
    #endregion


    #region 채팅
    public void Send()
    {
        PV.RPC("ChatRPC", RpcTarget.All, PhotonNetwork.NickName + " : " + ChatInput.text); // 원격프로시저 호출 (같은 룸에 있는 다른 플레이어의 함수(채팅) 호출)
        ChatInput.text = "";
        // 채팅메시지 전송 (현재플레이어 이름 : 채팅내용)
    }

    [PunRPC] // RPC는 플레이어가 속해있는 방 모든 인원에게 전달
    void ChatRPC(string msg)
    {
        bool isInput = false;
        for (int i = 0; i < ChatText.Length; i++)
            if (ChatText[i].text == "")
            {
                isInput = true;
                ChatText[i].text = msg;
                break;
            }
        if (!isInput) // 메시지 업데이트 (꽉차면 한칸씩 위로 올림)
        {
            for (int i = 1; i < ChatText.Length; i++) ChatText[i - 1].text = ChatText[i].text;
            ChatText[ChatText.Length - 1].text = msg;
        }
    }
    #endregion
}