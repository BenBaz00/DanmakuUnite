using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//[CustomEditor(typeof(BulletSpawn)), CanEditMultipleObjects]
//public class BulletSpawnEditor : Editor
//{
//    // TODO put variables here for different enums in bulletSpawn
//    public SerializedProperty
//        bulletType_prop,
//        maxBulletPool_prop,

//        bullet_firerate_prop,
//        bullet_degreeOffset_prop,

//        circle_sprockets_prop,
//        circle_curve_prop,
//        circle_direction_prop,
//        circle_spinSpeed_prop;

//    private void OnEnable()
//    {
//        // Setup SerializedProperties
//        bulletType_prop = serializedObject.FindProperty("bulletType");
//        maxBulletPool_prop = serializedObject.FindProperty("maxBulletPool");

//        bullet_firerate_prop = serializedObject.FindProperty("fireRate");
//        bullet_degreeOffset_prop = serializedObject.FindProperty("degreeOffset");

//        circle_sprockets_prop = serializedObject.FindProperty("sprockets");
//        circle_curve_prop = serializedObject.FindProperty("curve");
//        circle_direction_prop = serializedObject.FindProperty("direction");
//        circle_spinSpeed_prop = serializedObject.FindProperty("spinSpeed");
//    }

//    public override void OnInspectorGUI()
//    {
//        serializedObject.Update();

//        EditorGUILayout.PropertyField(pattern_prop);
//        EditorGUILayout.PropertyField(bulletType_prop);
//        EditorGUILayout.PropertyField(maxBulletPool_prop);

//        EditorGUILayout.PropertyField(bullet_firerate_prop);
//        EditorGUILayout.PropertyField(bullet_degreeOffset_prop);

//        Pattern pa = (Pattern)pattern_prop.enumValueIndex;
//        switch (pa)
//        {
//            case Pattern.player:
//                break;
//            case Pattern.line:
//                break;

//            case Pattern.circle:
//                EditorGUILayout.PropertyField(circle_sprockets_prop);
//                EditorGUILayout.PropertyField(circle_curve_prop);
//                EditorGUILayout.PropertyField(circle_direction_prop);
//                EditorGUILayout.PropertyField(circle_spinSpeed_prop);
//                break;

//            case Pattern.zigzag:
//                break;

//            case Pattern.diamond:
//                break;
//        }

//        serializedObject.ApplyModifiedProperties();
//    }
//}
