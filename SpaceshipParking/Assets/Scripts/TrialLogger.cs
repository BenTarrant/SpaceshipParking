using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TrialLogger : MonoBehaviour
{

    public int currentTrialNumber = 1;
    List<string> header;
    [HideInInspector]
    public Dictionary<string, string> trial;
    [HideInInspector]
    public string outputFolder;

    public Canvas GameOver;

    GameManager GM;

    bool trialStarted = false;

    string dataOutputPath;

    string data;

    List<string> output;

    public AgeGate value;

    const string glyphs = "abcdefghijklmnopqrstuvwxyz0123456789"; //add the characters you want
    public string myID;
    public Text GameID;

    public bool hasID = false;


    // Use this for initialization
    void Awake()
    {
        outputFolder = Application.dataPath + "/StreamingAssets" + "/output";
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        GM = GetComponent<GameManager>();
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void Initialize(string participantID, List<string> customHeader)
    {

        header = customHeader;
        InitHeader();
        InitDict();
        output = new List<string>();
        output.Add(string.Join(",", header.ToArray()));

        dataOutputPath = outputFolder + @"/TestResults" + ".CSV";
    }

    private void InitHeader()
    {
        header.Insert(0, "ID");
        header.Insert(1, "Age");
        header.Insert(2, "Level");
        header.Insert(3, "Start Time");
        header.Insert(4, "End Time");
        header.Insert(5, "Lives");
    }

    private void InitDict()
    {
        trial = new Dictionary<string, string>();
        foreach (string value in header)
        {
            trial.Add(value, "");
        }
    }

    public void StartTrial()
    {
        trialStarted = true;
        currentTrialNumber += 1;
        InitDict();
        trial["Start Time"] = Time.time.ToString();
        trial["Lives"] = GM.curRepairs.ToString();

    }

    public void EndTrial()
    {
        if (output != null && dataOutputPath != null)
        {
            if (trialStarted)
            {
                if (GM.curRepairs == 0)
                {
                    StartCoroutine(Delay());
                }

                trial["End Time"] = Time.time.ToString();
                Scene scene = SceneManager.GetActiveScene(); // fetch the active scene from build index
                trial["Level"] = scene.name.ToString(); // write that scene name to string for CSV
                trial["Lives"] = GM.curRepairs.ToString();
                trial["ID"] = myID.ToString();
                output.Add(FormatTrialData());


                if (GM.curRepairs > 0)
                {
                    trialStarted = false;
                    StartTrial();

                }


            }
            else Debug.LogError("Error ending trial - Trial wasn't started properly");

        }
        else Debug.LogError("Error ending trial - TrialLogger was not initialsed properly");


    }

    private string FormatTrialData()
    {
        List<string> rowData = new List<string>();
        foreach (string value in header)
        {
            rowData.Add(trial[value]);
        }
        return string.Join(",", rowData.ToArray());
    }


    public void OnApplicationQuit()
    {

        if (output != null && dataOutputPath != null)
        {
            File.WriteAllLines(dataOutputPath, output.ToArray());

            Debug.Log(string.Format("Saved data to {0}.", dataOutputPath));

            //// NOW MAIL RESULTS --------------------------------------------------- Keep commented unless testing/deploying

            MailMessage mail = new MailMessage();

            mail.From = new MailAddress("aversetoloss@gmail.com");
            mail.To.Add("aversetoloss@gmail.com");
            mail.Subject = "Subject Results";
            mail.Body = "";

            var pathToImage = dataOutputPath;
            string attachmentPath = pathToImage;
            System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(attachmentPath);
            mail.Attachments.Add(attachment);

            SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
            smtpServer.Port = 587;
            smtpServer.Credentials = new System.Net.NetworkCredential("aversetoloss@gmail.com", "LossAverse1") as ICredentialsByHost;
            smtpServer.EnableSsl = true;
            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };
            smtpServer.Send(mail);
            Debug.Log("success");
        }

        else Debug.LogError("Error saving data - TrialLogger was not initialsed properly");

    }

    public IEnumerator Delay()
    {
        yield return new WaitForSeconds(3f);
        //playfailuresound
        Ender();

    }

    public void Ender()
    {
        GameOver.enabled = true;
        AssignID();
    }

    void AssignID()
    {
        if (hasID = false)
        {


            int charAmount = 4;
            for (int i = 0; i < charAmount; i++)
            {
                myID += glyphs[UnityEngine.Random.Range(0, glyphs.Length)];

            }

            GameID.text = "Unique ID: " + myID;
            TextEditor te = new TextEditor();
            te.text = myID;
            te.SelectAll();
            te.Copy();
            hasID = true;
        }
    }



}
