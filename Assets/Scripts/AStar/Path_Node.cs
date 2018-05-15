using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path_Node<T> {
	public T data;
	Path_Edge<T>[] edges; //Nodes leading out from this node
	//Path_Node<Tile> somenodetilething
}
