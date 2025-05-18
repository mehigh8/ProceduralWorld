using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformInfo
{
    public Vector3 position;
    public Quaternion rotation;

    public TransformInfo(Vector3 position, Quaternion rotation)
    {
        this.position = position;
        this.rotation = rotation;
    }
}

public class LSystem : MonoBehaviour
{
    public float plantSize;
    public float branchAngle;
    public int iterations;
    public GameObject branch;
    public GameObject turtle;

    private string axiom = "X";
    private Stack<TransformInfo> transformStack;
    private Dictionary<char, string> rules;
    private string currentString = "";

    void Start()
    {
        transformStack = new Stack<TransformInfo>();
        rules = new Dictionary<char, string>
        {
            {'X', "[*+FX]X[+FX][/+F-FX]" },
            {'F', "FF" }
        };

        Generate();
    }

    private void Generate()
    {
        currentString = axiom;

        string newString = "";

        for (int i = 0; i < iterations; i++)
        {
            foreach (char c in currentString)
                newString += rules.ContainsKey(c) ? rules[c] : c.ToString();

            currentString = newString;
        }

        foreach (char c in currentString)
        {
            switch (c)
            {
                case 'F':
                    Vector3 initialPosition = turtle.transform.position;
                    turtle.transform.Translate(Vector3.up * plantSize);

                    GameObject currentBranch = Instantiate(branch, transform);
                    LineRenderer branchLineRenderer = currentBranch.GetComponent<LineRenderer>();
                    branchLineRenderer.SetPosition(0, initialPosition);
                    branchLineRenderer.SetPosition(1, turtle.transform.position);
                    break;
                case 'X':
                    break;
                case '+':
                    turtle.transform.Rotate(Vector3.back * branchAngle);
                    break;
                case '-':
                    turtle.transform.Rotate(Vector3.forward * branchAngle);
                    break;
                case '*':
                    turtle.transform.Rotate(Vector3.up * 120);
                    break;
                case '/':
                    turtle.transform.Rotate(Vector3.down * 120);
                    break;
                case '[':
                    transformStack.Push(new TransformInfo(turtle.transform.position, turtle.transform.rotation));
                    break;
                case ']':
                    TransformInfo ti = transformStack.Pop();
                    turtle.transform.position = ti.position;
                    turtle.transform.rotation = ti.rotation;
                    break;
                default:
                    Debug.LogError("Invalid LSystem Operation");
                    break;
            }
        }
    }

    void Update()
    {
        
    }
}
