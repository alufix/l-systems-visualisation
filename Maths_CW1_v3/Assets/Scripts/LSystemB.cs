using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TransformClass;
using UnityEngine.UI;


public class LSystemB : MonoBehaviour
{
    //[SerializeField] private float length = 10f; 
    public int iterations = 5;
    public float length = 20f;
    public float angle = 20f;
    public GameObject Branch;

    //set list of mats 
    public List<Material> ListOMats;

    private const string axiom = "F";

    private Stack<TransformInfo> transformStack;
    private Dictionary<char, string> rules;
    private string currentString = string.Empty;

    private Vector3 startingPosition;
    private Quaternion startingRotation;

    [Header("UIText")]
    public Text parameterChange;

    public void Start()
    {
        startingPosition = transform.position;
        startingRotation = transform.rotation;

        transformStack = new Stack<TransformInfo>();

        rules = new Dictionary<char, string>
        {
            { 'F', "F[+F]F[-F][F]" }
        };

        Anna.Helper.Generate(currentString, transform, iterations, length, angle, Branch, axiom, transformStack, rules, gameObject, ListOMats);
        //Debug.Log("Test LSystemB"); 
    }


    public void Reset()
    {
        iterations = 5;
        length = 20f;
        angle = 20f;
        //HotkeyGen();
        parameterChange.text = " ";
    }


    public void Update()
    {
        //check for user input 
        if (Input.GetKeyDown(KeyCode.I))
        {
            //if on 5 iterations (default), don't want it to grow more or Unity will crash 
            //so if on 5, and user presses I, reset iterations to 0 
            if (iterations == 5)
            {
                iterations = 0;
            }

            //then if iterations are 0, 1, 2, 3, or 4, will add one 
            //until it becomes 5 again in which case it will go to the first function and repeat 
            if (iterations >= 0)
            {
                iterations += 1;
            }

            //call HotkeyGen function (to reset transform, delete old tree, and create new tree)
            HotkeyGen();
            IterationsUI(); 
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            //default angle is 20 so can go in increments of 5 
            if (angle < 70f) 
            {
                angle += 5f;
                HotkeyGen();
                AngleUI(); 
            }
            else
            {
                //set angle to small 
                angle = 0f;
                HotkeyGen();
                AngleUI(); 
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            //if get L key - change Branch length (default is 20) 
            if (length < 30f)
            {
                length += 3f;
                HotkeyGen();
                LengthUI(); 
            }
            else
            {
                length = 3f;
                HotkeyGen();
                LengthUI(); 
            }

        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            //reset to default values 
            iterations = 5;
            length = 20f;
            angle = 20f;

            //call HotkeyGen function
            HotkeyGen();
            ResetUI(); 
        }

    }

    // ------------- HOTKEY GENERATOR FUNCTION (TO CLEAN UP AND GENERATE NEW TREE) --------------
    public void HotkeyGen()
    {
        //make new transform stack 
        transformStack = new Stack<TransformInfo>();
        transform.position = startingPosition;
        transform.rotation = startingRotation;

        //delete old rendering: 
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }

        //generate new one: 
        Anna.Helper.Generate(currentString, transform, iterations, length, angle, Branch, axiom, transformStack, rules, gameObject, ListOMats);
    }

    public void ResetUI()
    {
        parameterChange.text = "(Reset)";
    }

    public void IterationsUI()
    {
        string iterationString = iterations.ToString();
        parameterChange.text = "Iterations: " + iterationString;
    }

    public void AngleUI()
    {
        string angleString = angle.ToString();
        parameterChange.text = "Angle: " + angleString;
    }

    public void LengthUI()
    {
        string lengthString = length.ToString();
        parameterChange.text = "Length: " + lengthString;
    }
}
