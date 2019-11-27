using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player_Create : NetworkBehaviour
{
    private List<string> comments = new List<string>();

    GameObject projNamePanel;
    GameObject projTaskPanel;
    GameObject gddPanel;
    GameObject addMemberPanel;
    GameObject codePanel;
    GameObject uploadImagePanel;
    GameObject uploadCommentPanel;

    private Upload_Comment script;

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
    private InputField uploadedComment;

    //[SyncVar]
    //private string allComments;

    void Start()
    {
        GetData();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ProjName"))
        {
            ShowUI();
            HideUI();

            if (Input.GetKeyDown(KeyCode.Return) && projNamePanel.activeInHierarchy)
            {
                if (isServer)
                {
                    RpcChangeDashBoardName(other.gameObject, projName.text);
                }
                if (!isServer)
                {
                    CmdChangeDashboardName(other.gameObject, projName.text);
                }
            }
        }

        if (other.CompareTag("ProjTask"))
        {
            ShowUI();
            HideUI();

            if (Input.GetKeyDown(KeyCode.Return) && projTaskPanel.activeInHierarchy)
            {
                if (isServer)
                {
                    RpcChangeDashboardTasks(other.gameObject, projTaskName.text, projTaskDescription.text, projTaskMember.text);
                }
                if (!isServer)
                {
                    CmdChangeDashboardTasks(other.gameObject, projTaskName.text, projTaskDescription.text, projTaskMember.text);
                }
            }
        }

        if (other.CompareTag("ProjMember"))
        {
            ShowUI();
            HideUI();

            if (Input.GetKeyDown(KeyCode.Return) && addMemberPanel.activeInHierarchy)
            {
                if (isServer)
                {
                    RpcChangeDashboardMembers(other.gameObject, addMember.text);
                }
                if (!isServer)
                {
                    CmdChangeDashboardMembers(other.gameObject, addMember.text);
                }
            }
        }

        if (other.CompareTag("ProjGDD"))
        {
            ShowUI();
            HideUI();

            if (Input.GetKeyDown(KeyCode.Return) && gddPanel.activeInHierarchy)
            {
                if (isServer)
                {
                    RpcChangeDashboardGDD(other.gameObject, projOverview.text, projPlatforms.text, projGenre.text, projTargetAudience.text, projStoryline.text, projGameplay.text, projAssets.text, projTesting.text);
                }
                if (!isServer)
                {
                    CmdChangeDashboardGDD(other.gameObject, projOverview.text, projPlatforms.text, projGenre.text, projTargetAudience.text, projStoryline.text, projGameplay.text, projAssets.text, projTesting.text);
                }
            }
        }

        if (other.CompareTag("computer"))
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                codePanel.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                codePanel.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.Return) && codePanel.activeInHierarchy)
            {
                if (isServer)
                {
                    RpcChangeCode(other.gameObject, codeField.text);
                }
                if (!isServer)
                {
                    CmdChangeCode(other.gameObject, codeField.text);
                }
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                OpenBrowser();
            }
        }

        if (other.CompareTag("gallery"))
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                uploadImagePanel.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                uploadImagePanel.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.Return) && uploadImagePanel.activeInHierarchy)
            {
                uploadImagePanel.SetActive(false);


                if (isServer)
                {
                    RpcUploadImage(other.gameObject, urlInput.text, caption.text);
                    Debug.Log(other.gameObject.name);
                    Debug.Log("Called on server.");
                }

                Debug.Log("Called on enter.");

                if (!isServer)
                {
                    Debug.Log("Called on player.");
                    CmdUploadImage(other.gameObject, urlInput.text, caption.text);
                }
            }
        }

        if (other.CompareTag("comment"))
        {            
            if (Input.GetKeyDown(KeyCode.C))
            {
                uploadCommentPanel.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                uploadCommentPanel.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.Return) && uploadCommentPanel.activeInHierarchy)
            {
                uploadCommentPanel.SetActive(false);

                if (isServer)
                {
                    RpcUploadComment(other.gameObject, uploadedComment.text);
                }
                if (!isServer)
                {
                    CmdUploadComment(other.gameObject, uploadedComment.text);
                }
            }
        }
    }
    

    [Command]
    private void CmdUploadComment(GameObject other, string comment)
    {
        RpcUploadComment(other, comment);
    }

    [ClientRpc]
    private void RpcUploadComment(GameObject other, string comment)
    {
        Upload_Comment.comments.Add(comment);

        if (comments.Count >= 24)
        {
            Upload_Comment.comments.RemoveAt(0);
        }


        Debug.Log($"{string.Join(",", Upload_Comment.comments)}");
        other.GetComponent<Text>().text = string.Join("\n", Upload_Comment.comments);

    }

    [Command]
    private void CmdUploadImage(GameObject other, string url, string captionName)
    {
        RpcUploadImage(other, url, captionName);
    }

    [ClientRpc]
    private void RpcUploadImage(GameObject other, string url, string captionName)
    {
        StartCoroutine(LoadFromLikeCorouting(url, other));
        Debug.Log(other.gameObject.name + other.gameObject.name);

        GameObject.Find(other.gameObject.name + other.gameObject.name).GetComponent<Text>().text = captionName;
    }

    [Command]
    private void CmdChangeCode(GameObject other, string name)
    {
        RpcChangeCode(other, name);
    }

    [ClientRpc]
    private void RpcChangeCode(GameObject other, string name)
    {
        other.GetComponent<Text>().text = name;
    }

    [Command]
    private void CmdChangeDashboardTasks(GameObject other, string nameName, string nameDescription, string nameMember)
    {
        RpcChangeDashboardTasks(other, nameName, nameDescription, nameMember);
    }

    [ClientRpc]
    private void RpcChangeDashboardTasks(GameObject other, string nameName, string nameDescription, string nameMember)
    {
        other.GetComponent<Text>().text = $"Task name: {nameName}\nTask Description: {nameDescription}\nTask assigned to: {nameMember}";
    }

    [Command]
    private void CmdChangeDashboardMembers(GameObject other, string name)
    {
        RpcChangeDashboardMembers(other, name);
    }

    [ClientRpc]
    private void RpcChangeDashboardMembers(GameObject other, string name)
    {
        other.GetComponent<Text>().text = $"Members: \n{name}";
    }

    [Command]
    private void CmdChangeDashboardGDD(GameObject other, string nameOverview, string namePlatforms, string nameGenre, string nameTargetAud, string nameStory, string nameGameplay, string nameAssets, string nameTesting)
    {
        RpcChangeDashboardGDD(other, nameOverview, namePlatforms, nameGenre, nameTargetAud, nameStory, nameGameplay, nameAssets, nameTesting);        
    }

    [ClientRpc]
    private void RpcChangeDashboardGDD(GameObject other, string nameOverview, string namePlatforms, string nameGenre, string nameTargetAud, string nameStory, string nameGameplay, string nameAssets, string nameTesting)
    {
        other.GetComponent<Text>().text = $"Game Design Document \n1.Overview - {nameOverview}\n2.Platforms - {namePlatforms}\n3.Genre - {nameGenre}\n4.Target Audience - {nameTargetAud}\n5.Storryline and Characters - {nameStory}\n6.Gameplay - {nameGameplay}\n7.Assets - {nameAssets}\n8.Testing - {nameTesting}";
    }

    [Command]
    private void CmdChangeDashboardName(GameObject other, string name)
    {
        RpcChangeDashBoardName(other, name);
    }

    [ClientRpc]
    private void RpcChangeDashBoardName(GameObject other, string name)
    {
        other.GetComponent<Text>().text = name;
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

        GameObject parentUploadComment = GameObject.Find("ParentUploadComment");
        uploadCommentPanel = parentUploadComment.transform.GetChild(0).gameObject;

        projNamePanel.SetActive(true);
        projTaskPanel.SetActive(true);
        gddPanel.SetActive(true);
        addMemberPanel.SetActive(true);
        codePanel.SetActive(true);
        uploadImagePanel.SetActive(true);
        uploadCommentPanel.SetActive(true);

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
        uploadedComment = GameObject.Find("UploadComment").GetComponent<InputField>();


        projNamePanel.SetActive(false);
        projTaskPanel.SetActive(false);
        gddPanel.SetActive(false);
        addMemberPanel.SetActive(false);
        codePanel.SetActive(false);
        uploadImagePanel.SetActive(false);
        uploadCommentPanel.SetActive(false);
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

    private IEnumerator LoadFromLikeCorouting(string url, GameObject other)
    { 
        WWW wwwLoader = new WWW(url);
        yield return wwwLoader;

        other.GetComponent<Renderer>().material.color = Color.white;
        other.GetComponent<Renderer>().material.mainTexture = wwwLoader.texture;
        Debug.Log("Image has loaded.");
    }
}