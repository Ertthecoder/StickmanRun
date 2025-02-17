using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public GameObject player;
    private float posX;
    private float posY;
    private float posZ;
    private float playerStartX;
    private float playerStartY;

    // Update is called once per frame

    private void Start()
    {
        playerStartX = player.transform.position.x;
        playerStartY = player.transform.position.y;
    }
    void Update()
    { 
        gameObject.transform.position = player.transform.position + new Vector3(0, 0, -0.5f);
        
        posX = gameObject.transform.position.x;
        posY = gameObject.transform.position.y; 
        posZ = gameObject.transform.position.z;

        // Make the camera follow the player and set it so that it wont go outside the level


        if (posX < -2.8f)
        {
            gameObject.transform.position = new Vector3(-2.8f, posY, posZ - 0.5f);
            PlayerPosYControl(-2.8f);
        }
        else if (posX > 3)
        {
            gameObject.transform.position = new Vector3(3, posY, posZ - 0.5f);
            PlayerPosYControl(3);
        }

        if (posY < -5.8f)
        {
            gameObject.transform.position = new Vector3(posX, -5.8f, posZ - 0.5f);
            PlayerPosXControl(-5.8f);
        }
        else if (posY > 2.8f)
        {
            gameObject.transform.position = new Vector3(posX, 2.8f, posZ - 0.5f);
            PlayerPosXControl(2.8f);
        } 

    }

    // Functions for cross checking the positions to prevent camera slips
    private void PlayerPosXControl(float y)
    {
        if (posX < -2.8f)
        {
            gameObject.transform.position = new Vector3(-2.8f, y, posZ - 0.5f);
        }
        else if (posX > 3f)
        {
            gameObject.transform.position = new Vector3(3, y, posZ - 0.5f);
        }
    }

    private void PlayerPosYControl(float x)
    {
        if (posY < -5.8f)
        {
            gameObject.transform.position = new Vector3(x, -5.8f, posZ - 0.5f);
        }
        else if (posY > 2.8f)
        {
            gameObject.transform.position = new Vector3(x, 2.8f, posZ - 0.5f);
        }
    }
}
