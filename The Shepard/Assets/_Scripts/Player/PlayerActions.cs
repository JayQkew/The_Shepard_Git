using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerActions : MonoBehaviour
{
    public static PlayerActions Instance { get; private set; }

    [Header("Bark")]
    public bool canBark;
    public GameObject[] bark_affectedRadius;
    public GameObject[] bark_affectedBox;
    [SerializeField]
    private float bark_r;
    [SerializeField]
    private float bark_strength;
    [SerializeField]
    private GameObject boxPivot;
    [SerializeField]
    private Transform box;

    [Space(10)]
    [SerializeField]
    private LayerMask effectedAgents;
    [SerializeField]
    private LayerMask ground;
    public bool hoverOverUI;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        Bark_Box();

        BarkRadius_AoE();
        Bark_AoE();

        if (hoverOverUI)
        {
            Debug.Log("over a inputField");
            PlayerController.Instance.PlayerActions.canBark = false;
        }
        else
        {
            PlayerController.Instance.PlayerActions.canBark = true;
        }

        //ShootRay();
    }

    #region Bark 1
    //public void Bark1()
    //{
    //    foreach (GameObject agent in bark_affectedRadius)
    //    {
    //        Vector3 force = Bark1_Force(agent.transform.position) * bark1_strength;
    //        agent.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
    //        agent.GetComponent<SheepBehaviour>().inAura = true;
    //        agent.GetComponent<SheepBehaviour>().startled = true;
    //    }

    //}
    //private Vector3 Bark1_Force(Vector3 target)
    //{
    //    Vector3 dir = target - transform.position;
    //    Vector3 forceNorm = Vector3.ClampMagnitude(dir, 1);
    //    return forceNorm;
    //}

    #endregion

    #region Bark
    public void Bark()
    {
        if (canBark)
        {
            foreach (GameObject agent in bark_affectedBox)
            {
                agent.GetComponent<Rigidbody>().AddForce(Bark_Force() * bark_strength, ForceMode.Impulse);
                agent.GetComponent<SheepBehaviour>().inAura = true;
                agent.GetComponent<SheepBehaviour>().startled = true;
            }
        }
    }

    private Vector3 Bark_Force()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 direction = mousePos - playerPos;
        Vector3 worldDir = new Vector3(direction.x, 0, direction.y);

        return Vector3.ClampMagnitude(worldDir, 1);
    }

    private void BarkRadius_AoE()
    {
        RaycastHit[] hit = Physics.SphereCastAll(transform.position, bark_r, Vector3.up, 0, effectedAgents);
        List<GameObject> sur_agents = new List<GameObject>();

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].transform.gameObject != gameObject) sur_agents.Add(hit[i].transform.gameObject);
        }

        bark_affectedRadius = sur_agents.ToArray();
    }
    private void Bark_AoE()
    {
        RaycastHit[] hit = Physics.BoxCastAll(box.position, box.lossyScale / 2, Vector3.up, Quaternion.identity, 0, effectedAgents);
        List<GameObject> sur_agents = new List<GameObject>();

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].transform.gameObject != gameObject && bark_affectedRadius.Contains(hit[i].transform.gameObject))
            {
                sur_agents.Add(hit[i].transform.gameObject);
            }
        }

        bark_affectedBox = sur_agents.ToArray();
    }

    private void Bark_Box()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 playerPosition = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 direction = mousePosition - playerPosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        boxPivot.transform.rotation = Quaternion.Euler(new Vector3(0, -angle, 0));
    }
    #endregion

    #region Interact

    private GameObject ShootRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitData;
        Physics.Raycast(ray, out hitData, 1000, effectedAgents);

        if (hitData.collider != null)
        {
            return hitData.transform.gameObject;
        }
        else
        {
            return null;
        }
    }

    public void Interact()
    {
        GameObject agent = ShootRay();

        if (agent != null)
        {
            if (!agent.GetComponent<SheepUI>().go_canvas.activeSelf)
            {
                agent.GetComponent<SheepUI>().go_canvas.SetActive(true);

            }
            else
            {
                agent.GetComponent<SheepUI>().go_canvas.SetActive(false);
                PlayerController.Instance.PlayerActions.canBark = true;
            }
        }
    }

    
    #endregion
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, bark_r);
    }
}
