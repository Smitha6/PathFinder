using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour {

    Vector2 targetPos;
    //starting positions of top left node
    private static int startX = -6;
    private static int startY = -3;

    private int xPos = startX;
    private int yPos = startY;
    private int spacing = 2;

    // Use this for initialization
    void Start () {
        GameObject node = new GameObject();
        SpriteRenderer renderer = node.AddComponent<SpriteRenderer>();
        renderer.sprite = Resources.Load("Sprites/node_sprite", typeof(Sprite)) as Sprite;


        int rows = PathFinder.MATRIX_ROWS / 2;
        int cols = PathFinder.MATRIX_ROWS / 2;

        //draws grid
        for (int i = 0; i < rows; i++) {
            changePos(node, startX, yPos);

            xPos = startX;
            for (int j = 0; j < cols - 1; j++) {
                xPos += spacing;
                Instantiate(node);
                changePos(node, xPos, yPos);
            }
            Instantiate(node);
            yPos += spacing;
        }


    }

    // Update is called once per frame
    void Update () {
		
	}

    //changes the position of an object
    void changePos(GameObject obj, int x, int y){
        targetPos = obj.transform.position;
        targetPos.x = x;
        targetPos.y = y;
        obj.transform.position = targetPos;
    }
}
