using UnityEngine;

namespace UnityEditor.GraphToolsFoundation.Overdrive
{
    /// <summary>
    /// Extension methods for <see cref="IGraphAssetModel"/>.
    /// </summary>
    public static class GraphAssetModelExtensions
    {
        /// <summary>
        /// Gets the path on disk of af the graph asset model.
        /// </summary>
        /// <param name="self">The graph asset model.</param>
        /// <returns>The path of the graph asset model.</returns>
        public static string GetPath(this IGraphAssetModel self)
        {
            #if UNITY_EDITOR

            var obj = self as Object;
            return obj ? AssetDatabase.GetAssetPath(obj) : "";

            #else

            throw new System.Exception("Unavailable outside editor.");

            #endif
        }

        /// <summary>
        /// Gets the file id of af the graph asset model.
        /// </summary>
        /// <param name="self">The graph asset model.</param>
        /// <returns>The path of the graph asset model.</returns>
        public static long GetFileId(this IGraphAssetModel self)
        {
            #if UNITY_EDITOR

            var obj = self as Object;
            return AssetDatabase.TryGetGUIDAndLocalFileIdentifier(obj, out var _, out long fileId) ? fileId : 0;

            #else

            throw new System.Exception("Unavailable outside editor.");

            #endif
        }
    }
}
