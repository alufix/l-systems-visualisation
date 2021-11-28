using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System; 

public class UIScript : MonoBehaviour
{
    //variable for LSystem that is currently selected/active 
    public int selectedLSystem = 0;
    //--> Hierarchy order means LSystemA is 0, LSystemB is 1, etc. 

    //reference all the parameter menus: 
    public GameObject Menu1; 
    public GameObject Menu2;
    public GameObject Menu3;
    public GameObject Menu4;
    public GameObject Menu5;
    public GameObject Menu6;
    public GameObject Menu7;
    public GameObject Menu8;

    //reference the L-Systems (to call reset function) 
    public LSystemA A;
    public LSystemB B;
    public LSystemC C;
    public LSystemD D;
    public LSystemE E;
    public LSystemF F;
    public LSystemG G;
    public LSystemH H;


    public void Start()
    {
        //call the method: 
        SelectLSystem();

        //set Menu1 active and rest inactive: 
        Menu1.SetActive(true);
        Menu2.SetActive(false);
        Menu3.SetActive(false);
        Menu4.SetActive(false);
        Menu5.SetActive(false);
        Menu6.SetActive(false);
        Menu7.SetActive(false);
        Menu8.SetActive(false);
    }

    //need to use update because need to be constantly checking for user input 
    public void Update() 
    { 
        int previousSelectedLSystem = selectedLSystem; 

            //(script calls the Helper function so it will also send through its parameters) 
            //(script deals with all the further user inputs when it's loaded, i.e. A, S, D) 
        

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedLSystem = 0;
            Menu1.SetActive(true);
            Menu2.SetActive(false);
            Menu3.SetActive(false);
            Menu4.SetActive(false);
            Menu5.SetActive(false);
            Menu6.SetActive(false);
            Menu7.SetActive(false);
            Menu8.SetActive(false);
            A.Reset(); 
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectedLSystem = 1;
            Menu1.SetActive(false);
            Menu2.SetActive(true);
            Menu3.SetActive(false);
            Menu4.SetActive(false);
            Menu5.SetActive(false);
            Menu6.SetActive(false);
            Menu7.SetActive(false);
            Menu8.SetActive(false);
            B.Reset(); 
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            selectedLSystem = 2;
            Menu1.SetActive(false);
            Menu2.SetActive(false);
            Menu3.SetActive(true);
            Menu4.SetActive(false);
            Menu5.SetActive(false);
            Menu6.SetActive(false);
            Menu7.SetActive(false);
            Menu8.SetActive(false);
            C.Reset(); 
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4)
        {
            selectedLSystem = 3;
            Menu1.SetActive(false);
            Menu2.SetActive(false);
            Menu3.SetActive(false);
            Menu4.SetActive(true);
            Menu5.SetActive(false);
            Menu6.SetActive(false);
            Menu7.SetActive(false);
            Menu8.SetActive(false);
            D.Reset(); 
        }

        if (Input.GetKeyDown(KeyCode.Alpha5) && transform.childCount >= 5)
        {
            selectedLSystem = 4;
            Menu1.SetActive(false);
            Menu2.SetActive(false);
            Menu3.SetActive(false);
            Menu4.SetActive(false);
            Menu5.SetActive(true);
            Menu6.SetActive(false);
            Menu7.SetActive(false);
            Menu8.SetActive(false);
            E.Reset(); 
        }

        if (Input.GetKeyDown(KeyCode.Alpha6) && transform.childCount >= 6)
        {
            selectedLSystem = 5;
            Menu1.SetActive(false);
            Menu2.SetActive(false);
            Menu3.SetActive(false);
            Menu4.SetActive(false);
            Menu5.SetActive(false);
            Menu6.SetActive(true);
            Menu7.SetActive(false);
            Menu8.SetActive(false);
            F.Reset(); 
        }

        if (Input.GetKeyDown(KeyCode.Alpha7) && transform.childCount >= 7)
        {
            selectedLSystem = 6;
            Menu1.SetActive(false);
            Menu2.SetActive(false);
            Menu3.SetActive(false);
            Menu4.SetActive(false);
            Menu5.SetActive(false);
            Menu6.SetActive(false);
            Menu7.SetActive(true);
            Menu8.SetActive(false);
            G.Reset(); 
        }

        if (Input.GetKeyDown(KeyCode.Alpha8) && transform.childCount >= 8)
        {
            selectedLSystem = 7;
            Menu1.SetActive(false);
            Menu2.SetActive(false);
            Menu3.SetActive(false);
            Menu4.SetActive(false);
            Menu5.SetActive(false);
            Menu6.SetActive(false);
            Menu7.SetActive(false);
            Menu8.SetActive(true);
            H.Reset(); 
        }


        //------------------- end number keys ----------------------

        if (previousSelectedLSystem != selectedLSystem)
        {
            SelectLSystem(); 
        }
    }

    void SelectLSystem ()
    {
        //start by making an index because 'foreach' function doesn't have one 
        int i = 0; 

        //if L-System's index does not match selected L-System, then disable it 
        //and if it does match, then enable it 
        //take each of the transforms that are children to the object 
        //and loop through each one, referring to the current one as LSystem 
        foreach (Transform LSystem in transform)
        {
            //check if selected L-System matches the index 
            //this means only one L-System is active at any one time 

            if (i == selectedLSystem) 
                LSystem.gameObject.SetActive(true);
            else
                LSystem.gameObject.SetActive(false); 
            
            //each time you loop through (i.e. each LSystem you go through), add 1 to index 
            i++; 
        }
    }
}
