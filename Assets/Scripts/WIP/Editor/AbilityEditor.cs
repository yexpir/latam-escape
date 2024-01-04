/*namespace WIP.Editor
{
    using UnityEditor;

    [CustomEditor(typeof(Ability))]
    public class AbilityEditor : Editor {
        SerializedProperty myCustomClassProp;
        SerializedObject seriObj;

        void OnEnable() {
            seriObj = new SerializedObject(Ability);
            myCustomClassProp = seriObj.FindProperty("AbilityBehaviour");
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            
            seriObj.Update();

            EditorGUILayout.PropertyField(myCustomClassProp, true);

            seriObj.ApplyModifiedProperties();
        }
    }
}*/