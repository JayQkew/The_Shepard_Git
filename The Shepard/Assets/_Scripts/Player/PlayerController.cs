using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [Header("Movement")]
    public Rigidbody rb;
    public float moveMultiplier;
    public float jumpMultiplier;
    public float maxSpeed;
    public float drag;
    public float airDrag;
    public float groundCastY;
    public LayerMask ground;
    public bool grounded;

    [Header("Influence")]
    public float influence_r;
    public float influence_rMin;
    public float influence_rMax;
    public float forceMultiplier;
    public LayerMask sheep;

    [Header("Bark")]
    public int barkCount;
    public float barkMultiplier;
    public float barkTimer;
    public float barkTimerCurrent;
    public int barkMax;
    public GameObject barkEffect;

    private bool up;
    private bool down;
    private bool left;
    private bool right;
    private bool jump;

    [Header("Audio")]
    public AudioClip[] barkingSounds;
    public AudioSource barkAudio;
    public AudioClip landingSound;
    public AudioClip runningInGrass;
    public AudioSource landingAudio;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        BarkTimer();
        SpeedLimit();
        Presence();
        Drag();

        if (Input.GetKey(KeyCode.W)) up = true;
        if (Input.GetKey(KeyCode.S)) down = true;
        if (Input.GetKey(KeyCode.A)) left = true;
        if (Input.GetKey(KeyCode.D)) right = true;
        if (Input.GetKeyDown(KeyCode.Space)) jump = true;
        if (Input.GetMouseButtonDown(0)) Bark(true);
        else Bark(false);
    }

    private void FixedUpdate()
    {

        if (up)
        {
            Move(Vector3.forward);
            up = false;
        }
        if (down)
        {
            Move(Vector3.back);
            down = false;
        }
        if (left)
        {
            Move(Vector3.left);
            left = false;
        }
        if (right)
        {
            Move(Vector3.right);
            right = false;
        }

        if (jump)
        {
            Jump();
            jump = false;
        }
    }

    #region MOVEMENT
    private void SpeedLimit()
    {
        if (rb.velocity.y == 0)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }
        else
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, 10);
        }
    }

    private void Drag()
    {
        if (!Input.GetKey(KeyCode.W) &&
            !Input.GetKey(KeyCode.A) &&
            !Input.GetKey(KeyCode.S) &&
            !Input.GetKey(KeyCode.D) &&
            rb.velocity.y == 0)
        {
            rb.drag = drag;
        }
        else rb.drag = airDrag;
    }

    private void Move(Vector3 direction)
    {
        Vector3 moveDir = direction * moveMultiplier;
        rb.AddForce(moveDir, ForceMode.Force);
    }

    private void Jump()
    {
        if (Grounded())
        {
            Vector3 jumpForce = Vector3.up * jumpMultiplier;
            rb.drag = airDrag;
            rb.AddForce(jumpForce, ForceMode.Impulse);
        }
    }

    private void LandingSound()
    {
        landingAudio.clip = landingSound;
        landingAudio.Play();
    }

    private bool Grounded()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, groundCastY, ground);
        return grounded;
    }
    #endregion

    #region PRESENCE
    private void Presence()
    {

        GameObject[] sheep = AreaOfEffect(Mathf.Clamp(influence_r + (rb.velocity.magnitude * 0.1f), influence_rMin, influence_rMax));

        foreach (GameObject s in sheep)
        {
            Vector3 force = PushForce(s) * forceMultiplier;
            s.GetComponent<Rigidbody>().AddForce(force * Time.deltaTime, ForceMode.Force);
        }

    }

    private Vector3 PushForce(GameObject sheep)
    {
        Vector3 direction = sheep.transform.position - transform.position;
        Vector3 force_norm = Vector3.ClampMagnitude(direction, 1);
        return force_norm;
    }
    #endregion

    #region BARKING
    private void Bark(bool barking)
    {
        if (barking && barkCount > 0)
        {
            BarkSound();
            StartCoroutine(BarkEffect());
            GameObject[] sheep = AreaOfEffect(influence_r * 2);

            foreach (GameObject s in sheep)
            {
                Vector3 force = PushForce(s) * forceMultiplier * barkMultiplier;
                //StartCoroutine(s.GetComponent<SheepLogic>().BarkReact());
                s.GetComponent<Rigidbody>().AddForce(force * Time.deltaTime, ForceMode.Impulse);
            }

            barkCount--;
            //UIManager.Instance.BarkCountText();
            //StartCoroutine(UIManager.Instance.BarkAnim());
            //UIManager.Instance.BarkIdleAnim();
        }

    }
    private void BarkSound()
    {
        barkAudio.clip = barkingSounds[Random.Range(0, barkingSounds.Length)];
        barkAudio.Play();
    }

    private IEnumerator BarkEffect()
    {
        barkEffect.SetActive(true);
        yield return new WaitForSeconds(0.15f);
        barkEffect.SetActive(false);
    }

    private IEnumerator BarkCount()
    {
        if (barkCount < barkMax)
        {
            barkCount++;
            //UIManager.Instance.BarkCountText();
        }
        yield return new WaitForSeconds(barkTimer);
        yield return null;
        StartCoroutine(BarkCount());
    }

    private void BarkTimer()
    {
        if (barkTimerCurrent <= 0)
        {
            if (barkCount < barkMax)
            {
                barkCount++;
                //UIManager.Instance.BarkCountText();
                //StartCoroutine(UIManager.Instance.BarkAddAnim());
                //UIManager.Instance.BarkIdleAnim();
            }
            barkTimerCurrent = barkTimer;
        }
        else
        {
            if (barkCount < barkMax) barkTimerCurrent -= Time.deltaTime;
            else barkTimerCurrent = barkTimer;
        }
    }
    #endregion
    private GameObject[] AreaOfEffect(float influence)
    {
        RaycastHit[] hit = Physics.SphereCastAll(transform.position, influence, Vector3.up, 0.1f, sheep);
        List<GameObject> sur_boids = new List<GameObject>();

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].transform.gameObject != gameObject) sur_boids.Add(hit[i].transform.gameObject);
        }

        return sur_boids.ToArray();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, Mathf.Clamp(influence_r + (rb.velocity.magnitude * 0.5f), influence_rMin, influence_rMax));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "ground") LandingSound();
    }
}
