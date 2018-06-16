using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour {

    Vector2 targetPos;
    //starting positions of top left node
    private static int startX = -6;
    private static int startY = 3;

    private int wrapPos = 5; //0 - 8

    private int xPos = startX;
    private int yPos = startY;
    private int spacing = 2;

    private Color nodeColor = new Color32(144, 104, 190, 255);
    private Color lineCollor = new Color32(110, 211, 207, 255);
    private Color borderColor = new Color32(230, 39, 57, 255);

    public GameObject startNode;
    public GameObject endNode;

    public AudioSource click;
    

    // Use this for initialization
    void Start () {

        

        DrawGrid();
        DrawConnections();
        RemoveHangingNodes();
        
    }

    // Update is called once per frame
    void Update () {

        

        //gets node under mouse click
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 origin = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                         Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.zero, 0f);
            //get start node
            if (hit && startNode == null) {
                Debug.Log("IN1");

                startNode = hit.transform.gameObject;

                GameObject border = new GameObject();
                SpriteRenderer renderer = border.AddComponent<SpriteRenderer>();
                renderer.color = borderColor;
                renderer.sprite = Resources.Load("Sprites/node_sprite_border", typeof(Sprite)) as Sprite;
                renderer.sortingOrder = 0;

                border.transform.position = startNode.transform.position;

                click = GetComponent<AudioSource>();
                click.Play();
            }
            //get end node
            else if (startNode != null && endNode == null){

                endNode = hit.transform.gameObject;

                GameObject border = new GameObject();
                SpriteRenderer renderer = border.AddComponent<SpriteRenderer>();
                renderer.color = borderColor;
                renderer.sprite = Resources.Load("Sprites/node_sprite_border", typeof(Sprite)) as Sprite;
                renderer.sortingOrder = 0;

                border.transform.position = endNode.transform.position;

                click = GetComponent<AudioSource>();
                click.Play();

                //----RUN PATH FINDER HERE----
            }
                
        }
    }


    //changes the position of a node
    void ChangePos(GameObject obj, int x, int y){
        targetPos = obj.transform.position;
        targetPos.x = x;
        targetPos.y = y;
        obj.transform.position = targetPos;
    }

    void DrawGrid() {
        int numNodes = PathFinder.matrix.GetLength(0); //1 for cols

        GameObject node = new GameObject();
        BoxCollider2D boxCollider2D = node.AddComponent<BoxCollider2D>();
        SpriteRenderer renderer = node.AddComponent<SpriteRenderer>();
        renderer.color = nodeColor;
        renderer.sortingOrder = 1;
        renderer.sprite = Resources.Load("Sprites/node_sprite", typeof(Sprite)) as Sprite;
        
        for (int i = 0; i < numNodes; i++) {
            ChangePos(node, xPos, yPos);
            node.name = i + "";

            if (xPos > wrapPos)
            {
                xPos = startX;
                yPos -= spacing;
            }
            else
            {
                xPos += spacing;
            }
            
            if (i < numNodes - 1)
                Instantiate(node);
        }
        
    }

    void DrawLine(Vector2 start, Vector2 end, string name)
    {
        GameObject line = new GameObject();
        line.transform.position = start;
        line.AddComponent<LineRenderer>();
        line.name = name;
        LineRenderer lr = line.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.SetColors(lineCollor, lineCollor);
        lr.SetWidth(0.1f, 0.1f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }

    void DrawConnections() {

        int numNodes = PathFinder.MATRIX_ROWS;
        GameObject nodeStart;
        GameObject nodeEnd;

        for (int i = 0; i < numNodes; i++) {

            nodeStart = GetNode(i);

            for (int j = 0; j < PathFinder.MATRIX_COLUMNS; j++) {

                if (PathFinder.matrix[i, j] == 1) {
                    
                    nodeEnd = GetNode(j);

                    Vector2 start = nodeStart.transform.position;
                    Vector2 end = nodeEnd.transform.position;
                    DrawLine(start, end, j + "");  //connection is named after the node it points to
                }
                
            }
        }
        

    }

    void RemoveHangingNodes(){
        int numCon = 0;
        GameObject node;

        for(int i = 0; i < PathFinder.MATRIX_ROWS; i++) {

            for (int j = 0; j < PathFinder.MATRIX_COLUMNS; j++) {

                if(PathFinder.matrix[i, j] == 1){
                    numCon++;
                }

            }

            if(numCon == 0){
                node = GetNode(i);
                Destroy(node);
            }
            numCon = 0;

        }
    }

    public GameObject GetNode(int name) {
        GameObject node;
        string sName = name + "";


        //used because one node wont be a clone
        if (GameObject.Find(sName + "(Clone)") != null) 
            node = GameObject.Find(sName + "(Clone)");
        else
            node = GameObject.Find(sName);

        return node;
    }





}