using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player_Create : NetworkBehaviour
{
    GameObject projNamePanel;
    GameObject projTaskPanel;
    GameObject gddPanel;
    GameObject addMemberPanel;


    private InputField projName;
    private InputField projTaskName;
    private InputField projTaskDescription;
    private InputField projTaskMember;
    private InputField projOverview;
    private InputField projPlatforms;
    private InputField projGenre;
    private InputField projTargetAudience;
    private InputField projStoryline;
    private InputField projGameplay;
    private InputField projAssets;
    private InputField projTesting;
    private InputField addMember;

    //private Text projectNameDashboard;
    //private Text projectMembersDashboard;
    //private Text GDDDashboard;
    //private Text projectTasksDashboard;

    [SyncVar]
    private string projNameString;
    [SyncVar]
    private string projTaskNameString;
    [SyncVar]
    private string projTaskDescriptionString;
    [SyncVar]
    private string projTaskMemberString;
    [SyncVar]
    private string projOverviewString;
    [SyncVar]
    private string projPlatformsString;
    [SyncVar]
    private string projGenreString;
    [SyncVar]
    private string projTargetAudienceString;
    [SyncVar]
    private string projStorylineString;
    [SyncVar]
    private string projGameplayString;
    [SyncVar]
    private string projAssetsString;
    [SyncVar]
    private string projTestingString;
    [SyncVar]
    private string addMemberString;

    void Start()
    {
        GetData();
        if (isServer)
        {
            //RpcUpdateData();
        }
        else if(isLocalPlayer)
        {
            //CmdUpdateData();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        ShowUI();
        HideUI();

        if (other.CompareTag("ProjName"))
        {
            //if(isLocalPlayer)
            //{
            //    projNameString = projName.text;
            //    CmdChangeDashboardName(other.gameObject);
            //    Debug.Log("Proj name var for client is: " + projNameString);
            //}

            if (isServer)
            {
                projNameString = projName.text;
                RpcChangeDashBoardName(other.gameObject);
            }
        }
        if(other.CompareTag("ProjTask"))
        {
            if(isServer)
            {
                projTaskNameString = projTaskName.text;
                projTaskDescriptionString = projTaskDescription.text;
                projTaskMemberString = projTaskMember.text;
                RpcChangeDashboardTasks(other.gameObject);
            }
        }
        if(other.CompareTag("ProjMember"))
        {
            if(isServer)
            {
                addMemberString = addMember.text;
                RpcChangeDashboardMembers(other.gameObject);
            }
        }
        if(other.CompareTag("ProjGDD"))
        {
            if(isServer)
            {
                projOverviewString = projOverview.text;
                projPlatformsString = projPlatforms.text;
                projGenreString = projGenre.text;
                projTargetAudienceString = projTargetAudience.text;
                projStorylineString = projStoryline.text;
                projGameplayString = projGameplay.text;
                projAssetsString = projAssets.text;
                projTestingString = projTesting.text;

                RpcChangeDashboardGDD(other.gameObject);
            }
        }
    }

    [ClientRpc]
    private void RpcChangeDashboardTasks(GameObject other)
    {
        other.GetComponent<Text>().text = $"Task name: {projTaskNameString}\nTask Description: {projTaskDescriptionString}\nTask assigned to: {projTaskMemberString}";
    }

    [ClientRpc]
    private void RpcChangeDashboardMembers(GameObject other)
    {
        other.GetComponent<Text>().text = $"Members: \n{addMemberString}";
    }

    [ClientRpc]
    private void RpcChangeDashboardGDD(GameObject other)
    {
        other.GetComponent<Text>().text = $"Game Design Document \n1.Overview - {projOverviewString}\n2.Platforms - {projPlatformsString}\n3.Genre - {projGenreString}\n4.Target Audience - {projTargetAudienceString}\n5.Storryline and Characters - {projStorylineString}\n6.Gameplay - {projGameplayString}\n7.Assets - {projAssetsString}\n8.Testing - {projTestingString}";
    }

    [ClientRpc]
    private void RpcChangeDashBoardName(GameObject other)
    {
        other.GetComponent<Text>().text = projNameString;
    }

    [Command]
    private void CmdChangeDashboardName(GameObject other)
    {
        RpcChangeDashBoardName(other);
    }

    private void GetData()
    {
        GameObject parentProjName = GameObject.Find("ParentProjName");
        projNamePanel = parentProjName.transform.GetChild(0).gameObject;

        GameObject parentProjTask = GameObject.Find("ParentProjTask");
        projTaskPanel = parentProjTask.transform.GetChild(0).gameObject;

        GameObject parentGDD = GameObject.Find("ParentGDD");
        gddPanel = parentGDD.transform.GetChild(0).gameObject;

        GameObject parentAddMember = GameObject.Find("ParentAddMember");
        addMemberPanel = parentAddMember.transform.GetChild(0).gameObject;

        projNamePanel.SetActive(true);
        projTaskPanel.SetActive(true);
        gddPanel.SetActive(true);
        addMemberPanel.SetActive(true);

        projName = GameObject.Find("ProjName").GetComponent<InputField>();
        projTaskName = GameObject.Find("ProjTaskName").GetComponent<InputField>();
        projTaskDescription = GameObject.Find("ProjTaskDescription").GetComponent<InputField>();
        projTaskMember = GameObject.Find("ProjTaskMember").GetComponent<InputField>();
        projOverview = GameObject.Find("ProjOverview").GetComponent<InputField>();
        projPlatforms = GameObject.Find("ProjPlatforms").GetComponent<InputField>();
        projGenre = GameObject.Find("ProjGenre").GetComponent<InputField>();
        projTargetAudience = GameObject.Find("ProjTargetAudience").GetComponent<InputField>();
        projStoryline = GameObject.Find("ProjStoryline").GetComponent<InputField>();
        projGameplay = GameObject.Find("ProjGameplay").GetComponent<InputField>();
        projAssets = GameObject.Find("ProjAssets").GetComponent<InputField>();
        projTesting = GameObject.Find("ProjTesting").GetComponent<InputField>();

        addMember = GameObject.Find("AddMember").GetComponent<InputField>();

        //projectNameDashboard = GameObject.Find("ProjectNameDashboard").GetComponent<Text>();
        //projectMembersDashboard = GameObject.Find("ProjectMemberDashbaord").GetComponent<Text>();
        //GDDDashboard = GameObject.Find("GDDDashboard").GetComponent<Text>();
        //projectTasksDashboard = GameObject.Find("ProjectTasksDashboard").GetComponent<Text>();

        projNamePanel.SetActive(false);
        projTaskPanel.SetActive(false);
        gddPanel.SetActive(false);
        addMemberPanel.SetActive(false);
    }

    private void ShowUI()
    {
        if (!projNamePanel.activeInHierarchy && !projTaskPanel.activeInHierarchy && !addMemberPanel.activeInHierarchy && !gddPanel.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                projTaskPanel.SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                projNamePanel.SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.M))
            {
                addMemberPanel.SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.G))
            {
                gddPanel.SetActive(true);
            }
        }
    }

    private void HideUI()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            projNamePanel.SetActive(false);
            projTaskPanel.SetActive(false);
            gddPanel.SetActive(false);
            addMemberPanel.SetActive(false);
        }
    }
}

