using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class UILineRendererTool : Graphic
    {
        [SerializeField] private RectTransform rect;
    
        [SerializeField] private Color lineColor;
        [SerializeField] private float thickness = 10f;
    
        [SerializeField] private Vector2 start;
        [SerializeField] private Vector2 end;

        [SerializeField] private float angleModifier;
        public void SetColor(Color value)
        {
            lineColor = value;
        }

        public void PushVector(Vector2 direction)
        {
            end = rect.InverseTransformVector(direction);
        }
        
        protected override void OnPopulateMesh(VertexHelper helper)
        {
            helper.Clear();
            var angle = Mathf.Atan2(end.y - start.y, end.x - start.x) * Mathf.Rad2Deg + angleModifier;

            DrawPoint(start, helper, angle);
            DrawPoint(end, helper, angle);
        
            helper.AddTriangle(0, 1, 3);
            helper.AddTriangle(3, 2, 0);
        }

        private void DrawPoint(Vector2 point, VertexHelper helper, float angle)
        {
            UIVertex vertex = UIVertex.simpleVert;
            vertex.color = lineColor;

            vertex.position = Quaternion.Euler(0, 0, angle) * new Vector3(-thickness / 2, 0);
            vertex.position += new Vector3(point.x, point.y);
            helper.AddVert(vertex);
        
            vertex.position = Quaternion.Euler(0, 0, angle) * new Vector3(thickness / 2, 0);
            vertex.position += new Vector3(point.x, point.y);
            helper.AddVert(vertex);
        }
    }
}
