using UnityEngine;

[ExecuteInEditMode]
public class MessageGenerator : MonoBehaviour
{
    [SerializeField] private Actuator Act1;
    [SerializeField] private Actuator Act2;
    [SerializeField] private Actuator Act3;
    [SerializeField] private Actuator Act4;
    [SerializeField] private Actuator Act5;
    [SerializeField] private Actuator Act6;
    [Space]
    [SerializeField] public string Message;
    [Range(3,7)]
    [SerializeField] private int precision = 6;
    private char starter =      '<';
    private char seperator =    ';';
    private char terminator =   '>';
    
    void FixedUpdate()
    {
        ComposeMessage();
    }

    void ComposeMessage()
    {
        //read the values from the actuators
        string part1 = Act1.Utilisation.ToString(GlobalVars.myNumberFormat());
        string part2 = Act2.Utilisation.ToString(GlobalVars.myNumberFormat());
        string part3 = Act3.Utilisation.ToString(GlobalVars.myNumberFormat());
        string part4 = Act4.Utilisation.ToString(GlobalVars.myNumberFormat());
        string part5 = Act5.Utilisation.ToString(GlobalVars.myNumberFormat());
        string part6 = Act6.Utilisation.ToString(GlobalVars.myNumberFormat());

        //chop down the string size to the required precision
        part1 = setPrecision(part1, precision);
        part2 = setPrecision(part2, precision);
        part3 = setPrecision(part3, precision);
        part4 = setPrecision(part4, precision);
        part5 = setPrecision(part5, precision);
        part6 = setPrecision(part6, precision);
        
        //Compose the message
        Message = starter + part1 + seperator +
                            part2 + seperator +
                            part3 + seperator +
                            part4 + seperator +
                            part5 + seperator +
                            part6 + terminator;
    }

    string setPrecision(string s, int p)
    {
        if (p<3)
        {
            Debug.LogError("Precision of the strings in the message must be minimum 3 to have at least 1 digit after the decimal");
        }
        if (p > s.Length)
        {
            return s;
        }
        else
        {
            return  s.Substring(0, p);
        }
        
    }
}
