using UnityEngine;
using System.Collections;

public class Line : MonoBehaviour {

	Connection conn;
	// Use this for initialization

	public void setConnection(Connection conn) {
		this.conn = conn;
	}

	public Connection getConnection() {
		return this.conn;
	}
}
