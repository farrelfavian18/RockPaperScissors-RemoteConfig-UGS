using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;

public class PlayerItem : MonoBehaviour
{
    [SerializeField] TMP_Text playerName;
    [SerializeField] Image avatarImage;
    [SerializeField] Sprite[] avatarSprites;

    public void Set(Photon.Realtime.Player player)
    {
        if (player.CustomProperties.TryGetValue(PropertyNames.Player.AvatarIndex, out var value))
            avatarImage.sprite = avatarSprites[(int)value];

        playerName.text = player.NickName;

        if (player == PhotonNetwork.MasterClient)
            playerName.text = player.NickName + "(Master)";

    }
}
