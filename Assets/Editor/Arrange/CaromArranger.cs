using UnityEditor;
using UnityEngine;

namespace Editor.Arrange
{
    public class CaromArranger : EditorWindow
    {
        public GameObject blackPiecePrefab;
        public GameObject whitePiecePrefab;
        public GameObject redPiecePrefab;
        public int numberOfPiecesPerColor = 9;
        public float radius = 5f;

        [MenuItem("Window/Carom Arranger")]
        public static void ShowWindow()
        {
            GetWindow<CaromArranger>("Carrom Arranger");
        }

        private void OnGUI()
        {
            blackPiecePrefab = (GameObject)EditorGUILayout.ObjectField("Black Piece Prefab", blackPiecePrefab, typeof(GameObject), false);
            whitePiecePrefab = (GameObject)EditorGUILayout.ObjectField("White Piece Prefab", whitePiecePrefab, typeof(GameObject), false);
            redPiecePrefab = (GameObject)EditorGUILayout.ObjectField("Red Piece Prefab", redPiecePrefab, typeof(GameObject), false);
            numberOfPiecesPerColor = EditorGUILayout.IntField("Number of Pieces per Color", numberOfPiecesPerColor);
            radius = EditorGUILayout.FloatField("Radius", radius);

            if (GUILayout.Button("Arrange Pieces"))
            {
                for (int i = 0; i < numberOfPiecesPerColor * 2; i++)
                {
                    float angle = i * (360f / (numberOfPiecesPerColor * 2)) * Mathf.Deg2Rad;

                    float x = radius * Mathf.Cos(angle);
                    float y = radius * Mathf.Sin(angle);

                    GameObject piece = (GameObject)PrefabUtility.InstantiatePrefab(i % 2 == 0 ? blackPiecePrefab : whitePiecePrefab);
                    piece.transform.position = new Vector3(x, y, 0);

                    Undo.RegisterCreatedObjectUndo(piece, "Arrange Pieces");
                }

                GameObject redPiece = (GameObject)PrefabUtility.InstantiatePrefab(redPiecePrefab);
                redPiece.transform.position = Vector3.zero;

                Undo.RegisterCreatedObjectUndo(redPiece, "Arrange Pieces");
            }
        }
    }
}