using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TestScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public Slider sliderElement,sliderTwoElement;
    [SerializeField] private GameObject arrowHead;
    [SerializeField] private float maxDrag;
    [SerializeField] private float initialConstantForce;
    [SerializeField] private float forceApplied;
    [SerializeField] private GameObject circle;
    private Vector3 initailScale;
    private Vector2 startPosition;
    private Vector2 endPosition;
    private Vector2 direction;
    private float dragFactor;
    public bool isMoving = false;
    [SerializeField] private float circleOffset;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        /*arrowHead = transform.GetChild(1).gameObject;
        circle = transform.GetChild(2).gameObject;*/
        initailScale = circle.transform.localScale;
    }

    private void OnEnable()
    {
        sliderElement.onValueChanged.AddListener(StrikerXPos);
        sliderTwoElement.onValueChanged.AddListener(StrikerXPos);
    }

    private void OnDisable()
    {
        EventManager.Instance.OnRoundOver.RemoveAllListeners();
    }
    private void OnMouseDown()
    {
        if (isMoving) return;
        Vector2 touchPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        startPosition = touchPoint;
        arrowHead.SetActive(true);
        circle.SetActive(true);
        //print("start position "+startPosition);
    }

    private void OnMouseUp()
    {
        if (isMoving)
        {
            return;
        }
        StrikerMovement();
    }

    private void OnMouseDrag()
    {
        if (!isMoving)
        {
            Vector2 touchEndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            endPosition = touchEndPosition;
            //print("end position "+endPosition);
            direction = endPosition - startPosition;
        
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            arrowHead.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
            //print("direction" +direction);
            dragFactor = Mathf.Clamp(Mathf.Abs(direction.y), 0, maxDrag);
            dragFactor /=  maxDrag;
            //print("drag factor " +dragFactor);
            direction = -(direction.normalized);
            //print("direction normalized" +direction);
            forceApplied = initialConstantForce * dragFactor;
        
        
            //print("force Applied "+ forceApplied);
            ChangeCircleSize();
        }
        
    }
    void ChangeCircleSize()
    {
        circle.transform.localScale = initailScale + (dragFactor * initailScale * circleOffset);
    }
    
    public void StrikerMovement()
    {
        /*if (sliderElement.GetComponent<SliderOverUI>().CheckIsOverSlider())
            return;*/
        
        arrowHead.SetActive(false);
        circle.SetActive(false);
        rb.AddForce(direction * forceApplied,ForceMode2D.Impulse);
        isMoving = true;
    }
    
    public void ResetStriker(Vector2 playerPosition)
    {
        transform.localPosition = playerPosition;
        sliderElement.value = 0;
        sliderTwoElement.value = 0;
        arrowHead.SetActive(false);
        circle.SetActive(false);
        isMoving = false;
    }
    
    protected virtual void StrikerXPos(float value)
    {
        if (isMoving) return;
        if (GameManager.Instance._playerState == PlayerState.One)
        {
            transform.localPosition = new Vector3(value, -4f,0);
        }
        else
        {
            transform.localPosition = new Vector3(value, 4f,0);
        }
    }

    private void Update()
    {
        if (rb.velocity.magnitude < 0.3f && isMoving)
        {
            EventManager.Instance.OnRoundOver?.Invoke();
        }
        
        if (GameManager.Instance._playerState == PlayerState.One)
        {
            sliderElement.gameObject.SetActive(true);
            sliderTwoElement.gameObject.SetActive(false);
        }
        else
        {
            sliderElement.gameObject.SetActive(false);
            sliderTwoElement.gameObject.SetActive(true);
        }
    }
}