using UnityEngine;
using System.Collections;

public class AnimalCrossingMap : MonoBehaviour
{
	const float worldDepth = 20.0f;
	const float worldHeight = 2.0f;

	// Use this for initialization
	void Start ()
	{
		CirclePolar();
	}
	
	void Update()
	{
	}
	
	void init()
	{
		
		var mesh = this.GetComponent<MeshFilter>();
		Debug.Log((mesh != null) +"::"+ mesh.mesh.vertexCount);
		
		var str = "";
		
		Vector3[] verts = new Vector3[mesh.mesh.vertexCount];
		int i = 0;
		foreach(var v in mesh.mesh.vertices)
		{
			var diffZ = (v.z*transform.localScale.z) + (this.transform.position.z);
			var per = diffZ/worldDepth;
			if (per > 1.0f) per = 1.0f;
			else if (per < -1.0f) per = -1.0f;
			
			//str += "("+v.x+","+v.y+","+v.z+")";
			var y = worldHeight * Mathf.Cos(Mathf.PI * (per)) - worldHeight * Mathf.Cos(Mathf.PI * (0));;
			verts[i] = new Vector3(v.x, (v.y/this.transform.localScale.y) + y/this.transform.localScale.y,v.z);
			
			i++;
		}
		mesh.mesh.vertices = verts;
		
		var mcollider = this.GetComponent<MeshCollider>();
		if (mcollider!=null)
		{
			mcollider.sharedMesh = mesh.mesh;
		}
	//	Debug.Log(str);
	}
	
	void CirclePolar()
	{
		var mesh = this.GetComponent<MeshFilter>();
		Debug.Log((mesh != null) +"::"+ mesh.mesh.vertexCount+";;"+mesh.mesh.normals.Length);
		
		var str = "";
		
		Vector3[] verts = new Vector3[mesh.mesh.vertexCount];
		int i = 0;
		float r = 50.0f;
		foreach(var vert in mesh.mesh.vertices)
		{
			var v = transform.TransformPoint(vert);
			var per = (v.z)/(2*Mathf.PI*r);
			var fai = 2*Mathf.PI * (per);
			//Debug.Log(fai+":;"+Mathf.Sin(fai)+"::"+v.z+"::"+(this.transform.position.z+v.z)/worldDepth);
			var z = v.y* Mathf.Sin(fai) + r*Mathf.Sin(fai);
			//Debug.Log("cos::"+Mathf.Cos(fai));
			var y = v.y* Mathf.Cos(fai) + r* Mathf.Cos(fai);
		//	Debug.Log((this.transform.position.z+v.z)+"->"+(this.transform.position.z+v.z)*Mathf.Cos(fai)+"::"+Mathf.Cos(fai));
			
			verts[i] = transform.InverseTransformPoint(new Vector3(v.x, y, z));
			
			i++;
		}
		mesh.mesh.vertices = verts;
		mesh.mesh.RecalculateBounds();
		mesh.mesh.RecalculateNormals();
		mesh.mesh.Optimize();
		
		var mcollider = this.GetComponent<MeshCollider>();
		if (mcollider!=null)
		{
			mcollider.sharedMesh = mesh.mesh;
		}
		else
		{
			var bcollider = this.GetComponent<BoxCollider>();
			if (bcollider != null)
			{
				Destroy(bcollider);
				mcollider = gameObject.AddComponent<MeshCollider>();
				mcollider.sharedMesh = mesh.mesh;
			}
			else
			{
				var scollider = this.GetComponent<SphereCollider>();
				if (scollider != null)
				{
					
					var v = this.transform.position;
					var per = (v.z)/(2*Mathf.PI*r);
					var fai = 2*Mathf.PI * (per);
					//Debug.Log(fai+":;"+Mathf.Sin(fai)+"::"+v.z+"::"+(this.transform.position.z+v.z)/worldDepth);
					var z = v.y* Mathf.Sin(fai) + r*Mathf.Sin(fai);
					//Debug.Log("cos::"+Mathf.Cos(fai));
					var y = v.y* Mathf.Cos(fai) + r* Mathf.Cos(fai);
					scollider.center = transform.InverseTransformPoint(new Vector3(v.x, y, z));
				}
			}
		}
		
	}
}
