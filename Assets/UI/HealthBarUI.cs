using System.Xml.Serialization;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class HealthBarUI : VisualElement
{
    public HealthBarUI()
    {
        generateVisualContent += GenerateVisualContent;
    }

    private void GenerateVisualContent(MeshGenerationContext context)
    {
        float width = contentRect.width;
        float height = contentRect.height;

        var painter = context.painter2D;
        painter.BeginPath();
        painter.lineWidth = 5f;
        painter.MoveTo(new Vector2(20, 40));
        painter.LineTo(new Vector2(240, 40));
        painter.LineTo(new Vector2(240, 70));
        painter.LineTo(new Vector2(20, 70));
        painter.ClosePath();
        painter.fillColor = Color.white;
        painter.Fill(FillRule.NonZero);
        painter.Stroke();


        float fullWidth = 220f; // 180 - 20
        float innerPadding = 2.5f;
        float progressWidth = Mathf.Max(0, fullWidth * (progress / 100f) - innerPadding * 2);
        painter.BeginPath();
        painter.lineWidth = 0f;
        painter.MoveTo(new Vector2(20 + innerPadding, 45 - innerPadding));
        painter.LineTo(new Vector2(20 + innerPadding + progressWidth, 45 - innerPadding));
        painter.LineTo(new Vector2(20 + innerPadding + progressWidth, 65 + innerPadding));
        painter.LineTo(new Vector2(20 + innerPadding, 65 + innerPadding));
        painter.ClosePath();
        painter.fillColor = Color.green;
        painter.Fill(FillRule.NonZero);
        painter.Stroke();
    }

    [SerializeField, DontCreateProperty]
    float m_Progress;

    [UxmlAttribute, CreateProperty]
    public float progress{
        get => m_Progress;
        set{
            m_Progress = Mathf.Clamp(value,0.01f,100f);
            MarkDirtyRepaint();
        }
    }
    
}
