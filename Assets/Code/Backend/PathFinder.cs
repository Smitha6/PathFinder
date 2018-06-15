using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour {

    public const int MATRIX_ROWS = 6;  //number of nodes
    public const int MATRIX_COLUMNS = MATRIX_ROWS;
    public int[,] matrix = new int[MATRIX_ROWS, MATRIX_COLUMNS];

    // Use this for initialization
    void Start () {

        
        
        int ran;

        //fills out matrix.  1 == path  0 == no path
        for (int i = 0; i < MATRIX_ROWS; i++){
            for (int j = 0; j < MATRIX_COLUMNS; j++){

                //licklihood of there being a connection bewteen 2 nodes
                ran = Random.Range(0, 2);  //(wont ever use max num, 50-50 == 0->2)
                if (ran == 0){
                    matrix[i,j] = 1;
                }
                else{
                    matrix[i, j] = 0;
                }
                
            }
        }
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
