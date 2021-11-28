using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using TransformClass;

//setting namespace to be accessed in other scripts 
namespace Anna
{

    //main function of this script 
    public class Helper 
    {

        // 'Generate' function that generates a string 
        // Includes 'SET STRING' and 'DRAW L-SYSTEM' 
        static public void Generate(string currentString, Transform transform, int iterations, float length,
            float angle, GameObject Branch, string axiom, Stack<TransformInfo> transformStack,
            Dictionary<char, string> rules, GameObject gameObject, List<Material> ListOMats)
        {

            //set level that changes with each push 
            int level = 0;
            int numMats = ListOMats.Count;
            //then in Inspector, add the materials to the list 
            

        //--------------------------- SET STRING ---------------------------
        currentString = axiom;

            StringBuilder sb = new StringBuilder();

            // to do several iterations (for as many as variable is set to): 
            for (int i = 0; i < iterations; i++)
            {
                foreach (char c in currentString)
                {
                    sb.Append(rules.ContainsKey(c) ? rules[c] : c.ToString());
                }

                // now update string: 
                currentString = sb.ToString();

                // and then reset string builder: 
                sb = new StringBuilder();
            }


            //--------------------------- DRAW L-SYSTEM -----------------------------

            // 'Draw' function that receives the variables/paramters that are fed in from external scripts 
            {

                // loop through values again: 
                // do something different for each value, depending on what it is 
                foreach (char c in currentString)
                {
                    switch (c)
                    {
                        case 'F':
                            //store initial position - which is equal to our TreeSpawner 
                            Vector3 initialPosition = transform.position;

                            //draw straight line forward 
                            transform.Translate(Vector3.up * length);

                            //now draw the branch line: 
                            //gameObject.transform to ensure that the insantiated Branch belongs to the L-System game object 
                            //i.e. so that when UI moves to the next LSystem, the previously rendered Branches disappear with it 
                            GameObject treeSegment = GameObject.Instantiate(Branch, gameObject.transform);
                            treeSegment.GetComponent<LineRenderer>().SetPosition(0, initialPosition);
                            treeSegment.GetComponent<LineRenderer>().SetPosition(1, transform.position);

                            break;

                        case 'X':
                            break;

                        case 'Y':
                            break;

                        case 'Z':
                            break;

                        case 'V':
                            break;

                        case 'W':
                            break;

                        case '+':
                            // rotate clockwise: 
                            transform.Rotate(Vector3.forward * angle);
                            break;

                        case '-':
                            // rotate anticlockwise: 
                            //(could also do: transform.Rotate(Vector3.back * angle);) 
                            transform.Rotate(Vector3.forward * -(angle));
                            break;

                        case '[':
                            // save our transform info which is stored inside stack: 
                            transformStack.Push(new TransformInfo()
                            {
                                position = transform.position,
                                rotation = transform.rotation, 
                            });
                            level += 1;
                            Branch.GetComponent<Renderer>().material = ListOMats[level%numMats];
                            break;

                        case ']':
                            // go back to our previously-saved transform info values: 
                            // create new variable TransformInfo ti 
                            TransformInfo ti = transformStack.Pop();

                            // then set the position and rotation values: 
                            transform.position = ti.position;
                            transform.rotation = ti.rotation;

                            level -= 1; 
                            break;

                        //and last default one: 
                        default:
                            throw new InvalidOperationException("Invalid L-Tree Operation");
                    }
                }
            }
        }
    }
}

