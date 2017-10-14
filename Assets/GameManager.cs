using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject Dot;
    public GameObject DotStar;
    public GameObject PlayerMarker;
    bool isDotRotated = false;
    bool hasPlayerInput = false;
    int numberOfTaps = 0;
    int starsHit = 0;
    int targetHits = 0;
    public Text StarCount;
    public int threshold;
    [Range(0f, 5f)]
    public float rotationSpeed;
    

    int randomRotationAngle = 0;
    // Use this for initialization
    void Awake ()
    {
        DotStar.SetActive(false);
        StarCount.text = "0";
    }

    void Start () {
        StarRandomizer();
	}
	
	// Update is called once per frame
	void Update () {

        // set dot position, flip each time with player input
        if (!isDotRotated)
        {
            if (numberOfTaps % 2 == 1)
            {
                Dot.transform.rotation = Quaternion.Euler(0, 0, 0);
                randomRotationAngle = (int)Random.Range(0, 150);
            } else
            {
                Dot.transform.rotation = Quaternion.Euler(0, 0, 0);
                randomRotationAngle = (int)Random.Range(0, -150);
            }

            DotMove(randomRotationAngle);
        }

        // move marker around the lock, changing direction each time player input
        if (!hasPlayerInput)
        {
            // Set direction of marker movement
            if (numberOfTaps % 2 == 1)
            {
                MarkerMove(1);
            } else
            {
                MarkerMove(-1);
            }
        }
        
        // check for player input
        if (Input.GetMouseButtonDown(0))
        {
            hasPlayerInput = true;
            float MarkerAngle = Mathf.Rad2Deg*PlayerMarker.transform.rotation[2];
            float DotAngle = Mathf.Rad2Deg * Dot.transform.rotation[2];
            Debug.Log("marker rotation angle " + MarkerAngle);
            Debug.Log("dot rotation angle " + DotAngle);

            // check if marker overlaps with dot
            if (Mathf.Abs(MarkerAngle - DotAngle) < threshold) ;
            {
                targetHits++;
                Debug.Log("target has been hit");

                // check for star input
                if (DotStar.activeSelf == true)
                {
                    starsHit++;
                    Debug.Log("star is hit");
                    StarCount.text = starsHit.ToString();
                }
            }

            numberOfTaps++;
            hasPlayerInput = false;
            isDotRotated = false;
            StarRandomizer();
        }
        
	}

    // Rotate the dot
    void DotMove(int rotateAngle) { 
        Dot.transform.Rotate(new Vector3 (0,0,rotateAngle));
        isDotRotated = true;
    }
    
    void MarkerMove (int Direction)
    {
        PlayerMarker.transform.Rotate(new Vector3(0, 0, rotationSpeed * Direction));
    }

    void StarRandomizer()
    {
        int starRandomizer = (int)Random.Range(0, 4);

        if (starRandomizer % 4 == 0)
        {
            DotStar.SetActive(true);
        }
        else
        {
            DotStar.SetActive(false);
        }
    }
}
