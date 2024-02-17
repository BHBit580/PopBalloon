using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class Balloon : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private IntDataSO playerScore;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float maxYRange = 10f;
    [SerializeField] private float destroyAnimationSpeed = 2.5f;
    private Animator animator;
    private BalloonSpawner balloonSpawner;

    private void Start()
    {
        animator = GetComponent<Animator>();
        balloonSpawner = FindObjectOfType<BalloonSpawner>();
        AnimationEvent animationEvent = new AnimationEvent(); 
        animationEvent.functionName = "OnDestroyAnimationComplete";
        animationEvent.time = animator.GetCurrentAnimatorStateInfo(0).length; 
        animator.runtimeAnimatorController.animationClips[0].AddEvent(animationEvent);
    }

    private void Update()
    {
        transform.Translate(Vector3.up * (speed * Time.deltaTime));
        
        if (transform.position.y > maxYRange) DestroyBalloon();
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        playerScore.data++;
        animator.speed = destroyAnimationSpeed;
        animator.SetBool("Destroy", true);
    }
    
    private void OnDestroyAnimationComplete()
    {
        DestroyBalloon();
    }
    
    private void DestroyBalloon()
    {
        balloonSpawner.balloonsList.Remove(gameObject);
        Destroy(gameObject);
    }
}