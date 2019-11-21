using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using SFB;      //Standalone File Browser from GitHub (https://github.com/gkngkc/UnityStandaloneFileBrowser)
using System;

[ExecuteInEditMode]
public class LoadSaveSettings : MonoBehaviour
{
    [Header("Input Fields")]
    [SerializeField] TMP_InputField Wx;
    [SerializeField] TMP_InputField Wy;
    [SerializeField] TMP_InputField Wz;
    [SerializeField] TMP_InputField Ax;
    [SerializeField] TMP_InputField Ay;
    [SerializeField] TMP_InputField Az;
    [Header("File")]
    [SerializeField] string FileName = "ApplicationSettings";
    [ShowOnly] [SerializeField] string FileExtension = "AppSettings";
    

    string FullFileName;
    string DirectoryPath;
    string FilePath;

    SaveObject saveobject;
    
    private void Start()
    {
        saveobject = new SaveObject();

        string LoadFileName = "LastQuit." + FileExtension;
        string Path = System.IO.Path.Combine(Application.persistentDataPath, LoadFileName);
        
        if (File.Exists(Path))
        {
            saveobject = ReadObjectFromFile(Path);
            ReadDataFromObject();
        }
    }

    public void OnClickSave()
    {
        FullFileName = FileName + "." + FileExtension;
        Debug.Log("saving: " + FullFileName);
        try
        {
            FilePath = StandaloneFileBrowser.SaveFilePanel("Save Setting File", Application.persistentDataPath, FileName, FileExtension);

            DirectoryPath = Path.GetDirectoryName(FilePath);
            FileName = Path.GetFileNameWithoutExtension(FilePath);
            FullFileName = FileName + "." + FileExtension;

            Debug.Log("Saving .AppSettings file to: " + FilePath);
        }
        catch (System.Exception)
        {
            return;
        }
        
        WriteDataInObject();
        WriteObjectToFile(FilePath);
    }
    public void OnClickLoad()
    {
        Debug.Log("Loading " + FullFileName);
        try
        {
            FilePath = StandaloneFileBrowser.OpenFilePanel("Open .AppSettings File", Application.persistentDataPath, FileExtension, false)[0];

            DirectoryPath = Path.GetDirectoryName(FilePath);
            FileName = Path.GetFileNameWithoutExtension(FilePath);
            FullFileName = FileName + "." + FileExtension;
        }
        catch (System.Exception)
        {
            return;
        }
        
        saveobject = ReadObjectFromFile(FilePath);
        ReadDataFromObject();
    }

    private void OnApplicationQuit()
    {
        string saveFileName = "LastQuit." + FileExtension;
        string savePath = Path.Combine(Application.persistentDataPath, saveFileName);

        WriteDataInObject();
        WriteObjectToFile(savePath);
    }

    ////////////  LOAD DATA  ////////////
    private SaveObject ReadObjectFromFile(string Path)
    {
        string json;

        if (File.Exists(Path))
        {
            using (StreamReader reader = new StreamReader(File.OpenRead(Path)))
            {
                json = reader.ReadToEnd();
            }

            return JsonUtility.FromJson<SaveObject>(json);
        }
        else
        {
            Debug.Log("File with name -" + FileName + "- not found");
            return null;
        }
    }
    private void ReadDataFromObject()
    {
        Wx.text = saveobject.crashDetectionSettings.Threshold_Wx.ToString(GlobalVars.myNumberFormat());
        Wy.text = saveobject.crashDetectionSettings.Threshold_Wy.ToString(GlobalVars.myNumberFormat());
        Wz.text = saveobject.crashDetectionSettings.Threshold_Wz.ToString(GlobalVars.myNumberFormat());

        Ax.text = saveobject.crashDetectionSettings.Threshold_Ax.ToString(GlobalVars.myNumberFormat());
        Ay.text = saveobject.crashDetectionSettings.Threshold_Ay.ToString(GlobalVars.myNumberFormat());
        Az.text = saveobject.crashDetectionSettings.Threshold_Az.ToString(GlobalVars.myNumberFormat());
    }

    ////////////  Save DATA  ////////////
    private void WriteDataInObject()
    {
        saveobject.crashDetectionSettings.Threshold_Wx = Convert.ToSingle(Wx.text);
        saveobject.crashDetectionSettings.Threshold_Wy = Convert.ToSingle(Wy.text);
        saveobject.crashDetectionSettings.Threshold_Wz = Convert.ToSingle(Wz.text);

        saveobject.crashDetectionSettings.Threshold_Ax = Convert.ToSingle(Ax.text);
        saveobject.crashDetectionSettings.Threshold_Ay = Convert.ToSingle(Ay.text);
        saveobject.crashDetectionSettings.Threshold_Az = Convert.ToSingle(Az.text);

    }
    private void WriteObjectToFile(string path)
    {
        string json = JsonUtility.ToJson(saveobject, true);
        
        using (StreamWriter writer = new StreamWriter(File.Create(path)))
        {
            writer.Write(json);
        }
    }

    ////////////  Infrastructure  ////////////
    [System.Serializable]
    public class SaveObject
    {
        public CrashDetectionSettings crashDetectionSettings = new CrashDetectionSettings();
    }

    [System.Serializable]
    public class CrashDetectionSettings
    {
        public float Threshold_Wx;
        public float Threshold_Wy;
        public float Threshold_Wz;
        public float Threshold_Ax;
        public float Threshold_Ay;
        public float Threshold_Az;
    }
}



