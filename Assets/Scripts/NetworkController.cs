using UnityEngine.UI;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class NetworkController : MonoBehaviourPunCallbacks
{
  public TMP_Text textStatus = null;
  public GameObject btnStart = null;
  public byte maxPlayers = 4;

  private void Start()
  {
    PhotonNetwork.ConnectUsingSettings();
    btnStart.SetActive(false);
    Status("Connecting to server");
  }

  public override void OnConnectedToMaster()
  {
    base.OnConnectedToMaster();
    Status("Connected to " + PhotonNetwork.ServerAddress);
    btnStart.SetActive(true);
  }

  public void JoinRoom()
  {
    string roomName = "Room 00";
    Photon.Realtime.RoomOptions opts = new Photon.Realtime.RoomOptions();
    opts.IsOpen = true;
    opts.IsVisible = true;
    opts.MaxPlayers = maxPlayers;

    PhotonNetwork.JoinOrCreateRoom(roomName, opts, Photon.Realtime.TypedLobby.Default);
    btnStart.SetActive(false);
    Status("Joining " + roomName);
  }

  public override void OnJoinedRoom()
  {
    base.OnJoinedRoom();
    SceneManager.LoadScene("Main");
  }

  private void Status(string msg)
  {
    Debug.Log(msg);
    textStatus.text = msg;
  }

}
