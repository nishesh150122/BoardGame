using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Lean.Touch;

public abstract class StrikerAgent : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int strikerForce=500;
    [SerializeField] public Slider sliderElement;
    [SerializeField] private GameObject arrowHead;
    public bool isMoving=false;
    //public UnityEvent OnRoundOver = new UnityEvent();
    [SerializeField] protected Vector2 playerPosition;
    [SerializeField]private Vector3 touchPos;
    private float strikerRadius = 6f;
    private CircleCollider2D stikerCollider2D;

    private void Awake()
    {
        //stikerCollider2D = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        /*foreach (var playArea in GameManager.Instance.PlayAreas)
        {
            if (playArea.collisionObjects.Contains(this.gameObject))
            {
                print(playArea.collisionObjects[1]);
                CoinPlayAreaCheck();
            }
        }*/
        
      
        if (Input.touchCount>0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                // Get the current touch
                Touch touch = Input.GetTouch(i);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        if (sliderElement.GetComponent<SliderOverUI>().CheckIsOverSlider())
                            return;
                        
                        arrowHead.SetActive(true);
                        break;

                    case TouchPhase.Moved:
                        // Touch moved, do something else
                        if (isMoving)
                        {
                            return;
                        }
                        touchPos = Camera.main.ScreenToWorldPoint( touch.position);
                        //print(touchPos);
                        Vector3 mousedirection = touchPos - transform.position;
                        float angle = Mathf.Atan2(mousedirection.y, mousedirection.x) * Mathf.Rad2Deg;
                        arrowHead.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
                        break;

                    case TouchPhase.Ended:
                        // Touch ended, do another thing
                        if (isMoving)
                        {
                            return;
                        }
                        StrikerMovement(touchPos);
                        arrowHead.SetActive(false);
                        break;
                }
            }
        }
        if (rb.velocity.magnitude < 0.3f && isMoving)
        {
            EventManager.Instance.OnRoundOver?.Invoke();
        }
    }


    

    private void CoinPlayAreaCheck()
    {
        strikerPosition = transform.position;


        Collider2D[] coinsInWay = Physics2D.OverlapCircleAll(strikerPosition, strikerRadius);

        foreach (Collider2D coin in coinsInWay)
        {
            if (coin.gameObject.CompareTag("Coins"))
            {
                stikerCollider2D.isTrigger = true;
                transform.position = new Vector2(coin.transform.position.x + 5, transform.position.y);
            }
            else
            {
                stikerCollider2D.isTrigger = false;
            }
        }
    }

    private Vector2 strikerPosition;

    public void StrikerMovement(Vector3 mousePos)
    {
        if (sliderElement.GetComponent<SliderOverUI>().CheckIsOverSlider())
            return;
        
        
        isMoving = true;
        Vector3 direction = (mousePos - transform.position).normalized;
        Vector3 reverseDirection = -direction;
        rb.AddForce(reverseDirection * strikerForce, ForceMode2D.Impulse);
    }

    protected virtual void ResetStriker()
    {
        sliderElement.value = 0;
        arrowHead.SetActive(true);
        isMoving = false;
    }

    protected virtual void StrikerXPos(float value)
    {
        if (isMoving) return;
        if (GameManager.Instance.CheckActivePlayer()==1)
        {
            transform.localPosition = new Vector3(value, -4f,0);
        }
        else
        {
            transform.localPosition = new Vector3(value, 4f,0);
        }
    }
}