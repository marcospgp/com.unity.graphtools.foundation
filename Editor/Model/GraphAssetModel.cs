using System;
using UnityEngine;
using UnityEngine.GraphToolsFoundation.Overdrive;

namespace UnityEditor.GraphToolsFoundation.Overdrive
{
    /// <summary>
    /// Base class for graph assets.
    /// </summary>
    public abstract class GraphAssetModel : ScriptableObject, IGraphAssetModel
    {
        [SerializeReference]
        IGraphModel m_GraphModel;

        /// <inheritdoc />
        public bool Dirty
        {
            get;
            set;
        }

        /// <inheritdoc />
        public IGraphModel GraphModel => m_GraphModel;

        /// <inheritdoc />
        public string Name
        {
            get => name;
            set => name = value;
        }

        /// <inheritdoc />
        public string FriendlyScriptName => Name.CodifyStringInternal();

        /// <summary>
        /// The type of the graph model.
        /// </summary>
        protected abstract Type GraphModelType { get; }

        /// <inheritdoc />
        public void CreateGraph(string graphName, Type stencilType = null, bool markAssetDirty = true)
        {
            #if UNITY_EDITOR

            Debug.Assert(typeof(IGraphModel).IsAssignableFrom(GraphModelType));
            var graphModel = (IGraphModel)Activator.CreateInstance(GraphModelType);
            if (graphModel == null)
                return;

            // PF FIXME: graphName is not used.

            graphModel.StencilType = stencilType ?? graphModel.DefaultStencilType;

            graphModel.AssetModel = this;
            m_GraphModel = graphModel;

            if (markAssetDirty)
            {
                EditorUtility.SetDirty(this);
            }

            #else

            throw new Exception("Unavailable outside editor.");

            #endif
        }

        /// <summary>
        /// Implementation of OnEnable event function.
        /// </summary>
        protected virtual void OnEnable()
        {
            m_GraphModel?.OnEnable();
        }

        /// <summary>
        /// Implementation of OnDisable event function.
        /// </summary>
        protected virtual void OnDisable()
        {
            m_GraphModel?.OnDisable();
        }
    }
}
