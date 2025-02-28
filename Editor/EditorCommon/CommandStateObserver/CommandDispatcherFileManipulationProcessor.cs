using System;
using System.Linq;

namespace UnityEditor.GraphToolsFoundation.Overdrive
{
    class CommandDispatcherFileManipulationProcessor : AssetModificationProcessor
    {
        static string[] OnWillSaveAssets(string[] paths)
        {
            foreach (var path in paths)
            {
                // Ignore scene assets, which would throw exception
                // "Do not use ReadObjectThreaded on scene objects!"
                if (typeof(SceneAsset) == AssetDatabase.GetMainAssetTypeAtPath(path))
                {
                    continue;
                }

                var assets = AssetDatabase.LoadAllAssetsAtPath(path);

                foreach (var graphAssetModel in assets.OfType<IGraphAssetModel>())
                {
                    graphAssetModel.Dirty = false;
                }
            }

            return paths;
        }
    }
}
