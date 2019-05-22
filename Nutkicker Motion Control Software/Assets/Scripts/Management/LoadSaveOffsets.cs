using UnityEngine;
using System.IO;
using SFB;      //Standalone File Browser from GitHub (https://github.com/gkngkc/UnityStandaloneFileBrowser)

[ExecuteInEditMode]
public class LoadSaveOffsets : MonoBehaviour
{
    [Header("Hardware")]
    [SerializeField] Transformer Height;
    [SerializeField] Transformer Mot_LFC;
    [SerializeField] Transformer Mot_HFC;
    [SerializeField] Transformer Final;
    [Header("Inputs")]
    [SerializeField] PanelOffsets paneloffsets;
    [Header("File")]
    [SerializeField] string FileName = "Offset_Settings";
    [ShowOnly] [SerializeField] string FileExtension = "offsets";

    string FullFileName;
    string DirectoryPath;
    string FilePath;

    SaveObject saveobject;
    
    private void Start()
    {
        string LoadFileName = "LastQuit." + FileExtension;
        string FilePath = Path.Combine(Application.persistentDataPath, LoadFileName);

        if (File.Exists(FilePath))
        {
            saveobject = ReadObjectFromFile(FilePath);
            ReadDataFromObject();
            Debug.Log("Loading Offsets:" + LoadFileName);
        }
        else
        {
            saveobject = new SaveObject();
        }
    }

    public void OnClickSave()
    {
        FullFileName = FileName + "." + FileExtension;
        Debug.Log("Saving: " + FullFileName);
        try
        {
            FilePath = StandaloneFileBrowser.SaveFilePanel("Save Offset File", Application.persistentDataPath, FileName, FileExtension);

            DirectoryPath = Path.GetDirectoryName(FilePath);
            FileName = Path.GetFileNameWithoutExtension(FilePath);
            FullFileName = FileName + "." + FileExtension;

            Debug.Log("Saving to: " + "." + FilePath);
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

        paneloffsets.UpdateInputs();
    }
    private void OnApplicationQuit()
    {
        string saveFileName = "LastQuit." + FileExtension;
        string savePath = Path.Combine(Application.persistentDataPath, saveFileName);

        WriteDataInObject();
        WriteObjectToFile(savePath);
    }

    ////////////  LOAD DATA  ////////////
    private void ReadDataFromObject()
    {
        LoadOffsets(Height, saveobject.Offsets_Height);
        LoadOffsets(Mot_LFC, saveobject.Offsets_LFC);
        LoadOffsets(Mot_HFC, saveobject.Offsets_HFC);
        LoadOffsets(Final, saveobject.Offsets_Final);
    }
    private void LoadOffsets(Transformer transformer, Offsets offsets)
    {
        transformer.Offset_Sway = offsets.Sway;
        transformer.Offset_Heave = offsets.Heave;
        transformer.Offset_Surge = offsets.Surge;
        transformer.Offset_Yaw = offsets.Yaw;
        transformer.Offset_Pitch = offsets.Pitch;
        transformer.Offset_Roll = offsets.Roll;
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
    private void WriteDataInObject()
    {
        SaveOffsets(Height, saveobject.Offsets_Height);
        SaveOffsets(Mot_LFC, saveobject.Offsets_LFC);
        SaveOffsets(Mot_HFC, saveobject.Offsets_HFC);
        SaveOffsets(Final, saveobject.Offsets_Final);
    }
    private void SaveOffsets(Transformer transformer, Offsets offsets)
    {
        offsets.Sway = transformer.Offset_Sway;
        offsets.Heave = transformer.Offset_Heave;
        offsets.Surge = transformer.Offset_Surge;
        offsets.Yaw = transformer.Offset_Yaw;
        offsets.Pitch = transformer.Offset_Pitch;
        offsets.Roll = transformer.Offset_Roll;
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
        public Offsets Offsets_Height = new Offsets();
        public Offsets Offsets_LFC = new Offsets();
        public Offsets Offsets_HFC  = new Offsets();
        public Offsets Offsets_Final = new Offsets();
    }
    
    [System.Serializable]
    public class Offsets
    {
        public float Sway;
        public float Heave;
        public float Surge;
        public float Yaw;
        public float Pitch;
        public float Roll;
    }
}



