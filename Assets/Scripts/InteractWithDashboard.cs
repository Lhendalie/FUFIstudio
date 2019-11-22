using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class InteractWithDashboard : NetworkBehaviour
{
    GameObject projectNamePanel;
    GameObject projectTaskPanel;
    GameObject gddPanel;
    GameObject memberPanel;


    private InputField projectName;
    private InputField taskName;
    private InputField taskDescription;
    private InputField taskMember;
    private InputField projectOverview;
    private InputField platfroms;
    private InputField genre;
    private InputField targetAudience;
    private InputField storyline;
    private InputField gameplay;
    private InputField assets;
    private InputField testing;
    private InputField member;

    private Text projectNameDashboard;
    private Text projectMembersDashboard;
    private Text GGDdashboard;
    private Text tasksDashboard;

    [SyncVar]
    private string pn;
    [SyncVar]
    private string tn;
    [SyncVar]
    private string td;
    [SyncVar]
    private string tm;
    [SyncVar]
    private string po;
    [SyncVar]
    private string p;
    [SyncVar]
    private string g;
    [SyncVar]
    private string ta;
    [SyncVar]
    private string s;
    [SyncVar]
    private string gp;
    [SyncVar]
    private string a;
    [SyncVar]
    private string t;
    [SyncVar]
    private string m;

    private string pnd;
    private string pmd;
    private string gddd;
    private string tsd;

    // Start is called before the first frame update
    void Start()
    {
        GameObject parentProjName = GameObject.Find("ParentProjName");
        projectNamePanel = parentProjName.transform.GetChild(0).gameObject;

        GameObject parentProjTask = GameObject.Find("ParentProjTask");
        projectTaskPanel = parentProjTask.transform.GetChild(0).gameObject;

        GameObject parentGDD = GameObject.Find("ParentGDD");
        gddPanel = parentGDD.transform.GetChild(0).gameObject;

        GameObject parentMemebers = GameObject.Find("ParentMembers");
        memberPanel = parentMemebers.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isServer)
            return;

        if (other.CompareTag("dashboard"))
        {
            if (!projectNamePanel.activeInHierarchy && !projectTaskPanel.activeInHierarchy && !memberPanel.activeInHierarchy && !gddPanel.activeInHierarchy)
            {
                if (Input.GetKeyDown(KeyCode.T))
                {
                    projectTaskPanel.SetActive(true);

                    // userCode = GameObject.Find("UserCode").GetComponent<Text>();      
                }
                else if (Input.GetKeyDown(KeyCode.N))
                {
                    projectNamePanel.SetActive(true);
                }
                else if (Input.GetKeyDown(KeyCode.M))
                {
                    memberPanel.SetActive(true);
                }
                else if (Input.GetKeyDown(KeyCode.G))
                {
                    gddPanel.SetActive(true);
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                projectNamePanel.SetActive(false);
                projectTaskPanel.SetActive(false);
                gddPanel.SetActive(false);
                memberPanel.SetActive(false);
            }
        }       
    }

    [ClientRpc]
    private void RpcGetInput()
    {
        
    }

    [ClientRpc]
    void RpcShowInput()
    {

    }
}
