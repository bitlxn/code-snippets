using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class PlayerGroundCheck : MonoBehaviour
{

    [SerializeField] private PlayerController player;

    #region collision

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((player.groundLayer.value & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            player.playerOnGround = true;
            player.playerInAir = false;

            GameObject landParticles = Instantiate(player.landParticleSystem) as GameObject;

            landParticles.transform.position = player.playerTransform.position;

            player.playerLandFeedback.PlayFeedbacks();

        }
        else if ((player.wallLayer.value & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            player.playerOnWall = true;
            player.playerInAir = false;
        }
        else
        {
            player.playerInAir = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((player.groundLayer.value & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            player.playerOnGround = false;
            player.playerInAir = true;
            
            GameObject landParticles = Instantiate(player.landParticleSystem) as GameObject;

            landParticles.transform.position = player.playerTransform.position;
        }
        else if ((player.wallLayer.value & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            player.playerOnWall = false;
            player.playerInAir = true;
        }
        else
        {
            player.playerInAir = true;
        }
    }

    #endregion
}
