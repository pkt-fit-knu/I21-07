using System.Collections.Generic;

namespace DivideConquer
{
    internal class TreeNode
    {
        public string nodeData;

        public List<Relation> relations;

        public List<Attribute> attributes;

        public TreeNode(string nodeData, List<Relation> relations, List<Attribute> attributes)
        {
            this.nodeData = nodeData;

            this.relations = relations;

            this.attributes = attributes;
        }
    }
}
