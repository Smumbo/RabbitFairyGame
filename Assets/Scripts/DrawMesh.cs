using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * DrawMesh.cs
 * Draws a mesh onscreen using mouse position.
 * Code from Code Monkey: https://youtu.be/XozHdfHrb1U
 */
public class DrawMesh : MonoBehaviour {
    public float lineThickness = 1f;
    public float minDistance = 0.1f;
    private Mesh mesh;
    private Vector3 lastMousePosition;

    private void Update() {
        // mouse pressed
        if (Input.GetMouseButtonDown(0)) {
            // create a new mesh
            mesh = new Mesh();

            // initialize mesh properties
            Vector3[] vertices = new Vector3[4];
            Vector2[] uv = new Vector2[4];
            int[] triangles = new int[6];

            // initialize vertices at mouse position
            vertices[0] = Utils.GetMouseWorldPosition();
            vertices[1] = Utils.GetMouseWorldPosition();
            vertices[2] = Utils.GetMouseWorldPosition();
            vertices[3] = Utils.GetMouseWorldPosition();

            // initialize UVs
            uv[0] = Vector2.zero;
            uv[1] = Vector2.zero;
            uv[2] = Vector2.zero;
            uv[3] = Vector2.zero;

            // initialize triangle coordinates
            triangles[0] = 0;
            triangles[1] = 3;
            triangles[2] = 1;
            triangles[3] = 1;
            triangles[4] = 3;
            triangles[5] = 2;

            // set mesh properties
            mesh.vertices = vertices;
            mesh.uv =  uv;
            mesh.triangles = triangles;
            mesh.MarkDynamic();

            // set object's MeshFilter to the newly created mesh
            GetComponent<MeshFilter>().mesh = mesh;

            // update last mouse position
            lastMousePosition = Utils.GetMouseWorldPosition();
        }

        // mouse held
        if (Input.GetMouseButton(0)) {
            // if mouse hasn't moved enough, return
            if (Vector3.Distance(Utils.GetMouseWorldPosition(), lastMousePosition) <= minDistance)
            {
                return;
            }

            // expand mesh with new quad
            Vector3[] vertices = new Vector3[mesh.vertices.Length + 2];
            Vector2[] uv = new Vector2[mesh.uv.Length + 2];
            int[] triangles = new int[mesh.triangles.Length + 6];

            // copy old mesh into new mesh
            mesh.vertices.CopyTo(vertices, 0);
            mesh.uv.CopyTo(uv, 0);
            mesh.triangles.CopyTo(triangles, 0);

            // calculate vertex indices (2 old, 2 new)
            int vIndex = vertices.Length - 4;
            int vIndex0 = vIndex + 0;
            int vIndex1 = vIndex + 1;
            int vIndex2 = vIndex + 2;
            int vIndex3 = vIndex + 3;

            // calculate up and down vertices relative to mouse movement
            Vector3 mouseForwardVector = (Utils.GetMouseWorldPosition() - lastMousePosition).normalized;
            Vector3 normal2D = new Vector3(0, 0, -1f);
            Vector3 newVertexUp = Utils.GetMouseWorldPosition() + Vector3.Cross(mouseForwardVector, normal2D) * lineThickness;
            Vector3 newVertexDown = Utils.GetMouseWorldPosition() + Vector3.Cross(mouseForwardVector, normal2D * -1f) * lineThickness;

            // set coordinates for new vertices
            vertices[vIndex2] = newVertexUp;
            vertices[vIndex3] = newVertexDown;

            // set coordinates for new UVs
            uv[vIndex2] = Vector2.zero;
            uv[vIndex3] = Vector2.zero;

            int tIndex = triangles.Length - 6;
            triangles[tIndex + 0] = vIndex0;
            triangles[tIndex + 1] = vIndex2;
            triangles[tIndex + 2] = vIndex1;
            triangles[tIndex + 3] = vIndex1;
            triangles[tIndex + 4] = vIndex2;
            triangles[tIndex + 5] = vIndex3;

            // update mesh with new quad
            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;

            // update last mouse position
            lastMousePosition = Utils.GetMouseWorldPosition();
        }
    }
}
