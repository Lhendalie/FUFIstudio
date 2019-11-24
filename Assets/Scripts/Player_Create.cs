using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player_Create : NetworkBehaviour
{
    private Renderer planeRenderer;

    GameObject projNamePanel;
    GameObject projTaskPanel;
    GameObject gddPanel;
    GameObject addMemberPanel;
    GameObject codePanel;
    GameObject uploadImagePanel;

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
    private InputField codeField;
    private InputField urlInput;
    private InputField caption;

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
    [SyncVar]
    private string codeString;
    [SyncVar]
    private string urlInputString;
    [SyncVar]
    private string captionString;

    [SyncVar]
    private bool imageChanged;

    void Start()
    {
        GetData();
    }

    private void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        

        if (other.CompareTag("ProjName"))
        {
            ShowUI();
            HideUI();

            if (isServer)
            {
                projNameString = projName.text;
                RpcChangeDashBoardName(other.gameObject);
            }
        }
        if(other.CompareTag("ProjTask"))
        {
            ShowUI();
            HideUI();
            if (isServer)
            {
                projTaskNameString = projTaskName.text;
                projTaskDescriptionString = projTaskDescription.text;
                projTaskMemberString = projTaskMember.text;
                RpcChangeDashboardTasks(other.gameObject);
            }
        }
        if(other.CompareTag("ProjMember"))
        {
            ShowUI();
            HideUI();
            if (isServer)
            {
                addMemberString = addMember.text;
                RpcChangeDashboardMembers(other.gameObject);
            }
        }
        if(other.CompareTag("ProjGDD"))
        {
            ShowUI();
            HideUI();
            if (isServer)
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
        if(other.CompareTag("computer"))
        {
            if (isServer)
            {
                if (Input.GetKeyDown(KeyCode.K))
                {
                    codePanel.SetActive(true);
                }
                else if (Input.GetKeyDown(KeyCode.Escape))
                {
                    codePanel.SetActive(false);
                }

                codeString = codeField.text;
                RpcChangeCode(other.gameObject);
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                OpenBrowser();
            }
        }
        if (other.CompareTag("gallery"))
        {
            if (isServer)
            {
                if(Input.GetKeyDown(KeyCode.U))
                {
                    uploadImagePanel.SetActive(true);
                }
                if(Input.GetKeyDown(KeyCode.Escape))
                {
                    uploadImagePanel.SetActive(false);
                }
                else if(Input.GetKeyDown(KeyCode.Return) && uploadImagePanel.activeInHierarchy)
                {
                    urlInputString = urlInput.text;
                    imageChanged = true;
                    uploadImagePanel.SetActive(false);
                    RpcUploadImage();
                }
            }
        }
    }

    [ClientRpc]
    private void RpcUploadImage()
    {
        urlInputString = urlInput.text;
        captionString = caption.text;
        imageChanged = true;
        StartCoroutine(LoadFromLikeCorouting());
    }

    [ClientRpc]
    private void RpcChangeCode(GameObject other)
    {
        other.GetComponent<Text>().text = codeString;
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

        GameObject parentCode = GameObject.Find("ParentCode");
        codePanel = parentCode.transform.GetChild(0).gameObject;

        GameObject parentUploadImage = GameObject.Find("ParentUploadImage");
        uploadImagePanel = parentUploadImage.transform.GetChild(0).gameObject;

        projNamePanel.SetActive(true);
        projTaskPanel.SetActive(true);
        gddPanel.SetActive(true);
        addMemberPanel.SetActive(true);
        codePanel.SetActive(true);
        uploadImagePanel.SetActive(true);

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
        codeField = GameObject.Find("CodeInputField").GetComponent<InputField>();
        addMember = GameObject.Find("AddMember").GetComponent<InputField>();
        urlInput = GameObject.Find("URLInput").GetComponent<InputField>();
        caption = GameObject.Find("Caption").GetComponent<InputField>();

        planeRenderer = GameObject.Find("GalleryImage1").GetComponent<Renderer>();

        projNamePanel.SetActive(false);
        projTaskPanel.SetActive(false);
        gddPanel.SetActive(false);
        addMemberPanel.SetActive(false);
        codePanel.SetActive(false);
        uploadImagePanel.SetActive(false);
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

    private void OpenBrowser()
    {
        Application.OpenURL("https://www.google.co.uk/");
    }

    private IEnumerator LoadFromLikeCorouting()
    { 
        WWW wwwLoader = new WWW(urlInputString);
        yield return wwwLoader;

        planeRenderer.material.color = Color.white;
        planeRenderer.material.mainTexture = wwwLoader.texture;
        Debug.Log("Image has loaded.");
    }
}

