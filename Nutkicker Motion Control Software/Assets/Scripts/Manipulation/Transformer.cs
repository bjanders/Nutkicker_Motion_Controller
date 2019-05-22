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
    [SerializeField] public float Offset_Sway   = 0.0f;
    [SerializeField] public float Offset_Heave  = 0.0f;
    [SerializeField] public float Offset_Surge  = 0.0f;
    [Space]
    [SerializeField] public float Offset_Yaw    = 0.0f;
    [SerializeField] public float Offset_Pitch  = 0.0f;
    [SerializeField] public float Offset_Roll   = 0.0f;

    [Header("Output")]
    [SerializeField] private float Sway = 0;
    [SerializeField] private float Heave = 0;
    [SerializeField] private float Surge = 0;
    [Space]
    [SerializeField] private float Yaw = 0;
    [SerializeField] private float Pitch = 0;
    [SerializeField] private float Roll = 0;
    
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
    
    public void On_Height_Changed(string s)
    {
        float value = Convert.ToSingle(s, GlobalVars.myNumberFormat());
        Offset_Heave = value;
    }
}
