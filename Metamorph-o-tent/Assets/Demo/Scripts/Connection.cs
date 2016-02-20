using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Connection {
	
	List<NodeNew> nodes = new List<NodeNew> ();
	
	public Connection (NodeNew node1, NodeNew node2) {
		nodes.Add (node1);
		nodes.Add (node2);
	}
	
	public NodeNew getFirstNode() {
		return nodes[0];
	}
	
	public NodeNew getSecondNode() {
		return nodes[1];
	}
	
	public bool containsNode(NodeNew node) {
		return (node == this.getFirstNode () || node == this.getSecondNode ());
	}
	
}