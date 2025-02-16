using SymphonyFrameWork.Editor;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;

namespace SymphonyFrameWork.Config
{
    public partial class SceneManagerConfig : IActiveBuildTargetChanged
    {
        public int callbackOrder => 1;

        public void OnActiveBuildTargetChanged(BuildTarget previousTarget, BuildTarget newTarget)
        {
            Debug.Log($"{typeof(SceneManagerConfig)}のシーンリストを更新しました");
            OnEnable();
        }

        public void OnEnable()
        {
            _sceneList = EditorBuildSettings.scenes
                .Select(s => Path.GetFileNameWithoutExtension(s.path))
                .ToArray();
        }
    }

    /// <summary>
    /// Enum生成用のボタンを実行
    /// </summary>
    [CustomEditor(typeof(SceneManagerConfig))]
    public class MyScriptEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            GUILayout.Space(10);
            var manager = target as SceneManagerConfig;

            if (GUILayout.Button("SceneListを読み込む"))
            {
                manager.OnEnable();
            }

            GUILayout.Space(10);

            if (GUILayout.Button("Enumを生成する"))
            {
                EnumGenerator.EnumGenerate(manager.SceneList, nameof(manager.SceneList));
            }
        }
    }
}
