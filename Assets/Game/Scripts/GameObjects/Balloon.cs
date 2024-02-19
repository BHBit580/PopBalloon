using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class Balloon : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private IntDataSO playerScore;
    [SerializeField] private float maxYRange = 10f;
    [SerializeField] private float destroyAnimationSpeed = 2.5f;
    
    public double speed = 2f;
    
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        transform.Translate(Vector3.up * ((float)speed * Time.deltaTime));
        
        if (transform.position.y > maxYRange) DestroyBalloon();
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        playerScore.data++;
        animator.speed = destroyAnimationSpeed;
        animator.SetBool("Destroy", true);
    }
    
    
    
    private void DestroyBalloon()
    {
        Destroy(gameObject);
    }
}