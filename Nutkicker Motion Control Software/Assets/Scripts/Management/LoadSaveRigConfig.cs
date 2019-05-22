using UnityEngine;
using System.IO;
using TMPro;
using SFB;      //Standalone File Browser from GitHub (https://github.com/gkngkc/UnityStandaloneFileBrowser)

[ExecuteInEditMode]
public class LoadSaveRigConfig : MonoBehaviour
{
    [Header("Hardware")]
    [SerializeField] Platform Base;
    [SerializeField] Platform Final;
    [SerializeField] Actuators actuators;
    [Header("Input Panel")]
    [SerializeField] PanelRigConfig PanelRigConfig;
    [Header("File")]
    [SerializeField] string FileName = "Rig_Settings";
    [ShowOnly] [SerializeField] string FileExtension = "rig";

    string FullFileName;
    string DirectoryPath;
    string FilePath;

    SaveObject saveobject;
    
    private void Start()
    {
        string LoadFileName = "LastQuit." + FileExtension;
        string FilePath = Path.Combine(Application.persistentDataPath, LoadFileName);
        saveobject = new SaveObject();

        if (File.Exists(FilePath))
        {
            saveobject = ReadObjectFromFile(FilePath);
            ReadDataFromObject();
            Debug.Log("Loading Rig Config:" + LoadFileName);
        }

        PanelRigConfig.UpdateInputs();
    }

    public void OnClickSave()
    {
        FullFileName = FileName + "." + FileExtension;
        Debug.Log("saving: " + FullFileName);
        try
        {
            FilePath = StandaloneFileBrowser.SaveFilePanel("Save Rig File", Application.persistentDataPath, FileName, FileExtension);

            DirectoryPath = Path.GetDirectoryName(FilePath);
            FileName = Path.GetFileNameWithoutExtension(FilePath);
            FullFileName = FileName + "." + FileExtension;

            Debug.Log("Saving .rig file to: " + FilePath);
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
            FilePath = StandaloneFileBrowser.OpenFilePanel("Open Rig File", Application.persistentDataPath, FileExtension, false)[0];

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

        PanelRigConfig.UpdateInputs();
    }
    private void OnApplicationQuit()
    {
        string saveFileName = "LastQuit." + FileExtension;
        string savePath = Path.Combine(Application.persistentDataPath, saveFileName);

        WriteDataInObject();
        WriteObjectToFile(savePath);
    }

    ////////////  LOAD DATA  ////////////
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
    private void ReadDataFromObject()
    {
        Base.Alpha = saveobject.base_settings.AngleAlpha;
        Base.Radius = saveobject.base_settings.Radius;

        Final.Alpha = saveobject.final_settings.AngleAlpha;
        Final.Radius = saveobject.final_settings.Radius;

        actuators.Diameter = saveobject.actuator_settings.Diameter;
        actuators.MinLength = saveobject.actuator_settings.MinLength;
        actuators.MaxLength = saveobject.actuator_settings.MaxLength;
    }

    ////////////  Save DATA  ////////////
    void WriteDataInObject()
    {
        saveobject.base_settings.AngleAlpha = Base.Alpha;
        saveobject.base_settings.Radius = Base.Radius;

        saveobject.final_settings.AngleAlpha = Final.Alpha;
        saveobject.final_settings.Radius = Final.Radius;

        saveobject.actuator_settings.Diameter = actuators.Diameter;
        saveobject.actuator_settings.MinLength = actuators.MinLength;
        saveobject.actuator_settings.MaxLength = actuators.MaxLength;
    }
    private void WriteObjectToFile(string path)
    {
        string json = JsonUtility.ToJson(saveobject,true);
        
        using (StreamWriter writer = new StreamWriter(File.Create(path)))
        {
            writer.Write(json);
        }
    }

    ////////////  Infrastructure  ////////////
    [System.Serializable]
    public class SaveObject
    {
        public PlatformSettings base_settings;
        public PlatformSettings final_settings;
        public ActuatorSettings actuator_settings;
    }

    [System.Serializable]
    public class PlatformSettings
    {
        public float Radius;
        public float AngleAlpha;
    }

    [System.Serializable]
    public class ActuatorSettings
    {
        public float Diameter;
        public float MinLength;
        public float MaxLength;
    }
}



