using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour {

    public const int MATRIX_ROWS = 25;  //number of nodes (4)
    public const int MATRIX_COLUMNS = MATRIX_ROWS;
    public static int[,] matrix = new int[MATRIX_ROWS, MATRIX_COLUMNS];
    private int ran;

    // Use this for initialization
    void Start()
    {

    }

    //first to run
    void Awake() {



        int MATRIX_ROWS = Random.Range(10, 30);


        //fills out matrix.  1 == path  0 == no path
        for (int i = 0; i < MATRIX_ROWS; i++){
            for (int j = 0; j < MATRIX_COLUMNS; j++){

                //licklihood of there being a connection bewteen 2 nodes
                //will alwasy have 2 way connections
                ran = Random.Range(0, 20);  //(wont ever use max num, 50-50 == 0->2)
                if (ran == 0){
                    matrix[i, j] = 1;
                    matrix[j, i] = 1;
                }
                else{
                    matrix[i, j] = 0;
                    matrix[j, i] = 0;
                }
                
            }
        }
        
        /*
        matrix[0, 0] = 0;
        matrix[0, 1] = 1;
        matrix[0, 2] = 1;
        matrix[0, 3] = 1;

        matrix[1, 0] = 1;
        matrix[1, 1] = 0;
        matrix[1, 2] = 1;
        matrix[1, 3] = 1;

        matrix[2, 0] = 1;
        matrix[2, 1] = 1;
        matrix[2, 2] = 0;
        matrix[2, 3] = 1;

        matrix[3, 0] = 1;
        matrix[3, 1] = 1;
        matrix[3, 2] = 1;
        matrix[3, 3] = 0;
        */

    }
	
	// Update is called once per frame
	void Update () {
		
	}


}
