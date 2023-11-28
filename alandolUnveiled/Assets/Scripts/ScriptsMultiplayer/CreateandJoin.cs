using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using ExitGames.Client.Photon;


public class CreateandJoin : MonoBehaviourPunCallbacks
{
    public InputField roomInputField;
    public GameObject lobbyPanel;
    public GameObject roomPanel;
    public Text roomName;

    public RoomItem roomItemPrefab;
    List<RoomItem> roomItemsList = new List<RoomItem>();
    public Transform contentObject;

    public float timeBetweenUpdates = 1.5f;
    float nextUpdateTime;

    public List<PlayerItem> playerItemsList = new List<PlayerItem>(); //La lista que guarda el player item actual
    public PlayerItem playerItemPrefab; //guarda el prefab del jugador
    public Transform playerItemParent; //game object parent

    public GameObject playButton;
    AjusteEntreEscenas noDestruirEntreEscenas;

    //hacemos esto porque debemos estar dentro de un lobby de photon para poder crear un room
    private void Start()
    {
        PhotonNetwork.JoinLobby();

        noDestruirEntreEscenas = FindObjectOfType<AjusteEntreEscenas>();
    }

    public void OnClickCreate()
    {
        if(roomInputField.text.Length >= 1) //checar si el room no tiene un nombre vacio
        {
            PhotonNetwork.CreateRoom(roomInputField.text, new RoomOptions(){ MaxPlayers = 3, BroadcastPropsChangeToAll = true});
        }
    }

    public override void OnJoinedRoom()
    {
        lobbyPanel.SetActive(false);
        roomPanel.SetActive(true);
        roomName.text = "Room: " + PhotonNetwork.CurrentRoom.Name;
        UpdatePlayerList();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if(Time.time >= nextUpdateTime)
        {
            UpdateRoomList(roomList);
            nextUpdateTime = Time.time + timeBetweenUpdates;
        }
    }

    void UpdateRoomList(List<RoomInfo> list)
    {
        foreach(RoomItem item in roomItemsList)
        {
            Destroy(item.gameObject);
        }
        roomItemsList.Clear();

        foreach(RoomInfo room in list)
        {
            if (room.RemovedFromList)
            {
                return;
            }
            RoomItem newRoom = Instantiate(roomItemPrefab, contentObject);
            newRoom.SetRoomName(room.Name);
            roomItemsList.Add(newRoom);
        }
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void OnClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        roomPanel.SetActive(false);
        lobbyPanel.SetActive(true);
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    //Se divide en dos pasos: el primero se encarga de borrar todos los player item de la lista y el segundo es spawnear un player item para cada jugador en el room y agregarlo a la lista
    void UpdatePlayerList()
    {
        foreach(PlayerItem item in playerItemsList) 
        {
            Destroy(item.gameObject);
        }
        playerItemsList.Clear();

        if(PhotonNetwork.CurrentRoom == null)
        {
            return;
        }

        foreach(KeyValuePair<int,Player> player in PhotonNetwork.CurrentRoom.Players) //es un loop por el photon network, obtiene todos los jugadores dentro de un room y los recibe como un key value o doble lista (id, value)
        {
            PlayerItem newPlayerItem = Instantiate(playerItemPrefab, playerItemParent); //
            newPlayerItem.SetPlayerInfo(player.Value);

            if(player.Value == PhotonNetwork.LocalPlayer)
            {
                newPlayerItem.ApplyLocalChanges();
            }

            playerItemsList.Add(newPlayerItem);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
    }

    private void Update()
    {
        if(PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >= 1)
        {
            playButton.SetActive(true);
        }
        else
        {
            playButton.SetActive(false);
        }
    }

    public void OnClickPlayButton()
    {
        PhotonNetwork.LoadLevel("Nivel1");
        noDestruirEntreEscenas.gameObject.SetActive(false);
    }
}
