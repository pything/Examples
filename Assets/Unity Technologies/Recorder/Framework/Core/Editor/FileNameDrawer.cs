﻿using System.Linq;
using UnityEngine;

namespace UnityEditor.Recorder
{
    [CustomPropertyDrawer(typeof(Unity_Technologies.Recorder.Framework.Core.Engine.FileNameGenerator))]
    public class FileNameDrawer : PropertyDrawer
    {
        static string[] m_FileNameTags;

        static FileNameDrawer()
        {
            var temp = Unity_Technologies.Recorder.Framework.Core.Engine.FileNameGenerator.tagLabels.ToList();
            temp.Insert(0, "+ tag");
            m_FileNameTags = temp.ToArray();            
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            float tagWidth = 50;
            float txtWidth = position.width - tagWidth - 5;
            Rect txtRect = new Rect(position.x, position.y, txtWidth, position.height);
            Rect tagRect = new Rect(position.x + txtWidth + 5, position.y, tagWidth, position.height);

            EditorGUI.PropertyField(txtRect, property.FindPropertyRelative( "m_Pattern"), GUIContent.none);

            int value = EditorGUI.Popup(tagRect, 0, m_FileNameTags);
            if (value != 0)
            {
                var pattern = property.FindPropertyRelative("m_Pattern");
                pattern.stringValue = Unity_Technologies.Recorder.Framework.Core.Engine.FileNameGenerator.AddTag(pattern.stringValue, (Unity_Technologies.Recorder.Framework.Core.Engine.FileNameGenerator.ETags)(value - 1) );
            }

            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }
    }
}