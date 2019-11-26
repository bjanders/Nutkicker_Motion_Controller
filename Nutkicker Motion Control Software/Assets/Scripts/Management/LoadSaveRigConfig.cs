﻿using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using SFB;      //Standalone File Browser from GitHub (https://github.com/gkngkc/UnityStandaloneFileBrowser)

[ExecuteInEditMode]
public class LoadSaveRigConfig : MonoBehaviour
{
    [Header("Hardware")]
    [SerializeField] Platform Base;
    [SerializeField] Platform Final;
    [SerializeField] Platform ParkPosition;
    [SerializeField] Platform NeutralPosition;
    [SerializeField] Actuators actuators;
    [SerializeField] ServoManager servomanager;
    [Header("Input Panel")]
    [SerializeField] PanelRigConfig PanelRigConfig;
    [SerializeField] Toggle toggle_IsCrankArmSystem;
    [SerializeField] Toggle toggle_IsFlippedCranks;

    [Header("File")]
    [SerializeField] string FileName = "Rig_Settings";
    [ShowOnly] [SerializeField] string FileExtension = "rig";

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
            //Debug.Log("Loading Rig Config:" + LoadFileName);
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
        Base.Alpha = saveobject.base_settings.AngleAlpha;
        Base.Radius = saveobject.base_settings.Radius;

        Final.Alpha = saveobject.final_settings.AngleAlpha;
        Final.Radius = saveobject.final_settings.Radius;

        actuators.Diameter = saveobject.actuator_settings.Diameter;
        actuators.MinLength = saveobject.actuator_settings.MinLength;
        actuators.MaxLength = saveobject.actuator_settings.MaxLength;

        //Positions:
        ParkPosition.transform.localPosition = saveobject.park_position;
        NeutralPosition.transform.localPosition = saveobject.neutral_position;

        //Cranks:
        toggle_IsCrankArmSystem.isOn = saveobject.isCrankArmSystem;
        toggle_IsFlippedCranks.isOn = saveobject.crankarm_settings.IsFlipped;

        servomanager.azimuth = saveobject.crankarm_settings.Azimuth;
        servomanager.crank_Length = saveobject.crankarm_settings.CrankLength;
        servomanager.rod_Length = saveobject.crankarm_settings.RodLength;
    }

    ////////////  Save DATA  ////////////
    private void WriteDataInObject()
    {
        saveobject.base_settings.AngleAlpha = Base.Alpha;
        saveobject.base_settings.Radius = Base.Radius;

        saveobject.final_settings.AngleAlpha = Final.Alpha;
        saveobject.final_settings.Radius = Final.Radius;

        saveobject.actuator_settings.Diameter = actuators.Diameter;
        saveobject.actuator_settings.MinLength = actuators.MinLength;
        saveobject.actuator_settings.MaxLength = actuators.MaxLength;

        //Positions:
        saveobject.park_position = ParkPosition.transform.localPosition;
        saveobject.neutral_position = NeutralPosition.transform.localPosition;

        //Cranks:
        saveobject.isCrankArmSystem = toggle_IsCrankArmSystem.isOn;
        saveobject.crankarm_settings.IsFlipped = toggle_IsFlippedCranks.isOn;

        saveobject.crankarm_settings.Azimuth = servomanager.azimuth;
        saveobject.crankarm_settings.CrankLength = servomanager.crank_Length;
        saveobject.crankarm_settings.RodLength = servomanager.rod_Length;
        saveobject.crankarm_settings.IsFlipped = servomanager.FlipCranks;
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
        public PlatformSettings base_settings = new PlatformSettings();
        public PlatformSettings final_settings = new PlatformSettings();
        public ActuatorSettings actuator_settings = new ActuatorSettings();

        public Vector3 park_position = new Vector3();
        public Vector3 neutral_position = new Vector3();

        public bool isCrankArmSystem = false;
        public CrankArmSettings crankarm_settings = new CrankArmSettings();
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

    [System.Serializable]
    public class CrankArmSettings
    {
        public float Azimuth;
        public float CrankLength;
        public float RodLength;
        public bool IsFlipped;
    }
}



