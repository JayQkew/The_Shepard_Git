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
    [SerializeField]
    public LayerMask interactionAgents;
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

        ShootRay();
    }

    #region Bark
    public void Bark()
    {
        if (canBark)
        {
            foreach (GameObject agent in bark_affectedBox)
            {
                if (agent.tag == "sheep")
                {
                    agent.GetComponent<Rigidbody>().AddForce(Bark_Force() * bark_strength, ForceMode.Impulse);
                    agent.GetComponent<SheepBehaviour>().inAura = true;
                    agent.GetComponent<SheepBehaviour>().startled = true;
                }
                else if (agent.tag == "ducken")
                {
                    if (!PlayerController.Instance.followingDuckens.Contains(agent))
                    {
                        if (PlayerController.Instance.followingDuckens.Count > 0)
                        {
                            agent.GetComponent<DuckenManager>().followAgent = PlayerController.Instance.followingDuckens[PlayerController.Instance.followingDuckens.Count - 1];
                        }
                        else
                        {
                            agent.GetComponent<DuckenManager>().followAgent = gameObject;
                        }

                        agent.GetComponent<DuckenManager>().SwitchState(agent.GetComponent<DuckenManager>().DuckenFollowState);
                        PlayerController.Instance.followingDuckens.Add(agent);
                        MissionManager.Instance.BarkAtDuckens();
                    }
                }
                else if (agent.tag == "bird")
                {
                    agent.GetComponent<BirdLogic>().scared = true;
                    agent.GetComponent<BirdLogic>().scareAgent = gameObject;
                    MissionManager.Instance.BarkAtBird();
                }
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
        Physics.Raycast(ray, out hitData, 1000, interactionAgents);

        if (hitData.collider != null)
        {
            if (hitData.transform.tag == "sheep") MouseLogic.Instance.mouseState = MouseState.Interact;
            else if (hitData.transform.tag == "frog") MouseLogic.Instance.mouseState = MouseState.Interact;
            else if (hitData.transform.tag == "farmer") MouseLogic.Instance.mouseState = MouseState.Interact;
            else MouseLogic.Instance.mouseState = MouseState.None;
            return hitData.transform.gameObject;
        }
        else
        {
            if (hoverOverUI)
            {
                MouseLogic.Instance.mouseState = MouseState.Edit;
            }
            else
            {
                MouseLogic.Instance.mouseState = MouseState.None;
            }
            return null;
        }
    }

    public void Interact()
    {
        GameObject agent = ShootRay();

        if (agent != null)
        {
            if (agent.tag == "sheep")
            {
                if (!agent.GetComponent<SheepUI>().go_canvas.activeSelf)
                {
                    agent.GetComponent<SheepUI>().go_canvas.SetActive(true);
                    foreach (GameObject sheep in SheepTracker.Instance.allSheep)
                    {
                        if (sheep != agent)
                        {
                            sheep.GetComponent<SheepUI>().go_canvas.SetActive(false);
                        }
                    }
                }
                else
                {
                    agent.GetComponent<SheepUI>().go_canvas.SetActive(false);
                    PlayerController.Instance.PlayerActions.canBark = true;
                }
            }
            else if (agent.tag == "frog")
            {
                if (!agent.GetComponent<FrogManager>().found) MissionManager.Instance.FrogFound();
                agent.GetComponent<FrogManager>().found = true;
            }
            else if (agent.tag == "farmer")
            {
                if (!FarmerManager.Instance.interacted)
                {
                    MissionManager.Instance.FarmerInteract();
                    FarmerManager.Instance.interacted = true;
                }
            }
        }
    }


    #endregion
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, bark_r);
    }
}
