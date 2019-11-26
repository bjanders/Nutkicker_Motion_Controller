using System;
using UnityEngine;

[ExecuteInEditMode]
public class Transformer : MonoBehaviour
{
    //Inspector Entries:
    [Header("Rig")]
    [SerializeField] private Transform platform;

    [Header("Streams")]
    [SerializeField] private Stream Stream_Sway;
    [SerializeField] private Stream Stream_Heave;
    [SerializeField] private Stream Stream_Surge;
    [Space]
    [SerializeField] private Stream Stream_Yaw;
    [SerializeField] private Stream Stream_Pitch;
    [SerializeField] private Stream Stream_Roll;
    
    [Header("Gains")]
    [SerializeField] public float Gain_Master    = 1.0f;
    [Space]
    [SerializeField] public float Gain_Sway     = 1.0f;
    [SerializeField] public float Gain_Heave    = 1.0f;
    [SerializeField] public float Gain_Surge    = 1.0f;
    [Space]
    [SerializeField] public float Gain_Yaw      = 1.0f;
    [SerializeField] public float Gain_Pitch    = 1.0f;
    [SerializeField] public float Gain_Roll     = 1.0f;

    [Header("Offsets")]
    [SerializeField] public float Offset_Sway;
    [SerializeField] public float Offset_Heave;
    [SerializeField] public float Offset_Surge;
    [Space]
    [SerializeField] public float Offset_Yaw;
    [SerializeField] public float Offset_Pitch;
    [SerializeField] public float Offset_Roll;

    [Header("Output")]
    [SerializeField] private float Sway;
    [SerializeField] private float Heave;
    [SerializeField] private float Surge;
    [Space]
    [SerializeField] private float Yaw;
    [SerializeField] private float Pitch;
    [SerializeField] private float Roll;
    
    //////////////////////////////////////////////////////////
    private void Awake()
    {
        platform = GetComponent<Transform>();
    }
    private void Start()
    {
        platform.localPosition = new Vector3(Offset_Sway, Offset_Heave, Offset_Surge);
    }
    private void Update()
    {
        //Start at zero:
        Sway =  0;
        Heave = 0;
        Surge = 0;
        Yaw =   0;
        Pitch = 0;
        Roll =  0;
        
        //Add the offsets:
        Surge   += Offset_Surge;
        Sway    += Offset_Sway;
        Heave   += Offset_Heave;
        Yaw     += Offset_Yaw;
        Pitch   += Offset_Pitch;
        Roll    += Offset_Roll;

        //Anything to add from the streams?
        if (Stream_Surge != null) Surge +=  Stream_Surge.Youngest.Datavalue     * Gain_Surge    * Gain_Master;
        if (Stream_Sway != null) Sway +=    Stream_Sway.Youngest.Datavalue      * Gain_Sway     * Gain_Master;
        if (Stream_Heave != null) Heave +=  Stream_Heave.Youngest.Datavalue     * Gain_Heave    * Gain_Master;
        if (Stream_Yaw != null) Yaw +=      Stream_Yaw.Youngest.Datavalue       * Gain_Yaw      * Gain_Master;
        if (Stream_Pitch != null) Pitch +=  Stream_Pitch.Youngest.Datavalue     * Gain_Pitch    * Gain_Master;
        if (Stream_Roll != null) Roll +=    Stream_Roll.Youngest.Datavalue      * Gain_Roll     * Gain_Master;
        
        //Show it to the user:
        platform.localPosition = new Vector3(Sway, Heave, Surge);
        platform.localEulerAngles = new Vector3(Pitch, Yaw, Roll);
    }
    //////////////////////////////////////////////////////////
    
    public void On_OffsetHeaveChanged(string s)
    {
        Offset_Heave = Convert.ToSingle(s, GlobalVars.myNumberFormat());
    }

    public void On_GainSwayChanged(string s)
    {
        Gain_Sway = Convert.ToSingle(s, GlobalVars.myNumberFormat());
    }
    public void On_GainHeaveChanged(string s)
    {
        Gain_Heave = Convert.ToSingle(s, GlobalVars.myNumberFormat());
    }
    public void On_GainSurgeChanged(string s)
    {
        Gain_Surge = Convert.ToSingle(s, GlobalVars.myNumberFormat());
    }

    public void On_GainYawChanged(string s)
    {
        Gain_Yaw = Convert.ToSingle(s, GlobalVars.myNumberFormat());
    }
    public void On_GainPitchChanged(string s)
    {
        Gain_Pitch = Convert.ToSingle(s, GlobalVars.myNumberFormat());
    }
    public void On_GainRollChanged(string s)
    {
        Gain_Roll = Convert.ToSingle(s, GlobalVars.myNumberFormat());
    }
}
