using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SFB;      //Standalone File Browser from GitHub (https://github.com/gkngkc/UnityStandaloneFileBrowser)

[ExecuteInEditMode]
public class LoadSaveFilters: MonoBehaviour
{
    [Header("Filters")]
    [SerializeField] HighPass Wx_HP;
    [SerializeField] LowPassNthOrder Wx_HP_LP1;

    [SerializeField] HighPass Wy_HP;
    [SerializeField] LowPassNthOrder Wy_HP_LP1;

    [SerializeField] HighPass Wz_HP;
    [SerializeField] LowPassNthOrder Wz_HP_LP1;

    [SerializeField] LowPassNthOrder Ax_LP3;
    [SerializeField] HighPass Ax_HP;
    [SerializeField] LowPassNthOrder Ax_HP_LP2;
   
    //[SerializeField] LowPassNthOrder Ay_LP3;          //unused
    [SerializeField] HighPass Ay_HP;
    [SerializeField] LowPassNthOrder Ay_HP_LP2;

    [SerializeField] LowPassNthOrder Az_LP3;
    [SerializeField] HighPass Az_HP;
    [SerializeField] LowPassNthOrder Az_HP_LP2;

    [Header("Input Panel")]
    [SerializeField] PanelMotionTuning panelMotionTuning;
    [Header("File")]
    [SerializeField] string FileName;
    [ShowOnly][SerializeField] string FileExtension;

    string FullFileName;
    string DirectoryPath;
    string FilePath;

    SaveObject saveobject;
    
    private void Start()
    {
        saveobject = new SaveObject();

        FileName = "FilterSettings";
        FileExtension = "fltrs";

        string LoadFileName = "LastQuit." + FileExtension;
        string FilePath = Path.Combine(Application.persistentDataPath, LoadFileName);

        if (File.Exists(FilePath))
        {
            saveobject = ReadObjectFromFile(FilePath);
            ReadDataFromObject();
            Debug.Log("Loading Filters:" + LoadFileName);
        }
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

        panelMotionTuning.UpdateInputs();
    }
    private void OnApplicationQuit()
    {
        string saveFileName = "LastQuit." + FileExtension;
        string savePath = Path.Combine(Application.persistentDataPath, saveFileName );
        
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
        Wx_HP.EMA_alpha = saveobject.Setting_Wx_HP;
        Wx_HP_LP1.EMA_alpha = saveobject.Setting_Wx_HP_LP1;

        Wy_HP.EMA_alpha = saveobject.Setting_Wy_HP;
        Wy_HP_LP1.EMA_alpha = saveobject.Setting_Wy_HP_LP1;

        Wz_HP.EMA_alpha = saveobject.Setting_Wz_HP;
        Wz_HP_LP1.EMA_alpha = saveobject.Setting_Wz_HP_LP1;

        Ax_LP3.EMA_alpha = saveobject.Setting_Ax_LP3;
        Ax_HP.EMA_alpha = saveobject.Setting_Ax_HP;
        Ax_HP_LP2.EMA_alpha = saveobject.Setting_Ax_HP_LP2;

        //Ay_LP3.EMA_alpha = saveobject.Setting_Ay_LP3;         //unused
        Ay_HP.EMA_alpha = saveobject.Setting_Ay_HP;
        Ay_HP_LP2.EMA_alpha = saveobject.Setting_Ay_HP_LP2;

        Az_LP3.EMA_alpha = saveobject.Setting_Az_LP3;
        Az_HP.EMA_alpha = saveobject.Setting_Az_HP;
        Az_HP_LP2.EMA_alpha = saveobject.Setting_Az_HP_LP2;
    }

    ////////////  Save DATA  ////////////
    private void WriteDataInObject()
    {
        saveobject.Setting_Wx_HP = Wx_HP.EMA_alpha;
        saveobject.Setting_Wy_HP = Wy_HP.EMA_alpha;
        saveobject.Setting_Wz_HP = Wz_HP.EMA_alpha;

        saveobject.Setting_Ax_HP = Ax_HP.EMA_alpha;
        saveobject.Setting_Ay_HP = Ay_HP.EMA_alpha;
        saveobject.Setting_Az_HP = Az_HP.EMA_alpha;

        saveobject.Setting_Wx_HP_LP1 = Wx_HP_LP1.EMA_alpha;
        saveobject.Setting_Wy_HP_LP1 = Wy_HP_LP1.EMA_alpha;
        saveobject.Setting_Wz_HP_LP1 = Wz_HP_LP1.EMA_alpha;

        saveobject.Setting_Ax_HP_LP2 = Ax_HP_LP2.EMA_alpha;
        saveobject.Setting_Ax_LP3 = Ax_LP3.EMA_alpha;

        saveobject.Setting_Ay_HP_LP2 = Ay_HP_LP2.EMA_alpha;
        //saveobject.Setting_Ay_LP3 = Ax_LP3.EMA_alpha;             //unused

        saveobject.Setting_Az_HP_LP2 = Az_HP_LP2.EMA_alpha;
        saveobject.Setting_Az_LP3 = Az_LP3.EMA_alpha;
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
        //high pass filters
        public float Setting_Wx_HP;
        public float Setting_Wy_HP;
        public float Setting_Wz_HP;

        public float Setting_Ax_HP;
        public float Setting_Ay_HP;
        public float Setting_Az_HP;

        //low pass filters
        public float Setting_Wx_HP_LP1;
        public float Setting_Wy_HP_LP1;
        public float Setting_Wz_HP_LP1;

        public float Setting_Ax_HP_LP2;
        public float Setting_Ax_LP3;

        public float Setting_Ay_HP_LP2;
        //public float Setting_Ay_LP3;

        public float Setting_Az_HP_LP2;
        public float Setting_Az_LP3;
    }
}



