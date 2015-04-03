using UnityEngine;
using System.Collections;

public class Cyclop : MonoBehaviour {

    public CharacterController motion;
    public Animator animator;
    public AudioSource audio;
    public AudioClip roar;
    public float speed;
    public float height;
    public LayerMask looking, walls;
    public float checkDistance;
    public float radius;
    private float percentage;
    public float cooldown;
    private bool turned = false;
    private bool foundWall;
    private bool chasing;
    public float chasingTime;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (chasing || CheckPlayer())
        {
            if (!chasing)
            {
                audio.PlayOneShot(roar);
                chasing = true;
                StartCoroutine(StopChasing());
            }
            
            percentage = 1f;
            animator.SetFloat("Speed", percentage);
            motion.SimpleMove(percentage * speed * transform.forward);
        }
        else
        {
            chasing = false;
            if (foundWall && !turned && !CheckDirection(transform.right))
            {
                Turn(90f);
            }
            else
            {
                if (!CheckDirection(transform.forward))
                {
                    percentage = .7f;
                    animator.SetFloat("Speed", percentage);
                    motion.SimpleMove(percentage * speed * transform.forward);
                }
                else
                {
                    foundWall = true;
                    transform.Rotate(0, -90f, 0);
                }
            }
        }
	}

    private IEnumerator StopChasing() {
        yield return new WaitForSeconds(chasingTime);
        chasing = false;
    }

    private bool CheckPlayer()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        return Physics.SphereCast(ray, radius*3f, Mathf.Infinity, looking);
    }

    private IEnumerator CoolDown() {
        turned = true;
        yield return new WaitForSeconds(cooldown);
        turned = false;
    }

    private void Turn(float degY) {
        transform.Rotate(0, degY, 0);
        StartCoroutine(CoolDown());
    }

    private bool CheckDirection(Vector3 dir) {
        Ray ray = new Ray(transform.position + Vector3.up * height / 2, dir);
        Debug.DrawRay(transform.position + Vector3.up * height / 2, dir);
        return Physics.SphereCast(ray, radius, checkDistance, walls);
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag(ETag.Human.ToString()))
            GameMaster.Instance.PlayerDied();
    }
}
