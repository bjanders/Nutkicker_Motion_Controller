using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SFB;      //Standalone File Browser from GitHub (https://github.com/gkngkc/UnityStandaloneFileBrowser)

[ExecuteInEditMode]
public class LoadSaveGains: MonoBehaviour
{
    [SerializeField] Transformer Height;
    [SerializeField] Transformer Mot_LFC;
    [SerializeField] Transformer Mot_HFC;
    [SerializeField] Transformer Final;
    [Header("Input Panel")]
    [SerializeField] PanelMotionTuning panelMotionTuning;  
    [Header("File")]
    [SerializeField] string FileName = "GainSettings";
    [ShowOnly] [SerializeField] string FileExtension = "gains";

    string FullFileName;
    string DirectoryPath;
    string FilePath;

    SaveObject saveobject;
    
    private void Start()
    {
        saveobject = new SaveObject();

        string LoadFileName = "LastQuit." + FileExtension;
        string FilePath = Path.Combine(Application.persistentDataPath, LoadFileName);

        if (File.Exists(FilePath))
        {
            saveobject = ReadObjectFromFile(FilePath);
            ReadDataFromObject();
            //Debug.Log("Loading Gains:" + LoadFileName);
        }

        panelMotionTuning.UpdateInputs();
    }

    public void OnClickSave()
    {
        FullFileName = FileName + "." + FileExtension;
        Debug.Log("Saving: " + FullFileName);
        try
        {
            FilePath = StandaloneFileBrowser.SaveFilePanel("Save File", Application.persistentDataPath, FileName, FileExtension);

            DirectoryPath = Path.GetDirectoryName(FilePath);
            FileName = Path.GetFileNameWithoutExtension(FilePath);
            FullFileName = FileName + "." + FileExtension;

            Debug.Log("Saving to: " + "." + FilePath);
        }
        catch (System.Exception)
        {
            return;
        }
        
        SaveDataInObject();
        WriteObjectToFile(FilePath);
    }
    public void OnClickLoad()
    {
        Debug.Log("Loading " + FullFileName);

        try
        {
            FilePath = StandaloneFileBrowser.OpenFilePanel("Open File", Application.persistentDataPath, FileExtension, false)[0];

            DirectoryPath = Path.GetDirectoryName(FilePath);
            FileName = Path.GetFileNameWithoutExtension(FilePath);
            FullFileName = FileName + "." + FileExtension;

            //Debug.Log("Loading from: " + FilePath);
        }
        catch (System.Exception)
        {
            return;
        }
        
        saveobject = ReadObjectFromFile(FilePath);
        ReadDataFromObject();

        panelMotionTuning.UpdateInputs();
    }
    private void OnApplicationQuit()
    {
        string saveFileName = "LastQuit." + FileExtension;
        string savePath = Path.Combine(Application.persistentDataPath, saveFileName );
        
        SaveDataInObject();
        WriteObjectToFile(savePath);
    }

    ////////////  LOAD DATA  ////////////
    private void ReadDataFromObject()
    {
        LoadGains(Height, saveobject.Gains_Height);
        LoadGains(Mot_LFC, saveobject.Gains_LFC);
        LoadGains(Mot_HFC, saveobject.Gains_HFC);
        LoadGains(Final, saveobject.Gains_Final);
    }
    private void LoadGains(Transformer transformer, GainSettings gains)
    {
        transformer.Gain_Master = gains.Master;
        transformer.Gain_Sway = gains.Sway;
        transformer.Gain_Heave = gains.Heave;
        transformer.Gain_Surge = gains.Surge;
        transformer.Gain_Yaw = gains.Yaw;
        transformer.Gain_Pitch = gains.Pitch;
        transformer.Gain_Roll = gains.Roll;
    }
    private SaveObject ReadObjectFromFile(string FilePath)
    {
        string json;

        if (File.Exists(FilePath))
        {
            using (StreamReader reader = new StreamReader(File.OpenRead(FilePath)))
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

    ////////////  Save DATA  ////////////
    private void SaveDataInObject()
    {
        SaveGains(Height, saveobject.Gains_Height);
        SaveGains(Mot_LFC, saveobject.Gains_LFC);
        SaveGains(Mot_HFC, saveobject.Gains_HFC);
        SaveGains(Final, saveobject.Gains_Final);
    }
    private void SaveGains(Transformer transformer, GainSettings gains)
    {
        gains.Master = transformer.Gain_Master;
        gains.Sway = transformer.Gain_Sway;
        gains.Heave = transformer.Gain_Heave;
        gains.Surge = transformer.Gain_Surge;
        gains.Yaw = transformer.Gain_Yaw;
        gains.Pitch = transformer.Gain_Pitch;
        gains.Roll = transformer.Gain_Roll;
    }
    private void WriteObjectToFile(string path)
    {
        string json = JsonUtility.ToJson(saveobject, true);
        
        using (StreamWriter writer = new StreamWriter(File.Create(path)))
        {
            writer.Write(json);
        }
    }
    
    [System.Serializable]
    public class SaveObject
    {
        public GainSettings Gains_Height = new GainSettings();
        public GainSettings Gains_LFC = new GainSettings();
        public GainSettings Gains_HFC = new GainSettings();
        public GainSettings Gains_Final = new GainSettings();
    }

    [System.Serializable]
    public class GainSettings
    {
        public float Master;

        public float Sway;
        public float Heave;
        public float Surge;
        public float Yaw;
        public float Pitch;
        public float Roll;
    }
    
}



