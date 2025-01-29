using System;
using System.Collections.Generic;
using UnityEngine;

public class CaromCoinPlacer : MonoBehaviour
{

    public GameObject redCoinPrefab;
    public GameObject blackCoinPrefab;
    public GameObject whiteCoinPrefab;
    public GameObject queenPrefab;
    public float spacing;
    public int numberOfCoinsPerRow = 3; // Define how many coins you want per row
    public float radius = 1f; // define the radius of each coin

    void Start()
    {
        print("hello");
        Vector2 center = new Vector2(0, 0); // queen position

        float rowDistance = Mathf.Sqrt(3) * radius; // distance between each row
        float coinDistance = 2 * radius; // distance between each coin

        int totalRows = (int)Mathf.Sqrt(numberOfCoinsPerRow);

        for (int i = 0; i < totalRows; i++)
        {
            for (int j = 0; j < numberOfCoinsPerRow; j++)
            {
                Vector2 newPos = new Vector2(center.x + j * coinDistance + (i % 2) * radius, center.y + i * rowDistance);
                GameObject coin;

                // Instantiate different colored coins
                if ((i+j) % 2 == 0)
                {
                    coin = Instantiate(blackCoinPrefab, newPos, Quaternion.identity);
                }
                else
                {
                    coin = Instantiate(whiteCoinPrefab, newPos, Quaternion.identity);
                }

                // Add a CircleCollider2D to the coin
                coin.AddComponent<CircleCollider2D>();
            }
        }

        // Instantiate the queen at the center
        GameObject queen = Instantiate(queenPrefab, center, Quaternion.identity);
        queen.AddComponent<CircleCollider2D>();
    }




    public void PlaceCoinsInPattern()
    {

        // Place the red coin in the center.
        GameObject redCoin = Instantiate(redCoinPrefab, Vector3.zero, Quaternion.identity);
        redCoin.transform.parent = this.transform;

        // Angles for Y shape, three arms at 120 degrees separation
        float[] arms = new float[] { 0, 120, 240 }; 

        // Place the white coins in a Y shape.
        for(int arm = 0; arm < arms.Length; arm++)
        {
            float angle = arms[arm] * Mathf.Deg2Rad; 

            // Two white coins per arm
            for(int coin = 0; coin < 2; coin++)
            {
                Vector3 pos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * (spacing + coin * spacing);
                GameObject whiteCoin = Instantiate(whiteCoinPrefab, pos + redCoin.transform.position, Quaternion.identity);
                whiteCoin.transform.parent = this.transform;
            }
        }
    
        // Place the black coins in V shapes around the Y shape.
        for(int arm = 0; arm < arms.Length; arm++)
        {
            float angle = arms[arm] * Mathf.Deg2Rad; 

            // Three black coins per V shape
            for(int coin = 0; coin < 3; coin++)
            {
                // Calculate positions to form a V shape
                Vector3 pos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * (spacing * (coin + 2.5f));
            
                if (coin != 1)
                    pos += new Vector3(Mathf.Cos(angle + Mathf.PI / 2), Mathf.Sin(angle + Mathf.PI / 2), 0f) * (spacing / 2 * (coin == 0 ? -1 : 1));
                
                GameObject blackCoin = Instantiate(blackCoinPrefab, pos + redCoin.transform.position, Quaternion.identity);
                blackCoin.transform.parent = this.transform;
            }
        }
    }
}

  