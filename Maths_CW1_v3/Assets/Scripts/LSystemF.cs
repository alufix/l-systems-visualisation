using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TransformClass;
using UnityEngine.UI;


public class LSystemF : MonoBehaviour
{
    //[SerializeField] private float length = 10f; 
    public int iterations = 5;
    public float length = 14f;
    public float angle = 22.5f;
    public GameObject Branch;

    //set list of mats 
    public List<Material> ListOMats;

    private const string axiom = "X";

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
            { 'X', "F-[[X]+X]+F[+FX]-X" },
            { 'F', "FF" }
        };

        Anna.Helper.Generate(currentString, transform, iterations, length, angle, Branch, axiom, transformStack, rules, gameObject, ListOMats);
        //Debug.Log("Test LSystemF");

    }

    public void Reset()
    {
        iterations = 5;
        length = 14f;
        angle = 22.5f;

        parameterChange.text = " ";
    }

    public void Update()
    {
        //check for user input 
        if (Input.GetKeyDown(KeyCode.I))
        {
            //5 iterations by default
            if (iterations == 5)
            {
                iterations = 0;
            }

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
            //default angle is 22.5 so can go in increments of 5 
            if (angle < 80f)
            {
                angle += 5f;
                HotkeyGen();
                AngleUI();
            }
            else
            {
                //set angle to small 
                angle = 2.5f;
                HotkeyGen();
                AngleUI();
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            //if get L key - change Branch length (default is 14) 
            if (length < 26f)
            {
                length += 2f;
                HotkeyGen();
                LengthUI();
            }
            else
            {
                length = 2f;
                HotkeyGen();
                LengthUI();
            }

        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            //reset to default values 
            iterations = 5;
            length = 14f;
            angle = 22.5f;

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
