using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] public float moveSpeed;
    [SerializeField] float sideSpeed;


    [SerializeField] Transform rewardPoint;
    [SerializeField] Animator _animator;
    

    private bool isFighting = false;
    private bool isDancing = false;

    private void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            var otherInitialSize = other.gameObject.transform.localScale;
            
            isFighting = true;
            var fightingPoint = other.GetComponent<EnemyController>().fightingPoint;
            gameObject.transform.DOMove(fightingPoint.transform.position, 0.8f).OnComplete(() =>
            {
                var targetScale = other.gameObject.transform.localScale - gameObject.transform.localScale;
                targetScale = new Vector3(Mathf.Clamp(targetScale.x, 0, 999), Mathf.Clamp(targetScale.y, 0, 999), Mathf.Clamp(targetScale.z, 0, 999));
                other.transform.DOScale(targetScale, 0.5f).OnComplete(() =>
                {
                    if (other.transform.localScale.x < .5f)
                    {
                        Destroy(other.gameObject);

                    }
                });

                targetScale = gameObject.transform.localScale - otherInitialSize;
                targetScale = new Vector3(Mathf.Clamp(targetScale.x, 0, 999), Mathf.Clamp(targetScale.y, 0, 999), Mathf.Clamp(targetScale.z, 0, 999));
                gameObject.transform.DOScale(targetScale,0.5f).OnComplete(() 
                    =>
                {
                    if (gameObject.transform.localScale.magnitude < gameObject.GetComponent<SizeController>().minGrowSize.magnitude )
                    {
                        Destroy(gameObject);
                        
                    }
                    isFighting = false;
                });
            });
        }else if (other.CompareTag("Pad"))
        {
            other.GetComponent<Collider>().enabled = false;
            isFighting = true;
            JumpPadController controller = other.GetComponent<JumpPadController>();
            transform.DOJump(controller.targetPoint.position, controller.jumpPower, controller.numOfJump, controller.jumpDuration).SetEase(Ease.Linear).OnComplete(() =>
            {
                isFighting = false;
            });
        }

        else if (other.CompareTag("FinishLine"))
        {
            isFighting = true;
            gameObject.transform.DOMove(rewardPoint.transform.position, 1.5f).SetEase(Ease.Linear).OnComplete(() =>
            {
                _animator.SetBool("IsFinished", true);
                isDancing = true;
                StartCoroutine(WaitForDance());
            });

        }
    }

   


    private void Update()
    {
        Move();    
    }

    IEnumerator WaitForDance()
    {

        yield return new WaitForSeconds(3);
        SceneController.instance.LoadGame();  
    }

    private void MoveForward()
    {
        gameObject.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.World);
    }

    private void MoveSideward()
    {
        if(Input.GetAxis("Horizontal") != 0)        
            gameObject.transform.Translate(Vector3.right * sideSpeed * Time.deltaTime* Input.GetAxis("Horizontal"), Space.World);        
    }

    private void Move()
    {
        if (!isFighting)
        {
            MoveForward();
            MoveSideward();
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -5, 5), transform.position.y, transform.position.z);
        }
        
    }



    
}
