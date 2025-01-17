﻿/*  
    10/13/2021
    support: David Olshefski - http://deenvironment.com/
    support: Cristian Pop - https://boxophobic.com/
*/
using UnityEngine;
using UnityEditor;
using System;

public class DE_DrawerSliderRemap : MaterialPropertyDrawer
{
    public string nameMin = "";
    public string nameMax = "";
    public float min = 0;
    public float max = 0;
    public float top = 0;
    public float down = 0;

    bool showAdvancedOptions = false;

    public DE_DrawerSliderRemap(string nameMin, string nameMax, float min, float max)
    {
        this.nameMin = nameMin;
        this.nameMax = nameMax;
        this.min = min;
        this.max = max;
        this.top = 0;
        this.down = 0;
    }

    public DE_DrawerSliderRemap(string nameMin, string nameMax, float min, float max, float top, float down)
    {
        this.nameMin = nameMin;
        this.nameMax = nameMax;
        this.min = min;
        this.max = max;
        this.top = top;
        this.down = down;
    }

    public override void OnGUI(Rect position, MaterialProperty prop, String label, MaterialEditor editor)
    {
        var internalPropMin = MaterialEditor.GetMaterialProperty(editor.targets, nameMin);
        var internalPropMax = MaterialEditor.GetMaterialProperty(editor.targets, nameMax);

        if (internalPropMin.displayName != null && internalPropMax.displayName != null)
        {
            var stylePopup = new GUIStyle(EditorStyles.popup)
            {
                fontSize = 9,
            };

            var styleButton = new GUIStyle(EditorStyles.label)
            {

            };

            var internalValueMin = internalPropMin.floatValue;
            var internalValueMax = internalPropMax.floatValue;
            Vector4 propVector = prop.vectorValue;

            EditorGUI.BeginChangeCheck();

            if (internalValueMin <= internalValueMax)
            {
                propVector.w = 0;
            }
            else
            {
                propVector.w = 1;
            }

            if (propVector.w == 0)
            {
                propVector.x = internalValueMin;
                propVector.y = internalValueMax;
            }
            else
            {
                propVector.x = internalValueMax;
                propVector.y = internalValueMin;
            }

            GUILayout.Space(top);

            EditorGUI.showMixedValue = prop.hasMixedValue;

            // Add this to get the material
            var material = editor.target as Material;


            //Check render pipeline
            if (material.GetTag("RenderPipeline", false) == "HDRenderPipeline")
            {
                // all offsets are reandom, need to tets it

                GUILayout.BeginHorizontal();

                // add this offset
                GUILayout.Space(18);

                // add this offset -31
                if (GUILayout.Button(label, styleButton, GUILayout.Width(EditorGUIUtility.labelWidth - 31)))
                {
                    showAdvancedOptions = !showAdvancedOptions;
                }

                EditorGUILayout.MinMaxSlider(ref propVector.x, ref propVector.y, min, max);

                // add this offset -12
                GUILayout.Space(-12);

                propVector.w = (float)EditorGUILayout.Popup((int)propVector.w, new string[] { "Remap", "Invert" }, stylePopup, GUILayout.Width(65));

                GUILayout.EndHorizontal();
            }
            else
            {
                // old spacing
                GUILayout.BeginHorizontal();
                //GUILayout.Space(-1);

                if (GUILayout.Button(label, styleButton, GUILayout.Width(EditorGUIUtility.labelWidth)))
                {
                    showAdvancedOptions = !showAdvancedOptions;
                }

                EditorGUILayout.MinMaxSlider(ref propVector.x, ref propVector.y, min, max);

                GUILayout.Space(2);

                propVector.w = (float)EditorGUILayout.Popup((int)propVector.w, new string[] { "Remap", "Invert" }, stylePopup, GUILayout.Width(50));

                GUILayout.EndHorizontal();
            }


            if (showAdvancedOptions)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(-1);
                GUILayout.Label("      Remap Min", GUILayout.Width(EditorGUIUtility.labelWidth));
                propVector.x = EditorGUILayout.Slider(propVector.x, min, max);
                GUILayout.Space(2);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(-1);
                GUILayout.Label("      Remap Max", GUILayout.Width(EditorGUIUtility.labelWidth));
                propVector.y = EditorGUILayout.Slider(propVector.y, min, max);
                GUILayout.Space(2);
                GUILayout.EndHorizontal();
            }

            if (propVector.w == 0f)
            {
                internalValueMin = propVector.x;
                internalValueMax = propVector.y;
            }
            else if (propVector.w == 1f)
            {
                internalValueMin = propVector.y;
                internalValueMax = propVector.x;
            }

            EditorGUI.showMixedValue = false;
            if (EditorGUI.EndChangeCheck())
            {
                prop.vectorValue = propVector;
                internalPropMin.floatValue = internalValueMin;
                internalPropMax.floatValue = internalValueMax;
            }

            GUILayout.Space(down);
        }
    }

    public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
    {
        return -2;
    }
}